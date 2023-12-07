using System;
using System.Linq;

namespace SharpRISCV.Core.V2.ParseTree
{
    class Node
    {
        public string Name { get; }
        public List<Node> Children { get; }

        public Node(string name)
        {
            Name = name;
            Children = new List<Node>();
        }

        public void AddChild(Node child)
        {
            Children.Add(child);
        }
    }
}
