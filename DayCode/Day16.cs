using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2019
{
    public class Day16
    {
        public static void solveDay16()
        {
            Day16 d16 = new Day16();
            d16.loadInputs("inputs/day16.txt");
            d16.solvePuzzle1();
            d16.solvePuzzle2();
        }

        List<long> inputs;
        bool inputsLoaded;
        int[] pattern;

        public Day16()
        {
            inputs = new List<long>();
            inputsLoaded = false;
            pattern = new int[] { 0, 1, 0, -1 };
        }

        public void loadInputs(string fileLoc)
        {
            if (File.Exists(fileLoc))
            {
                string input = File.ReadAllText(fileLoc);

                long i = 0;
                var numbers = input.ToArray().Select(c => long.TryParse(c.ToString(), out i) ? i : -1).ToArray().ToList();

                numbers.RemoveAll(n => n == -1);

                inputsLoaded = true;
            }
            else
            {
                Console.WriteLine("Day16: Invalid File Location");
            }
        }

        public void solvePuzzle1()
        {
            if (!inputsLoaded) return;

            long result = 0;

            List<long> testList = new List<long> { 1, 2, 3, 4, 5, 6, 7, 8 };
            List<long> restR = FFT(testList, 4);

            Console.WriteLine("Day16: Puzzle 1 solution - " + result);
        }

        public void solvePuzzle2()
        {
            if (!inputsLoaded) return;

            long result = 0;

            Console.WriteLine("Day16: Puzzle 2 solution - " + result);
        }

        List<long> FFT(List<long> listIn, int steps)
        {
            long[] stepArr = listIn.ToArray();
            long[] nextArr = new long[stepArr.Length];

            for (int i = 0; i < steps; i++)
            {
                for (int n = 0; n < stepArr.Length; n++)
                {
                    List<int> patternList = new List<int>();
                    int currentP = 0;
                    for (int p = 0; p < stepArr.Length + 1; p=p)
                    {
                        for (int p2 = 0; p2 < n+1; p2++)
                        {
                            patternList.Add(pattern[currentP]);
                            p++;
                        }
                        if (currentP == 3) currentP = 0;
                        else currentP++;
                    }
                    patternList.RemoveAt(0);
                    long nextN = 0;

                    for (int p = 0; p < stepArr.Length; p++)
                    {
                        nextN += patternList[p] * stepArr[p];
                    }

                    var NNS = nextN.ToString();
                    nextN = long.Parse(NNS.Substring(NNS.Length-1,1));

                    nextArr[n] = nextN;
                }
                stepArr = (long[])nextArr.Clone();
            }

            return nextArr.ToList();
        }
    }
}