using System;
using System.IO;
using System.Collections;

namespace AdventOfCode2019
{
    public class Day1
    {
        ArrayList inputs;
        bool inputsLoaded;

        public Day1()
        {
            inputs = new ArrayList();
            inputsLoaded = false;
        }
        public void loadInputs(string fileLocation)
        {
            int totalFuel = 0;

            if (File.Exists(fileLocation))
            {
                string text = File.ReadAllText(fileLocation);

                string[] numbers = text.Split(null);

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

                Console.WriteLine("Day1: Puzzle 1 Solution is " + totalFuel);
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

                Console.WriteLine("Day1: Puzzle 2 Solution is " + totalFuel);
            }
        }

        int getFuelFromMass(int massIn)
        {
            return (massIn / 3) - 2;
        }
    }
}