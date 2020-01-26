using System;
using System.IO;
using System.Collections.Generic;

namespace AdventOfCode2019
{
    public class Day10
    {
        OrbitTree oTree;

        public static void solveDay10()
        {
            Day10 d10 = new Day10();
            d10.loadInputs("inputs/Day10.txt");
            d10.solvePuzzle1();
            d10.solvePuzzle2();
        }

        bool[][] asteroidInPlace;
        List<Asteroid> asteroids;
        bool inputsLoaded;

        public Day10()
        {
            oTree = new OrbitTree();
            inputsLoaded = false;
        }

        public void loadInputs(string fileLocation)
        {
            if (File.Exists(fileLocation))
            {
                string[] lines = File.ReadAllText(fileLocation).Split(Environment.NewLine);

                inputsLoaded = true;
            }
            else
            {
                Console.WriteLine("Day10: Invalid File Location");
            }
        }

        public void solvePuzzle1()
        {
            if (inputsLoaded)
            {
                Console.WriteLine("Day10: Puzzle 1 solution - " + 0);
            }
        }

        public void solvePuzzle2()
        {
            if (inputsLoaded)
            {
                Console.WriteLine("Day10: Puzzle 2 solution - " + 0);
            }
        }
    }
}