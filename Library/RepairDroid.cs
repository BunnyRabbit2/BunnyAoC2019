using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.IO;

namespace AdventOfCode2019
{
    class RepairDroid
    {
        IntcodeComputer icp;
        Dictionary<(int, int), char> tiles;
        public List<List<char>> map;
        public Point adjStart;
        int posX, posY;
        CompassDirections facing;

        public RepairDroid(string fileLocation)
        {
            icp = new IntcodeComputer(fileLocation);
            tiles = new Dictionary<(int, int), char>();
            map = new List<List<char>>();
            posX = posY = 0;
            facing = CompassDirections.North;
            adjStart = new Point(0, 0);
        }

        public Point getOxygenSystemLoc(bool adjusted = false)
        {
            Point osl = new Point(0, 0);

            if (tiles.Any(t => t.Value == '+'))
            {
                var pos = tiles.First(t => t.Value == '+').Key;
                osl.X = pos.Item1;
                osl.Y = pos.Item2;
            }

            if (adjusted)
            {
                osl.X += adjStart.X;
                osl.Y += adjStart.Y;
            }

            return osl;
        }

        public void createTiles()
        {
            bool terminated = false;
            tiles.Add((posX, posY), '.');
            int steps = 0;

            while (!terminated)
            {
                if (move(CompassDirection.getDirectionToRight(facing), out terminated))
                {
                    facing = CompassDirection.getDirectionToRight(facing);
                }
                else if (!move(facing, out terminated))
                {
                    facing = CompassDirection.getDirectionToLeft(facing);
                }
                steps++;

                if (posX == 0 && posY == 0 && steps > 10)
                {
                    convertTilesToMap();
                    terminated = true;
                }
            }
        }

        bool move(CompassDirections direction, out bool terminated)
        {
            bool moved = false;

            icp.addInput((int)direction);
            long output = icp.runIntcodeProgram(out terminated);

            char newTile = ' ';

            if (output == 0)
            {
                newTile = '#';
            }
            else if (output == 1)
            {
                newTile = '.';
                moved = true;
            }
            else if (output == 2)
            {
                newTile = '+';
                moved = true;
            }

            var newPos = (posX, posY);
            switch (direction)
            {
                case CompassDirections.North:
                    newPos.Item2++;
                    if (tiles.ContainsKey(newPos))
                        tiles[newPos] = newTile;
                    else
                        tiles.Add(newPos, newTile);
                    break;
                case CompassDirections.East:
                    newPos.Item1++;
                    if (tiles.ContainsKey(newPos))
                        tiles[newPos] = newTile;
                    else
                        tiles.Add(newPos, newTile);
                    break;
                case CompassDirections.South:
                    newPos.Item2--;
                    if (tiles.ContainsKey(newPos))
                        tiles[newPos] = newTile;
                    else
                        tiles.Add(newPos, newTile);
                    break;
                case CompassDirections.West:
                    newPos.Item1--;
                    if (tiles.ContainsKey(newPos))
                        tiles[newPos] = newTile;
                    else
                        tiles.Add(newPos, newTile);
                    break;
            }

            if (moved)
            {
                posX = newPos.Item1;
                posY = newPos.Item2;
            }

            return moved;
        }

        void drawPoint(int x, int y, char tile)
        {
            while (map.Count - 1 < y)
            {
                map.Add(new List<char>());
            }

            while (map[y].Count - 1 < x)
            {
                map[y].Add(' ');
            }

            map[y][x] = tile;
        }

