using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var lines = System.IO.File.ReadAllLines(@"input.txt");

            Part2(lines);
        }

        private static void Part2(string[] lines)
        {
            int safeCount = 0;

            foreach (var line in lines)
            {
                var levels = line.Split(' ');
                var intlevels = levels.Select(x => int.Parse(x)).ToList();

                bool safe = CheckLevels(intlevels);

                int i = 0;
                while (!safe && (i < intlevels.Count))
                {
                    // Check all variants
                    var listcopy = new int[intlevels.Count];
                    intlevels.CopyTo(listcopy);
                    var shortlist = listcopy.ToList();
                    shortlist.RemoveAt(i);
                    safe = CheckLevels(shortlist);

                    i++;
                }

                Console.Write($"{levels[0]}, {levels[1]}, ... :");
                if (safe) Console.WriteLine("Safe"); else Console.WriteLine("Unsafe");

                if (safe)
                    safeCount++;
            }

            Console.WriteLine($"Number of safe: {safeCount}");
            Console.ReadKey();
        }

        private static bool CheckLevels(List<int> intlevels)
        {
            bool safe = true;
            bool increasing = intlevels[0] < intlevels[1];

            for (var i = 0; i < intlevels.Count - 1; i++)
            {
                if (increasing)
                {
                    var stillIncreasing = intlevels[i] < intlevels[i + 1];
                    if (!stillIncreasing)
                        safe = false;
                }
                else
                {
                    var stillDecreasing = intlevels[i] > intlevels[i + 1];
                    if (!stillDecreasing)
                        safe = false;
                }
                var diff = Math.Abs(intlevels[i] - intlevels[i + 1]);
                if (diff < 1 || diff > 3)
                    safe = false;
            }

            return safe;
        }

        private static void Part1(string[] lines)
        {
            int safeCount = 0;

            foreach (var line in lines)
            {
                var levels = line.Split(' ');
                var intlevels = levels.Select(x => int.Parse(x)).ToList();

                bool safe = true;
                bool increasing = intlevels[0] < intlevels[1];

                for (var i = 0; i < intlevels.Count - 1; i++)
                {
                    if (increasing)
                    {
                        var stillIncreasing = intlevels[i] < intlevels[i + 1];
                        if (!stillIncreasing)
                            safe = false;
                    }
                    else
                    {
                        var stillDecreasing = intlevels[i] > intlevels[i + 1];
                        if (!stillDecreasing)
                            safe = false;
                    }
                    var diff = Math.Abs(intlevels[i] - intlevels[i + 1]);
                    if (diff < 1 || diff > 3)
                        safe = false;
                }

                Console.Write($"{levels[0]}, {levels[1]}, ... :");
                if (safe) Console.WriteLine("Safe"); else Console.WriteLine("Unsafe"); 

                if (safe)
                    safeCount++;
            }

            Console.WriteLine($"Number of safe: {safeCount}");
            Console.ReadKey();
        }
    }
}
