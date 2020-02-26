using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.IO;

namespace AdventOfCode2019
{
    class AOCHelpers
    {
        // Got this code from http://csharphelper.com/blog/2014/08/generate-all-of-the-permutations-of-a-set-of-objects-in-c/
        public static List<List<T>> GeneratePermutations<T>(List<T> items)
        {
            // Make an array to hold the permutation we are building.
            T[] current_permutation = new T[items.Count];

            // Make an array to tell whether an item is in the current selection.
            bool[] in_selection = new bool[items.Count];

            // Make a result list.
            List<List<T>> results = new List<List<T>>();

            // Build the combinations recursively.
            PermuteItems<T>(items, in_selection,
                current_permutation, results, 0);

            // Return the results.
            return results;
        }

        // Recursively permute the items that are not yet in the current selection.
        private static void PermuteItems<T>(List<T> items, bool[] in_selection,
            T[] current_permutation, List<List<T>> results,
            int next_position)
        {
            // See if all of the positions are filled.
            if (next_position == items.Count)
            {
                // All of the positioned are filled. Save this permutation.
                results.Add(current_permutation.ToList());
            }
            else
            {
                // Try options for the next position.
                for (int i = 0; i < items.Count; i++)
                {
                    if (!in_selection[i])
                    {
                        // Add this item to the current permutation.
                        in_selection[i] = true;
                        current_permutation[next_position] = items[i];

                        // Recursively fill the remaining positions.
                        PermuteItems<T>(items, in_selection,
                            current_permutation, results,
                            next_position + 1);

                        // Remove the item from the current permutation.
                        in_selection[i] = false;
                    }
                }
            }
        }

        public static float GetDistanceBetweenTwoPoints(float x1, float y1, float x2, float y2)
        {
            return (float)Math.Sqrt(Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2));
        }

        public static float GetBearingBetweenTwoPoints(float x1, float y1, float x2, float y2, bool returnDegrees = true)
        {
            double a, o;

            if (y1 < y2)
                a = y2 - y1;
            else
                a = y1 - y2;

            if (x1 < x2)
                o = x2 - x1;
            else
                o = x1 - x2;

            float theta = (float)Math.Atan(o / a);

            theta = theta * (180.0f / 3.142f);

            if (y1 < y2)
            {
                // south of player
                if (x1 > x2)
                    // east of player
                    theta = 180.0f + theta;
                else
                    // west of player
                    theta = 180.0f - theta;
            }
            else
            {
                // north of player
                if (x1 > x2)
                {
                    theta = 360.0f - theta;
                }
            }

            if (returnDegrees)
                return theta;
            else
                return theta * (3.142f / 180.0f);
        }

        public static long GCF(long a, long b)
        {
            while (b != 0)
            {
                long temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }

        public static long LCM(long a, long b)
        {
            return a * b / GCF(a, b);
        }

        public static int getShortestPath(Point start, Point destination, List<List<char>> map)
        {
            if (map[start.Y][start.X] != 'S' || map[destination.Y][destination.X] != '+')
                return -1;

            bool[,] visited = new bool[map.Count, map[1].Count];

            visited[start.X, start.Y] = true;

            Func<Point, List<List<char>>, bool> isValid = null;
            isValid = (p, m) =>
            {
                return (p.X >= 0) && (p.X < m.Count) &&
                        (p.Y >= 0) && (p.Y < m[1].Count);
            };

            Action writeMap = () =>
            {
                List<string> lines = new List<string>();
                foreach (List<char> line in map)
                {
                    string newLine = new string(line.ToArray());
                    lines.Add(newLine);
                }

                File.WriteAllLines("MAP2.txt", lines);
            };

            Queue<queueNode> q = new Queue<queueNode>();

            queueNode s = new queueNode(start, 0);
            q.Enqueue(s);

            int[] rowNum = { -1, 0, 0, 1 };
            int[] colNum = { 0, -1, 1, 0 };

            while (q.Count != 0)
            {
                queueNode curr = q.Peek();
                Point pt = curr.pt;

                if (pt.X == destination.X && pt.Y == destination.Y)
                    return curr.dist;

                if(curr.dist > 250)
                    map[pt.Y][pt.X] = 'O';

                // writeMap();

                q.Dequeue();

                for (int i = 0; i < 4; i++)
                {
                    int row = pt.Y + rowNum[i];
                    int col = pt.X + colNum[i];

                    if (isValid(pt, map) && (map[row][col] == '.' || map[row][col] == '+'))
                    {
                        visited[col, row] = true;
                        map[row][col] = 'X';
                        queueNode AdjCell = new queueNode(new Point(col, row), curr.dist + 1);
                        q.Enqueue(AdjCell);
                    }
                }
            }
            return -1;
        }
    }

    class queueNode
    {
        public Point pt;
        public int dist;

        public queueNode(Point ptIn, int distIn)
        {
            pt = ptIn;
            dist = distIn;
        }
    }
}