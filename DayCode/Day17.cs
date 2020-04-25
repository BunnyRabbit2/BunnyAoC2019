using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2019
{
    public class Day17
    {
        public static void solveDay17()
        {
            Day17 d17 = new Day17();
            d17.solvePuzzle1();
            d17.solvePuzzle2();
        }

        string programLoc;
        IntcodeComputer icP;

        public Day17()
        {
            programLoc = "inputs/day17.txt";
        }

        public void solvePuzzle1()
        {
            icP = new IntcodeComputer(programLoc);

            List<long> outputs = new List<long>();

            bool terminated = false;

            while(!terminated)
            {
                long nextO = icP.runIntcodeProgram(out terminated);
                //outputs.append(nextO);
            }
            
            long result = 0;

            Console.WriteLine("Day17: Puzzle 1 solution - " + result);
        }

        public void solvePuzzle2()
        {
            long result = 0;

            Console.WriteLine("Day17: Puzzle 2 solution - " + result);
        }
    }
}