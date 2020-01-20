using System;

namespace AdventOfCode2019
{
    class Line
    {
        public int X1 { get; }
        public int Y1 { get; }
        public int X2 { get; }
        public int Y2 { get; }

        public int MaxX { get; }
        public int MinX { get; }
        public int MaxY { get; }
        public int MinY { get; }

        public int A { get; }
        public int B { get; }
        public int C { get; }
        public int Length { get; }

        public Line(int x1, int y1, int x2, int y2)
        {
            X1 = x1;
            Y1 = y1;
            X2 = x2;
            Y2 = y2;

            MaxX = Math.Max(x1, x2);
            MinX = Math.Min(x1, x2);
            MaxY = Math.Max(y1, y2);
            MinY = Math.Min(y1, y2);

            A = y2 - y1;
            B = x2 - x1;
            C = A * x1 + B * y1;

            Length = MaxX - MinX + MaxY - MinY;
        }

        public bool hasPoint(int xIn, int yIn)
        {
            if (xIn < MinX || xIn > MaxX || yIn < MinY || yIn > MaxY)
                return false;
            else
                return true;
        }
    }
}