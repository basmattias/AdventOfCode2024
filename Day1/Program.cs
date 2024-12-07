using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var lines = System.IO.File.ReadAllLines(@"input.txt");
            var loclist1 = new List<int>();
            var loclist2 = new List<int>();
            foreach (var line in lines)
            {
                var cols = line.Split(' ');
                loclist1.Add(int.Parse(cols[0]));
                loclist2.Add(int.Parse(cols[3]));
            }

            //Part1(loclist1, loclist2);
            Part2(loclist1, loclist2);
        }

        private static void Part2(List<int> loclist1, List<int> loclist2)
        {
            int totalScore = 0;

            for (int i = 0; i < loclist1.Count; i++)
            {
                // Find number of occurances in list2
                var cnt = loclist2.Where(x => x == loclist1[i]).Count();
                Console.WriteLine($"{loclist1[i]}, {cnt}");
                var score = loclist1[i] * cnt;
                totalScore += score;
            }

            Console.Write("Total poäng: ");
            Console.WriteLine(totalScore);
        }

        private static void Part1(List<int> loclist1, List<int> loclist2)
        {
            var sorted1 = loclist1.OrderBy(x => x).ToArray();
            var sorted2 = loclist2.OrderBy(x => x).ToArray();

            int totalDist = 0;
            for (int i = 0; i < sorted1.Length; i++)
            {
                var dist = Math.Abs(sorted1[i] - sorted2[i]);
                Console.WriteLine(dist);
                totalDist += dist;
            }

            Console.Write("Total distans: ");
            Console.WriteLine(totalDist);
        }
    }
}
