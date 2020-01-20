using System;
using System.IO;
using System.Collections.Generic;
using System.Drawing;

namespace AdventOfCode2019
{
    public class Day3
    {
        public static void solveDay3()
        {
            Day3 d3 = new Day3();
            d3.loadInputs("inputs/day3.txt");
            d3.solvePuzzle1();
            d3.solvePuzzle2();
        }

        List<Line> line1Coords, line2Coords;
        List<Intersection> intersections;
        bool inputsLoaded;

        public Day3()
        {
            inputsLoaded = false;
        }

        public void loadInputs(string fileLocation)
        {
            if (File.Exists(fileLocation))
            {
                string[] lines = File.ReadAllText(fileLocation).Split(Environment.NewLine);

                line1Coords = convertStringToCoords(lines[0]);
                line2Coords = convertStringToCoords(lines[1]);
                createIntersections();

                inputsLoaded = true;
            }
            else
            {
                Console.WriteLine("Day3: Invalid File Location");
            }
        }

        List<Line> convertStringToCoords(string input)
        {
            List<Line> alOut = new List<Line>();

            string[] instructions = input.Split(",");
            int currentX = 0, currentY = 0;

            foreach (var i in instructions)
            {
                char dir = i[0];
                int dist = 0;
                int.TryParse(i.Substring(1), out dist);

                switch (dir)
                {
                    case 'R':
                        alOut.Add(new Line(currentX, currentY, currentX + dist, currentY));
                        currentX += dist;
                        break;

                    case 'L':
                        alOut.Add(new Line(currentX, currentY, currentX - dist, currentY));
                        currentX -= dist;
                        break;

                    case 'U':
                        alOut.Add(new Line(currentX, currentY, currentX, currentY + dist));
                        currentY += dist;
                        break;

                    case 'D':
                        alOut.Add(new Line(currentX, currentY, currentX, currentY - dist));
                        currentY -= dist;
                        break;

                    default:
                        break;
                }
            }

            return alOut;
        }

        void createIntersections()
        {
            intersections = new List<Intersection>();

            for (int i = 0; i < line1Coords.Count; i++)
            {
                Line l = (Line)line1Coords[i];

                for (int j = 0; j < line2Coords.Count; j++)
                {
                    Line l2 = (Line)line2Coords[j];

                    int denom = l.A * l2.B - l2.A * l.B;

                    if (denom == 0)
                        continue;

                    int p1 = (l2.B * l.C - l.B * l2.C) / denom;
                    int p2 = (l.A * l2.C - l2.A * l.C) / denom;

                    if (l.hasPoint(p1, p2) && l2.hasPoint(p1, p2))
                    {
                        // Add last bit of length on
                        int l1pLength = lengthBetweenTwoPoints(l.X1, l.Y1, p1, p2);
                        int l2pLength = lengthBetweenTwoPoints(l2.X1, l2.Y1, p1, p2);

                        int totalWireLength = getWireLengthToPoint(i, j, p1, p2);

                        intersections.Add(new Intersection(p1, p2, totalWireLength));
                    }
                }
            }
        }

        int lengthBetweenTwoPoints(int x1, int y1, int x2, int y2)
        {
            int maxX = Math.Max(x1, x2);
            int minX = Math.Min(x1, x2);
            int maxY = Math.Max(y1, y2);
            int minY = Math.Min(y1, y2);

            return maxX - minX + maxY - minY;
        }

        int getWireLengthToPoint(int index1, int index2, int p1, int p2)
        {
            int length = 0;

            for (int i = 0; i < index1; i++)
            {
                length += ((Line)line1Coords[i]).Length;
            }
            for (int i = 0; i < index2; i++)
            {
                length += ((Line)line2Coords[i]).Length;
            }

            length += lengthBetweenTwoPoints(((Line)line1Coords[index1]).X1, ((Line)line1Coords[index1]).Y1, p1, p2);
            length += lengthBetweenTwoPoints(((Line)line2Coords[index2]).X1, ((Line)line2Coords[index2]).Y1, p1, p2);

            return length;
        }

        public void solvePuzzle1()
        {
            if (inputsLoaded)
            {
                Intersection currentShortest = new Intersection();

                foreach (Intersection i in intersections)
                {
                    if (currentShortest.Equals(default(Intersection)))
                    {
                        currentShortest = i;
                    }
                    else
                    {
                        if (i.ManDist < currentShortest.ManDist)
                            currentShortest = i;
                    }
                }

                Console.WriteLine("Day3: Puzzle 1 Solution - " + currentShortest.ManDist);
            }
        }

        public void solvePuzzle2()
        {
            if (inputsLoaded)
            {
                Intersection currentShortest = new Intersection();

                foreach (Intersection i in intersections)
                {
                    if (currentShortest.Equals(default(Intersection)))
                    {
                        currentShortest = i;
                    }
                    else
                    {
                        if (i.WireLength < currentShortest.WireLength)
                            currentShortest = i;
                    }
                }

                Console.WriteLine("Day3: Puzzle 2 Solution - " + currentShortest.WireLength);
            }
        }
    }
}