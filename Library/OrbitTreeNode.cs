using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2019
{
    class OrbitTreeNode
    {
        public int ParentIndex { get; }
        public List<int> childrenIndexes;
        public string Planetcode { get; }
        public int DistanceToRoot { get; }

        public OrbitTreeNode()
        {
            ParentIndex = -1;
            childrenIndexes = new List<int>();
            Planetcode = "";
            DistanceToRoot = -1;
        }

        public OrbitTreeNode(string codeIn, int dtrIn, int pIn = -1)
        {
            ParentIndex = pIn; // root node has -1 as parent
            childrenIndexes = new List<int>();
            Planetcode = codeIn;
            DistanceToRoot = dtrIn;
        }

        public static bool AddOrbitToTree(List<OrbitTreeNode> orbits, string parent, string child)
        {
            if(parent == "root")
            {
                orbits.Add(new OrbitTreeNode(child, 0));
            }
            else if (parent != "")
            {
                OrbitTreeNode otn = new OrbitTreeNode();
                int pIndex = -1;

                for (int i = 0; i < orbits.Count; i++)
                {
                    if (orbits[i].Planetcode == parent)
                    {
                        otn = orbits[i];
                        pIndex = i;
                        break;
                    }
                }

                if(!otn.Equals(default(OrbitTreeNode)))
                {
                    OrbitTreeNode newNode = new OrbitTreeNode(child, otn.DistanceToRoot + 1, pIndex);
                    orbits.Add(newNode);
                    orbits[pIndex].childrenIndexes.Add(orbits.IndexOf(newNode));
                }
                else
                {
                    return false; // No parent found and new node isn't root, shit did not work
                }
            }

            return true; // Shit worked
        }

        public static bool AddOrbitToTree(List<OrbitTreeNode> orbits, string orbitCode)
        {
            string[] codes = orbitCode.Split(')');

            return OrbitTreeNode.AddOrbitToTree(orbits, codes[0], codes[1]);
        }
    }
}