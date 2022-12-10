// https://adventofcode.com/2022/day/10

PartOne();
PartTwo();

void PartOne()
{
    StreamReader sr = new("./input.txt");

    int cycle = 1;
    int x = 1;
    bool isSecondCycle = false;
    string[] currentCommand = new string[2];
    int sumOfSignalStrenghts = 0;

    while (!sr.EndOfStream)
    {
        if(cycle == 20 || ((cycle - 20) % 40) == 0) sumOfSignalStrenghts += x * cycle;
        
        if (!isSecondCycle) currentCommand = sr.ReadLine()!.Split(' ');
        
        string instruction = currentCommand[0];

        if(instruction == "noop")
        {
            cycle++;
            continue;
        }

        if (isSecondCycle)
        {
            x += Convert.ToInt32(currentCommand[1]);
            isSecondCycle= false;
            cycle++;
            continue;
        }

        cycle++;
        isSecondCycle = true;
    }

    Console.WriteLine($"[Part 1] - The sum of the six measured signal strengths : {sumOfSignalStrenghts}");
}

void PartTwo()
{
    StreamReader sr = new("./input.txt");

    int cycle = 1;
    int x = 1; // sprite position start
    bool isSecondCycle = false;
    string[] currentCommand = new string[2];
    int position = 0;

    while (!sr.EndOfStream)
    {
        if (!isSecondCycle) currentCommand = sr.ReadLine()!.Split(' ');

        string instruction = currentCommand[0];

        Console.Write(position >= x - 1 && position <= x + 1 ? "#" : ".");
        position++;

        if (position % 40 == 0)
        {
            Console.WriteLine();
            position= 0;
        }

        if (instruction == "noop")
        {
            cycle++;
            continue;
        }

        if (isSecondCycle)
        {
            x += Convert.ToInt32(currentCommand[1]);
            isSecondCycle = false;
            cycle++;
            continue;
        }

        cycle++;
        isSecondCycle = true;
    }
}