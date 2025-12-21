using System.Net;
using System.Security.Cryptography.X509Certificates;

namespace Day7;

class Program
{
    const string inputFilename = @"C:\Users\chris\OneDrive\Hobbies\Coding\Advent of Code 2025\day-7-input.txt";

    private static readonly List<string> _tachyonManifold = [];
    private static readonly Dictionary<(int splitterX, int splitterY), long> _cache = [];

    static void Main(string[] args)
    {
        // Read and store all of the source data
        foreach (string currentLine in File.ReadLines(inputFilename))
        {
            _tachyonManifold.Add(currentLine);
        }

        // Look for the starting stream
        int startPos = 0;
        for (int x = 0; x < _tachyonManifold[0].Length; x++)
        {
            if (_tachyonManifold[0][x] == 'S')
            {
                startPos = x;
                break;
            }
        }

        long timelineCount = 1 + CountTimelines(startPos, 1);
        Console.WriteLine($"Total number of timelines: {timelineCount}");
    }

    private static long CountTimelines(int splitterX, int splitterY)
    {
        if (_cache.ContainsKey((splitterX, splitterY)))
        {
            return _cache[(splitterX, splitterY)];
        }

        int nextLeftSplitterY = FindNextSplitterY(splitterX - 1, splitterY);
        int nextRightSplitterY = FindNextSplitterY(splitterX + 1, splitterY);

        _cache[(splitterX, splitterY)] = 1 
                                         + (nextLeftSplitterY == -1 ? 0 : CountTimelines(splitterX - 1, nextLeftSplitterY))
                                         + (nextRightSplitterY == -1 ? 0 : CountTimelines(splitterX + 1, nextRightSplitterY));

        return _cache[(splitterX, splitterY)];
    }

    private static int FindNextSplitterY(int splitterX, int startY)
    {
        while (startY < _tachyonManifold.Count
               && _tachyonManifold[startY][splitterX] != '^')
        {
            startY++;
        }

        if (startY == _tachyonManifold.Count)
        {
            return -1;
        }

        return startY;
    }
}
