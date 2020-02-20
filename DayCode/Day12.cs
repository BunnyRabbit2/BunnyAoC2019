using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2019
{
    public class Day12
    {
        public static void solveDay12()
        {
            Day12 d12 = new Day12();
            d12.loadInputs("inputs/Day12.txt");
            d12.solvePuzzle1();
            d12.solvePuzzle2();
        }

        bool inputsLoaded;
        List<Moon> moons;

        public Day12()
        {
            moons = new List<Moon>();
        }

        public void loadInputs(string fileLocation)
        {
            if (File.Exists(fileLocation))
            {
                string[] inputs = File.ReadAllText(fileLocation).Split(Environment.NewLine);

                foreach (var line in inputs)
                {
                    moons.Add(new Moon(line));
                }

                inputsLoaded = true;
            }
            else
            {
                Console.WriteLine("Day12: Invalid File Location");
            }
        }

        void resetSimulation()
        {
            foreach(Moon m in moons)
            {
                m.resetMoon();
            }
        }

        void simulationStep()
        {
            foreach (Moon m1 in moons)
            {
                foreach (Moon m2 in moons)
                {
                    m1.applyGravity(m2);
                }
            }

            foreach (Moon m in moons)
            {
                m.moveStep();
            }
        }

        public void solvePuzzle1()
        {
            if (!inputsLoaded) return;

            int stepsToRun = 1000;

            resetSimulation();

            for (int i = 0; i < stepsToRun; i++)
            {
                simulationStep();
            }

            int result = 0;

            foreach (Moon m in moons)
            {
                result += m.getEnergy();
            }

            Console.WriteLine("Day12: Puzzle 1 solution - " + result);
        }

        public void solvePuzzle2()
        {
            if (!inputsLoaded) return;

            resetSimulation();

            int steps = 0;
            long result = 0;
            
            long xCycle, yCycle, zCycle;
            xCycle = yCycle = zCycle = -1;

            while(true)
            {
                simulationStep();
                steps++;

                if(xCycle == -1 && moons.All(m => m.VX == 0))
                    xCycle = steps*2;

                if(yCycle == -1 && moons.All(m => m.VY == 0))
                    yCycle = steps*2;

                if(zCycle == -1 && moons.All(m => m.VZ == 0))
                    zCycle = steps*2;

                if(xCycle > -1 && yCycle > -1 && zCycle > -1)
                {
                    result = AOCHelpers.LCM(xCycle, AOCHelpers.LCM(yCycle,zCycle));
                    break;
                }

                if(steps > int.MaxValue)
                    break;
            }

            Console.WriteLine("Day12: Puzzle 2 solution - " + result);
        }
    }
}