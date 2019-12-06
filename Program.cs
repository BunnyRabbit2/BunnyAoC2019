using System;

namespace AdventOfCode2019
{
    class Program
    {
        static void Main(string[] args)
        {
            solveDay2();
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
        }

    }
}
