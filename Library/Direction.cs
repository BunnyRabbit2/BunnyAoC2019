using System;
using System.Drawing;
using System.Collections.Generic;

namespace AdventOfCode2019
{
    public enum CompassDirections
    {
        North = 1,
        East = 4,
        South = 2,
        West = 3
    }

    public static class CompassDirection
    {
        public static CompassDirections getDirectionToRight(CompassDirections dirIn)
        {
            CompassDirections toRight = CompassDirections.North;
            switch (dirIn)
            {
                case CompassDirections.North:
                    toRight = CompassDirections.East;
                    break;
                case CompassDirections.East:
                    toRight = CompassDirections.South;
                    break;
                case CompassDirections.South:
                    toRight = CompassDirections.West;
                    break;
                case CompassDirections.West:
                    toRight = CompassDirections.North;
                    break;
            }

            return toRight;
        }

        public static CompassDirections getDirectionToLeft(CompassDirections dirIn)
        {
            CompassDirections toLeft = CompassDirections.North;
            switch (dirIn)
            {
                case CompassDirections.North:
                    toLeft = CompassDirections.West;
                    break;
                case CompassDirections.East:
                    toLeft = CompassDirections.North;
                    break;
                case CompassDirections.South:
                    toLeft = CompassDirections.East;
                    break;
                case CompassDirections.West:
                    toLeft = CompassDirections.South;
                    break;
            }

            return toLeft;
        }

        public static CompassDirections getDirectionBehind(CompassDirections dirIn)
        {
            CompassDirections toBehind = CompassDirections.North;
            switch (dirIn)
            {
                case CompassDirections.North:
                    toBehind = CompassDirections.South;
                    break;
                case CompassDirections.East:
                    toBehind = CompassDirections.West;
                    break;
                case CompassDirections.South:
                    toBehind = CompassDirections.North;
                    break;
                case CompassDirections.West:
                    toBehind = CompassDirections.East;
                    break;
            }

            return toBehind;
        }
    }

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