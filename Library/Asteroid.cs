using System;

struct Asteroid
{
    public int X { get; }
    public int Y { get; }
    public bool blocked;

    public Asteroid(int xIn, int yIn)
    {
        X = xIn;
        Y = yIn;
        blocked = false;
    }
}