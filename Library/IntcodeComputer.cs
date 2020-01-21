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

        public static int[] runIntcodeProgram(int[] icPIn, int resultAddress = 0, int[] inputsIn = null)
        {
            int[] inputs = inputsIn ?? new int[0];
            List<int> outputs = new List<int>();
            int currentInput = 0;

            for (int i = 0; i < icPIn.Length; i += 0)
            {
                if (icPIn[i] == 99) // Exit opcode
                    break;

                int opcode = icPIn[i] % 10; // Gets the final two digits no matter what else is there

                int firstMode = (icPIn[i] / 100) % 10;
                int secondMode = (icPIn[i] / 1000) % 10;
                int thirdMode = (icPIn[i] / 10000) % 10;

                int firstParam = icPIn[i + 1];
                int secondParam = icPIn[i + 2];
                int thirdParam = icPIn[i + 3];

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
                    outputs.Add(icPIn[firstParam]);

                    if (icPIn[i + 2] == 99) // Program terminating
                        return outputs.ToArray();

                    i += 2;
                    continue;
                }
                int firstValue = (firstMode == 1) ? firstParam : icPIn[firstParam];
                int secondValue = (secondMode == 1) ? secondParam : icPIn[secondParam];

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
                else
                {
                    Console.WriteLine("IntcodeComputer error. Program opcode not recognised - " + opcode);
                    break; // Forgot this before. Caused an infinite loop and locked shit up. Nice.
                }
            }

            outputs.Add(icPIn[resultAddress]);
            return outputs.ToArray();
        }
    }
}