namespace Day1;

class Program
{
    const string inputFilename = @"C:\Users\chris\OneDrive\Hobbies\Coding\Advent of Code 2025\day-1-input.txt";
    const int dialSize = 100;

    static void Main(string[] args)
    {
        int result = 0;
        int currentPosition = 50;

        foreach (string currentInstruction in File.ReadLines(inputFilename))
        {
            int direction = currentInstruction[0].Equals('R') ? 1 : -1;
            _ = int.TryParse(currentInstruction[1..], out int magnitude);

            currentPosition += magnitude * direction;

            while (currentPosition < 0)
            {
                currentPosition += dialSize;
            }

            while (currentPosition > dialSize - 1)
            {
                currentPosition -= dialSize;
            }

            if (currentPosition == 0)
            {
                result ++;
            }
        }

        Console.WriteLine($"Number of stops at zero: {result}");
    }
}
