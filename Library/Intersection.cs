using System;

struct Intersection
{
    public int X { get; }
    public int Y { get; }
    public int ManDist { get; }
    public int WireLength { get; }

    public Intersection(int xIn, int yIn, int wireLengthIn)
    {
        X = xIn;
        Y = yIn;
        ManDist = Math.Abs(xIn) + Math.Abs(yIn);
        WireLength = wireLengthIn;
    }
}