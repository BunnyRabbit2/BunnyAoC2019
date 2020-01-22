using System;
using System.IO;
using System.Collections;
using System.Linq;

namespace AdventOfCode2019
{
    public class Day9
    {
        public static void solveDay9()
        {
            Day9 d9 = new Day9();
            d9.loadIntCodeProgram("inputs/day9.txt");
            d9.solvePuzzle1();
            d9.solvePuzzle2();
        }

        int[] intCodeProgram;
        bool intCodeProgramLoaded;

        public Day9()
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
                int result = IntcodeComputer.runIntcodeProgram(icP, inputsIn: input);

                Console.WriteLine("Day9: Puzzle 1 solution - " + result);
            }
        }

        public void solvePuzzle2()
        {
            if (intCodeProgramLoaded)
            {
                int[] icP = (int[])intCodeProgram.Clone();

                int[] input = new int[] { 5 };
                int result = IntcodeComputer.runIntcodeProgram(icP, inputsIn: input);

                Console.WriteLine("Day9: Puzzle 2 solution - " + result);
            }
        }
    }
}