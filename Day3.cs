using System;
using System.IO;
using System.Collections;
using System.Drawing;

namespace AdventOfCode2019
{
    class Line
    {
        public int X1 { get; }
        public int Y1 { get; }
        public int X2 { get; }
        public int Y2 { get; }

        public int MaxX { get; }
        public int MinX { get; }
        public int MaxY { get; }
        public int MinY { get; }

        public int A { get; }
        public int B { get; }
        public int C { get; }
        public int Length { get; }

        public Line(int x1, int y1, int x2, int y2)
        {
            X1 = x1;
            Y1 = y1;
            X2 = x2;
            Y2 = y2;

            MaxX = Math.Max(x1, x2);
            MinX = Math.Min(x1, x2);
            MaxY = Math.Max(y1, y2);
            MinY = Math.Min(y1, y2);

            A = y2 - y1;
            B = x2 - x1;
            C = A * x1 + B * y1;

            Length = MaxX - MinX + MaxY - MinY;
        }

        public bool hasPoint(int xIn, int yIn)
        {
            if (xIn < MinX || xIn > MaxX || yIn < MinY || yIn > MaxY)
                return false;
            else
                return true;
        }
    }

    struct Intersection
    {
        public int X { get; }
        public int Y { get; }
        public int ManDist { get; }
        public int WireLength { get; }

        public Intersection(int xIn, int yIn, int wireLengthIn)
        {
            X = xIn;
            Y = yIn;
            ManDist = Math.Abs(xIn) + Math.Abs(yIn);
            WireLength = wireLengthIn;
        }
    }

    public class Day3
    {
        ArrayList line1Coords, line2Coords;
        ArrayList intersections;
        bool inputsLoaded;

        public Day3()
        {
            inputsLoaded = false;
        }

        public void loadInputs(string fileLocation)
        {
            if (File.Exists(fileLocation))
            {
                string text = File.ReadAllText(fileLocation);

                string[] lines = text.Split("\r\n");

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

        ArrayList convertStringToCoords(string input)
        {
            ArrayList alOut = new ArrayList();

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
            intersections = new ArrayList();

            foreach (Line l in line1Coords)
            {

                foreach (Line l2 in line2Coords)
                {

                    int denom = l.A * l2.B - l2.A * l.B;

                    if (denom == 0)
                        continue;

                    int p1 = (l2.B * l.C - l.B * l2.C) / denom;
                    int p2 = (l.A * l2.C - l2.A * l.C) / denom;

                    if (l.hasPoint(p1, p2) && l2.hasPoint(p1, p2))
                        intersections.Add(new Intersection(p1, p2, -1));
                }
            }
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

                Console.WriteLine("Day3: Puzzle 1 Solution is " + currentShortest.ManDist);
            }
        }
    }
}