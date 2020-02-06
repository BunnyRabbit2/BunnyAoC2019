using System;
using System.Drawing;
using System.Collections.Generic;

namespace AdventOfCode2019
{
    public enum Directions
    {
        Up,
        Right,
        Down,
        Left
    }

    public static class Direction
    {
        public static readonly Dictionary<Directions, Point> DirectionValues = new Dictionary<Directions, Point>
            {
                { Directions.Up, new Point(0, -1)},
                { Directions.Right, new Point(1, 0)},
                { Directions.Down, new Point(0, 1)},
                { Directions.Left, new Point(-1, 0)},
            };

        public static Directions TurnRight(Directions directionIn)
        {
            if ((int)directionIn >= DirectionValues.Count - 1)
            {
                directionIn = (Directions)0;
            }
            else
            {
                directionIn = (Directions)(int)directionIn + 1;
            }

            return directionIn;
        }

        public static Directions TurnLeft(Directions directionIn)
        {
            if ((int)directionIn == 0)
            {
                directionIn = (Directions)(int)DirectionValues.Count - 1;
            }
            else
            {
                directionIn = (Directions)(int)directionIn - 1;
            }

            return directionIn;
        }
    }
}