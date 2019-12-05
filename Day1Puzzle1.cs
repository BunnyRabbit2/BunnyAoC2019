using System;
using System.IO;
using System.Collections;

namespace AdventOfCode2019
{
    // Take input of module mass. Fuel = half mass rounded down -2.

    public class Day1Puzzle1
    {
        ArrayList inputs;
        bool inputsLoaded;

        public Day1Puzzle1()
        {
            inputs = new ArrayList();
            inputsLoaded = false;
        }

        public void loadInputs(string fileLocation)
        {
            if(File.Exists(fileLocation))
            {
                string text = File.ReadAllText(fileLocation);

                string[] numbers = text.Split(null);

                foreach( var s in numbers )
                {
                    inputs.Add(int.Parse(s));
                }

                foreach( var i in inputs)
                {
                    Console.WriteLine(i);
                }

                inputsLoaded = true;
            }
            else
            {
                Console.WriteLine("Day1Puzzle1: Invalid File Location");
            }
        }
    }
}