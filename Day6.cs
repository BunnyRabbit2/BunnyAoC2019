using System;
using System.IO;
using System.Collections;

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

        string[] orbits;
        bool inputsLoaded;

        public Day6()
        {
            inputsLoaded = false;
        }

        public void loadInputs(string fileLocation)
        {
            if (File.Exists(fileLocation))
            {
                orbits = File.ReadAllText(fileLocation).Split(Environment.NewLine);

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