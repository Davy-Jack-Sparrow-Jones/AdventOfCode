// https://adventofcode.com/2022/day/3

PartOne();
PartTwo();

void PartOne()
{
    int prioritiesSum = 0;

    using StreamReader sr = new("./input.txt");

    while (!sr.EndOfStream)
    {
        string line = sr.ReadLine()!;

        string firstHalf = line[..(line.Length / 2)];
        string secondHalf = line.Substring(firstHalf.Length, firstHalf.Length);

        char duplicate = firstHalf.Where(c => secondHalf.Contains(c)).First();

        prioritiesSum += char.IsUpper(duplicate) ?
            duplicate - 38 : // 65 - 38 = 27
            duplicate - 96;  // 97 - 96 = 1
    }

    Console.WriteLine($"[Part 1] - Sum of priorities : {prioritiesSum}");
}

void PartTwo()
{
    int badgePrioritiesSum = 0;

    using StreamReader sr = new("./input.txt");

    while (!sr.EndOfStream)
    {
        string[] elfGroupItems = new string[3];

        for(int i = 0; i < 3; i++) elfGroupItems[i] = sr.ReadLine()!;
        
        char commonBadge = elfGroupItems[0].Where(b => elfGroupItems[1].Contains(b) && elfGroupItems[2].Contains(b)).First();

        badgePrioritiesSum += char.IsUpper(commonBadge) ?
            commonBadge - 38 : // 65 - 38 = 27
            commonBadge - 96;  // 97 - 96 = 1
    }

    Console.WriteLine($"[Part 2] - Sum of badge priorities : {badgePrioritiesSum}");
}