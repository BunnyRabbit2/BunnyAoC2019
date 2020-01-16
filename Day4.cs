using System;
using System.IO;

namespace AdventOfCode2019
{
    public class Day4
    {
        int checkMin, checkMax;
        bool inputsLoaded;

        public Day4()
        {
            checkMin = -1;
            checkMax = -1;
            inputsLoaded = false;
        }

        public void loadInputs(string fileLocation)
        {
            if (File.Exists(fileLocation))
            {
                string text = File.ReadAllText(fileLocation);

                string[] numbers = text.Split('-');

                int.TryParse(numbers[0], out checkMin);
                int.TryParse(numbers[1], out checkMax);

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
                Console.WriteLine("Checkmin = " + checkMin + " - Checkmax = " + checkMax);
            }
        }
    }
}