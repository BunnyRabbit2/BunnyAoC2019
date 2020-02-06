using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace AdventOfCode2019
{
    class HullPainterRobot
    {
        IntcodeComputer icp;
        List<List<char>> hullPaint;
        Dictionary<(int, int), int> panels;
        int positionX, positionY;
        Directions directionFacing;

        public HullPainterRobot(string fileLocation)
        {
            icp = new IntcodeComputer(fileLocation);
            hullPaint = new List<List<char>>();
            panels = new Dictionary<(int, int), int>();
            positionX = 0;
            positionY = 0;
            directionFacing = Directions.Up;
        }

        public void paintPanels(int startingPanelColour)
        {
            long nextI = 0;
            bool terminated = false;
            long nextInput = startingPanelColour;

            panels.Add((0, 0), startingPanelColour); // Add start point

            while (!terminated)
            {
                bool panelCheck = panels.ContainsKey((positionX, positionY));
                if (!panelCheck)
                    panels.Add((positionX, positionY), 0);

                nextInput = panels[(positionX, positionY)];

                long output = icp.runIntcodeProgramPausable(icp.getIntcodeProgram(), out nextI, out terminated,
                    inputsIn: new long[] { nextInput }, restartIndex: nextI);

                // First input is the colour to paint
                panels[(positionX, positionY)] = (int)output;

                if (terminated)
                    break;

                output = icp.runIntcodeProgramPausable(icp.getIntcodeProgram(), out nextI, out terminated, restartIndex: nextI);

                // Second output is direction to move.
                if (output == 0)
                {
                    directionFacing = Direction.TurnLeft(directionFacing);
                }
                else if (output == 1)
                {
                    directionFacing = Direction.TurnRight(directionFacing);
                }
                positionX += Direction.DirectionValues[directionFacing].X;
                positionY += Direction.DirectionValues[directionFacing].Y;
            }
        }

        public int panelsPainted()
        {
            return panels.Count;
        }

        void drawPoint(int x, int y, int value)
        {
            while (hullPaint.Count - 1 < y)
            {
                hullPaint.Add(new List<char>());
            }

            while (hullPaint[y].Count - 1 < x)
            {
                hullPaint[y].Add(' ');
            }

            char paint = (int)value == 1 ? '#' : ' ';
            hullPaint[y][x] = paint;
        }

        void transferPanelsToPaint()
        {
            int minX, minY;
            minX = minY = 0;

            foreach(var pair in panels)
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

            foreach(var pair in panels)
            {
                int pX = pair.Key.Item1;
                int pY = pair.Key.Item2;
                int paint = pair.Value;

                drawPoint(pX + minX, pY + minY, paint);
            }
        }

        public void displayHullPanels()
        {
            transferPanelsToPaint();
            foreach (List<char> line in hullPaint)
            {
                Console.WriteLine(new string(line.ToArray()));
            }
        }
    }
}