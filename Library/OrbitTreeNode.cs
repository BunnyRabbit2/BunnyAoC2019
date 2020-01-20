using System;
using System.Collections.Generic;

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
    }
}