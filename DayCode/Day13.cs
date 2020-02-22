using System;
using System.IO;
using System.Collections.Generic;

namespace AdventOfCode2019
{
    public class Day13
    {
        public static void solveDay13()
        {
            Day13 d13 = new Day13();
            d13.solvePuzzle1();
            d13.solvePuzzle2();
        }

        string programLoc;

        public Day13()
        {
            programLoc = "inputs/day13.txt";
        }

        public void solvePuzzle1()
        {
            ArcadeCabinet ac = new ArcadeCabinet(programLoc);
            ac.drawScreen();
            int result = ac.getNoOfTilesOfType(2);

            Console.WriteLine("Day13: Puzzle 1 solution - " + result);
        }

        public void solvePuzzle2()
        {
            ArcadeCabinet ac = new ArcadeCabinet(programLoc);

            long result = ac.playGame();

            Console.WriteLine("Day13: Puzzle 2 solution - " + result);
        }
    }
}