using System;
using System.IO;
using System.Collections;

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

        ArrayList inputs;
        bool inputsLoaded;

        public Day1()
        {
            inputs = new ArrayList();
            inputsLoaded = false;
        }
        public void loadInputs(string fileLocation)
        {
            if (File.Exists(fileLocation))
            {
                string text = File.ReadAllText(fileLocation);

                string[] numbers = text.Split(Environment.NewLine);

                foreach (var s in numbers)
                {
                    int test = -1;

                    int.TryParse(s, out test);

                    if (test != -1 && test != 0)
                        inputs.Add(test);
                }

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