namespace Day3;

class Program
{
    const string inputFilename = @"C:\Users\chris\OneDrive\Hobbies\Coding\Advent of Code 2025\day-3-input.txt";

    static void Main(string[] args)
    {
        long totalJoltage = 0;

        foreach (string currentBank in File.ReadLines(inputFilename))
        {
            Console.WriteLine($"Processing Bank: {currentBank}");

            long joltage = HighestNDigits(currentBank, 12);
            Console.WriteLine($"\tHighest joltage: {joltage}");

            totalJoltage += joltage;
        }

        Console.WriteLine($"{Environment.NewLine}Total joltage: {totalJoltage}");
    }

    static long HighestNDigits(string input, int digitCount)
    {
        int highestDigitPos = 0;
        string result = string.Empty;

        // Loop once for each digit in the output
        for (int d = 0; d < digitCount; d ++)
        {
            // Iterate the possible digits which could be used for the current digit (d) of the output

            // 01234567890123456789 : Input length = 20
            // xxxxxxxxx12345678901 : Digit Count = 12, x = possible first digit positions

            int earliestPos = d == 0 ? 0 : highestDigitPos + 1;
            int latestPos = input.Length - digitCount + d;
            highestDigitPos = earliestPos;

            for (int x = earliestPos; x <= latestPos; x++)
            {
                if (input[x] > input[highestDigitPos])
                {
                    highestDigitPos = x;
                }
            }

            result += input[highestDigitPos];
        }

        // Assume success!
        _ = long.TryParse(result, out long resultVal);

        return resultVal;
    }
}