        void convertTilesToMap()
        {
            int minX, minY;
            minX = minY = 0;

            foreach (var pair in tiles)
            {
                int pX = pair.Key.Item1;
                int pY = pair.Key.Item2;

                if (pX < minX) minX = pX;
                if (pY < minY) minY = pY;
            }

            if (minX < 0) minX *= -1;
            else minX = 0;
            if (minY < 0) minY *= -1;
            else minY = 0;

            foreach (var pair in tiles)
            {
                int pX = pair.Key.Item1;
                int pY = pair.Key.Item2;
                char tile = pair.Value;

                drawPoint(pX + minX, pY + minY, tile);
            }

            int maxX = 0;
            foreach (var line in map)
            {
                if (line.Count > maxX)
                    maxX = line.Count;
            }
            foreach (var line in map)
            {
                while (line.Count - 1 < maxX)
                {
                    line.Add(' ');
                }
            }

            adjStart = new Point(minX, minY);
            map[adjStart.Y][adjStart.X] = 'S';
        }

        public void displayMap()
        {
            convertTilesToMap();
            foreach (List<char> line in map)
            {
                Console.WriteLine(new string(line.ToArray()));
            }
        }

        public void writeMap()
        {
            List<string> lines = new List<string>();
            foreach (List<char> line in map)
            {
                string newLine = new string(line.ToArray());
                lines.Add(newLine);
            }

            File.WriteAllLines("MAP.txt", lines);
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

            Queue<QueueNode> q = new Queue<QueueNode>();

            QueueNode s = new QueueNode(start, 0);
            q.Enqueue(s);

            int[] rowNum = { -1, 0, 0, 1 };
            int[] colNum = { 0, -1, 1, 0 };

            while (q.Count != 0)
            {
                QueueNode curr = q.Peek();
                Point pt = curr.pt;

                if (pt.X == destination.X && pt.Y == destination.Y)
                    return curr.dist;

                if (curr.dist > 250)
                    map[pt.Y][pt.X] = 'O';

                // writeMap(); // FOR DEBUG PURPOSES ONLY

                q.Dequeue();

                for (int i = 0; i < 4; i++)
                {
                    int row = pt.Y + rowNum[i];
                    int col = pt.X + colNum[i];

                    if (isValid(pt, map) && (map[row][col] == '.' || map[row][col] == '+'))
                    {
                        visited[col, row] = true;
                        map[row][col] = 'X';
                        QueueNode AdjCell = new QueueNode(new Point(col, row), curr.dist + 1);
                        q.Enqueue(AdjCell);
                    }
                }
            }
            return -1;
        }

        public static int getOxygenFillTime(Point oxyLoc, List<List<char>> map)
        {
            int steps = -1; // So the first location isn't counted

            bool[,] visited = new bool[map.Count, map[1].Count];
            visited[oxyLoc.X, oxyLoc.Y] = true;

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

                File.WriteAllLines("MAP3.txt", lines);
            };

            List<QueueNode> l = new List<QueueNode>();
            List<QueueNode> nextL = new List<QueueNode>();

            QueueNode s = new QueueNode(oxyLoc, 0);
            nextL.Add(s);
            map[oxyLoc.Y][oxyLoc.X] = 'X';

            int[] rowNum = { -1, 0, 0, 1 };
            int[] colNum = { 0, -1, 1, 0 };

            while (nextL.Count != 0)
            {
                // writeMap(); // FOR DEBUG PURPOSES ONLY
                l.Clear();
                l = nextL.ToArray().ToList();
                nextL.Clear();
                steps++;

                foreach (var qn in l)
                {
                    Point pt = qn.pt;

                    for (int i = 0; i < 4; i++)
                    {
                        int row = pt.Y + rowNum[i];
                        int col = pt.X + colNum[i];

                        if (isValid(pt, map))
                            if (visited[col, row] == false && map[row][col] != '#' && map[row][col] != ' ')
                            {
                                visited[col, row] = true;
                                map[row][col] = 'X';
                                QueueNode AdjCell = new QueueNode(new Point(col, row), qn.dist + 1);
                                nextL.Add(AdjCell);
                            }
                    }
                }
            }
            return steps;
        }
    }

    class QueueNode
    {
        public Point pt;
        public int dist;

        public QueueNode(Point ptIn, int distIn)
        {
            pt = ptIn;
            dist = distIn;
        }
    }
}