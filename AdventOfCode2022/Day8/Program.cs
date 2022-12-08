// https://adventofcode.com/2022/day/8

PartOne();
PartTwo();

void PartOne()
{
    StreamReader sr = new("./input.txt");

    int visibleTreeCount = 0;
    int rowCount;
    int columnCount;

    string[] patch = sr.ReadToEnd().Split("\n", StringSplitOptions.RemoveEmptyEntries);
    rowCount = patch.Length;
    columnCount = patch[0].Length;

    for(int i = 0; i < patch.Length; i++)
    {
        string treeLine = patch[i];
        for(int j = 0; j < treeLine.Length; j++)
        {
            // check if edge tree
            if (i == 0 || j == 0 || i == rowCount - 1 || j == columnCount - 1)
            {
                visibleTreeCount++;
                continue;
            }

            bool left = true;
            bool right = true;
            bool up = true;
            bool down = true;

            // left
            for(int x = 0; x < j; x++)
            {
                if (Convert.ToInt32(treeLine[x]) >= Convert.ToInt32(treeLine[j]))
                {
                    left = false;
                    break;
                }
            }

            // right
            for (int x = j + 1; x < columnCount; x++)
            {
                if (Convert.ToInt32(treeLine[x]) >= Convert.ToInt32(treeLine[j]))
                {
                    right = false;
                    break;
                }
            }

            // up
            for (int x = 0; x < i; x++)
            {
                if (Convert.ToInt32(patch[x][j]) >= Convert.ToInt32(patch[i][j]))
                {
                    up = false;
                    break;
                }
            }

            // Down
            for (int x = i + 1; x < rowCount; x++)
            {
                if (Convert.ToInt32(patch[x][j]) >= Convert.ToInt32(patch[i][j]))
                {
                    down = false;
                    break;
                }
            }

            if (left || right || up || down) visibleTreeCount++;
        }
    }

    Console.WriteLine($"[Part 1] - Number of visible trees from outside : {visibleTreeCount}");
}

void PartTwo()
{
    StreamReader sr = new("./input.txt");

    int highestScenicScore = 0;
    int rowCount;
    int columnCount;

    string[] patch = sr.ReadToEnd().Split("\n", StringSplitOptions.RemoveEmptyEntries);
    rowCount = patch.Length;
    columnCount = patch[0].Length;

    for (int i = 0; i < patch.Length; i++)
    {
        string treeLine = patch[i];
        for (int j = 0; j < treeLine.Length; j++)
        {
            // check if edge tree
            if (i == 0 || j == 0 || i == rowCount - 1 || j == columnCount - 1) continue;

            int leftDistance = 1;
            int rightDistance = 1;
            int upDistance = 1;
            int downDistance = 1;

            // Left
            for (int x = j - 1; x > 0; x--)
            {
                if (Convert.ToInt32(treeLine[x]) < Convert.ToInt32(treeLine[j]))
                {
                    leftDistance++;
                    continue;
                }

                break;
            }

            // Right
            for (int x = j + 1; x < columnCount - 1; x++)
            {
                if (Convert.ToInt32(treeLine[x]) < Convert.ToInt32(treeLine[j]))
                {
                    rightDistance++;
                    continue;
                }

                break;
            }

            // Up
            for (int x = i - 1; x > 0; x--)
            {
                if (Convert.ToInt32(patch[x][j]) < Convert.ToInt32(patch[i][j]))
                {
                    upDistance++;
                    continue;
                }

                break;
            }

            // Down
            for (int x = i + 1; x < rowCount - 1; x++)
            {
                if (Convert.ToInt32(patch[x][j]) < Convert.ToInt32(patch[i][j]))
                {
                    downDistance++;
                    continue;
                }

                break;
            }

            int scenicScore = leftDistance * rightDistance * upDistance * downDistance;

            if(highestScenicScore < scenicScore)            
                highestScenicScore = scenicScore;   
        }
    }

    Console.WriteLine($"[Part 2] - Highest scenic score : {highestScenicScore}");
}