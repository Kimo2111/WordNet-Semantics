using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Collections;
namespace AlgoKimo
{
    public class WordGraph
    {
       
        List<Node> directedGraph = new List<Node>();
        public WordGraph()
        {
         this.directedGraph = new List<Node>();
        }
        
        public int Count
        {
            get
            {
                return directedGraph.Count;
            }
        }
       public List<Node> Nodes
        {
            get
            {
                return directedGraph;
            }
        }
        public void addNode(int id, string noun, string gloss)
        {
          directedGraph.Add(new Node(id, noun, gloss));
        }
        public void addEdge(int n1, int n2)
        {
            Node parent = FindWithID(n1);
            Node child = FindWithID(n2);
            parent.addChild(child);
                
            
        }
        public void setChild(int id)
        {
            Node child = FindWithID(id);
            child.setIsAChild();
        }
        public Node FindWithID(int id)
        {
            foreach (Node item in directedGraph)
            {
                if (item.SynID == id)
                {
                    return item;
                }
            }
            return null;
        }

        public Node FindWithNoun(string noun)
        {
            foreach (Node item in directedGraph)
            {
                if (item.Value.Equals(noun))
                {
                    return item;
                }
            }
            return null;
        }
        public Node FindRoot()
        {
            foreach (Node item in directedGraph)
            {
                if(!item.isAChild)
                {
                    return item;
                }
            }
            return null;
        }
       
        // Using BFS to find shortest path between the entered string and the root
        // Returns a list that contains the path
        /* public List<int> FindPath(string d)
        {
            int s;
            List<int> path = new List<int>();
            Node current = FindRoot();
            Node toBeFound = FindWithNoun(d);
            int toBeFoundID = toBeFound.SynID;
            
            

            // Mark all nodes as White (not visited)(false)
            bool[] visited = new bool[directedGraph.Count];

            // Create a queue for BFS Algorithm
            LinkedList<int> queue = new LinkedList<int>();

            // Mark the root node as visited and enqueue it in BFS queue
            visited[current.SynID] = true;
            queue.AddLast(current.SynID);
            

            
            
            while (queue.Count != 0)
            {

                // Updating queue information and path list
                
                s = queue.First.Value;
                queue.RemoveFirst();
                path.Add(s);
                current = FindWithID(s);
                int n;
               


                // Get all childs of the current node S
                // Start comparing using Node ID
                // If Node ID matches
                // Return the path 
                // Else
                // If A child has not been visited and is not marked black
                // Mark it gray and enqueue it in BFS queue
                // Else Black the node
                
                foreach (var child in current.childs)
                {

                    n = child.SynID;

                    // If this adjacent node is the destination node,
                    // then return true
                    if (n == toBeFoundID)
                    {
                        path.Add(child.SynID);
                        return path;
                            
                    }

                    // If not found continue BFS and mark nodes either "Gray" or "Black"
                    if (!visited[n])
                    {
                        visited[n] = true;
                        Node temp = FindWithID(n);
                        // If true, node is blacked (Not to be added in queue to avoid visiting again)
                        if(temp.childs.Count == 0)
                        {
                            continue;
                        }
                        // Else node is grayed (Added to queue to be revisted)
                        else 
                            queue.AddLast(n);
                        
                        
                    }
                }
                
            }

            // If BFS is complete without visited d
            return path;
        }*/
        public int SCA(List<int> i, List<int> j)
        {
            int bigIndex;

            if(i.Count >= j.Count)
            {
                bigIndex = j.Count;
            }
            else
            {
                bigIndex = i.Count;
            }

            for(int k = bigIndex-1; k >= 0; k--)
            {
                if(i[k] == j[k])
                {
                    return i[k];
                }
            }
            
            return -1;
        }

        // Recursive function to find path using DFS Algo
        public List<int> DFSRec(int s, int x, bool[] visited, List<int> p)
        {
            // Mark the current node as visited and add it to path list
           
            visited[s] = true;
            p.Add(s);
            
            Node children = FindWithID(s);
            
            

            while (p.Count > 0)
            {
                // Rec for all child nodes
                
                
                foreach (var child in children.childs)
                {
                    if (!visited[child.SynID])
                    {
                        if (child.SynID == x)
                        {
                            visited[child.SynID] = true;
                            
                            p.Add(child.SynID);
                            Console.WriteLine("Path found");
                            return p;
                            
                        }
                        else
                        {
                            // If the current node has no childs mark as black 
                            if (child.childs.Count == 0)
                            {
                                visited[child.SynID] = true;
                                continue;
                            }
                            else
                            {
                                // If it has childs call for the recursive function to check child values
                               p = DFSRec(child.SynID, x, visited, p);
                                
                                
                            }
                        }
                    }
                    
                }
                if(p[p.Count-1] == x )
                {
                    break;
                }
                else
                {
                    p.RemoveAt(p.Count - 1);
                    break;
                }
                
            }
            return p;
            
        }

        // DFS function using DFSRec
        
        public List<int> FindPathFromRoot(int x)
        {
            // Mark all nodes not visited
            bool[] visited = new bool[directedGraph.Count];
            List<int> path = new List<int>();
            Node s = FindRoot();
            if(s.SynID == x)
            {
                path.Add(x);
                return path;
            }
            
            // Calling Recursive function
            return DFSRec(s.SynID, x ,visited, path);
            
        }
        // Takes SCA and the point and gets the shortest path
        public List<int> FindPathFromSCA(int x, int y)
        {
            
            // Mark all nodes not visited
            bool[] visited = new bool[directedGraph.Count];
            List<int> path = new List<int>();
            Node s = FindWithID(x);
            if (s.SynID == y)
            {
                path.Add(x);
                return path;
            }

            // Calling Recursive function
            return DFSRec(s.SynID, y, visited, path);
        }
        public int Distance(int x, int y)
        {
            List<int> path1 = FindPathFromRoot(x);
            List<int> path2 = FindPathFromRoot(y);
            int SA = SCA(path1, path2);
            path1 = FindPathFromSCA(SA, x);
            path2 = FindPathFromSCA(SA, y);
            int distance = 0;
            path1.RemoveAt(0);
            distance = path1.Count + path2.Count - 1;
            return distance;
        }
        public override string ToString()
        {
            Console.WriteLine("========================================\n");
            Console.WriteLine("New Graph Adjacency List Implementation:\n");
            Console.WriteLine("----------[Non Zero Index Based]--------\n");
            Console.WriteLine("========================================\n");
            StringBuilder nodeString = new StringBuilder();
            for (int i = 0; i < Count; i++)
            {
                nodeString.Append(directedGraph[i].ToString());
                if (i < Count - 1)
                {
                    nodeString.Append("\n");
                }
            }
            return nodeString.ToString();
        }
    }
}
