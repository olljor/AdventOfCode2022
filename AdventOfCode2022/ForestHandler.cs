using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day8;
internal class ForestHandler
{
    public int[,] forest = new int[0, 0];

    private List<IDictionary<int, int>> northSide;
    private List<IDictionary<int, int>> westSide;
    private List<IDictionary<int, int>> eastSide;
    private List<IDictionary<int, int>> southSide;

    // Turns the input data from a list of strings to a matrix 
    public void SetForest(IEnumerable<string> inputData)
    {
        var inputDataList = inputData.ToList();

        forest = new int[inputDataList.Count, inputDataList.First().Length];
        for (int i = 0; i < inputDataList.Count(); i++)
        {
            for (int j = 0; j < inputDataList[i].Length; j++)
            {
                forest[i, j] = int.Parse(inputDataList[i][j].ToString());
            }
        }
    }

    internal int FindVisibleTrees()
    {
        // The circumference of the forest
        int visibleTrees = (forest.GetLength(0) * 2) + (forest.GetLength(1) * 2) - 4;

        for (int i = 1; i < forest.GetLength(0) - 1; i++)
        {
            for (int j = 1; j < forest.GetLength(1) - 1; j++)
            {
                int hight = forest[i, j];
                if (northSide[j - 1][hight] >= i)
                {
                    visibleTrees++;
                    continue;
                }
                else if (westSide[i - 1][hight] >= j)
                {
                    visibleTrees++;
                    continue;
                }
                else if (eastSide[i - 1][hight] <= j)
                {
                    visibleTrees++;
                    continue;
                }
                else if (southSide[j - 1][hight] <= i)
                {
                    visibleTrees++;
                    continue;
                }
            }
        }

        return visibleTrees;
    }

    public async Task CircleForestAsync()
    {
        var northTask = Task.Run(() => FindLineOfSightNorth());
        var westTask = Task.Run(() => FindLineOfSightWest());
        var eastTask = Task.Run(() => FindLineOfSightEast());
        var southTask = Task.Run(() => FindLineOfSightSouth());

        await Task.WhenAll(northTask, westTask, eastTask, southTask);

        northSide = northTask.Result;
        westSide = westTask.Result;
        eastSide = eastTask.Result;
        southSide = southTask.Result;

        Console.WriteLine(northSide.Count);
        Console.WriteLine(westSide.Count);
        Console.WriteLine(eastSide.Count);
        Console.WriteLine(southSide.Count);

        foreach (var item in northSide[0])
        {
            Console.WriteLine(item.Key + " index " + item.Value);
        }
    }

    // Finds when each possible height of the trees appears from the North
    private List<IDictionary<int, int>> FindLineOfSightNorth()
    {
        List<IDictionary<int, int>> sightLine = new List<IDictionary<int, int>>();

        for (int i = 1; i < forest.GetLength(1) - 1; i++)
        {
            sightLine.Add(LineOfSightNorth(i));
        }
        return sightLine;
    }
    private IDictionary<int, int> LineOfSightNorth(int line)
    {
        IDictionary<int, int> dictionary = new Dictionary<int, int>
        {
            { 0, -1 },
            { 1, -1 },
            { 2, -1 },
            { 3, -1 },
            { 4, -1 },
            { 5, -1 },
            { 6, -1 },
            { 7, -1 },
            { 8, -1 },
            { 9, -1 }
        };

        int highestTree = -1;
        for (int i = 0; i < forest.GetLength(1); i++)
        {
            if (forest[i, line] > highestTree)
            {
                for (int j = highestTree + 1; j <= forest[i, line]; j++)
                {
                    dictionary[j] = i;
                }
                highestTree = forest[i, line];
            }
        }
        return dictionary;
    }


    // Finds when each possible height of the trees appears from the West
    private List<IDictionary<int, int>> FindLineOfSightWest()
    {
        List<IDictionary<int, int>> sightLine = new List<IDictionary<int, int>>();

        for (int i = 1; i < forest.GetLength(0) - 1; i++)
        {
            sightLine.Add(LineOfSightWest(i));
        }

        return sightLine;
    }
    private IDictionary<int, int> LineOfSightWest(int line)
    {
        IDictionary<int, int> dictionary = new Dictionary<int, int>
        {
            { 0, -1 },
            { 1, -1 },
            { 2, -1 },
            { 3, -1 },
            { 4, -1 },
            { 5, -1 },
            { 6, -1 },
            { 7, -1 },
            { 8, -1 },
            { 9, -1 }
        };

        int highestTree = -1;
        for (int i = 0; i < forest.GetLength(1); i++)
        {
            if (forest[line, i] > highestTree)
            {
                for (int j = highestTree + 1; j <= forest[line, i]; j++)
                {
                    dictionary[j] = i;
                }
                highestTree = forest[line, i];
            }
        }
        return dictionary;
    }


    // Finds when each possible height of the trees appears from the East
    private List<IDictionary<int, int>> FindLineOfSightEast()
    {
        List<IDictionary<int, int>> sightLine = new List<IDictionary<int, int>>();

        for (int i = 1; i < forest.GetLength(0) - 1; i++)
        {
            sightLine.Add(LineOfSightEast(i));
        }
        return sightLine;
    }
    private IDictionary<int, int> LineOfSightEast(int line)
    {
        IDictionary<int, int> dictionary = new Dictionary<int, int>
        {
            { 0, -1 },
            { 1, -1 },
            { 2, -1 },
            { 3, -1 },
            { 4, -1 },
            { 5, -1 },
            { 6, -1 },
            { 7, -1 },
            { 8, -1 },
            { 9, -1 }
        };

        int highestTree = -1;
        for (int i = forest.GetLength(0) - 1; i >= 0; i--)
        {
            if (forest[line, i] > highestTree)
            {
                for (int j = highestTree + 1; j <= forest[line, i]; j++)
                {
                    dictionary[j] = i;
                }
                highestTree = forest[line, i];
            }
        }
        return dictionary;
    }


    // Finds when each possible height of the trees appears from the south
    private List<IDictionary<int, int>> FindLineOfSightSouth()
    {
        List<IDictionary<int, int>> sightLine = new List<IDictionary<int, int>>();

        for (int i = 1; i < forest.GetLength(1) - 1; i++)
        {
            sightLine.Add(LineOfSightSouth(i));
        }
        return sightLine;
    }
    private IDictionary<int, int> LineOfSightSouth(int line)
    {
        IDictionary<int, int> dictionary = new Dictionary<int, int>
        {
            { 0, -1 },
            { 1, -1 },
            { 2, -1 },
            { 3, -1 },
            { 4, -1 },
            { 5, -1 },
            { 6, -1 },
            { 7, -1 },
            { 8, -1 },
            { 9, -1 }
        };

        int highestTree = -1;
        for (int i = forest.GetLength(0) - 1; i >= 0; i--)
        {
            if (forest[i, line] > highestTree)
            {
                for (int j = highestTree + 1; j <= forest[i, line]; j++)
                {
                    dictionary[j] = i;
                }
                highestTree = forest[i, line];
            }
        }
        return dictionary;
    }
}
