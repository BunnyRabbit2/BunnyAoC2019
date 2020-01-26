using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2019
{
    public class IntcodeComputer
    {
        long[] inputs;
        bool terminated, programLoaded;
        long nextI;
        long result;
        long relativeBase;
        long[] intcodeProgram;

        public IntcodeComputer()
        {
            setDefaults();
        }

        public IntcodeComputer(string fileLocation)
        {
            setDefaults();
            loadIntCodeProgram(fileLocation);
        }

        void setDefaults()
        {
            terminated = false;
            programLoaded = false;
            nextI = 0;
            result = 0;
            relativeBase = 0;
        }

        public long[] getIntcodeProgram()
        {
            return intcodeProgram;
        }

        public void loadIntCodeProgram(String fileLocation)
        {
            if (File.Exists(fileLocation))
            {
                intcodeProgram = File.ReadAllText(fileLocation).Split(',').Select(l => long.Parse(l)).ToArray();
                programLoaded = true;
            }
            else
            {
                Console.WriteLine("Intcode Computer: Invalid File Location");
            }
        }

        public long runIntcodeProgram(long resultAddress = -1, long[] inputsIn = null)
        {
            long[] inputs = inputsIn ?? new long[0];
            // Reset variables before running program
            nextI = 0;
            result = 0;
            relativeBase = 0;
            terminated = false;

            long[] icP = (long[])intcodeProgram.Clone();

            while (!terminated)
            {
                result = runIntcodeProgramPausable(icP, out nextI, out terminated, inputsIn: inputs, restartIndex: nextI, resultAddress: resultAddress);
            }

            return result;
        }

        public long runIntcodeProgramPausable(long[] icPIn, out long nextI, out bool programTerminated,
                                                    long resultAddress = -1, long[] inputsIn = null, long restartIndex = 0)
        {
            if (!programLoaded)
            {
                nextI = -1;
                programTerminated = true;
                return -1; // Program isn't loaded
            }

            long[] inputs = inputsIn ?? new long[0];
            Dictionary<long, long> icPExtra = new Dictionary<long, long>();
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
                        if (firstMode == 2)
                        {
                            setValueToAddress(firstParam + relativeBase, inputs[currentInput], icPIn, icPExtra);
                        }
                        else
                        {
                            setValueToAddress(firstParam, inputs[currentInput], icPIn, icPExtra);
                        }
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
                    else if (firstMode == 2)
                    {
                        output = getValueFromAddress(firstParam + relativeBase, icPIn, icPExtra);
                    }
                    else
                    {
                        output = getValueFromAddress(firstParam, icPIn, icPExtra);
                    }

                    nextI = i + 2;
                    if (secondParam == 99)
                        programTerminated = true;
                    else
                        programTerminated = false;

                    return output;
                }
                long firstValue = 0;
                if (firstMode == 1)
                    firstValue = firstParam;
                else if (firstMode == 2)
                    firstValue = getValueFromAddress(firstParam + relativeBase, icPIn, icPExtra);
                else
                    firstValue = getValueFromAddress(firstParam, icPIn, icPExtra);

                long secondValue = 0;
                if (secondMode == 1)
                    secondValue = secondParam;
                else if (secondMode == 2)
                    secondValue = getValueFromAddress(secondParam + relativeBase, icPIn, icPExtra);
                else
                    secondValue = getValueFromAddress(secondParam, icPIn, icPExtra);

                long thirdValue = 0;
                if (thirdMode == 2)
                    thirdValue = thirdParam + relativeBase;
                else
                    thirdValue = thirdParam;

                if (opcode == 1) // addition opcode
                {
                    setValueToAddress(thirdValue, firstValue + secondValue, icPIn, icPExtra);
                    i += 4;
                    continue;
                }
                else if (opcode == 2) // multiplication opcode
                {
                    setValueToAddress(thirdValue, firstValue * secondValue, icPIn, icPExtra);
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
                        setValueToAddress(thirdValue, 1, icPIn, icPExtra);
                    else
                        setValueToAddress(thirdValue, 0, icPIn, icPExtra);

                    i += 4;
                    continue;
                }
                else if (opcode == 8)
                {
                    if (firstValue == secondValue)
                        setValueToAddress(thirdValue, 1, icPIn, icPExtra);
                    else
                        setValueToAddress(thirdValue, 0, icPIn, icPExtra);

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
                return getValueFromAddress(resultAddress, icPIn, icPExtra);
        }

        void setValueToAddress(long address, long value, long[] icPIn, Dictionary<long, long> icPEIn)
        {
            if (address >= icPIn.Length)
            {
                long newAdd = address - icPIn.Length;
                if (icPEIn.ContainsKey(newAdd))
                {
                    icPEIn[newAdd] = value;
                }
                else
                {
                    icPEIn.Add(newAdd, value);
                }
            }
            else
            {
                icPIn[address] = value;
            }
        }

        public void setValueToAddress(long address, long value)
        {
            if (address < intcodeProgram.Length)
            {
                intcodeProgram[address] = value;
            }
            else
            {
                Console.WriteLine("Invalid address to set value");
            }
        }

        long getValueFromAddress(long address, long[] icPIn, Dictionary<long, long> icPEIn)
        {
            if (address >= icPIn.Length)
            {
                long newAdd = address - icPIn.Length;
                long output = 0;
                if (icPEIn.ContainsKey(newAdd))
                {
                    output = icPEIn[newAdd];
                }
                else
                {
                    icPEIn.Add(newAdd, 0);
                    output = 0;
                }
                return output;
            }
            else
            {
                return icPIn[address];
            }
        }
    }
}