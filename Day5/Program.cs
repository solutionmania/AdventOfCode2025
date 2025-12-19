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
    }
}
