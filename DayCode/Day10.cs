using System;
using System.IO;
using System.Collections.Generic;

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
                    for(int j = 0; j < asteroidInPlace[i].Length; j++)
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
                            Asteroid newA = new Asteroid(x,y);
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
                Asteroid bestPlacement = new Asteroid(-1,-1);

                for(int i = 0; i < asteroids.Count; i++)
                {
                    Asteroid a = asteroids[i];
                    List<int> blockedAsteroids = new List<int>();

                    foreach (Asteroid b in asteroids)
                    {
                        if (b.id != a.id)
                        {
                            int dx = Math.Max(a.X, b.X) - Math.Min(a.X, b.X);
                            int dy = Math.Max(a.Y, b.Y) - Math.Min(a.Y, b.Y);

                            int gcd = AOCHelpers.GCD(dx, dy);
                            if (gcd != 0)
                            {
                                dx /= gcd;
                                dy /= gcd;
                            }

                            int x = b.X;
                            int y = b.Y;

                            while(x > 0 && x < asteroidInPlace.Length && y > 0 && y < asteroidInPlace[0].Length)
                            {
                                x += dx;
                                y += dy;

                                if(x < 0 || x >= asteroidInPlace.Length || y < 0 || y >= asteroidInPlace[0].Length)
                                    break;

                                if(asteroidInPlace[x][y] != -1)
                                {
                                    if(!blockedAsteroids.Contains(asteroidInPlace[x][y]))
                                        blockedAsteroids.Add(asteroidInPlace[x][y]);
                                }
                            }
                        }
                    }

                    asteroids[i].asteroidsInSight = asteroids.Count - blockedAsteroids.Count;
                }

                for(int i = 0; i < asteroids.Count; i++)
                {
                    if(asteroids[i].asteroidsInSight > bestPlacement.asteroidsInSight)
                        bestPlacement = asteroids[i];
                }

                int result = bestPlacement.asteroidsInSight;
                Console.WriteLine("Day10: Puzzle 1 solution - " + result);
            }
        }

        public void solvePuzzle2()
        {
            if (inputsLoaded)
            {
                Console.WriteLine("Day10: Puzzle 2 solution - " + 0);
            }
        }
    }
}