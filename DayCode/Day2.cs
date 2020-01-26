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
            d2.solvePuzzle1();
            d2.solvePuzzle2();
        }

        IntcodeComputer icP;
        string programLoc;

        public Day2()
        {
            programLoc = "inputs/day2.txt";
        }

        public void solvePuzzle1()
        {
            icP = new IntcodeComputer(programLoc);
            // Pre running operations
            icP.setValueToAddress(1, 12);
            icP.setValueToAddress(2, 2);

            long result = icP.runIntcodeProgram(0);

            Console.WriteLine("Day2: Puzzle 1 solution - " + result);

        }

        public void solvePuzzle2()
        {
            // Shame I have to brute force this motherfucker
            long wantedResult = 19690720;
            int noun = 0;
            int verb = 0;
            icP = new IntcodeComputer(programLoc);

            for (int n = 0; n < 99; n++)
            {
                for (int v = 0; v < 99; v++)
                {
                    icP.setValueToAddress(1, n);
                    icP.setValueToAddress(2, v);

                    long test = icP.runIntcodeProgram(0);

                    if (test == wantedResult)
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