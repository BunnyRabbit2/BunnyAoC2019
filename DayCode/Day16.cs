using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2019
{
    public class Day16
    {
        public static void solveDay16()
        {
            Day16 d16 = new Day16();
            d16.loadInputs("inputs/day16.txt");
            d16.solvePuzzle1();
            d16.solvePuzzle2();
        }

        List<long> inputs;
        bool inputsLoaded;

        public Day16()
        {            
            inputs = new List<long>();
            inputsLoaded = false;
        }

        public void loadInputs(string fileLoc)
        {
            if (File.Exists(fileLoc))
            {
                string input = File.ReadAllText(fileLoc);

                long i = 0; 
                var numbers = input.ToArray().Select(c => long.TryParse(c.ToString(), out i) ? i : -1).ToArray().ToList();

                numbers.RemoveAll(n => n == -1);

                inputsLoaded = true;
            }
            else
            {
                Console.WriteLine("Day16: Invalid File Location");
            }
        }

        public void solvePuzzle1()
        {
            if(!inputsLoaded) return;

            long result = 0;

            Console.WriteLine("Day16: Puzzle 1 solution - " + result);
        }

        public void solvePuzzle2()
        {
            if(!inputsLoaded) return;

            long result = 0;

            Console.WriteLine("Day16: Puzzle 2 solution - " + result);
        }
    }
}