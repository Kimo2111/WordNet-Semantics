using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;

namespace AlgoKimo
{
   public class Node
    {
        int ID;
        string noun;
        string gloss;
        List<Node> children;
        bool IAC = false;
        public Node(int id, string noun, string gloss)
        {
            this.ID = id;
            this.noun = noun;
            this.gloss = gloss;
            children = new List<Node>();
        }
        public string Value
        {
            get
            {
                return noun;
            }
        }

        public int SynID
        {
            get
            {
                return ID;
            }
        }
        public List<Node> childs
        {
            get
            {
                return children;
            }
        }
        public bool isAChild
        {
            get
            {
                return IAC;
            }
        }
        public void addChild(Node child)
        {
            children.Add(child);
        }
        public void setIsAChild()
        {
            IAC = true;
        }
        
        
        public override string ToString()
        {
            StringBuilder nodeString = new StringBuilder();
            nodeString.Append("[Node Value: " + Value + " With Children");
            foreach (var item in children)
            {
                nodeString.Append(" -> " + item.Value);
            }
            nodeString.Append(" ]");
            return nodeString.ToString();
        }
    }
}
