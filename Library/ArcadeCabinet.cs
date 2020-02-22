using System;
using System.Collections.Generic;

namespace AdventOfCode2019
{
    class ArcadeCabinet
    {
        IntcodeComputer icp;
        Dictionary<(int, int), int> screen;

        public ArcadeCabinet(string programLoc)
        {
            icp = new IntcodeComputer(programLoc);
            screen = new Dictionary<(int, int), int>();
        }

        public void drawScreen()
        {
            bool terminated = false;

            while (!terminated)
            {
                int x, y, id;

                x = (int)icp.runIntcodeProgram(out terminated);
                y = (int)icp.runIntcodeProgram(out terminated);
                id = (int)icp.runIntcodeProgram(out terminated);

                if (screen.ContainsKey((x, y)))
                {
                    screen[(x, y)] = id;
                }
                else
                {
                    screen.Add((x, y), id);
                }
            }
        }

        public int getNoOfTilesOfType(int type)
        {
            int total = 0;

            foreach (var tile in screen)
            {
                if (tile.Value == type)
                {
                    total++;
                }
            }

            return total;
        }

        void addJoystickInput(int ballX, int paddleX)
        {
            if (ballX < paddleX)
                icp.addInput(-1);
            else if (ballX > paddleX)
                icp.addInput(1);
            else
                icp.addInput(0);
        }

        public long playGame()
        {
            long score = 0;

            icp.setValueToAddress(0, 2);

            bool terminated = false;
            var ballCoords = (-1, -1);
            var paddleCoords = (-1, -1);

            while (!terminated)
            {
                int x, y, id;

                x = (int)icp.runIntcodeProgram(out terminated);
                if (x == -2)
                {
                    addJoystickInput(ballCoords.Item1, paddleCoords.Item1);
                    x = (int)icp.runIntcodeProgram(out terminated);
                }
                y = (int)icp.runIntcodeProgram(out terminated);
                if (y == -2)
                {
                    addJoystickInput(ballCoords.Item1, paddleCoords.Item1);
                    y = (int)icp.runIntcodeProgram(out terminated);
                }
                id = (int)icp.runIntcodeProgram(out terminated);
                if (id == -2)
                {
                    addJoystickInput(ballCoords.Item1, paddleCoords.Item1);
                    id = (int)icp.runIntcodeProgram(out terminated);
                }

                if (x == -1 && y == 0)
                    score = id;

                if (screen.ContainsKey((x, y)))
                {
                    screen[(x, y)] = id;
                }
                else
                {
                    screen.Add((x, y), id);
                }

                if (id == 3)
                    paddleCoords = (x, y);
                else if (id == 4)
                    ballCoords = (x, y);
            }

            return score;
        }
    }
}