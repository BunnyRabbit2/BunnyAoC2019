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

        int[] intCodeProgram;
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
                int[] icP = (int[])intCodeProgram.Clone();

                int[] input = new int[] { 1 };
                int[] output = IntcodeComputer.runIntcodeProgram(icP, inputsIn: input);
                int result = output[output.Length-1];

                Console.WriteLine("Day5: Puzzle 1 solution - " + result);
            }
        }

        public void solvePuzzle2()
        {
            if (intCodeProgramLoaded)
            {
                int[] icP = (int[])intCodeProgram.Clone();

                int[] input = new int[] { 5 };
                int[] output = IntcodeComputer.runIntcodeProgram(icP, inputsIn: input);
                int result = output[output.Length-1];

                Console.WriteLine("Day5: Puzzle 2 solution - " + result);
            }
        }
    }
}