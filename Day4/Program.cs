using System.Collections;
using System.Runtime.CompilerServices;

namespace Day4;

class Program
{
    const string inputFilename = @"C:\Users\chris\OneDrive\Hobbies\Coding\Advent of Code 2025\day-4-input.txt";
    const char accessibleLimit = (char)4;

    static void Main(string[] args)
    {
        int result = 0;
        int rollsRemovedThisRound = 0;

        // Retrieve all of the source data
        List<char[]> rollMap = [];
        foreach (string currentRow in File.ReadLines(inputFilename))
        {
            rollMap.Add(currentRow.ToCharArray());
        }

        do
        {
            List<char[]> paperRows = [];

            int row = 0;

            foreach(char[] currentRow in rollMap)
            {
                // First time in, add an extra row. We'll always store one more row than we have read from the file
                if (row == 0)
                {                
                    char[] newRow = new char[currentRow.Length];
                    paperRows.Add(newRow);
                }

                // Add a row after the current row
                paperRows.Add(new char[currentRow.Length]);

                for (int col = 0; col < currentRow.Length; col ++)
                {
                    if (currentRow[col] == '@')
                    {
                        // Increment values in a 3x3 grid around the current coordinates, apart from the current coordinate
                        for (int x = col - 1; x <= col + 1; x ++)
                        {
                            for (int y = row - 1; y <= row + 1; y ++)
                            {
                                if (x >= 0 
                                    && x < currentRow.Length 
                                    && y >= 0
                                    && !(x == col && y == row)
                                    && paperRows[y][x] != '.')
                                {
                                    paperRows[y][x]++;
                                }
                            }
                        }
                    }
                    else
                    {
                        paperRows[row][col] = '.';
                    }
                }

                row++;
            }

            // Remove the final row of data as this has extraneous 'overflow' data in it
            paperRows.RemoveAt(paperRows.Count - 1);

            // Display the accessible rolls (and remove them from the map)
            rollsRemovedThisRound = 0;
            for (int currentRow = 0; currentRow < paperRows.Count; currentRow ++)
            {
                string currentRowData = string.Empty;

                for (int currentCol = 0; currentCol < paperRows[currentRow].Length; currentCol++)
                {
                    char currentRoll = paperRows[currentRow][currentCol];

                    if (currentRoll < accessibleLimit)
                    {
                        currentRowData += 'x';
                        rollMap[currentRow][currentCol] = '.';
                        rollsRemovedThisRound++;
                    }
                    else
                    {
                        currentRowData += ' ';                    
                    }
                }

                Console.WriteLine(currentRowData);
            }

            // Count the available rolls of paper...
            result += paperRows.Sum(r => r.Count(p => p < accessibleLimit));

            Console.WriteLine($"Rolls removed this round: {rollsRemovedThisRound}");
        }
        while (rollsRemovedThisRound > 0);

        Console.WriteLine($"Total rolls removed: {result}");
    }
}
