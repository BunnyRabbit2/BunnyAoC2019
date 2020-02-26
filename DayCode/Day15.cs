using System;
using System.IO;
using System.Collections;
using System.Drawing;
using System.Linq;

namespace AdventOfCode2019
{
    public class Day15
    {
        public static void solveDay15()
        {
            Day15 d15 = new Day15();
            d15.solvePuzzle1();
            d15.solvePuzzle2();
        }

        string programLoc;

        public Day15()
        {            
            programLoc = "inputs/day15.txt";
        }

        public void solvePuzzle1()
        {
            RepairDroid robot = new RepairDroid(programLoc);
            robot.createTiles();
            Point osl = robot.getOxygenSystemLoc(true);
            // robot.displayMap();

            long result = AOCHelpers.getShortestPath(robot.adjStart, osl, robot.map);

            Console.WriteLine("Day15: Puzzle 1 solution - " + result);
        }

        public void solvePuzzle2()
        {
            RepairDroid robot = new RepairDroid(programLoc);

            long result = 0;

            Console.WriteLine("Day15: Puzzle 2 solution - " + result);
        }
    }
}