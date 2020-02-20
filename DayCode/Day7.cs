using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2019
{
    public class Day7
    {
        public static void solveDay7()
        {
            Day7 d7 = new Day7();
            d7.solvePuzzle1();
            d7.solvePuzzle2();
        }

        IntcodeComputer icP;
        string programLoc;

        public Day7()
        {
            programLoc = "inputs/day7.txt";
        }

        public void solvePuzzle1()
        {
            icP = new IntcodeComputer(programLoc);

            List<List<int>> phaseSettings = AOCHelpers.GeneratePermutations(new List<int>() { 0, 1, 2, 3, 4 });

            long maxSignal = 0;

            // Run each permutation of the phase settings through the amps to find the highest output
            foreach (List<int> ps in phaseSettings)
            {
                long signal = 0;

                for (int i = 0; i < ps.Count; i++)
                {
                    icP.resetProgram();

                    icP.addInput(ps[i]);
                    icP.addInput(signal);

                    long output = icP.runIntcodeProgramFull();
                    signal = output;
                }

                if (signal > maxSignal)
                    maxSignal = signal;
            }

            Console.WriteLine("Day7: Puzzle 1 solution - " + maxSignal);
        }

        public void solvePuzzle2()
        {
            icP = new IntcodeComputer(programLoc);
            
            List<List<int>> phaseSettings = AOCHelpers.GeneratePermutations(new List<int>() { 5, 6, 7, 8, 9 });

            long maxSignal = 0;

            // Run each permutation of the phase settings through the amps to find the highest output
            foreach (List<int> ps in phaseSettings)
            {
                long signal = 0;
                List<IntcodeComputer> amps = new List<IntcodeComputer>();
                for (int i = 0; i < 5; i++)
                {
                    amps.Add(new IntcodeComputer(programLoc));
                }
                bool[] terminated = new bool[5];

                // First loop round the amps
                for (int i = 0; i < ps.Count; i++)
                {
                    amps[i].addInput(ps[i]);
                    amps[i].addInput(signal);

                    signal = amps[i].runIntcodeProgram(out terminated[i]);
                }

                while (!terminated[4])
                {
                    for (int i = 0; i < 5; i++)
                    {
                        amps[i].addInput(signal);
                        signal = amps[i].runIntcodeProgram(out terminated[i]);
                    }
                }

                if (signal > maxSignal)
                    maxSignal = signal;
            }

            Console.WriteLine("Day7: Puzzle 2 solution - " + maxSignal);
        }
    }
}