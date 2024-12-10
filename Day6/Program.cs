using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day6
{
    internal class Position
    {
        public Position(int row, int col)
        {
            Row = row;
            Col = col;
        }

        public static Position operator +(Position a, Position b) => new Position(a.Row + b.Row, a.Col + b.Col);

        public int Row { get; set; }
        public int Col { get; set; }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            var map = System.IO.File.ReadAllLines(@"input.txt");

            Position guardPosition = FindGuardPosition(map);

            Position moveOffset = new Position(-1, 0);

            var visited = new List<Position>();
            visited.Add(guardPosition);

            do
            {
                var newPosition = guardPosition + moveOffset;
                if (!WithinMap(map, newPosition))
                {
                    // We're done
                    break;
                }

                var item = map[newPosition.Row][newPosition.Col];
                if (item == '#')
                {
                    // Blocked - turn 90 right
                    moveOffset = Turn90(moveOffset);
                }
                else
                {
                    guardPosition = newPosition;
                    if (!visited.Any(x => x.Row == guardPosition.Row && x.Col == guardPosition.Col))
                        visited.Add(guardPosition);
                }
            } 
            while (WithinMap(map, guardPosition));

            foreach (var visit in visited)
            {
                Console.Write($"({visit.Row}, {visit.Col}) ");
            }
            Console.WriteLine();

            Console.WriteLine($"Number of positions: {visited.Count}");
            Console.ReadKey();
        }

        private static Position Turn90(Position moveOffset)
        {
            if (moveOffset.Row == -1 && moveOffset.Col == 0)
                return new Position(0, 1);
            else if (moveOffset.Row == 0 && moveOffset.Col == 1)
                return new Position(1, 0);
            else if (moveOffset.Row == 1 && moveOffset.Col == 0)
                return new Position(0, -1);

            return new Position(-1, 0);
        }

        private static bool WithinMap(string[] map, Position position)
        {
            if ((position.Row >= 0) &&
                (position.Col >= 0) &&
                (position.Row < map.Length) &&
                (position.Col < map[0].Length))
                return true;

            return false;
        }

        private static Position FindGuardPosition(string[] map)
        {
            for (int i = 0; i < map.Length; i++)
            {
                var pos = map[i].IndexOf('^');
                if (pos >= 0)
                    return new Position(i, pos);
            }

            return default(Position);
        }
    }
}
