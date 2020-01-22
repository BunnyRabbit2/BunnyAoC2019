using System;
using System.IO;
using System.Linq;

namespace AdventOfCode2019
{
    public class Day1
    {
        public static void solveDay1()
        {
            Day1 d1 = new Day1();
            d1.loadInputs("inputs/day1.txt");
            d1.solvePuzzle1();
            d1.solvePuzzle2();
        }

        int[] inputs;
        bool inputsLoaded;

        public Day1()
        {
            inputsLoaded = false;
        }
        public void loadInputs(string fileLocation)
        {
            if (File.Exists(fileLocation))
            {
                inputs = File.ReadAllText(fileLocation).Split(Environment.NewLine).Select(l => int.Parse(l)).ToArray();

                inputsLoaded = true;
            }
            else
            {
                Console.WriteLine("Day1: Invalid File Location");
            }
        }

        public void solvePuzzle1()
        {
            if (inputsLoaded)
            {
                int totalFuel = 0;

                foreach (int i in inputs)
                {
                    totalFuel += getFuelFromMass(i);
                }

                Console.WriteLine("Day1: Puzzle 1 Solution - " + totalFuel);
            }
        }

        public void solvePuzzle2()
        {
            if(inputsLoaded)
            {
                int totalFuel = 0;

                foreach (int i in inputs)
                {
                    int nextFuel = i;

                    while(true)
                    {
                        int fuel = getFuelFromMass(nextFuel);
                        
                        if(fuel < 1)
                            break;
                        
                        nextFuel = fuel;
                        totalFuel += fuel;
                    }
                }

                Console.WriteLine("Day1: Puzzle 2 Solution - " + totalFuel);
            }
        }

        int getFuelFromMass(int massIn)
        {
            return (massIn / 3) - 2;
        }
    }
}