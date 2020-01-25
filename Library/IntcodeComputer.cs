using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2019
{
    public class IntcodeComputer
    {
        public static long[] loadIntCodeProgram(String fileLocation)
        {
            long[] IntcodeProgram;

            if (File.Exists(fileLocation))
            {
                IntcodeProgram = File.ReadAllText(fileLocation).Split(',').Select(l => long.Parse(l)).ToArray();

                return IntcodeProgram;
            }
            else
            {
                Console.WriteLine("Intcode Computer: Invalid File Location");
                return new long[0];
            }
        }

        public static long runIntcodeProgram(long[] icPIn, long resultAddress = -1, long[] inputsIn = null, long relativeBaseIn = 0)
        {
            long[] inputs = inputsIn ?? new long[0];
            bool terminated = false;
            long nextI = 0;
            long result = 0;
            
            while (!terminated)
            {
                result = IntcodeComputer.runIntcodeProgramPausable(icPIn, out nextI, out terminated, relativeBase: relativeBaseIn,
                                                                    inputsIn: inputs, restartIndex: nextI, resultAddress: resultAddress);
            }

            return result;
        }

        public static long runIntcodeProgramPausable(long[] icPIn, out long nextI, out bool programTerminated, long relativeBase = 0,
                                                    long resultAddress = -1, long[] inputsIn = null, long restartIndex = 0)
        {
            long[] inputs = inputsIn ?? new long[0];
            List<long> icPExtra = new List<long>();
            long currentInput = 0;
            long output = 0;

            for (long i = restartIndex; i < icPIn.Length; i += 0)
            {
                if (icPIn[i] == 99) // Exit opcode
                    break;

                long opcode = icPIn[i] % 10; // Gets the final two digits no matter what else is there

                long firstMode = (icPIn[i] / 100) % 10;
                long secondMode = (icPIn[i] / 1000) % 10;
                long thirdMode = (icPIn[i] / 10000) % 10;

                long firstParam = 0;
                long secondParam = 0;
                long thirdParam = 0;

                if (i + 1 < icPIn.Length - 1)
                    firstParam = icPIn[i + 1];
                if (i + 2 < icPIn.Length - 1)
                    secondParam = icPIn[i + 2];
                if (i + 3 < icPIn.Length - 1)
                    thirdParam = icPIn[i + 3];

                if (opcode == 3) // Input opcode
                {
                    if (currentInput < inputs.Length)
                    {
                        icPIn[firstParam] = inputs[currentInput];
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
                        output = icPIn[firstParam];
                    }

                    nextI = i + 2;
                    if (secondParam == 99)
                        programTerminated = true;
                    else
                        programTerminated = false;

                    return output;
                }
                long firstValue = 0;
                if(firstMode == 1) firstValue = firstParam;
                else if(firstMode == 2) firstValue = icPIn[firstParam + relativeBase];
                else firstValue = icPIn[firstParam];

                long secondValue = 0;
                if(secondMode == 1) secondValue = secondParam;
                else if(secondMode == 2) secondValue = icPIn[secondParam + relativeBase];
                else secondValue = icPIn[secondParam];

                if (opcode == 1) // addition opcode
                {
                    icPIn[thirdParam] = firstValue + secondValue;
                    i += 4;
                    continue;
                }
                else if (opcode == 2) // multiplication opcode
                {
                    icPIn[thirdParam] = firstValue * secondValue;
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
                        icPIn[thirdParam] = 1;
                    else
                        icPIn[thirdParam] = 0;

                    i += 4;
                    continue;
                }
                else if (opcode == 8)
                {
                    if (firstValue == secondValue)
                        icPIn[thirdParam] = 1;
                    else
                        icPIn[thirdParam] = 0;

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

            // For Intcode programs with no opcode 4
            nextI = -1;
            programTerminated = true;
            if (resultAddress == -1)
                return output;
            else
                return icPIn[resultAddress];
        }

        void addValueToAddress(long address, long value, long[] icPIn)
        {
            
        }
    }
}