using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace NumberTheory
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = Console.ReadLine();
            while(!string.IsNullOrEmpty(input))
            {
                string[] split = input.Split();
                switch(split[0])
                {
                    case "gcd":
                        Console.WriteLine(ee(Convert.ToInt64(split[1]), Convert.ToInt64(split[2]))[2]);
                        break;
                    case "exp":
                        Console.WriteLine(exp(Convert.ToInt64(split[1]), Convert.ToInt64(split[2]), Convert.ToInt64(split[3])));
                        break;
                    case "inverse":
                        long inv = inverse(Convert.ToInt64(split[1]), Convert.ToInt64(split[2]));
                        if (inv < 0)
                            Console.WriteLine("none");
                        else
                            Console.WriteLine(inv);
                        break;
                    case "isprime":
                        isPrime(Convert.ToInt64(split[1]));
                        break;
                    case "key":
                        RSA(Convert.ToInt64(split[1]), Convert.ToInt64(split[2]));
                        break;
                }
                input = Console.ReadLine();
            }
        }

        public static long[] ee(long a, long b)
        {
            if (b == 0)
            {
                long[] ret = { 1, 0, a };
                return ret;
            }
            else
            {
                long[] temp = ee(b, a % b);
                long[] ret = { temp[1], temp[0] - (a / b) * temp[1], temp[2] };
                return ret;
            }
        }

        public static long exp(long x, long y, long n)
        {
            if (y == 0)
                return 1;
            long z = exp(x, y / 2, n);
            if (y % 2 == 0)
                return z * z % n;
            else
                return x * ((z * z) % n) % n;
        }

        public static long inverse(long a, long n)
        {
            long[] arr = ee(a, n);
            if (arr[2] == 1)
            {
                if (arr[0] < 0)
                    return arr[0] + n;
                else
                    return arr[0] % n;
            }
            else
                return -1;
        }

        public static void isPrime(long p)
        {
            if (Fermat(p, 2) && Fermat(p, 3) && Fermat(p, 5))
                Console.WriteLine("yes");
            else
                Console.WriteLine("no");
        }

        public static bool Fermat(long p, long a)
        {
            return exp(a, p - 1, p) == 1;
        }

        public static void RSA(long p, long q)
        {
            long N = p * q;
            long e = 0;
            long d = 0;

            long calc = (p - 1) * (q - 1);

            bool done = false;
            for(int i = 2; !done; i++)
            {
                if(ee(i, calc)[2] == 1)
                {
                    e = i;
                    done = true;
                }
            }

            d = inverse(e, calc);

            Console.WriteLine(N.ToString() + " " + e.ToString() + " " + d.ToString());
        }
    }
}
