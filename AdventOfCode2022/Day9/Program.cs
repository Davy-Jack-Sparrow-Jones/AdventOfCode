// https://adventofcode.com/2022/day/9

PartOne();
PartTwo();

void PartOne()
{
    int visitedPositions = Solve(new StreamReader("./input.txt"), 2);
    Console.WriteLine($"[Part 1] - The tail of the rope visited {visitedPositions} positions at least once.");
}

void PartTwo()
{
    int visitedPositions = Solve(new StreamReader("./input.txt"), 10);
    Console.WriteLine($"[Part 2] - The tail of the rope visited {visitedPositions} positions at least once.");
}

static int Solve(StreamReader sr, int ropeSize)
{
    List<Position> visitedTailPositions = new();

    Position[] rope = new Position[ropeSize];
    for (int i = 0; i < ropeSize; i++) rope[i] = new(0, 0);

    Position head = rope[0];
    Position tail = rope[^1];

    while (!sr.EndOfStream)
    {
        string[] instructions = sr.ReadLine()!.Split(' ');
        string direction = instructions[0];
        int amount = Convert.ToInt32(instructions[1]);

        for (int i = 0; i < amount; i++)
        {
            head.Move(direction);

            for (int j = 1; j < rope.Length; j++)
            {
                Position relHead = rope[j - 1];
                Position relTail = rope[j];

                MoveTail(relHead, ref relTail);
            }

            // Check if tail already visited this position
            if (!visitedTailPositions.Any(t => t.X == tail.X && t.Y == tail.Y)) 
                visitedTailPositions.Add(new Position(tail.X, tail.Y));
        }
    }

    return visitedTailPositions.Count;
}

static void MoveTail(Position head, ref Position tail)
{
    int xDiff = Math.Abs(head.X - tail.X);
    int yDiff = Math.Abs(head.Y - tail.Y);

    if (xDiff <= 1 && yDiff <= 1) return; // No movement required;

    if ((xDiff == 2 && yDiff >= 1) || (xDiff >= 1 && yDiff == 2)) // Diagonal movement required
    {
        if (head.X > tail.X && head.Y > tail.Y) tail.Move(MoveKind.UpRight);
        else if (head.X > tail.X && head.Y < tail.Y) tail.Move(MoveKind.DownRight);
        else if (head.X < tail.X && head.Y > tail.Y) tail.Move(MoveKind.UpLeft);
        else tail.Move(MoveKind.DownLeft);

        return;
    }

    if (xDiff == 0) // Vertical movement required
    {
        if (head.Y > tail.Y) tail.Move(MoveKind.Up);
        else tail.Move(MoveKind.Down);

        return;
    }

    if(yDiff == 0) // Horizontal movement required
    {
        if (head.X > tail.X) tail.Move(MoveKind.Right);
        else tail.Move(MoveKind.Left);
    }
}

class Position
{
    public int X;
    public int Y;

    public Position(int x, int y)
    {
        this.X = x;
        this.Y = y;
    }

    public void Move(MoveKind move)
    {
        switch (move)
        {
            case MoveKind.Right:
                this.X++;
                break;
            case MoveKind.Left:
                this.X--;
                break;
            case MoveKind.Up:
                this.Y++;
                break;
            case MoveKind.Down:
                this.Y--;
                break;
            case MoveKind.UpRight:
                this.X++;
                this.Y++;
                break;
            case MoveKind.UpLeft:
                this.X--;
                this.Y++;
                break;
            case MoveKind.DownRight:
                this.X++;
                this.Y--;
                break;
            case MoveKind.DownLeft:
                this.X--;
                this.Y--;
                break;
        }
    }

    public void Move(string direction)
    {
        switch (direction)
        {
            case "R":
                Move(MoveKind.Right);
                break;
            case "L":
                Move(MoveKind.Left);
                break;
            case "U":
                Move(MoveKind.Up);
                break;
            case "D":
                Move(MoveKind.Down);
                break;
        }
    }
}

public enum MoveKind
{
    Right,
    Left,
    Up,
    Down,
    UpRight,
    UpLeft,
    DownRight,
    DownLeft
}