using System;
using System.IO;
using System.Collections;
using System.Linq;

namespace AdventOfCode2019
{
    public class Day2
    {
        ArrayList intCodeProgram;
        bool intCodeProgramLoaded;

        public Day2()
        {
            intCodeProgram = new ArrayList();
            intCodeProgramLoaded = false;
        }

        public void loadIntCodeProgram(string fileLocation)
        {
            if (File.Exists(fileLocation))
            {
                string text = File.ReadAllText(fileLocation);

                string[] numbers = text.Split(",");

                foreach (var s in numbers)
                {
                    int test = -1;

                    int.TryParse(s, out test);

                    if (test != -1)
                        intCodeProgram.Add(test);
                }

                intCodeProgramLoaded = true;
            }
            else
            {
                Console.WriteLine("Day2: Invalid File Location");
            }
        }

        public void solvePuzzle1()
        {
            if (intCodeProgramLoaded)
            {
                int[] icP = intCodeProgram.OfType<int>().ToArray();

                // Pre running operations
                icP[1] = 12;
                icP[2] = 2;

                icP = IntcodeComputer.runIntcodeProgram(icP);

                Console.WriteLine("Day2: Puzzle 1 solution - " + icP[0]);
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
                        int[] icP = intCodeProgram.OfType<int>().ToArray();

                        icP[1] = n;
                        icP[2] = v;

                        int[] test = IntcodeComputer.runIntcodeProgram(icP);

                        if(test[0] == wantedResult)
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