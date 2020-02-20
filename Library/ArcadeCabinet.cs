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

            while(!terminated)
            {
                int x, y, id;

                x = (int)icp.runIntcodeProgram(out terminated);
                y = (int)icp.runIntcodeProgram(out terminated);
                id = (int)icp.runIntcodeProgram(out terminated);

                if (screen.ContainsKey((x,y)))
                {
                    screen[(x,y)] = id;
                }
                else
                {
                    screen.Add((x,y), id);
                }
            }
        }

        public int getNoOfTilesOfType(int type)
        {
            int total = 0;

            foreach(var tile in screen)
            {
                if(tile.Value == type)
                {
                    total++;
                }
            }

            return total;
        }
    }
}