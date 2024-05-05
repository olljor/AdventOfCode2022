
using Day8;

class Day8Main
{

    public static async Task Main()
    {
        IEnumerable<string> inputs = File.ReadLines("C:\\Users\\olljo\\source\\repos\\AdventOfCode2022\\AdventOfCode2022\\Input_day8.txt");
        ForestHandler forestHandler = new ForestHandler();

        forestHandler.SetForest(inputs.ToList());
        await forestHandler.CircleForestAsync();

        int numberOfVisibleTrees = forestHandler.FindVisibleTrees();
        Console.WriteLine(numberOfVisibleTrees);
    }
}