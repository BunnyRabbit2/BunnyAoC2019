using System;
using System.IO;
using System.Collections.Generic;

namespace AdventOfCode2019
{
    public class Day6
    {
        public static void solveDay6()
        {
            Day6 d6 = new Day6();
            d6.loadInputs("inputs/day6.txt");
            d6.solvePuzzle1();
            d6.solvePuzzle2();
        }

        string[] orbitCodes;
        List<OrbitTreeNode> orbits;
        bool inputsLoaded;

        public Day6()
        {
            orbits = new List<OrbitTreeNode>();
            inputsLoaded = false;
        }

        public void loadInputs(string fileLocation)
        {
            if (File.Exists(fileLocation))
            {
                orbitCodes = File.ReadAllText(fileLocation).Split(Environment.NewLine);

                foreach(string o in orbitCodes)
                {
                    OrbitTreeNode.AddOrbitToTree(orbits, o);
                }
                OrbitTreeNode.SetParentChildRelationships(orbits);

                OrbitTreeNode.SetDistancesFromRoot(orbits);

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
                int totalOrbits = 0;

                foreach(OrbitTreeNode n in orbits)
                {
                    totalOrbits += n.distanceToRoot;
                }

                Console.WriteLine("Day6: Puzzle 1 solution - " + totalOrbits);
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