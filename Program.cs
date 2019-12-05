using System;

namespace AdventOfCode2019
{
    class Program
    {
        static void Main(string[] args)
        {
            Day1 d1 = new Day1();
            d1.loadInputs("inputs/day1.txt");
            d1.solvePuzzle1();
            d1.solvePuzzle2();
        }
    }
}
