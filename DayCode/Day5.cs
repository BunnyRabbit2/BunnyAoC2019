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
            d5.solvePuzzle1();
            d5.solvePuzzle2();
        }

        string programLoc;
        IntcodeComputer icP;

        public Day5()
        {
            programLoc = "inputs/day5.txt";
        }

        public void solvePuzzle1()
        {
            icP = new IntcodeComputer(programLoc);
            long[] input = new long[] { 1 };
            long result = icP.runIntcodeProgram(inputsIn: input);

            Console.WriteLine("Day5: Puzzle 1 solution - " + result);
        }

        public void solvePuzzle2()
        {
            icP = new IntcodeComputer(programLoc);
            long[] input = new long[] { 5 };
            long result = icP.runIntcodeProgram(inputsIn: input);

            Console.WriteLine("Day5: Puzzle 2 solution - " + result);
        }
    }
}