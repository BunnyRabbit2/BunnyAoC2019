using System;

namespace AdventOfCode2019
{
    class Program
    {
        static void Main(string[] args)
        {
            solveDay3();
        }

        static void solveDay1()
        {
            Day1 d1 = new Day1();
            d1.loadInputs("inputs/day1.txt");
            d1.solvePuzzle1();
            d1.solvePuzzle2();
        }

        static void solveDay2()
        {
            Day2 d2 = new Day2();
            d2.loadIntCodeProgram("inputs/day2.txt");
            d2.solvePuzzle1();
            d2.solvePuzzle2();
        }

        static void solveDay3()
        {
            Day3 d3 = new Day3();
            d3.loadInputs("inputs/day3.txt");
            d3.solvePuzzle1();
            d3.solvePuzzle2();
        }

    }
}
