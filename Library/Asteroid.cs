using System;

class Asteroid
{
    public int X { get; }
    public int Y { get; }
    public int asteroidsInSight;
    public int id { get; }

    public Asteroid(int xIn, int yIn)
    {
        X = xIn;
        Y = yIn;
        asteroidsInSight = 0;
        id = AsteroidId;
        AsteroidId++;
    }

    public static int AsteroidId = 0;
}