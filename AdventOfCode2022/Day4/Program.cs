// https://adventofcode.com/2022/day/4

PartOne();
PartTwo();

void PartOne()
{
    int fullyContainedCounter = 0;

    using StreamReader sr = new("./input.txt");

    while (!sr.EndOfStream)
    {
        string[] sections = sr.ReadLine()!.Split(',');
        int[] firstSections = sections[0].Split('-').Select(int.Parse).ToArray();
        int[] secondSections = sections[1].Split('-').Select(int.Parse).ToArray();

        if ((secondSections[0] >= firstSections[0] && secondSections[1] <= firstSections[1]) ||
            (firstSections[0] >= secondSections[0] && firstSections[1] <= secondSections[1]))
            fullyContainedCounter++;
    }

    Console.WriteLine($"[Part 1] - Pairs where one range fully contains the other : {fullyContainedCounter}");
}

void PartTwo()
{
    int overlapCounter = 0;

    using StreamReader sr = new("./input.txt");

    while (!sr.EndOfStream)
    {
        string[] sections = sr.ReadLine()!.Split(',');
        int[] firstSections = sections[0].Split('-').Select(int.Parse).ToArray();
        int[] secondSections = sections[1].Split('-').Select(int.Parse).ToArray();

        if (firstSections.Any(s => s >= secondSections[0] && s <= secondSections[1]) ||
            secondSections.Any(s => s >= firstSections[0] && s <= firstSections[1]))
            overlapCounter++;
    }

    Console.WriteLine($"[Part 2] - Pairs where one range overlaps with the other : {overlapCounter}");
}