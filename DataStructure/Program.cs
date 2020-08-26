using DataStructure.Data_Structure_1;
using DataStructure.Data_Structure_2;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataStructure
{

    class Program
    {
        static void Main(string[] args)
        {
            var weightedGraph = new WeightedGraph();
            
            weightedGraph.AddNode("A");
            weightedGraph.AddNode("B");
            weightedGraph.AddNode("C");
            weightedGraph.AddNode("D");
            
            // weightedGraph.AddEdge("A","B",3);
            // weightedGraph.AddEdge("A","D",2);
            // weightedGraph.AddEdge("A","C",4);
            //
            // weightedGraph.AddEdge("B","E",1);
            // weightedGraph.AddEdge("B","D",6);
            //
            // weightedGraph.AddEdge("D","E",5);
            //
            // weightedGraph.AddEdge("D","C",1);

            weightedGraph.AddEdge("A","B",3);
            weightedGraph.AddEdge("A","C",1);
            weightedGraph.AddEdge("C","B",2);
            weightedGraph.AddEdge("C","D",5);
            weightedGraph.AddEdge("B","D",4);

            var spanningTree = weightedGraph.MinimumSpanning();
            spanningTree.Print();
        }

        private static int Fact(int number)
        {
            if (number == 0) return 1;

            return number * Fact(number - 1);
        }

        private static void TreeSection()
        {
            var tree = new Tree<string>();
            tree.Insert(20, "a");

            tree.Insert(10, "b");
            tree.Insert(9, "-b");
            tree.Insert(8, "--b");
            tree.Insert(7, "---b");


            tree.Insert(30, "c");
            tree.Insert(35, "d");
            tree.Insert(40, "e");
            tree.Insert(45, "f");

            // tree.FindNodesAtKDistance(3);
            tree.PrintAncestors(7);
        }

        private static void PrintTraversalTree(Dictionary<int, string> list, string title)
        {
            Console.WriteLine(title + ": ");
            foreach (var item in list)
            {
                Console.Write($"{item.Key}, ");
            }
            Console.WriteLine(Environment.NewLine);
        }

        private static void HashTableGetGetRepeatedCharacter()
        {
            var hashset = new System.Collections.Generic.HashSet<char>();

            var words = "a green apple";

            foreach (var word in words)
            {
                if (hashset.Contains(word))
                {
                    Console.WriteLine(word);
                    break;
                }

                hashset.Add(word);
            }
        }

        private static void HashTableGetNonRepeatedCharacter()
        {
            var dic = new Dictionary<char, int>();

            var input = "a green apple";
            foreach (var a in input)
            {
                if (!dic.ContainsKey(a))
                    dic.Add(a, 0);

                dic[a] += 1;
            }

            Console.WriteLine(dic.First(d => d.Value == 1));
        }

        private static void StackCompiler()
        {
            string code = @"
            namespace DataStructure
            {
                class Program
                {
                    static void Main(string[] args)
                    {
                        string code = @""

                        "";
                        StackHelper.Compile(code);
                    }
                }
            }
            ";
            StackHelper.Compile(code);
        }
    }

   
}
