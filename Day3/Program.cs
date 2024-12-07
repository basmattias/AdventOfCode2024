using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Day3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var input = System.IO.File.ReadAllText(@"input.txt");

            var dos = FindPositions(input, @"do\(\)").OrderByDescending(x => x);
            var donts = FindPositions(input, @"don\'t\(\)").OrderByDescending(x => x);

            Console.WriteLine("Do");
            Console.WriteLine(string.Join(", ", dos.ToArray()));

            Console.WriteLine("Don't");
            Console.WriteLine(string.Join(", ", donts.ToArray()));

            var pattern = @"mul\(\d{1,3}\,\d{1,3}\)";

            Regex r = new Regex(pattern);
            var matches = r.Matches(input);

            var sum = 0;

            for (int ctr = 0; ctr < matches.Count; ctr++)
            {
                var mulstring = matches[ctr].Value;
                var pos = matches[ctr].Index;
                Console.Write("{0}. {1} at position {2} = ", ctr, mulstring, pos);

                // Check if we are in do or dont mode
                var latestDoPos = dos.FirstOrDefault(x => x < pos);
                var latestDontPos = donts.FirstOrDefault(x => x < pos);
                var ok = true;
                if ((latestDontPos != null) && ((latestDoPos == null) || (latestDontPos > latestDoPos)))
                    ok = false;

                var commapos = mulstring.IndexOf(',');
                var fact1 = mulstring.Substring(4, commapos - 4);
                var fact2 = mulstring.Substring(commapos + 1, mulstring.Length - commapos - 2);
                var prod = int.Parse(fact1) * int.Parse(fact2);
                Console.WriteLine($"{prod} - {(ok ? "Do" : "Don't")} Do: {latestDoPos} Dont: {latestDontPos}");

                if (ok)
                    sum += prod;
            }

            Console.WriteLine(sum);
        }

        private static IEnumerable<int?> FindPositions(string input, string pattern)
        {
            Regex r = new Regex(pattern);
            var matches = r.Matches(input);

            var positions = new List<int?>();

            for (int i = 0; i < matches.Count; i++)
            {
                positions.Add(matches[i].Index);
            }

            return positions;
        }
    }
}
