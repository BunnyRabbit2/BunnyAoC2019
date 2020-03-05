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
                inputs = input.ToArray().Select(c => long.TryParse(c.ToString(), out i) ? i : -1).ToArray().ToList();

                inputs.RemoveAll(n => n == -1);

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

            string result = "";

            var resultR = FFT(inputs, 100);

            for (int i = 0; i < 8; i++)
            {
                result += resultR[i];
            }

            // List<long> testList = new List<long> { 1, 2, 3, 4, 5, 6, 7, 8 };
            // List<long> restR = FFT(testList, 4);

            Console.WriteLine("Day16: Puzzle 1 solution - " + result);
        }

        public void solvePuzzle2()
        {
            if (!inputsLoaded) return;

            string result = "";

            List<long> insaneList = new List<long>();
            for (int i = 0; i < 10000; i++)
            {
                foreach(var n in inputs)
                {
                    insaneList.Add(n);
                }
            }

            string messagePos = "";

            for (int i = 0; i < 7; i++)
            {
                messagePos += inputs[i];
            }

            int messageP = int.Parse(messagePos);

            var resultR = FFT(insaneList, 100);

            for (int i = messageP; i < messageP+8; i++)
            {
                result += resultR[i];
            }

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
                    int currentP = 0;
                    bool first = true;
                    long nextN = 0;
                    bool breakLoop = false;

                    for (int p = 0; p < stepArr.Length + 1; p=p)
                    {
                        for (int p2 = 0; p2 < n+1; p2++)
                        {
                            if(first)
                            {
                                first = false;
                                continue;
                            }
                            if(p >= stepArr.Length)
                            {
                                breakLoop = true;
                                break;
                            }

                            nextN += pattern[currentP] * stepArr[p];
                            p++;
                        }
                        if(breakLoop) break;
                        if (currentP == 3) currentP = 0;
                        else currentP++;
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