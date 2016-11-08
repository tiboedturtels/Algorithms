using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dijkstra_sKattis
{
    class Program
    {
        static void Main(string[] args)
        {
            string line = Console.ReadLine();
            while(line != "0 0 0 0")
            {
                //Take in values
                string[] vals = line.Split();
                int numNodes = Convert.ToInt32(vals[0]);
                int numEdges = Convert.ToInt32(vals[1]);
                int numQueries = Convert.ToInt32(vals[2]);
                int startIndex = Convert.ToInt32(vals[3]);

                //Set up Node array
                Node[] Nodes = new Node[numNodes];
                for(int i = 0; i < numNodes; i++)
                {
                    Nodes[i] = new Node(i);
                }

                //Build graph
                for(int i = 0; i < numEdges; i++)
                {
                    string[] s = Console.ReadLine().Split();
                    int first = Convert.ToInt32(s[0]);
                    int second = Convert.ToInt32(s[1]);
                    int weight = Convert.ToInt32(s[2]);

                    Nodes[first].AddEdge(Nodes[second], weight);
                }
            }
        }

        static void Dijkstras(Node[] Nodes, int StartingIndex, int numQueries)
        {
            //Set up
            int[] dist = new int[Nodes.Length];
            Node[] prev = new Node[Nodes.Length];
            BinaryHeap PQ = new BinaryHeap(Nodes.Length);
            for(int i = 0; i < dist.Length; i++)
            {
                dist[i] = int.MaxValue;
                prev = null;
            }

            dist[StartingIndex] = 0;
            PQ.InsertOrChange(Nodes[StartingIndex], 0);

            //Do Dijkstra's
            while (!PQ.isEmpty())
            {
                Node u = PQ.Pop();
                for(int i = 0; i < u.ConnectedNodes.Count; i++)
                {
                    Node v = u.ConnectedNodes[i];
                    if(dist[v.ListIndex] > dist[u.ListIndex] + u.Weights[i])
                    {
                        dist[v.ListIndex] = dist[u.ListIndex] + u.Weights[i];
                        prev[v.ListIndex] = u;
                        PQ.InsertOrChange(v, dist[v.ListIndex]);
                    }
                }
            }

            //Print ouput
            for(int i = 0; i < numQueries; i++)
            {
                int checkIndex = Convert.ToInt32(Console.ReadLine());
                if (dist[checkIndex] == int.MaxValue)
                    Console.WriteLine("Impossible");
                else
                    Console.WriteLine(dist[checkIndex]);
            }
            Console.WriteLine();
        }
    }

    class Node
    {
        public int ListIndex;
        public List<Node> ConnectedNodes;
        public List<int> Weights;

        public Node(int i)
        {
            ListIndex = i;
            ConnectedNodes = new List<Node>();
            Weights = new List<int>();
        }

        public void AddEdge(Node endNode, int weight)
        {
            ConnectedNodes.Add(endNode);
            Weights.Add(weight);
        }
    }

    class BinaryHeap
    {
        List<HeapNode> heap;
        int[] PQIndices;

        public BinaryHeap(int size)
        {
            heap = new List<HeapNode>();
            PQIndices = new int[size];
            for(int i = 0; i < PQIndices.Length; i++)
            {
                PQIndices[i] = -1;
            }
        }

        public Node Pop()
        {
            Node ret = heap[0].Data;
            heap[0] = heap[heap.Count - 1];
            heap.RemoveAt(heap.Count - 1);
            PQIndices[ret.ListIndex] = -1;
            HeapifyDown();
            return ret;
        }

        public void InsertOrChange(Node n, int weight)
        {
            if (PQIndices[n.ListIndex] != -1)
            {
                int indexOnHeap = PQIndices[n.ListIndex];
                heap[indexOnHeap].Weight = weight;
                HeapifyUp(indexOnHeap);
            }
            else
            {
                HeapNode h = new HeapNode(n, weight);
                heap.Add(h);
                PQIndices[h.Data.ListIndex] = heap.Count - 1;
                HeapifyUp(heap.Count - 1);
            }
        }

        public void HeapifyDown()
        {
            int currentIndex = 0;
            int left = currentIndex * 2 + 1;
            int right = left + 1;
            bool done = false;
            while (!done && left < heap.Count - 1)
            {
                if(right > heap.Count - 1)
                {
                    if(heap[currentIndex].Weight > heap[left].Weight)
                    {
                        swap(currentIndex, left);
                        done = true;
                    }
                }
                else if(heap[left].Weight > heap[currentIndex].Weight && heap[right].Weight > heap[currentIndex].Weight)
                {
                    done = true;
                }
                else
                {
                    int smallest = left;
                    if (heap[right].Weight < heap[left].Weight)
                        smallest = right;

                    swap(smallest, currentIndex);

                    currentIndex = smallest;
                    left = currentIndex * 2 + 1;
                    right = left + 1;
                }
            }

        }

        public void HeapifyUp(int startIndex)
        {
            int currentIndex = startIndex;
            bool done = false;
            while(currentIndex != 0 && !done)
            {
                int parent = (currentIndex - 1) / 2;
                if (heap[currentIndex].Weight < heap[parent].Weight)
                {
                    swap(currentIndex, parent);
                    currentIndex = parent;
                }
                else
                {
                    done = true;
                }
            }
        }

        private void swap(int i1, int i2)
        {
            HeapNode temp = heap[i1];

            heap[i1] = heap[i2];
            PQIndices[heap[i1].Data.ListIndex] = heap[i2].Data.ListIndex;

            heap[i2] = temp;
            PQIndices[heap[i2].Data.ListIndex] = temp.Data.ListIndex;
        }

        public bool isEmpty()
        {
            return heap.Count == 0;
        }
    }

    class HeapNode
    {
        public Node Data;
        public int Weight;

        public HeapNode(Node d, int w)
        {
            Data = d;
            Weight = w;
        }
    }
}
