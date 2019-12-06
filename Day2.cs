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
            int[] icP = intCodeProgram.OfType<int>().ToArray();

            // Pre running operations
            icP[1] = 12;
            icP[2] = 2;
            
            icP = IntcodeComputer.runIntcodeProgram(icP);

            Console.WriteLine("Day2: Puzzle 1 solution - " + icP[0]);
        }
    }
}