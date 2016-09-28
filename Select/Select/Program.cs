using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Select
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] A = { 1, 2, 5, 8, 13, 14, 14, 143, 150 };
            int[] B = { 1, 2, 5, 8, 13, 14, 14, 143, 150 };
            Console.WriteLine(select(A, B, 10));
            Console.ReadLine();
        }

        static int select(int[] A, int[] B, int k)
        {
            return select(A, 0, A.Length - 1, B, 0, B.Length - 1, k);
        }

        static int select (int[] A, int loA, int hiA, int[] B, int loB, int hiB, int k)
        {
            if (hiA < loA)
                return B[k - loA];
            if (hiB < loB)
                return A[k - loB];

            int i = (loA + hiA) / 2;
            int j = (loB + hiB) / 2;

            if(A[i] <= B[j])
            {
                if (k > i + j)
                    return select(A, i + 1, hiA, B, loB, hiB, k);
                else
                    return select(A, loA, hiA, B, loB, j - 1, k);
            }

            else
            {
                if (k > i + j)
                    return select(A, loA, hiA, B, j + 1, hiB, k);
                else
                    return select(A, loA, i - 1, B, loB, hiB, k);
            }
        }

    }

    
}
