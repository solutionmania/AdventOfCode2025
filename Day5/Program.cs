using System.Runtime.CompilerServices;

namespace Day5;

class Program
{
    const string inputFilename = @"C:\Users\chris\OneDrive\Hobbies\Coding\Advent of Code 2025\day-5-input.txt";

    static void Main(string[] args)
    {
        bool readingRanges = true;
        int freshCount = 0;

        List<Range> freshRanges = [];

        foreach (string currentLine in File.ReadLines(inputFilename))
        {
            if (string.IsNullOrEmpty(currentLine))
            {
                // The blank line separates the ranges from the IDs, so switch modes
                readingRanges = false;
            }
            else if (readingRanges)
            {
                // We're still reading ranges...
                freshRanges.Add(new Range(currentLine));
            }
            else
            {
                // This is an ID to check
                _ = long.TryParse(currentLine, out long currentId);

                foreach (Range currentRange in freshRanges)
                {
                    if (currentRange.Contains(currentId))
                    {
                        Console.WriteLine($"{currentId} is fresh!");
                        freshCount++;

                        break;
                    }
                }
            }
        }

        Console.WriteLine($"Total number of fresh ingredients: {freshCount}");

        // Part 2... Reduce the ranges so that they don't overlap
        for (int x = 0; x < freshRanges.Count; x++)
        {
            for (int y = 0; y < freshRanges.Count; y ++)
            {
                // Make sure we don't compare a range with itself
                if (x == y)
                {
                    continue;
                }

                // If there is no overlap, we have nothing to do
                if (!freshRanges[x].OverlapsWith(freshRanges[y]))
                {
                    continue;
                }

                // The ranges overlap - adjust the second range such that they don't
                if (freshRanges[x].EntirelyContains(freshRanges[y]))
                {
                    // The second range is entirely consumed by the first, so we can remove it
                    freshRanges.RemoveAt(y);
                }
                else if (freshRanges[x].Contains(freshRanges[y].LowValue) 
                         && !freshRanges[x].Contains(freshRanges[y].HighValue))
                {
                    // The second range overlaps the top end of this range
                    freshRanges[y].LowValue = freshRanges[x].HighValue + 1;
                }
                else if (freshRanges[x].Contains(freshRanges[y].HighValue)
                         && !freshRanges[x].Contains(freshRanges[y].LowValue))
                {
                    // The second range overlaps the bottom end of this range
                    freshRanges[y].HighValue = freshRanges[x].LowValue - 1;
                }

                // There is another possible overlap scenario - the first range entirely consumed by the second.
                // But we don't need to worry about this as we'll eventually perform the removal the other way around.
            }
        }

        long totalFresh = freshRanges.Sum(r => r.Size);

        Console.WriteLine($"Total number of fresh products {totalFresh}");
    }

    private class Range
    {
        public long LowValue { get; set; }
        public long HighValue { get; set; }

        public Range() {}
        public Range(string rangeString)
        {
            string[] limits = rangeString.Split('-');

            _ = long.TryParse(limits[0], out long lowValue);
            _ = long.TryParse(limits[1], out long highValue);

            LowValue = lowValue;
            HighValue = highValue;
        }

        public bool Contains(long value)
        {
            return (value >= LowValue && value <= HighValue);
        }

        public bool OverlapsWith(Range otherRange) => (HighValue >= otherRange.LowValue && LowValue <= otherRange.HighValue);
        public bool EntirelyContains(Range otherRange) => (otherRange.LowValue >= LowValue && otherRange.HighValue <= HighValue);
        public long Size => HighValue - LowValue + 1;
    }
}
