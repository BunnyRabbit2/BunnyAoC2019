using System;
using System.Numerics;

namespace AdventOfCode2019
{
    class Moon
    {
        public static int MoonId = 0;

        public int id { get; }

        int sX, sY, sZ;

        int pX, pY, pZ;
        public int PX { get{return pX;} }
        public int PY { get{return pY;} }
        public int PZ { get{return pZ;} }
        public Vector3 getPosition()
        {
            return new Vector3(pX, pY, pZ);
        }

        int vX, vY, vZ;
        public int VX { get{return vX;} }
        public int VY { get{return vY;} }
        public int VZ { get{return vZ;} }

        public Moon()
        {
            pX = pY = pZ = 0;
            vX = vY = vZ = 0;
            id = Moon.MoonId;
            Moon.MoonId++;
        }

        public Moon(string moonDataIn)
        {
            // String in format <x=0, y=0, z=0>
            moonDataIn = moonDataIn.Substring(1, moonDataIn.Length - 2);
            string[] pos = moonDataIn.Split(',');

            foreach (string line in pos)
            {
                string[] parts = line.Trim().Split('=');
                if (parts[0] == "x")
                    pX = int.Parse(parts[1]);
                else if (parts[0] == "y")
                    pY = int.Parse(parts[1]);
                else if (parts[0] == "z")
                    pZ = int.Parse(parts[1]);
            }

            sX = pX;
            sY = pY;
            sZ = pZ;

            vX = vY = vZ = 0;

            id = Moon.MoonId;
            Moon.MoonId++;
        }

        public void resetMoon()
        {
            pX = sX;
            pY = sY;
            pZ = sZ;
            vX = vY = vZ = 0;
        }

        public void moveStep()
        {
            pX += vX;
            pY += vY;
            pZ += vZ;
        }

        public void applyGravity(Moon moonIn)
        {
            if (id != moonIn.id)
            {
                if (pX < moonIn.PX) vX++;
                else if (pX > moonIn.PX) vX--;

                if (pY < moonIn.PY) vY++;
                else if (pY > moonIn.PY) vY--;

                if (pZ < moonIn.PZ) vZ++;
                else if (pZ > moonIn.PZ) vZ--;
            }
        }

        public int getEnergy()
        {
            int potK = Math.Abs(pX) + Math.Abs(pY) + Math.Abs(pZ);
            int kinK = Math.Abs(vX) + Math.Abs(vY) + Math.Abs(vZ);

            return potK * kinK;
        }
    }
}