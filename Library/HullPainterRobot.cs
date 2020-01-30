using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace AdventOfCode2019
{
    class HullPainterRobot
    {
        IntcodeComputer icp;
        int[][] hullPaint;
        Dictionary<Point, int> panels;
        int positionX, positionY;
        int directionFacing; // 0 = Up, 1 = Right, 2 = Down, 3 = Left

        public HullPainterRobot(string fileLocation)
        {
            icp = new IntcodeComputer(fileLocation);
            panels = new Dictionary<Point, int>();
            positionX = 0;
            positionY = 0;
            directionFacing = 0;
        }

        public void paintPanels(int startingPanelColour)
        {
            long nextI = 0;
            bool terminated = false;
            long nextInput = startingPanelColour;

            panels.Add(new Point(0, 0), startingPanelColour); // Add start point

            while (!terminated)
            {
                Point currentPanel = new Point(positionX, positionY);
                bool panelCheck = panels.Keys.Any(key => key.X == currentPanel.X && key.Y == currentPanel.Y);
                if (panelCheck)
                {
                    var panelKey = from panel in panels
                                   where panel.Key.X == currentPanel.X && panel.Key.Y == currentPanel.Y
                                   select panel;
                    currentPanel = panelKey.First().Key;
                }
                else
                {
                    panels.Add(currentPanel, 0);
                }

                nextInput = panels[currentPanel];

                long output = icp.runIntcodeProgramPausable(icp.getIntcodeProgram(), out nextI, out terminated,
                    inputsIn: new long[] { nextInput }, restartIndex: nextI);

                // First input is the colour to paint
                panels[currentPanel] = (int)output;

                if (nextI == -1)
                    break;

                output = icp.runIntcodeProgramPausable(icp.getIntcodeProgram(), out nextI, out terminated, restartIndex: nextI);

                // Second output is direction to move.
                if (output == 0)
                {
                    if (directionFacing == 0)
                        directionFacing = 3;
                    else
                        directionFacing--;
                }
                else
                {
                    if (directionFacing == 3)
                        directionFacing = 0;
                    else
                        directionFacing++;
                }

                if (directionFacing == 0)
                    positionY++;
                else if (directionFacing == 1)
                    positionX++;
                else if (directionFacing == 2)
                    positionY--;
                else if (directionFacing == 3)
                    positionX--;
            }
        }

        public int panelsPainted()
        {
            return panels.Count;
        }

        public void displayHullPanels()
        {
            int maxX, maxY, minX, minY;
            maxX = maxY = minX = minY = 0;

            foreach (var key in panels.Keys)
            {
                if (key.X > maxX)
                    maxX = key.X;
                else if (key.Y > maxY)
                    maxY = key.Y;
                else if (key.X < minX)
                    minX = key.X;
                else if (key.Y < minY)
                    minY = key.Y;
            }
            int width = maxX - minX +1;
            int height = maxY - minY +1;

            hullPaint = new int[width][];
            for(int i = 0; i < width; i++)
            {
                hullPaint[i] = new int[height];
            }

            foreach(var panel in panels)
            {
                int newX = panel.Key.X-minX;
                int newY = panel.Key.Y-minY;
                hullPaint[newX][newY] = panel.Value;
            }

            for(int y = 0; y < height; y++)
            {
                string line = "";

                for(int x = 0; x< width; x++)
                {
                    if(hullPaint[x][y] == 1)
                        line += "#";
                    else
                        line += ".";
                }
                Console.WriteLine(line);
            }
        }
    }
}