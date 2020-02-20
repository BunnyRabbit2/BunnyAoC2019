using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2019
{
    public class Day12
    {
        public static void solveDay12()
        {
            Day12 d12 = new Day12();
            d12.loadInputs("inputs/Day12.txt");
            d12.solvePuzzle1();
            d12.solvePuzzle2();
        }

        bool inputsLoaded;
        List<Moon> moons;

        public Day12()
        {            
            moons = new List<Moon>();
        }

        public void loadInputs(string fileLocation)
        {
            if (File.Exists(fileLocation))
            {
                string[] inputs = File.ReadAllText(fileLocation).Split(Environment.NewLine);

                foreach (var line in inputs)
                {
                    moons.Add(new Moon(line));
                }

                inputsLoaded = true;
            }
            else
            {
                Console.WriteLine("Day12: Invalid File Location");
            }
        }


        public void solvePuzzle1()
        {
            int result = 0;

            Console.WriteLine("Day12: Puzzle 1 solution - " + result);
        }

        public void solvePuzzle2()
        {
            int result = 0;

            Console.WriteLine("Day12: Puzzle 2 solution - " + result);
        }
    }
}