using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CeilingFunction
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Node> Shapes = new List<Node>();

            string[] firstLine = (Console.ReadLine()).Split();
            int numTrees = Convert.ToInt32(firstLine[0]);
            int numNodes = Convert.ToInt32(firstLine[1]);

            for(int i = 0; i < numTrees; i++)
            {
                int[] values = Array.ConvertAll((Console.ReadLine()).Split(), int.Parse);

                //set up tree and evaluates shape
                Node root = new Node(values[0]);
                for(int j = 1; j < numNodes; j++)
                {
                    root.PlaceNewNode(values[j]);
                }


                //int[] shape = new int[20];
                //root.EvaluateShape(ref shape, 0, 0);


                //adds to list if needed
                bool noMatch = true;
                for(int j = 0; j < Shapes.Count() && noMatch; j++)
                {
                    noMatch = !AreSameShape(Shapes[j], root);
                }
                if (noMatch)
                    Shapes.Add(root);
            }

            Console.WriteLine(Shapes.Count);
            Console.ReadLine();
        }

        /*
        public static bool sameShape(int[] a1, int[] a2)
        {
            for(int i = 0; i < a1.Length; i++)
            {
                if (a1[i] != a2[i])
                    return false;
            }
            return true;
        }
        */

        public static bool AreSameShape(Node n1, Node n2)
        {
            bool N1HasLeft = n1.Left != null;
            bool N1HasRight = n1.Right != null;
            bool N2HasLeft = n2.Left != null;
            bool N2HasRight = n2.Right != null;

            bool LeftIsGood;
            bool RightIsGood;

            if (N1HasLeft && N2HasLeft)
                LeftIsGood = AreSameShape(n1.Left, n2.Left);
            else
                LeftIsGood = N1HasLeft == N2HasLeft;

            if (N1HasRight && N2HasRight)
                RightIsGood = AreSameShape(n1.Right, n2.Right);
            else
                RightIsGood = N1HasRight == N2HasRight;

            return LeftIsGood && RightIsGood;

            /*
            if(N1HasLeft && N2HasLeft)
            {
                if(N1HasRight && N2HasRight)
                {
                    return AreSameShape(n1.Right, n2.Right) && AreSameShape(n1.Left, n2.Left);
                }
                return N1HasRight == N2HasRight;
            }

            return N1HasLeft == N2HasLeft;
            */
        }

    }


    class Node
    {
        public int Value;
        public Node Left;
        public Node Right;

        public Node(int v)
        {
            Value = v;
        }

        public void PlaceNewNode(int v)
        {
            if(v > Value)
            {
                if (Right == null)
                    Right = new Node(v);
                else
                    Right.PlaceNewNode(v);
            }

            else
            {
                if (Left == null)
                    Left = new Node(v);
                else
                    Left.PlaceNewNode(v);
            }
        }

        public void EvaluateShape(ref int[] shape, int depth, int index)
        {
            shape[depth] += Convert.ToInt32(Math.Pow(2.0, index));
            if (Left != null)
                Left.EvaluateShape(ref shape, depth + 1, 2 * index);
            if(Right != null)
                Right.EvaluateShape(ref shape, depth + 1, 2 * index + 1);
        }
    }
}
