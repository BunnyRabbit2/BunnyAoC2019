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
            adjStart = new Point(0,0);
        }

        public Point getOxygenSystemLoc(bool adjusted = false)
        {
            Point osl = new Point(0,0);

            if(tiles.Any(t => t.Value == '+'))
            {
                var pos = tiles.First(t => t.Value == '+').Key;
                osl.X = pos.Item1;
                osl.Y = pos.Item2;
            }

            if(adjusted)
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
                if(move(CompassDirection.getDirectionToRight(facing), out terminated))
                {
                    facing = CompassDirection.getDirectionToRight(facing);
                }
                else if(!move(facing, out terminated))
                {
                    facing = CompassDirection.getDirectionToLeft(facing);
                }
                steps++;

                if(posX == 0 && posY == 0 && steps > 10)
                {
                    convertTilesToMap();
                    writeMap();
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

            foreach(var pair in tiles)
            {
                int pX = pair.Key.Item1;
                int pY = pair.Key.Item2;

                if(pX < minX) minX = pX;
                if(pY < minY) minY = pY;
            }

            if(minX < 0) minX *= -1;
            else minX = 0;
            if(minY < 0) minY *= -1;
            else minY = 0;

            foreach(var pair in tiles)
            {
                int pX = pair.Key.Item1;
                int pY = pair.Key.Item2;
                char tile = pair.Value;

                drawPoint(pX + minX, pY + minY, tile);
            }

            adjStart = new Point(minX,minY);
        }

        public void displayMap()
        {
            convertTilesToMap();
            map[adjStart.X][adjStart.Y] = 'S';
            foreach (List<char> line in map)
            {
                Console.WriteLine(new string(line.ToArray()));
            }
        }

        void writeMap()
        {
            List<string> lines = new List<string>();
            map[adjStart.X][adjStart.Y] = 'S';
            foreach (List<char> line in map)
            {
                string newLine = new string(line.ToArray());
                lines.Add(newLine);
            }

            File.WriteAllLines("MAP.txt", lines);
        }
    }
}