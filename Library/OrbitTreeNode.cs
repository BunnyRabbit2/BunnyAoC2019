using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2019
{
    class OrbitTreeNode
    {
        public OrbitTreeNode parentNode;
        public List<OrbitTreeNode> childNodes;
        public string parentCode;
        public string Planetcode { get; }
        public int distanceToRoot;

        public OrbitTreeNode()
        {
            childNodes = new List<OrbitTreeNode>();
            Planetcode = "";
            parentCode = "";
            distanceToRoot = -1;
        }

        public OrbitTreeNode(string codeIn, string pCodeIn)
        {
            childNodes = new List<OrbitTreeNode>();
            Planetcode = codeIn;
            parentCode = pCodeIn;
            distanceToRoot = -1;
        }

        public static void AddOrbitToTree(List<OrbitTreeNode> orbits, string parent, string child)
        {
            OrbitTreeNode otn = new OrbitTreeNode();
            int pIndex = -1;
            int cIndex = -1;

            for (int i = 0; i < orbits.Count; i++)
            {
                if (orbits[i].Planetcode == parent)
                {
                    pIndex = i;
                }
                if (orbits[i].Planetcode == child)
                {
                    cIndex = i;
                }
            }

            if (pIndex == -1)
            {
                orbits.Add(new OrbitTreeNode(parent, ""));

            }

            if (cIndex != -1)
            {
                orbits[cIndex].parentCode = parent;
            }
            else
            {
                orbits.Add(new OrbitTreeNode(child, parent));
            }
        }

        public static void AddOrbitToTree(List<OrbitTreeNode> orbits, string orbitCode)
        {
            if (orbitCode != "")
            {
                string[] codes = orbitCode.Split(')');

                OrbitTreeNode.AddOrbitToTree(orbits, codes[0], codes[1]);
            }
        }

        public static void SetParentChildRelationships(List<OrbitTreeNode> orbits)
        {
            foreach (OrbitTreeNode otn in orbits)
            {
                int pIndex = -1;

                for (int i = 0; i < orbits.Count; i++)
                {
                    if (orbits[i].Planetcode == otn.parentCode)
                    {
                        pIndex = i;
                        break;
                    }
                }

                if (pIndex != -1)
                {
                    otn.parentNode = orbits[pIndex];
                    orbits[pIndex].childNodes.Add(otn);
                }
            }
        }

        public static void SetDistancesFromRoot(List<OrbitTreeNode> orbits)
        {
            int rIndex = -1;

            for (int i = 0; i < orbits.Count; i++)
            {
                if (orbits[i].parentCode == "")
                {
                    rIndex = i;
                    break;
                }
            }

            OrbitTreeNode root = orbits[rIndex];
            root.distanceToRoot = 0;

            foreach(OrbitTreeNode c in root.childNodes)
            {
                OrbitTreeNode.SetChildrenDistance(c);
            }
        }

        public static void SetChildrenDistance(OrbitTreeNode node)
        {
            node.distanceToRoot = node.parentNode.distanceToRoot + 1;
            foreach(OrbitTreeNode c in node.childNodes)
            {
                OrbitTreeNode.SetChildrenDistance(c);
            }
        }
    }
}