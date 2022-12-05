// https://adventofcode.com/2022/day/5

PartOne();
PartTwo();

void PartOne()
{
    StreamReader sr = new("./input.txt");

    string[] sections = sr.ReadToEnd()!.Split("\n\n");

    Stack[] stacks = FillStacks(sections[0]);

    foreach(string line in sections[1].Split('\n'))
    {
        if (string.IsNullOrEmpty(line))
            continue;

        string[] elements = line.Split(' ');
        int amount = Convert.ToInt32(elements[1]);
        int from = Convert.ToInt32(elements[3]);
        int to = Convert.ToInt32(elements[5]);

        for(int i = 0; i < amount; i++)
            stacks[to - 1].Push(stacks[from - 1].Pop());
    }

    Console.Write("[Part 1] - Stack top crates : ");
    foreach(Stack stack in stacks)    
        Console.Write(stack.Peek());
    Console.WriteLine();    
}

void PartTwo()
{
    StreamReader sr = new("./input.txt");

    string[] sections = sr.ReadToEnd()!.Split("\n\n");

    Stack[] stacks = FillStacks(sections[0]);

    foreach (string line in sections[1].Split('\n', StringSplitOptions.RemoveEmptyEntries))
    {
        string[] elements = line.Split(' ');
        int amount = Convert.ToInt32(elements[1]);
        int from = Convert.ToInt32(elements[3]);
        int to = Convert.ToInt32(elements[5]);

        Stack helperStack = new();
        for (int i = 0; i < amount; i++)        
            helperStack.Push(stacks[from - 1].Pop());

        for(int i = 0; i < amount; i++)
            stacks[to - 1].Push(helperStack.Pop()); 
    }

    Console.Write("[Part 2] - Stack top crates : ");
    foreach (Stack stack in stacks)
        Console.Write(stack.Peek());
}

static Stack[] FillStacks(string stackString)
{
    Stack[] stacks = new Stack[9];

    for (int i = 0; i < 9; i++)
    {
        stacks[i] = new Stack();
    }

    string[] contents = stackString.Split('\n');

    for(int i = contents.Length - 1; i >= 0; i--)
    {
        string line = contents[i];

        int stackCounter = 0;
        for (int j = 1; j < line.Length; j += 4)
        {
            char crate = line[j];
            if (char.IsUpper(crate))
                stacks[stackCounter].Push(crate);

            stackCounter++;
        }
    }

    return stacks;
}