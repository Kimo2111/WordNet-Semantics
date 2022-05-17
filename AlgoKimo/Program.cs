using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;

namespace AlgoKimo
{
    class Program
    {
        static void Main(string[] args)
        {
            WordGraph wordTree = new WordGraph();
            string defaultPathSample = @"C:\Users\hp\Desktop\Testcases\Sample\";
            string defaultPathComp = @"C:\Users\hp\Desktop\Testcases\Complete\";

            // ******************** SYNSETS ***************************

 
            // Read synset file into a string array.
            string[] lines = System.IO.File.ReadAllLines(defaultPathSample + @"Case1\Input\1synsets.txt");

            // Display the file content.
            Console.WriteLine("Content of sysnet file: ");
            foreach (string line in lines)
            {
                Console.WriteLine(line);
            }
            
            // Dividing input of synsets and adding new nodes
            foreach (string sInput in lines)
            {
                int startpoints = 0;
                int counters = 2;
                int ID = 0;
                string nounStr = "";
                string glossStr = "";

                for (int i = 0; i < sInput.Length; i++)
                {
                    if (sInput[i] == ',')
                    {
                        if (counters == 2)
                        {
                            string IDstr = sInput.Substring(startpoints, i);
                            ID = Convert.ToInt32(IDstr);
                          
                            startpoints = i + 1;
                            counters--;

                        }
                        else if(counters < 2 && startpoints < i )
                        {
                            int dif = i - startpoints ;
                            nounStr = sInput.Substring(startpoints, dif);
                            startpoints = i + 1;
                            dif = sInput.Length - startpoints;
                            glossStr = sInput.Substring(startpoints, dif);



                        }
                        
                    }
                   




                }
            wordTree.addNode(ID, nounStr, glossStr);
            }

            
            // ***************** HYPERNYMS ***********************


            // Read Hypernym file into a string array.
            lines = System.IO.File.ReadAllLines(defaultPathSample + @"Case1\Input\2hypernyms.txt");
            
            // Display the file content.
            Console.WriteLine("Content of Hypernym file: ");
            foreach (string line in lines)
            {
                Console.WriteLine(line);
            }
            
            // Reading every hypernym input and assigning childs
            foreach(string input in lines)
            {
                int childID = 0;
                int counter = 0;
                bool childNotFound = true;
                int parentNo = 0;
                int startpoint = 0;
                


                // Determining number of parents
                for (int i = 0; i < input.Length; i++)
                {
                    if (input[i] == ',')
                    {
                        parentNo++;
                    }
                }
                int[] parentID = new int[parentNo];

                // Dividing input and Assigning 

                for (int i = 0; i < input.Length; i++)
                {
                    if (input[i] == ',')
                    {

                        if (childNotFound == true)
                        {
                            string IDstr = input.Substring(startpoint, i);
                            childID = Convert.ToInt32(IDstr);
                            startpoint = i + 1;
                            childNotFound = false;
                            // If only one parent
                            if (parentNo == 1)
                            {
                                int dif = input.Length - startpoint;
                                IDstr = input.Substring(startpoint, dif);
                                int temp = Convert.ToInt32(IDstr);
                                parentID[counter] = temp;


                            }
                        }

                        else
                        {
                            int dif = i - startpoint;
                            string IDstr = input.Substring(startpoint, dif);
                            int temp = Convert.ToInt32(IDstr);
                            parentID[counter] = temp;
                            startpoint = i + 1;
                            counter++;
                            parentNo--;
                            if (parentNo == 1)
                            {
                                dif = input.Length - startpoint;
                                IDstr = input.Substring(startpoint, dif);
                                temp = Convert.ToInt32(IDstr);
                                parentID[counter] = temp;
                            }
                        }




                    }
                }


                // adding Edges
                for (int i = 0; i < parentID.Length; i++)
                {
                    
                    wordTree.addEdge(parentID[i], childID);
                    wordTree.setChild(childID);
                    
                    
                }
            }

            

            // ***************** RELATION QUERIES ***************************
            Node root = wordTree.FindRoot();
            // Reading input for Relation Queries
            lines = System.IO.File.ReadAllLines(defaultPathSample + @"Case1\Input\3RelationsQueries.txt");
            
            // Display the file content.
            Console.WriteLine("Content of Relation Queries file = ");
            foreach (string line in lines)
            {
                
                Console.WriteLine(line);
            }

            // Creating the query and calling for the function
            foreach (string relationQuery in lines)
            {
                string firstNoun = "";
                string secondNoun = "";
                int startpoint = 0;

                for (int i = 0; i < relationQuery.Length; i++)
                {
                    if (relationQuery[i] == ',')
                    {

                        firstNoun = relationQuery.Substring(startpoint, i);
                        startpoint = i + 1;
                        int dif = relationQuery.Length - startpoint;
                        secondNoun = relationQuery.Substring(startpoint, dif);
                    }

                }

                Node first = wordTree.FindWithNoun(firstNoun);
                Node second = wordTree.FindWithNoun(secondNoun);
                
               
                
                

                

            }


            // ***************** OUTCAST QUERIES *****************************


            // Reading input for Outcast Queries
            lines = System.IO.File.ReadAllLines(defaultPathSample + @"Case1\Input\4OutcastQueries.txt");
            
            // Dividing into an array of strings and then comparing/calling function
            foreach (string outcastQuery in lines)
            {
                // Determining number of nodes being compared to each other
                int members = 0;
                for (int i = 0; i < outcastQuery.Length; i++)
                {
                    
                    if (outcastQuery[i] == ',')
                    {
                        members++;
                    }
                }
                members++;
                int memcount = 0;
                string[] synArray = new string[members];
                // Dividing input into members 
                int startpoint = 0;

                for (int i = 0; i < outcastQuery.Length; i++)
                {
                    if (outcastQuery[i] == ',')
                    {
                        if (memcount == 0)
                        {
                            synArray[memcount] = outcastQuery.Substring(startpoint, i);
                            startpoint = i + 1;
                            memcount++;
                        }
                        else
                        {
                            int dif = i - startpoint;
                            synArray[memcount] = outcastQuery.Substring(startpoint, dif);
                            startpoint = i + 1;
                            memcount++;
                            if (memcount == members - 1)
                            {
                                dif = outcastQuery.Length - startpoint;
                                synArray[memcount] = outcastQuery.Substring(startpoint, dif);
                            }
                        }
                    }

                }

               
            }
            
            
            
            
            





        }
    }
}