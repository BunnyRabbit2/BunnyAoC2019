using System;
using System.IO;
using System.Collections;
using System.Linq;

namespace AdventOfCode2019
{
    public class Day7
    {
        public static void solveDay7()
        {
            Day7 d7 = new Day7();
            d7.loadIntCodeProgram("inputs/day7.txt");
            d7.solvePuzzle1();
            d7.solvePuzzle2();
        }

        int[] intCodeProgram;
        bool intCodeProgramLoaded;

        public Day7()
        {
            intCodeProgramLoaded = false;
        }

        public void loadIntCodeProgram(string fileLocation)
        {
            intCodeProgram = IntcodeComputer.loadIntCodeProgram(fileLocation);

            if(intCodeProgram.Length > 0)
                intCodeProgramLoaded = true;
        }

        public void solvePuzzle1()
        {
            if (intCodeProgramLoaded)
            {
                
            }
        }

        public void solvePuzzle2()
        {
            if (intCodeProgramLoaded)
            {
                
            }
        }
    }
}