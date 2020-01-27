using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2019
{
    public class Day10
    {
        OrbitTree oTree;

        public static void solveDay10()
        {
            Day10 d10 = new Day10();
            d10.loadInputs("inputs/Day10.txt");
            d10.solvePuzzle1();
            d10.solvePuzzle2();
        }

        int[][] asteroidInPlace;
        List<Asteroid> asteroids;
        bool inputsLoaded;

        public Day10()
        {
            asteroids = new List<Asteroid>();
            inputsLoaded = false;
        }

        public void loadInputs(string fileLocation)
        {
            if (File.Exists(fileLocation))
            {
                string[] lines = File.ReadAllText(fileLocation).Split(Environment.NewLine);

                int height = lines.Length;
                int width = lines[0].Length;

                asteroidInPlace = new int[width][];
                for (int i = 0; i < width; i++)
                {
                    asteroidInPlace[i] = new int[height];
                    for (int j = 0; j < asteroidInPlace[i].Length; j++)
                    {
                        asteroidInPlace[i][j] = -1;
                    }
                }

                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        char c = lines[y][x];

                        if (c == '#')
                        {
                            Asteroid newA = new Asteroid(x, y);
                            asteroids.Add(newA);
                            asteroidInPlace[x][y] = newA.id;
                        }
                    }
                }

                inputsLoaded = true;
            }
            else
            {
                Console.WriteLine("Day10: Invalid File Location");
            }
        }

        public void solvePuzzle1()
        {
            if (inputsLoaded)
            {
                Asteroid bestPlacement = new Asteroid();

                for (int i = 0; i < asteroids.Count; i++)
                {
                    Asteroid a = asteroids[i];
                    int count = 0;
                    List<float> angles = new List<float>();

                    foreach (Asteroid b in asteroids)
                    {
                        if (b.id != a.id)
                        {
                            float angle = AOCHelpers.GetBearingBetweenTwoPoints(a.X, a.Y, b.X, b.Y);

                            if (!angles.Contains(angle))
                            {
                                angles.Add(angle);
                                count++;
                            }
                        }
                    }

                    asteroids[i].asteroidsInSight = count;
                }

                for (int i = 0; i < asteroids.Count; i++)
                {
                    if (asteroids[i].asteroidsInSight > bestPlacement.asteroidsInSight)
                        bestPlacement = asteroids[i];
                }

                int result = bestPlacement.asteroidsInSight;
                Console.WriteLine("Day10: Puzzle 1 solution - " + result + " from Asteroid " + bestPlacement.id);
            }
        }

        // Only provides a solution if puzzle 1 has been run first to find the station placement
        public void solvePuzzle2()
        {
            if (inputsLoaded)
            {
                Asteroid station = new Asteroid();

                for (int i = 0; i < asteroids.Count; i++)
                {
                    if (asteroids[i].asteroidsInSight > station.asteroidsInSight)
                        station = asteroids[i];
                }

                Dictionary<float, List<Asteroid>> targets = new Dictionary<float, List<Asteroid>>();

                foreach (Asteroid b in asteroids)
                {
                    if (b.id != station.id)
                    {
                        float angle = AOCHelpers.GetBearingBetweenTwoPoints(station.X, station.Y, b.X, b.Y);

                        if (!targets.ContainsKey(angle))
                        {
                            targets[angle] = new List<Asteroid>();
                        }
                        targets[angle].Add(b);
                    }
                }

                float[] keysInOrder = targets.Keys.OrderBy(x => x).ToArray();
                foreach (var list in targets.Values)
                {
                    list.Sort((a, b) => AOCHelpers.GetDistanceBetweenTwoPoints(station.X, station.Y, a.X, a.Y)
                            .CompareTo(AOCHelpers.GetDistanceBetweenTwoPoints(station.X, station.Y, b.X, b.Y)));
                }

                Asteroid twoHundredthAst = new Asteroid();

                int keyIdx = 0;
                for (int i = 0; i < 200; i++)
                {
                    List<Asteroid> circuit;
                    do
                    {
                        circuit = targets[keysInOrder[keyIdx]];
                        keyIdx = (++keyIdx) % keysInOrder.Length;

                    } while (circuit.Count == 0);

                    twoHundredthAst = circuit[0];
                    circuit.RemoveAt(0);
                }

                int result = twoHundredthAst.X * 100 + twoHundredthAst.Y;

                Console.WriteLine("Day10: Puzzle 2 solution - " + result);
            }
        }
    }
}