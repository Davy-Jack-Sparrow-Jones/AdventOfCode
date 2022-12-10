// https://adventofcode.com/2022/day/1

PartOne();
PartTwo();

static void PartOne()
{
    int mostCalories = 0;

    using StreamReader sr = new("./input.txt");
    int currentElfCalories = 0;

    while (!sr.EndOfStream)
    {
        string line = sr.ReadLine()!;
        int calories = string.IsNullOrEmpty(line) ? 0 : int.Parse(line);

        if (calories != 0)
        {
            currentElfCalories += calories;
            continue;
        }

        if(currentElfCalories > mostCalories)        
            mostCalories = currentElfCalories;
        
        currentElfCalories = 0;
    }

    Console.WriteLine($"[Part 1] - Max calories: {mostCalories}");
}

static void PartTwo()
{
    int[] mostCalories = new int[3];

    using StreamReader sr = new("./input.txt");
    int currentElfCalories = 0;

    while (!sr.EndOfStream)
    {
        string line = sr.ReadLine()!;
        int calories = string.IsNullOrEmpty(line) ? 0 : int.Parse(line);

        if (calories != 0)
        {
            currentElfCalories += calories;
            continue;
        }

        if (currentElfCalories >= mostCalories[0])
        {
            mostCalories[1] = mostCalories[0];
            mostCalories[0] = currentElfCalories;
        }
        else if (currentElfCalories >= mostCalories[1])
        {
            mostCalories[2] = mostCalories[1];
            mostCalories[1] = currentElfCalories;
        }
        else if (currentElfCalories >= mostCalories[2])
        {
            mostCalories[2] = currentElfCalories;
        }

        currentElfCalories = 0;
    }

    int top3CaloriesCombined = mostCalories[0] + mostCalories[1] + mostCalories[2];

    Console.WriteLine($"[Part 2] - Top 3 combined: {top3CaloriesCombined}");
}