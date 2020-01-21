using System;
using System.IO;
using System.Collections.Generic;
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

            if (intCodeProgram.Length > 0)
                intCodeProgramLoaded = true;
        }

        public void solvePuzzle1()
        {
            if (intCodeProgramLoaded)
            {
                List<List<int>> phaseSettings = AOCHelpers.GeneratePermutations(new List<int>() { 0, 1, 2, 3, 4 });

                
                int maxSignal = 0;

                foreach (List<int> ps in phaseSettings)
                {
                    int signal = 0;

                    for (int i = 0; i < ps.Count; i++)
                    {
                        int[] outputs = IntcodeComputer.runIntcodeProgram((int[])intCodeProgram.Clone(), inputsIn: new int[] { ps[i], signal });
                        signal = outputs[outputs.Length - 1];
                    }

                    if(signal > maxSignal)
                        maxSignal = signal;
                }

                Console.WriteLine("Day7: Puzzle 1 solution - " + maxSignal);
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