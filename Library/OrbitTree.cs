using System;
using System.Collections.Generic;

namespace AdventOfCode2019
{
    class OrbitTree
    {
        List<OrbitTreeNode> orbits;

        public OrbitTree()
        {
            orbits = new List<OrbitTreeNode>();
        }

        public void createTree(string[] orbitCodes)
        {
            foreach (string o in orbitCodes)
            {
                addOrbitToTree(orbits, o);
            }
            setParentChildRelationships(orbits);

            setDistancesFromRoot(orbits);
        }

        public void addOrbitToTree(List<OrbitTreeNode> orbits, string parent, string child)
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

        public void addOrbitToTree(List<OrbitTreeNode> orbits, string orbitCode)
        {
            if (orbitCode != "")
            {
                string[] codes = orbitCode.Split(')');

                addOrbitToTree(orbits, codes[0], codes[1]);
            }
        }

        public void setParentChildRelationships(List<OrbitTreeNode> orbits)
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

        public void setDistancesFromRoot(List<OrbitTreeNode> orbits)
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

            foreach (OrbitTreeNode c in root.childNodes)
            {
                setChildrenDistance(c);
            }
        }

        public void setChildrenDistance(OrbitTreeNode node)
        {
            node.distanceToRoot = node.parentNode.distanceToRoot + 1;
            foreach (OrbitTreeNode c in node.childNodes)
            {
                setChildrenDistance(c);
            }
        }

        public int getTotalOrbits()
        {
            int totalOrbits = 0;

            foreach (OrbitTreeNode n in orbits)
            {
                totalOrbits += n.distanceToRoot;
            }

            return totalOrbits;
        }
    }
}