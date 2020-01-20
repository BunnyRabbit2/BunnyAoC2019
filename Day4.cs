using System;
using System.IO;
using System.Collections;

namespace AdventOfCode2019
{
    public class Day4
    {
        public static void solveDay4()
        {
            Day4 d4 = new Day4();
            d4.loadInputs("inputs/day4.txt");
            d4.solvePuzzle1();
            d4.solvePuzzle2();
        }

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
                ArrayList correctPasswords = new ArrayList();

                for (int i = checkMin; i <= checkMax; i++)
                {
                    if (checkPasswordPuzzle1(i))
                        correctPasswords.Add(i);
                }

                Console.WriteLine("Day4: Puzzle 1 Solution - " + correctPasswords.Count);
            }
        }

        public void solvePuzzle2()
        {
            if (inputsLoaded)
            {
                ArrayList correctPasswords = new ArrayList();

                for (int i = checkMin; i <= checkMax; i++)
                {
                    if (checkPasswordPuzzle2(i))
                        correctPasswords.Add(i);
                }

                Console.WriteLine("Day4: Puzzle 2 Solution - " + correctPasswords.Count);
            }
        }

        bool checkPasswordPuzzle1(int pIn)
        {
            string pString = pIn.ToString();

            //Rule 1.
            if (pString.Length != 6)
                return false;

            if (pIn < checkMin || pIn > checkMax)
                return false;

            bool hasDouble = false;

            for (int i = 0; i < 5; i++)
            {
                if (pString[i] > pString[i + 1])
                    return false;
                if (pString[i] == pString[i + 1])
                {
                    hasDouble = true;
                }
            }

            return hasDouble;
        }

        bool checkPasswordPuzzle2(int pIn)
        {
            string pString = pIn.ToString();

            //Rule 1.
            if (pString.Length != 6)
                return false;

            if (pIn < checkMin || pIn > checkMax)
                return false;

            bool hasDouble = false;

            for (int i = 0; i < 5; i++)
            {
                if (pString[i] > pString[i + 1])
                    return false;
                if (pString[i] == pString[i + 1])
                {
                    if(i == 4)
                    {
                        if(pString[i-1] != pString[i])
                            hasDouble = true;
                    }
                    else if(i == 0)
                    {
                        if(pString[i+1] != pString[i+2])
                            hasDouble = true;
                    }
                    else if(pString[i-1] != pString[i] && pString[i+1] != pString[i+2])
                    {
                        hasDouble = true;
                    }
                }
            }

            return hasDouble;
        }
    }
}