namespace Day3;

class Program
{
    const string inputFilename = @"C:\Users\chris\OneDrive\Hobbies\Coding\Advent of Code 2025\day-3-input.txt";

    static void Main(string[] args)
    {
        int totalJoltage = 0;

        foreach (string currentBank in File.ReadLines(inputFilename))
        {
            Console.WriteLine($"Processing Bank: {currentBank}");

            int joltage = HighestTwoDigits(currentBank);
            Console.WriteLine($"\tHighest joltage: {joltage}");

            totalJoltage += joltage;
        }

        Console.WriteLine($"{Environment.NewLine}Total joltage: {totalJoltage}");
    }

    static int HighestTwoDigits(string input)
    {
        int highestFirstDigitPos = 0;
        for (int x = 1; x < input.Length - 1; x ++)
        {
            if (input[x] > input[highestFirstDigitPos])
            {
                highestFirstDigitPos = x;
            }
        }

        int highestSecondDigitPos = highestFirstDigitPos + 1;
        for (int x = highestSecondDigitPos; x < input.Length; x ++)
        {
            if (input[x] > input[highestSecondDigitPos])
            {
                highestSecondDigitPos = x;
            }
        }

        // Assume success!
        _ = int.TryParse($"{input[highestFirstDigitPos]}{input[highestSecondDigitPos]}", out int result);

        return result;
    }
}
