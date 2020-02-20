using System;
using System.Numerics;

namespace AdventOfCode2019
{
    class Moon
    {
        int pX, pY, pZ;
        public int PX() { return pX; }
        public int PY() { return pY; }
        public int PZ() { return pZ; }
        public Vector3 getPosition()
        {
            return new Vector3(pX,pY,pZ);
        }

        int vX, vY, vZ;

        public Moon()
        {
            pX = pY = pZ = 0;
            vX = vY = vZ = 0;
        }

        public Moon(int xIn, int yIn, int zIn)
        {
            pX = xIn;
            pY = yIn;
            pZ = zIn;
            vX = vY = vZ = 0;
        }

        public Moon(string moonDataIn)
        {
            // String in format <x=0, y=0, z=0>
            moonDataIn = moonDataIn.Substring(1, moonDataIn.Length - 2);
            string[] pos = moonDataIn.Split(',');

            foreach(string line in pos)
            {
                string[] parts = line.Trim().Split('=');
                if(parts[0] == "x")
                    pX = int.Parse(parts[1]);
                else if(parts[0] == "y")
                    pY = int.Parse(parts[1]);
                else if(parts[0] == "z")
                    pZ = int.Parse(parts[1]);
            }

            vX = vY = vZ = 0;

        }

        public void moveStep()
        {
            pX += vX;
            pY += vY;
            pZ += vZ;
        }
        
        public void setVelocity(Vector3 velIn)
        {
            vX = (int)velIn.X;
            vY = (int)velIn.Y;
            vZ = (int)velIn.Z;
        }

        public void changeVX(bool posX)
        {
            if(posX) vX += 1;
            else vX -= 1;
        }

        public void changeVY(bool posY)
        {
            if(posY) vY += 1;
            else vY -= 1;
        }

        public void changeVZ(bool posZ)
        {
            if(posZ) vZ += 1;
            else vZ -= 1;
        }
    }
}