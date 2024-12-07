using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day4
{
    internal class Program
    {
        static int cols = 0;
        static int rows = 0;
        static string xmas = "XMAS";

        static void Main(string[] args)
        {
            var input = System.IO.File.ReadAllLines(@"input.txt");

            cols = input[0].Length;
            rows = input.Length;

            var wordCount = Part2(input);

            Console.WriteLine($"Antal träffar: {wordCount}");
            Console.ReadKey();
        }

        private static object Part2(string[] input)
        {
            var wordCount = 0;

            // Use every point as starting point
            for (int y = 1; y < rows - 1; y++)
            {
                for (int x = 1; x < cols - 1; x++)
                {
                    var current = input[y][x];
                    if (current != 'A')
                        continue;

                    // Search MAS/MAS
                    var match = CheckForMas(input, x, y, "MMSS");
                    if (match) { Console.WriteLine($"MAS/MAS ({y}, {x})"); wordCount++; }

                    // Search MAS/SAM
                    match = CheckForMas(input, x, y, "MSMS");
                    if (match) { Console.WriteLine($"MAS/SAM ({y}, {x})"); wordCount++; }

                    // Search SAM/MAS
                    match = CheckForMas(input, x, y, "SMSM");
                    if (match) { Console.WriteLine($"SAM/MAS ({y}, {x})"); wordCount++; }

                    // Search SAM/SAM
                    match = CheckForMas(input, x, y, "SSMM");
                    if (match) { Console.WriteLine($"SAM/SAM ({y}, {x})"); wordCount++; }
                }
            }

            return wordCount;
        }

        private static bool CheckForMas(string[] input, int x, int y, string matches)
        {
            // matches: top left, top right, bottom left, bottom right

            if (input[y - 1][x - 1] != matches[0])
                return false;
            if (input[y - 1][x + 1] != matches[1])
                return false;
            if (input[y + 1][x - 1] != matches[2])
                return false;
            if (input[y + 1][x + 1] != matches[3])
                return false;

            return true;
        }

        private static int Part1(string[] input)
        {
            var wordCount = 0;

            // Use every point as starting point
            for (int y = 0; y < rows; y++)
            {
                for (int x = 0; x < cols; x++)
                {
                    var current = input[y][x];
                    if (current != 'X')
                        continue;

                    // Search horizontal
                    // and reverse
                    var match = CheckForXMas(input, x, y, +1, 0);
                    if (match) { Console.WriteLine($"Horiz + ({y}, {x})"); wordCount++; }
                    match = CheckForXMas(input, x, y, -1, 0);
                    if (match) { Console.WriteLine($"Horiz - ({y}, {x})"); wordCount++; }

                    // Search vertical
                    // and reverse
                    match = CheckForXMas(input, x, y, 0, +1);
                    if (match) { Console.WriteLine($"Vert + ({y}, {x})"); wordCount++; }
                    match = CheckForXMas(input, x, y, 0, -1);
                    if (match) { Console.WriteLine($"Vert - ({y}, {x})"); wordCount++; }

                    // Search diagonal +1, +1
                    // and reverse -1, -1
                    match = CheckForXMas(input, x, y, +1, +1);
                    if (match) { Console.WriteLine($"Dia ++ ({y}, {x})"); wordCount++; }
                    match = CheckForXMas(input, x, y, -1, -1);
                    if (match) { Console.WriteLine($"Dia -- ({y}, {x})"); wordCount++; }

                    // Search diagonal +1 -1 
                    // and reverse -1, +1
                    match = CheckForXMas(input, x, y, +1, -1);
                    if (match) { Console.WriteLine($"Dia +- ({y}, {x})"); wordCount++; }
                    match = CheckForXMas(input, x, y, -1, +1);
                    if (match) { Console.WriteLine($"Dia -+ ({y}, {x})"); wordCount++; }
                }
            }

            return wordCount;
        }

        private static bool CheckForXMas(string[] input, int x, int y, int colOffset, int rowOffset)
        {
            bool match = true;

            int position = 0;
            while (position < xmas.Length)
            {
                if (input[y][x] != xmas[position])
                {
                    match = false;
                    break;
                }
                else if (position == xmas.Length - 1)
                {
                    // We're done
                }
                else
                {
                    x = x + colOffset;
                    if ((x < 0) || (x >= cols))
                    {
                        match = false;
                        break;
                    }

                    y = y + rowOffset;
                    if ((y < 0) || (y >= rows))
                    {
                        match = false;
                        break;
                    }
                }

                position++;
            }

            return match;
        }
    }
}
