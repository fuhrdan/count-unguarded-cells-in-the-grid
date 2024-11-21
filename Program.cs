#define GREEN 0
#define RED 1
#define BLOCK 2

int countUnguarded(int m, int n, int** guards, int guardsSize, int* guardsColSize, int** walls, int wallsSize, int* wallsColSize)
{
    int directions[4][2] = {{0, 1}, {1, 0}, {0, -1}, {-1, 0}};
    
    // Allocate grid and initialize to GREEN
    int** grid = (int**)malloc(m * sizeof(int*));
    for (int i = 0; i < m; i++)
    {
        grid[i] = (int*)malloc(n * sizeof(int));
        for (int j = 0; j < n; j++)
        {
            grid[i][j] = GREEN;
        }
    }

    for (int i = 0; i < guardsSize; i++)
    {
        grid[guards[i][0]][guards[i][1]] = BLOCK;
    }

    // Place BLOCK for walls
    for (int i = 0; i < wallsSize; i++)
    {
        grid[walls[i][0]][walls[i][1]] = BLOCK;
    }

    for (int i = 0; i < guardsSize; i++)
    {
        int guardRow = guards[i][0];
        int guardCol = guards[i][1];

        for (int d = 0; d < 4; d++)
        {
            int dr = directions[d][0];
            int dc = directions[d][1];
            int nr = guardRow + dr;
            int nc = guardCol + dc;

            while (nr >= 0 && nr < m && nc >= 0 && nc < n && grid[nr][nc] != BLOCK)
            {
                if (grid[nr][nc] == GREEN)
                {
                    grid[nr][nc] = RED;
                }
                nr += dr;
                nc += dc;
            }
        }
    }

    int result = 0;
    for (int i = 0; i < m; i++)
    {
        for (int j = 0; j < n; j++)
        {
            if (grid[i][j] == GREEN)
            {
                result++;
            }
        }
    }
    for (int i = 0; i < m; i++)
    {
        free(grid[i]);
    }
    free(grid);

    return result;
}