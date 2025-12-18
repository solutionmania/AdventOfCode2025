using Microsoft.Win32.SafeHandles;

namespace Day2;

class Program
{
    const string inputFilename = @"C:\Users\chris\OneDrive\Hobbies\Coding\Advent of Code 2025\day-2-input.txt";

    static void Main(string[] args)
    {
        List<Range> ranges = [];

        // Parse the source file - assume it's well-formed!
        using (StreamReader reader = new(inputFilename))
        {
            int readVal;
            bool lowVal = true;
            Range currentRange = new();

            while ((readVal = reader.Read()) != -1)
            {
                char readChar = (char)readVal;

                if (readChar == '-')
                {
                    lowVal = false;
                }
                else if (readChar == ',')
                {
                    ranges.Add(currentRange);
                    currentRange = new();
                    lowVal = true;
                }
                else
                {
                    if (lowVal)
                    {
                        currentRange.LowVal += readChar;
                    }
                    else
                    {
                        currentRange.HighVal += readChar;
                    }
                }
            }

            // The file doesn't end with a comma, so add the final captured range to the list
            ranges.Add(currentRange);
        }

        // Iterate the ranges, looking for invalid IDs
        long sumOfInvalidIds = 0;

        foreach (Range currentRange in ranges)
        {
            // Assume all values are valid integers
            _ = long.TryParse(currentRange.LowVal, out long lowVal);
            _ = long.TryParse(currentRange.HighVal, out long highVal);

            Console.WriteLine($"Examining Range: {lowVal}-{highVal}");

            for (long x = lowVal; x <= highVal; x++)
            {
                if (!IsValid(x))
                {
                    Console.WriteLine($"\tFound invalid ID: {x}");
                    sumOfInvalidIds += x;
                }
            }
        }

        Console.WriteLine($"Sum of Invalid IDs: {sumOfInvalidIds}");
    }

    private static bool IsValid(long value)
    {
        string valueString = value.ToString();

        // If the string representation isn't an even number of characters long, it must be valid
        if (valueString.Length % 2 != 0)
        {
            return true;
        }

        if (valueString[..(valueString.Length / 2)].Equals(valueString[(valueString.Length / 2)..]))
        {
            return false;
        }

        return true;
    }

    private class Range
    {
        public string LowVal { get; set; } = string.Empty;
        public string HighVal { get; set; } = string.Empty;
    }
}
