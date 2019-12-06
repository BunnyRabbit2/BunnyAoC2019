using System;
using System.IO;
using System.Collections;
using System.Linq;

namespace AdventOfCode2019
{
    public class Day2
    {
        ArrayList intCodeProgram;
        bool intCodeProgramLoaded;

        public Day2()
        {
            intCodeProgram = new ArrayList();
            intCodeProgramLoaded = false;
        }

        public void loadIntCodeProgram(string fileLocation)
        {
            if (File.Exists(fileLocation))
            {
                string text = File.ReadAllText(fileLocation);

                string[] numbers = text.Split(",");

                foreach (var s in numbers)
                {
                    int test = -1;

                    int.TryParse(s, out test);

                    if (test != -1)
                        intCodeProgram.Add(test);
                }

                intCodeProgramLoaded = true;
            }
            else
            {
                Console.WriteLine("Day2: Invalid File Location");
            }
        }

        public void solvePuzzle1()
        {
            int[] icP = intCodeProgram.OfType<int>().ToArray();

            // Pre running operations
            icP[1] = 12;
            icP[2] = 2;
            
            for(int i = 0; i < icP.Length; i += 4)
            {
                if(icP[i] == 99) // Exit code
                    break;

                int[] program = {icP[i], icP[i+1], icP[i+2], icP[i+3]};

                int first = icP[program[1]];
                int second = icP[program[2]];
                int result = 0;

                if(program[0] == 1)
                {
                    result = first + second;
                    icP[program[3]] = result;
                }
                else if(program[0] == 2)
                {
                    result = first * second;
                    icP[program[3]] = result;
                }
                else
                {
                    Console.WriteLine("Day2: Puzzle2 error. program opcode not recognised - " + program[0]);
                }
            }

            Console.WriteLine("Day2: Puzzle 1 solution - " + icP[0]);
        }
    }
}