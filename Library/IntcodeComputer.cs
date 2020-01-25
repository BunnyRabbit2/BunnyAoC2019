using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2019
{
    public class IntcodeComputer
    {
        public static int[] loadIntCodeProgram(String fileLocation)
        {
            int[] intCodeProgram;

            if (File.Exists(fileLocation))
            {
                intCodeProgram = File.ReadAllText(fileLocation).Split(',').Select(l => int.Parse(l)).ToArray();

                return intCodeProgram;
            }
            else
            {
                Console.WriteLine("Intcode Computer: Invalid File Location");
                return new int[0];
            }
        }

        public static int runIntcodeProgram(int[] icPIn, int resultAddress = -1, int[] inputsIn = null, int relativeBaseIn = 0)
        {
            int[] inputs = inputsIn ?? new int[0];
            bool terminated = false;
            int nextI = 0;
            int result = 0;
            
            while (!terminated)
            {
                result = IntcodeComputer.runIntcodeProgramPausable(icPIn, out icPIn, out nextI, out terminated, relativeBase: relativeBaseIn,
                                                                    inputsIn: inputs, restartIndex: nextI, resultAddress: resultAddress);
            }

            return result;
        }

        public static int runIntcodeProgramPausable(int[] icPIn, out int[] icPOut, out int nextI, out bool programTerminated, int relativeBase = 0,
                                                    int resultAddress = -1, int[] inputsIn = null, int restartIndex = 0)
        {
            int[] inputs = inputsIn ?? new int[0];
            List<int> icP = icPIn.ToList();
            int currentInput = 0;
            int output = 0;

            for (int i = restartIndex; i < icP.Count; i += 0)
            {
                if (icP[i] == 99) // Exit opcode
                    break;

                int opcode = icP[i] % 10; // Gets the final two digits no matter what else is there

                int firstMode = (icP[i] / 100) % 10;
                int secondMode = (icP[i] / 1000) % 10;
                int thirdMode = (icP[i] / 10000) % 10;

                int firstParam = 0;
                int secondParam = 0;
                int thirdParam = 0;

                if (i + 1 < icP.Count - 1)
                    firstParam = icP[i + 1];
                if (i + 2 < icP.Count - 1)
                    secondParam = icP[i + 2];
                if (i + 3 < icP.Count - 1)
                    thirdParam = icP[i + 3];

                if (opcode == 3) // Input opcode
                {
                    if (currentInput < inputs.Length)
                    {
                        icP[firstParam] = inputs[currentInput];
                        currentInput++;
                        i += 2;
                    }
                    else
                    {
                        Console.WriteLine("IntcodeComputer error. Program asked for input not provided");
                        Console.WriteLine(">>> Number input being asked for: " + currentInput);
                        Console.WriteLine(">>> Length of input array: " + inputs.Length);
                        break;
                    }
                    continue;
                }
                else if (opcode == 4) // Output opcode
                {
                    if (firstMode == 1)
                    {
                        output = firstParam;
                    }
                    else
                    {
                        output = icP[firstParam];
                    }

                    nextI = i + 2;
                    if (secondParam == 99)
                        programTerminated = true;
                    else
                        programTerminated = false;

                    icPOut = icP.ToArray();
                    return output;
                }
                int firstValue = 0;
                if(firstMode == 1) firstValue = firstParam;
                else if(firstMode == 2) firstValue = icP[firstParam + relativeBase];
                else firstValue = icP[firstParam];

                int secondValue = 0;
                if(secondMode == 1) secondValue = secondParam;
                else if(secondMode == 2) secondValue = icP[secondParam + relativeBase];
                else secondValue = icP[secondParam];

                if (opcode == 1) // addition opcode
                {
                    icP[thirdParam] = firstValue + secondValue;
                    i += 4;
                    continue;
                }
                else if (opcode == 2) // multiplication opcode
                {
                    icP[thirdParam] = firstValue * secondValue;
                    i += 4;
                    continue;
                }
                else if (opcode == 5)
                {
                    if (firstValue != 0)
                        i = secondValue;
                    else
                        i += 3;
                    continue;
                }
                else if (opcode == 6)
                {
                    if (firstValue == 0)
                        i = secondValue;
                    else
                        i += 3;
                    continue;
                }
                else if (opcode == 7)
                {
                    if (firstValue < secondValue)
                        icP[thirdParam] = 1;
                    else
                        icP[thirdParam] = 0;

                    i += 4;
                    continue;
                }
                else if (opcode == 8)
                {
                    if (firstValue == secondValue)
                        icP[thirdParam] = 1;
                    else
                        icP[thirdParam] = 0;

                    i += 4;
                    continue;
                }
                else if (opcode == 9)
                {
                    relativeBase += firstValue;

                    i += 2;
                    continue;
                }
                else
                {
                    Console.WriteLine("IntcodeComputer error. Program opcode not recognised - " + opcode);
                    break; // Forgot this before. Caused an infinite loop and locked shit up. Nice.
                }
            }

            // For intcode programs with no opcode 4
            nextI = -1;
            programTerminated = true;
            icPOut = icP.ToArray();
            if (resultAddress == -1)
                return output;
            else
                return icP[resultAddress];
        }
    }
}