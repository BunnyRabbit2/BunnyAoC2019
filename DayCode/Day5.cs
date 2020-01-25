using System;
using System.IO;
using System.Collections;
using System.Linq;

namespace AdventOfCode2019
{
    public class Day5
    {
        public static void solveDay5()
        {
            Day5 d5 = new Day5();
            d5.loadIntCodeProgram("inputs/day5.txt");
            d5.solvePuzzle1();
            d5.solvePuzzle2();
        }

        long[] intCodeProgram;
        bool intCodeProgramLoaded;

        public Day5()
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
                long[] icP = (long[])intCodeProgram.Clone();

                long[] input = new long[] { 1 };
                long result = IntcodeComputer.runIntcodeProgram(icP, inputsIn: input);

                Console.WriteLine("Day5: Puzzle 1 solution - " + result);
            }
        }

        public void solvePuzzle2()
        {
            if (intCodeProgramLoaded)
            {
                long[] icP = (long[])intCodeProgram.Clone();

                long[] input = new long[] { 5 };
                long result = IntcodeComputer.runIntcodeProgram(icP, inputsIn: input);

                Console.WriteLine("Day5: Puzzle 2 solution - " + result);
            }
        }
    }
}