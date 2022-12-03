PartOne();
PartTwo();

static void PartOne()
{
    StreamReader sr = new("./input.txt");

    int totalScore = 0;

    while (!sr.EndOfStream)
    {
        string line = sr.ReadLine()!;
        Shape theirs = GetShape(line[0]);
        Shape mine = GetShape(line[2]);

        totalScore += GetRoundScore(theirs, mine);
    }

    Console.WriteLine($"[Part 1] - Total score : {totalScore}");
}

static void PartTwo()
{
    StreamReader sr = new("./input.txt");

    int totalScore = 0;

    while (!sr.EndOfStream)
    {
        string line = sr.ReadLine()!;
        Shape theirs = GetShape(line[0]);
        Result result = GetResult(line[2]);

        Shape mine = GetRequiredShapeForExpectedResult(theirs, result);

        totalScore += GetRoundScore(theirs, mine);
    }

    Console.WriteLine($"[Part 2] - Total score : {totalScore}");
}

static int GetRoundScore(Shape theirs, Shape mine)
{
    int score = (int)mine;

    if(theirs == mine)
    {
        score += (int)Result.Draw;
    }
    else if(theirs == Shape.Rock && mine == Shape.Paper ||
            theirs == Shape.Paper && mine == Shape.Scissors ||
            theirs == Shape.Scissors && mine == Shape.Rock)
    {
        score += (int)Result.Win;
    }

    return score;
}

static Shape GetRequiredShapeForExpectedResult(Shape theirs, Result expectedResult)
{
    if(expectedResult == Result.Draw)
    {
        return theirs;
    }

    if (expectedResult == Result.Win)
    {
        return theirs switch
        {
            Shape.Rock => Shape.Paper,
            Shape.Paper => Shape.Scissors,
            Shape.Scissors => Shape.Rock,
            _ => throw new InvalidCastException("Unknown shape")
        };
    }

    if(expectedResult == Result.Lose)
    {
        return theirs switch
        {
            Shape.Rock => Shape.Scissors,
            Shape.Paper => Shape.Rock,
            Shape.Scissors => Shape.Paper,
            _ => throw new InvalidCastException("Unknown shape")
        };
    }

    throw new InvalidCastException("Unknown result");
}

static Shape GetShape(char shapeCode)
{
    return shapeCode switch
    {
        'A' or 'X' => Shape.Rock,
        'B' or 'Y' => Shape.Paper,
        'C' or 'Z' => Shape.Scissors,
        _ => throw new InvalidCastException("Unknown shape code")
    };
}


static Result GetResult(char resultCode)
{
    return resultCode switch
    {
        'X' => Result.Lose,
        'Y' => Result.Draw,
        'Z' => Result.Win,
        _ => throw new InvalidCastException("Unknown result code")
    };
}

enum Shape
{
    Rock = 1,
    Paper = 2,
    Scissors = 3
}

enum Result
{
    Lose = 0,
    Draw = 3,
    Win = 6
}