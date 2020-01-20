using System;
using System.IO;
using System.Collections;
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

        public static int[] runIntcodeProgram(int[] icPIn)
        {
            for(int i = 0; i < icPIn.Length; i += 0)
            {
                if(icPIn[i] == 99) // Exit opcode
                    break;

                int opcode = icPIn[i] % 10; // Gets the final two digits no matter what else is there

                if(opcode == 1) // addition opcode
                {
                    // Result = first + second
                    icPIn[icPIn[i+3]] = icPIn[icPIn[i+1]] + icPIn[icPIn[i+2]];
                    i += 4;
                }
                else if(opcode == 2) // multiplication opcode
                {
                    // Result = first * second
                    icPIn[icPIn[i+3]] = icPIn[icPIn[i+1]] * icPIn[icPIn[i+2]];
                    i += 4;
                }
                else
                {
                    Console.WriteLine("IntcodeComputer error. Program opcode not recognised - " + opcode);
                }
            }

            return icPIn;
        }
    }
}