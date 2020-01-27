using System;

class Asteroid
{
    public int X { get; }
    public int Y { get; }
    public int asteroidsInSight;
    public int id { get; }

    public Asteroid()
    {
        X = -1;
        Y = -1;
        asteroidsInSight = 0;
        id = AsteroidId;
        AsteroidId++;
    }

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