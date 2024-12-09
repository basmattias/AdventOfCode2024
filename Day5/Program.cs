using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day5
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var lines = System.IO.File.ReadAllLines(@"input.txt");

            var rules = new List<Tuple<int, int>>();
            var pagelists = new List<List<int>>();

            foreach (var line in lines)
            {
                if (line.Contains('|'))
                {
                    // Add rule
                    var parts = line.Split('|');
                    rules.Add(Tuple.Create(int.Parse(parts[0]), int.Parse(parts[1])));
                }
                else if (string.IsNullOrWhiteSpace(line))
                {
                    // Do nothing
                }
                else
                {
                    // Add pagelist
                    var parts = line.Split(',');
                    pagelists.Add(parts.Select(x => int.Parse(x)).ToList());
                }
            }

            // var total = Part1(pagelists, rules);
            var total = Part2(pagelists, rules);

            Console.WriteLine($"Sum of mid numbers: {total}");
            Console.ReadKey();
        }

        private static int Part2(List<List<int>> pagelists, List<Tuple<int, int>> rules)
        {
            // Now process the pagelists
            var sum = 0;

            foreach (var pagelist in pagelists)
            {
                var ok = CheckRules(pagelist, rules);
                if (!ok)
                {
                    while (!ok)
                    {
                        var rule = FindFirstInvalidRule(pagelist, rules);
                        if (rule != null)
                        {
                            SwapPages(pagelist, rule);
                        }

                        ok = CheckRules(pagelist, rules);
                    }

                    if (ok)
                    {
                        Console.WriteLine(string.Join(",", pagelist.ToArray()));
                        var middleNumber = pagelist[pagelist.Count / 2];
                        sum += middleNumber;
                    }
                }
            }

            return sum;
        }

        private static void SwapPages(List<int> pagelist, Tuple<int, int> rule)
        {
            var indexOfFirstPage = pagelist.IndexOf(rule.Item1);
            var indexOfSecondage = pagelist.IndexOf(rule.Item2);

            var x = pagelist[indexOfSecondage];
            pagelist[indexOfSecondage] = pagelist[indexOfFirstPage];
            pagelist[indexOfFirstPage] = x;
        }

        private static Tuple<int, int> FindFirstInvalidRule(List<int> pagelist, List<Tuple<int, int>> rules)
        {
            foreach (var page in pagelist)
            {
                var applicableRules = rules.Where(x => x.Item1 == page);
                if (applicableRules.Any())
                {
                    var indexOfPage = pagelist.IndexOf(page);

                    foreach (var rule in applicableRules)
                    {
                        var indexOfFollower = pagelist.IndexOf(rule.Item2);
                        if ((indexOfFollower != -1) && (indexOfFollower < indexOfPage))
                        {
                            return rule;
                        }
                    }
                }
            }

            return null;
        }

        private static int Part1(List<List<int>> pagelists, List<Tuple<int, int>> rules)
        {
            // Now process the pagelists
            var correct = 0;
            var sum = 0;

            foreach (var pagelist in pagelists)
            {
                var ok = CheckRules(pagelist, rules);
                if (ok)
                {
                    var middleNumber = pagelist[pagelist.Count / 2];
                    sum += middleNumber;

                    Console.WriteLine($"Ok, middle = {middleNumber}");
                    correct++;
                }
            }

            return sum;
        }

        private static bool CheckRules(List<int> pagelist, List<Tuple<int, int>> rules)
        {
            foreach (var page in pagelist)
            {
                var applicableRules = rules.Where(x => x.Item1 == page);
                if (applicableRules.Any())
                {
                    var indexOfPage = pagelist.IndexOf(page);

                    foreach (var rule in applicableRules)
                    {
                        var indexOfFollower = pagelist.IndexOf(rule.Item2);
                        if ((indexOfFollower != -1) && (indexOfFollower < indexOfPage))
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
        }
    }
}
