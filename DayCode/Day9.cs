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
            d9.solvePuzzle1();
            d9.solvePuzzle2();
        }

        IntcodeComputer icP;
        string programLoc;

        public Day9()
        {
            programLoc = "inputs/day9.txt";
        }

        public void solvePuzzle1()
        {
            icP = new IntcodeComputer(programLoc);

            long[] input = new long[] { 1 };
            long result = icP.runIntcodeProgram(inputsIn: input);

            Console.WriteLine("Day9: Puzzle 1 solution - " + result);
        }

        public void solvePuzzle2()
        {

            icP = new IntcodeComputer(programLoc);

            long[] input = new long[] { 2 };
            long result = icP.runIntcodeProgram(inputsIn: input);

            Console.WriteLine("Day9: Puzzle 2 solution - " + result);
        }
    }
}