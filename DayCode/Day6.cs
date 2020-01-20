using System;
using System.IO;
using System.Collections.Generic;

namespace AdventOfCode2019
{
    public class Day6
    {
        OrbitTree oTree;

        public static void solveDay6()
        {
            Day6 d6 = new Day6();
            d6.loadInputs("inputs/day6.txt");
            d6.solvePuzzle1();
            d6.solvePuzzle2();
        }

        string[] orbitCodes;
        bool inputsLoaded;

        public Day6()
        {
            oTree = new OrbitTree();
            inputsLoaded = false;
        }

        public void loadInputs(string fileLocation)
        {
            if (File.Exists(fileLocation))
            {
                orbitCodes = File.ReadAllText(fileLocation).Split(Environment.NewLine);

                oTree.createTree(orbitCodes);

                inputsLoaded = true;
            }
            else
            {
                Console.WriteLine("Day4: Invalid File Location");
            }
        }

        public void solvePuzzle1()
        {
            if (inputsLoaded)
            {
                Console.WriteLine("Day6: Puzzle 1 solution - " + oTree.getTotalOrbits());
            }
        }

        public void solvePuzzle2()
        {
            if (inputsLoaded)
            {
                
            }
        }
    }
}