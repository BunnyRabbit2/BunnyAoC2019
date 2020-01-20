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

        public int getDistanceBetweenTwoOrbits(string startNode, string endNode)
        {
            int distance = 0;

            OrbitTreeNode start = new OrbitTreeNode();
            OrbitTreeNode end = new OrbitTreeNode();

            for (int i = 0; i < orbits.Count; i++)
            {
                if (orbits[i].Planetcode == startNode)
                {
                    start = orbits[i];
                    break;
                }
            }
            for (int i = 0; i < orbits.Count; i++)
            {
                if (orbits[i].Planetcode == endNode)
                {
                    end = orbits[i];
                    break;
                }
            }

            List<OrbitTreeNode> startRoute = getRouteToRoot(start);
            List<OrbitTreeNode> endRoute = getRouteToRoot(end);

            OrbitTreeNode matchingNode = new OrbitTreeNode("UNSET", "UNSET");

            bool breakLoop = false;

            foreach(OrbitTreeNode sNode in startRoute)
            {
                foreach(OrbitTreeNode eNode in endRoute)
                {
                    if(sNode.Planetcode == eNode.Planetcode)
                    {
                        matchingNode = eNode;
                        breakLoop = true;
                        break;
                    }
                }
                if(breakLoop) break;
            }

            if(matchingNode.parentCode != "UNSET")
            {
                distance = startRoute.Count + endRoute.Count - (matchingNode.distanceToRoot * 2);
                distance -= 2; // Dont include the start or end
            }

            return distance;
        }

        public List<OrbitTreeNode> getRouteToRoot(OrbitTreeNode nodeIn)
        {
            List<OrbitTreeNode> route = new List<OrbitTreeNode>();

            OrbitTreeNode currentNode = nodeIn;

            while(currentNode.parentCode != "")
            {
                route.Add(currentNode);
                currentNode = currentNode.parentNode;
            }

            return route;
        }

        public List<OrbitTreeNode> getRouteToNode(OrbitTreeNode nodeIn, OrbitTreeNode targetNode)
        {
            List<OrbitTreeNode> route = new List<OrbitTreeNode>();

            OrbitTreeNode currentNode = nodeIn;

            while(currentNode.parentCode != targetNode.parentCode)
            {
                route.Add(currentNode);
                currentNode = currentNode.parentNode;
            }

            return route;
        }
    }
}