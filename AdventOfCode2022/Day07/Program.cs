// https://adventofcode.com/2022/day/7

PartOne();
PartTwo();

void PartOne()
{
    StreamReader sr = new("./input.txt");

    DeviceDirectory dir = BuildDirectoryTreeFromCommands(sr);

    int totalSize = GetSumOfDirectorySizes(dir, 100000);

    Console.WriteLine($"[Part 1] - The sum of the total sizes is : {totalSize}");
}

void PartTwo()
{
    StreamReader sr = new("./input.txt");

    DeviceDirectory dir = BuildDirectoryTreeFromCommands(sr);

    int unusedSpace = 70_000_000 - dir.GetSize();
    int requiredSize = 30_000_000 - unusedSpace;

    List<DeviceDirectory> deletionCandidates = GetDirectoriesAboveSize(dir, requiredSize);

    int smallest = deletionCandidates[0].GetSize();

    foreach(DeviceDirectory candidateDir in deletionCandidates)
    {
        int size = candidateDir.GetSize();
        if(size < smallest)
        {
            smallest= size;
        }
    }

    Console.WriteLine($"[Part 2] - The size of the smallest directory required to free up enough space : {smallest}");
}

static DeviceDirectory BuildDirectoryTreeFromCommands(StreamReader sr)
{
    DeviceDirectory currentDir = new("/");
    string[]? prevLine = null;

    // Build directory tree
    while (!sr.EndOfStream)
    {
        string[] line = prevLine is null ? sr.ReadLine()!.Split(' ') : prevLine;
        prevLine = null;

        if (line[1] == "ls")
        {
            while (true && !sr.EndOfStream)
            {
                string[] dirContent = sr.ReadLine()!.Split(' ');

                if (dirContent[0] == "$")
                {
                    prevLine = dirContent;
                    break;
                }

                if (dirContent[0] == "dir")
                {
                    currentDir.Children.Add(new DeviceDirectory(dirContent[1], currentDir));
                    continue;
                }

                currentDir.Files.Add(new DeviceFile(dirContent[1], Convert.ToInt32(dirContent[0])));
            }
        }
        else // cd
        {
            if (line[2] == "/") continue;

            if (line[2] == "..")
            {
                currentDir = currentDir.Parent!;
                continue;
            }

            currentDir = currentDir.Children.Where(c => c.Name == line[2]).Single();
        }
    }

    // move to top directory
    while (currentDir.Parent is not null)
    {
        currentDir = currentDir.Parent!;
    }

    return currentDir;
}

static int GetSumOfDirectorySizes(DeviceDirectory directory, int maxDirSize)
{
    int size = directory.GetSize();
    int totalSize = size < maxDirSize ? size : 0;

    foreach(DeviceDirectory child in directory.Children)
        totalSize += GetSumOfDirectorySizes(child, maxDirSize);
    
    return totalSize;
}

static List<DeviceDirectory> GetDirectoriesAboveSize(DeviceDirectory directory, int size)
{
    List<DeviceDirectory> dirs = new();
    foreach(DeviceDirectory child in directory.Children)
    {
        if (child.GetSize() < size) continue;

        dirs.Add(child);
        dirs.AddRange(GetDirectoriesAboveSize(child, size));
    }

    return dirs;
}

class DeviceDirectory
{
    public string Name { get; set; }
    public DeviceDirectory? Parent;
    public List<DeviceDirectory> Children = new();
    public List<DeviceFile> Files= new();

    public DeviceDirectory(string name, DeviceDirectory? parent = null)
    {
        this.Name = name;
        this.Parent = parent;
    }

    public int GetSize()
    {
        int fileSize = this.Files.Select(f => f.Size).Sum();
        int totalSize = this.Children.Select(c => c.GetSize()).Sum() + fileSize;

        return totalSize;
    }       
}

class DeviceFile
{
    public string Name { get; set; }
    public int Size { get; set; }

    public DeviceFile(string name, int size)
    {
        this.Name=name;
        this.Size= size;
    }
}