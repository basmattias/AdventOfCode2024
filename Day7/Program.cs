using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day7
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var input = System.IO.File.ReadAllLines(@"input.txt");

            long totalSum = 0;

            foreach (var line in input)
            {
                var colonpos = line.IndexOf(':');
                long sum = long.Parse(line.Substring(0, colonpos));
                var terms = line.Substring(colonpos + 2).Split(' ');
                var valueTerms = terms.Select(x => long.Parse(x)).ToArray();

                Console.Write(sum + " : ");

                var operatorCount = terms.Length - 1;
                var size = (int)Math.Pow(2, operatorCount);
                var operatorSets = new string[size];
                var sums = new long[size];

                int maxLength = 0;
                for (int i = 0; i < size; i++)
                {
                    operatorSets[i] = ToBinary(i);
                    if (operatorSets[i].Length > maxLength)
                        maxLength = operatorSets[i].Length;
                }

                for (int i = 0; i < size; i++)
                {
                    while (operatorSets[i].Length < maxLength)
                        operatorSets[i] = "+" + operatorSets[i];
                }

                for (int i = 0; i < size; i++)
                {
                    sums[i] = valueTerms[0];

                    for (int j = 1; j < terms.Length; j++)
                    {
                        if (operatorSets[i][j - 1] == '+')
                            sums[i] += valueTerms[j];
                        else
                            sums[i] *= valueTerms[j];
                    }
                }

                if (sums.Any(x => x == sum))
                {
                    Console.WriteLine("Valid");
                    totalSum += sum;
                }
                else
                {
                    Console.WriteLine("Invalid");
                }
            }

            Console.WriteLine();
            Console.WriteLine($"The total sum is {totalSum}");
            Console.ReadKey();
        }

        private static string ToBinary(int decimalNumber)
        {
            int remainder;
            string result = string.Empty;
            while (decimalNumber > 0)
            {
                remainder = decimalNumber % 2;
                decimalNumber /= 2;
                result = remainder.ToString() + result;
            }

            if (string.IsNullOrEmpty(result))
                result = "0";

            result = result.Replace("0", "+");
            result = result.Replace("1", "*");

            return result;
        }

        private static void Evaluate(Stack<long> stack, int op, long sum, List<long> sumList)
        {
            var term = stack.Peek();

            if (op == 0)
            {
                sum += term;
                stack.Pop();
                Evaluate(stack, 0, sum, sumList);
                Evaluate(stack, 1, sum, sumList);
            }
            else
            {
                sum *= term;
                stack.Pop();
                Evaluate(stack, 0, sum, sumList);
                Evaluate(stack, 1, sum, sumList);
            }

            if (stack.Count == 0)
            {
                sumList.Add(sum);
            }
        }
    }
}
