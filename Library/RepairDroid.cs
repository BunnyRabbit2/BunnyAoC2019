using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace AdventOfCode2019
{
    class RepairDroid
    {
        IntcodeComputer icp;
        Dictionary<(int, int), char> tiles;
        int posX, posY;

        public RepairDroid(string fileLocation)
        {
            icp = new IntcodeComputer(fileLocation);
            tiles = new Dictionary<(int, int), char>();
            posX = posY = 0;
        }

        public Point getOxygenSystemLoc()
        {
            Point osl = new Point(0,0);

            if(tiles.Any(t => t.Value == '+'))
            {
                var pos = tiles.First(t => t.Value == '+').Key;
                osl.X = pos.Item1;
                osl.Y = pos.Item2;
            }

            return osl;
        }

        public void createTiles()
        {
            bool terminated = false;
            tiles.Add((posX, posY), '.');

            Random rand = new Random();

            while (!terminated && !tiles.Any(t => t.Value == '+'))
            {
                CompassDirections dir = (CompassDirections)rand.Next(1,5);
                int dist = rand.Next(1,21);
                while(move(dir, out terminated) && dist > 0)
                {
                    dist--;
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
    }
}