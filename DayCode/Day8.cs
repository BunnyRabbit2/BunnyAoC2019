using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2019
{
    public class Day8
    {
        public static void solveDay8()
        {
            Day8 d8 = new Day8();
            d8.loadInputs("inputs/day8.txt");
            d8.solvePuzzle1();
            d8.solvePuzzle2();
        }

        int[] inputs;
        bool inputsLoaded;

        public Day8()
        {
            inputsLoaded = false;
        }
        public void loadInputs(string fileLocation)
        {
            if (File.Exists(fileLocation))
            {
                inputs = File.ReadAllText(fileLocation).ToCharArray().Select(l => int.Parse(l.ToString())).ToArray();

                inputsLoaded = true;
            }
            else
            {
                Console.WriteLine("Day8: Invalid File Location");
            }
        }

        public void solvePuzzle1()
        {
            if (inputsLoaded)
            {
                List<int[]> layers = SpaceImageFormat.turnDataIntoLayers(inputs, 25, 6);
                int verfication = SpaceImageFormat.verifyData(layers);

                Console.WriteLine("Day8: Puzzle 1 Solution - " + verfication);
            }
        }

        public void solvePuzzle2()
        {
            if (inputsLoaded)
            {
                Console.WriteLine("Day8: Puzzle 2 Solution - ");
            }
        }
    }
}