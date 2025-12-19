namespace Day7;

class Program
{
    const string inputFilename = @"C:\Users\chris\OneDrive\Hobbies\Coding\Advent of Code 2025\day-7-input.txt";

    static void Main(string[] args)
    {
        bool started = false;
        int[] activeStreams = [];
        int splitCount = 0;
        int lineCount = 1;

        foreach (string currentLine in File.ReadLines(inputFilename))
        {
            if (!started)
            {
                activeStreams = new int[currentLine.Length];

                // Look for the starting stream
                for (int x = 0; x < currentLine.Length; x++)
                {
                    if (currentLine[x] == 'S')
                    {
                        activeStreams[x] = lineCount;
                        started = true;
                        break;
                    }
                }
            }
            else
            {
                // Check whether any of our existing streams have hit a splitter
                for (int x = 0; x < currentLine.Length; x ++)
                {
                    if (activeStreams[x] > 0
                        && activeStreams[x] <= lineCount 
                        && currentLine[x] == '^')
                    {
                        splitCount++;
                        activeStreams[x - 1] = lineCount + 1;
                        activeStreams[x] = 0;
                        activeStreams[x + 1] = lineCount + 1;
                    }
                }
            }

            lineCount++;
        }

        Console.WriteLine($"Number of Splits: {splitCount}");
    }
}
