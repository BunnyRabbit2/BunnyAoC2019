using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2019
{
    class IntcodeComputer
    {
        long result;
        long relativeBase;
        long[] intcodeProgram, initialProgram;
        Dictionary<long, long> extraMemory;
        long readAddress;
        List<long> inputs;
        int currentInput;
        bool terminated, programLoaded;

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
            result = 0;
            relativeBase = 0;
            extraMemory = new Dictionary<long, long>();
            readAddress = 0;
            inputs = new List<long>();
            currentInput = 0;
            terminated = programLoaded = false;
        }

        public void resetProgram()
        {
            result = 0;
            relativeBase = 0;
            extraMemory = new Dictionary<long, long>();
            readAddress = 0;
            inputs = new List<long>();
            currentInput = 0;
            terminated = false;
            intcodeProgram = (long[])initialProgram.Clone();
        }

        public void addInput(long input)
        {
            inputs.Add(input);
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
                initialProgram = (long[])intcodeProgram.Clone();
                programLoaded = true;
            }
            else
            {
                Console.WriteLine("Intcode Computer: Invalid File Location");
            }
        }

        public void setValueToAddress(long address, long value)
        {
            if (address >= intcodeProgram.Length)
            {
                long newAdd = address - intcodeProgram.Length;
                if (extraMemory.ContainsKey(newAdd))
                {
                    extraMemory[newAdd] = value;
                }
                else
                {
                    extraMemory.Add(newAdd, value);
                }
            }
            else
            {
                intcodeProgram[address] = value;
            }
        }

        long getValueFromAddress(long address)
        {
            if (address >= intcodeProgram.Length)
            {
                long newAdd = address - intcodeProgram.Length;
                long output = 0;
                if (extraMemory.ContainsKey(newAdd))
                {
                    output = extraMemory[newAdd];
                }
                else
                {
                    extraMemory.Add(newAdd, 0);
                    output = 0;
                }
                return output;
            }
            else
            {
                return intcodeProgram[address];
            }
        }

        public long runIntcodeProgramFull(long resultAddress = -1)
        {
            while (!terminated)
            {
                result = runIntcodeProgram(out terminated, resultAddress: resultAddress);
            }

            return result;
        }

        public long runIntcodeProgram(out bool programTerminated, long resultAddress = -1)
        {
            if (!programLoaded)
            {
                programTerminated = true;
                return -1; // Program isn't loaded
            }

            long output = 0;

            while (!terminated)
            {
                if (intcodeProgram[readAddress] == 99) // Exit opcode
                    break;

                long opcode = intcodeProgram[readAddress] % 10; // Gets the final two digits no matter what else is there

                long firstMode = (intcodeProgram[readAddress] / 100) % 10;
                long secondMode = (intcodeProgram[readAddress] / 1000) % 10;
                long thirdMode = (intcodeProgram[readAddress] / 10000) % 10;

                long firstParam = 0;
                long secondParam = 0;
                long thirdParam = 0;

                if (readAddress + 1 < intcodeProgram.Length - 1)
                    firstParam = intcodeProgram[readAddress + 1];
                if (readAddress + 2 < intcodeProgram.Length - 1)
                    secondParam = intcodeProgram[readAddress + 2];
                if (readAddress + 3 < intcodeProgram.Length - 1)
                    thirdParam = intcodeProgram[readAddress + 3];

                if (opcode == 3) // Input opcode
                {
                    if (currentInput < inputs.Count)
                    {
                        if (firstMode == 2)
                        {
                            setValueToAddress(firstParam + relativeBase, inputs[currentInput]);
                        }
                        else
                        {
                            setValueToAddress(firstParam, inputs[currentInput]);
                        }
                        currentInput++;
                        readAddress += 2;
                    }
                    else
                    {
                        Console.WriteLine("IntcodeComputer error. Program asked for input not provided");
                        Console.WriteLine(">>> Number input being asked for: " + currentInput);
                        Console.WriteLine(">>> Length of input array: " + inputs.Count);
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
                        output = getValueFromAddress(firstParam + relativeBase);
                    }
                    else
                    {
                        output = getValueFromAddress(firstParam);
                    }

                    readAddress += 2;
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
                    firstValue = getValueFromAddress(firstParam + relativeBase);
                else
                    firstValue = getValueFromAddress(firstParam);

                long secondValue = 0;
                if (secondMode == 1)
                    secondValue = secondParam;
                else if (secondMode == 2)
                    secondValue = getValueFromAddress(secondParam + relativeBase);
                else
                    secondValue = getValueFromAddress(secondParam);

                long thirdValue = 0;
                if (thirdMode == 2)
                    thirdValue = thirdParam + relativeBase;
                else
                    thirdValue = thirdParam;

                if (opcode == 1) // addition opcode
                {
                    setValueToAddress(thirdValue, firstValue + secondValue);
                    readAddress += 4;
                    continue;
                }
                else if (opcode == 2) // multiplication opcode
                {
                    setValueToAddress(thirdValue, firstValue * secondValue);
                    readAddress += 4;
                    continue;
                }
                else if (opcode == 5)
                {
                    if (firstValue != 0)
                        readAddress = secondValue;
                    else
                        readAddress += 3;
                    continue;
                }
                else if (opcode == 6)
                {
                    if (firstValue == 0)
                        readAddress = secondValue;
                    else
                        readAddress += 3;
                    continue;
                }
                else if (opcode == 7)
                {
                    if (firstValue < secondValue)
                        setValueToAddress(thirdValue, 1);
                    else
                        setValueToAddress(thirdValue, 0);

                    readAddress += 4;
                    continue;
                }
                else if (opcode == 8)
                {
                    if (firstValue == secondValue)
                        setValueToAddress(thirdValue, 1);
                    else
                        setValueToAddress(thirdValue, 0);

                    readAddress += 4;
                    continue;
                }
                else if (opcode == 9)
                {
                    relativeBase += firstValue;

                    readAddress += 2;
                    continue;
                }
                else
                {
                    Console.WriteLine("IntcodeComputer error. Program opcode not recognised - " + opcode);
                    break; // Forgot this before. Caused an infinite loop and locked shit up. Nice.
                }
            }

            // For Intcode programs with no opcode 4
            programTerminated = true;
            if (resultAddress == -1)
                return output;
            else
                return getValueFromAddress(resultAddress);
        }
    }
}