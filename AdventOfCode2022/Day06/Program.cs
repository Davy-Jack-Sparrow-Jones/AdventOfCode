// https://adventofcode.com/2022/day/6

PartOne();
PartTwo();

void PartOne()
{
    Console.WriteLine($"[Part 1] - Characters before start-of-packet marker : {CharactersBeforePacketStart(4)}");
}

void PartTwo()
{
    Console.WriteLine($"[Part 2] - Characters before start-of-message marker : {CharactersBeforePacketStart(14)}");
}

static int CharactersBeforePacketStart(int chunkSize)
{
    StreamReader sr = new("./input.txt");

    string signal = sr.ReadToEnd();

    for (int i = 0; i < signal.Length; i++)
    {
        string chunk = signal.Substring(i, chunkSize);
        bool hasDuplicates = false;
        for (int j = 0; j < chunk.Length; j++)
        {
            if (chunk.LastIndexOf(chunk[j]) != j)
            {
                hasDuplicates = true;
                break;
            }
        }

        if (!hasDuplicates) return i + chunkSize;       
    }

    throw new Exception("No start-of-package marker contained.");
}