using System;
using System.IO;
using System.Collections;
using System.Linq;

namespace AdventOfCode2019
{
    public class Day2
    {
        public static void solveDay2()
        {
            Day2 d2 = new Day2();
            d2.loadIntCodeProgram("inputs/day2.txt");
            d2.solvePuzzle1();
            d2.solvePuzzle2();
        }

        int[] intCodeProgram;
        bool intCodeProgramLoaded;

        public Day2()
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
                int[] icP = (int[])intCodeProgram.Clone();

                // Pre running operations
                icP[1] = 12;
                icP[2] = 2;

                int result = IntcodeComputer.runIntcodeProgram(icP);

                Console.WriteLine("Day2: Puzzle 1 solution - " + result);
            }
        }

        public void solvePuzzle2()
        {
            if (intCodeProgramLoaded)
            {
                // Shame I have to brute force this motherfucker
                int wantedResult = 19690720;
                int noun = 0;
                int verb = 0;

                for(int n = 0; n < 99; n++)
                {
                    for(int v = 0; v < 99; v++)
                    {
                        int[] icP = (int[])intCodeProgram.Clone();

                        icP[1] = n;
                        icP[2] = v;

                        int test = IntcodeComputer.runIntcodeProgram(icP);

                        if(test == wantedResult)
                        {
                            noun = n;
                            verb = v;
                            n = 99;
                            v = 99;
                        }
                    }
                }

                int solution = 100 * noun + verb;

                Console.WriteLine("Day2: Puzzle 2 solution - " + solution);
            }
        }
    }
}