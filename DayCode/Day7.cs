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
            d7.loadIntCodeProgram("inputs/day7.txt");
            d7.solvePuzzle1();
            d7.solvePuzzle2();
        }

        long[] intCodeProgram;
        bool intCodeProgramLoaded;

        public Day7()
        {
            intCodeProgramLoaded = false;
        }

        public void loadIntCodeProgram(string fileLocation)
        {
            intCodeProgram = IntcodeComputer.loadIntCodeProgram(fileLocation);

            if (intCodeProgram.Length > 0)
                intCodeProgramLoaded = true;
        }

        public void solvePuzzle1()
        {
            if (intCodeProgramLoaded)
            {
                List<List<int>> phaseSettings = AOCHelpers.GeneratePermutations(new List<int>() { 0, 1, 2, 3, 4 });

                long maxSignal = 0;

                // Run each permutation of the phase settings through the amps to find the highest output
                foreach (List<int> ps in phaseSettings)
                {
                    long signal = 0;

                    for (int i = 0; i < ps.Count; i++)
                    {
                        long output = IntcodeComputer.runIntcodeProgram((long[])intCodeProgram.Clone(), inputsIn: new long[] { ps[i], signal });
                        signal = output;
                    }

                    if (signal > maxSignal)
                        maxSignal = signal;
                }

                Console.WriteLine("Day7: Puzzle 1 solution - " + maxSignal);
            }
        }

        public void solvePuzzle2()
        {
            if (intCodeProgramLoaded)
            {
                List<List<int>> phaseSettings = AOCHelpers.GeneratePermutations(new List<int>() { 5, 6, 7, 8, 9 });

                long maxSignal = 0;

                // Run each permutation of the phase settings through the amps to find the highest output
                foreach (List<int> ps in phaseSettings)
                {
                    long signal = 0;
                    List<long[]> amps = new List<long[]>();
                    for (int i = 0; i < 5; i++)
                    {
                        amps.Add((long[])intCodeProgram.Clone());
                    }
                    long[] ampNextI = new long[5];
                    bool[] terminated = new bool[5];

                    // First loop round the amps
                    for (int i = 0; i < ps.Count; i++)
                    {
                        signal = IntcodeComputer.runIntcodeProgramPausable(amps[i], out ampNextI[i], out terminated[i],
                                        inputsIn: new long[] { ps[i], signal });
                    }

                    while (!terminated[4])
                    {
                        for (int i = 0; i < 5; i++)
                        {
                            signal = IntcodeComputer.runIntcodeProgramPausable(amps[i], out ampNextI[i], out terminated[i],
                                        inputsIn: new long[] { signal }, restartIndex: ampNextI[i]);
                        }
                    }

                    if (signal > maxSignal)
                        maxSignal = signal;
                }

                Console.WriteLine("Day7: Puzzle 2 solution - " + maxSignal);
            }
        }
    }
}