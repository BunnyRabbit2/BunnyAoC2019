using System;
using System.IO;
using System.Collections;
using System.Linq;

namespace AdventOfCode2019
{
    public class Day5
    {
        ArrayList intCodeProgram;
        bool intCodeProgramLoaded;

        public Day5()
        {
            intCodeProgram = new ArrayList();
            intCodeProgramLoaded = false;
        }

        public void loadIntCodeProgram(string fileLocation)
        {
            intCodeProgram = IntcodeComputer.loadIntCodeProgram(fileLocation);

            if(intCodeProgram.Count > 0)
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