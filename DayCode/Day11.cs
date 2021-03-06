using System;
using System.IO;
using System.Collections;
using System.Linq;

namespace AdventOfCode2019
{
    public class Day11
    {
        public static void solveDay11()
        {
            Day11 d11 = new Day11();
            d11.solvePuzzle1();
            d11.solvePuzzle2();
        }

        string programLoc;

        public Day11()
        {            
            programLoc = "inputs/day11.txt";
        }

        public void solvePuzzle1()
        {
            HullPainterRobot robot = new HullPainterRobot(programLoc);

            robot.paintPanels(0);

            long result = robot.panelsPainted();

            Console.WriteLine("Day11: Puzzle 1 solution - " + result);
        }

        public void solvePuzzle2()
        {
            HullPainterRobot robot = new HullPainterRobot(programLoc);

            robot.paintPanels(1);

            Console.WriteLine("Day11: Puzzle 2 solution");
            robot.displayHullPanels();
        }
    }
}