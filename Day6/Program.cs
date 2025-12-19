using System.Text.RegularExpressions;

namespace Day6;

class Program
{
    const string inputFilename = @"C:\Users\chris\OneDrive\Hobbies\Coding\Advent of Code 2025\day-6-input.txt";

    enum Operator
    {
        Add,
        Multiply
    }

    static void Main(string[] args)
    {
        List<List<long>> problemOperands = [];
        List<Operator> problemOperators = [];

        // Retrieve question data
        foreach (string currentLine in File.ReadLines(inputFilename))
        {
            // Remove duplicate spaces
            string processedLine = Regex.Replace(currentLine, @"\s+", " ");

            string[] tokens = [.. processedLine.Split(' ').Where(t => !string.IsNullOrWhiteSpace(t))];

            if (tokens[0].Equals("+") || tokens[0].Equals("*"))
            {
                problemOperators = [.. tokens.Select(o => o.Equals("*") ? Operator.Multiply : Operator.Add)];
            }
            else
            {
                problemOperands.Add([.. tokens.Select(o => Convert.ToInt64(o))]);
            }
        }

        // Calculate the results
        long[] problemResults = new long[problemOperators.Count];

        for (int p = 0; p < problemOperators.Count; p++)
        {
            // When we're multiplying, we need to initialise our starting value to 1, not 0
            if (problemOperators[p] == Operator.Multiply)
            {
                problemResults[p] = 1;
            }

            for (int o = 0; o < problemOperands.Count; o ++)
            {
                switch (problemOperators[p])
                {
                    case Operator.Add:
                        problemResults[p] += problemOperands[o][p];
                        break;
                    
                    case Operator.Multiply:
                        problemResults[p] *= problemOperands[o][p];
                        break;
                }
            }
        }

        long totalOfResults = problemResults.Sum();

        Console.WriteLine($"Total of all results: {totalOfResults}");
    }
}
