using System.Security.Cryptography.X509Certificates;
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
        List<string> problemOperandRows = [];
        string problemOperators = string.Empty;

        // Retrieve question data
        foreach (string currentLine in File.ReadLines(inputFilename))
        {
            if (currentLine[0] == '+' || currentLine[0] == '*')
            {
                problemOperators = currentLine;
            }
            else
            {
                problemOperandRows.Add(currentLine);
            }
        }

        // Iterate the problems, using the operator positions as column boundary markers
        int x = 0;
        long totalOfResults = 0;
        while(x < problemOperators.Length)
        {
            int nextX = x + 1;
            while (problemOperators[nextX] == ' ')
            {
                nextX++;

                if (nextX == problemOperators.Length)
                {
                    nextX++;
                    break;
                }
            }

            // Iterate the operand rows, a column at a time, from right to left
            long currentResult = 0;
            for (int col = nextX - 2; col >= x; col--)
            {
                long currentOperand = 0;

                for (int row = 0; row < problemOperandRows.Count; row++)
                {
                    if (problemOperandRows[row][col] != ' ')
                    {
                        _ = int.TryParse(problemOperandRows[row][col].ToString(), out int currentDigit);
                        
                        currentOperand = (currentOperand == 0) ? currentDigit : currentOperand * 10 + currentDigit;
                    }
                }

                if (problemOperators[x] == '+')
                {
                    currentResult += currentOperand;
                }
                else
                {
                    currentResult = (currentResult == 0) ? currentOperand : currentResult * currentOperand;
                }
            }

            totalOfResults += currentResult;
            x = nextX;
        }

        Console.WriteLine($"Total of all results: {totalOfResults}");
    }
}
