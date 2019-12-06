using System;
using System.IO;
using System.Collections;
using System.Linq;

namespace AdventOfCode2019
{
    public class IntcodeComputer
    {
        public static int[] runIntcodeProgram(int[] icPIn)
        {
            for(int i = 0; i < icPIn.Length; i += 4)
            {
                if(icPIn[i] == 99) // Exit opcode
                    break;

                int[] program = {icPIn[i], icPIn[i+1], icPIn[i+2], icPIn[i+3]};

                int first = icPIn[program[1]];
                int second = icPIn[program[2]];
                int result = 0;

                if(program[0] == 1) // addition opcode
                {
                    result = first + second;
                    icPIn[program[3]] = result;
                }
                else if(program[0] == 2) // multiplication opcode
                {
                    result = first * second;
                    icPIn[program[3]] = result;
                }
                else
                {
                    Console.WriteLine("IntcodeComputer error. Program opcode not recognised - " + program[0]);
                }
            }

            return icPIn;
        }
    }
}