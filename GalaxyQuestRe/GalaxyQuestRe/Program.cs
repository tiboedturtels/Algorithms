﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalaxyQuest
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] firstLine = (Console.ReadLine()).Split();
            long diameter = Convert.ToInt64(firstLine[0]);
            int totalStars = Convert.ToInt32(firstLine[1]);
            
            List<Galaxy> sortedGalaxies = new List<Galaxy>();

            int mostStars = 0;

            for (int i = 0; i < totalStars; i++)
            {
                string[] line = (Console.ReadLine()).Split();
                long x = Convert.ToInt64(line[0]);
                long y = Convert.ToInt64(line[1]);

                int k = CheckGalaxies(sortedGalaxies, 0, sortedGalaxies.Count - 1, x, y, diameter);
                
                
                /*
                bool foundGalaxy = false;
                for (int j = 0; j < Galaxies.Count && !foundGalaxy; j++)
                {
                    if (Galaxies[j].checkStar(x, y, diameter))
                    {
                        GalaxiesOfStars.Add(j);
                        foundGalaxy = true;
                    }
                }
                if (!foundGalaxy)
                {
                    Galaxy g = new Galaxy(x, y);
                    Galaxies.Add(g);
                    GalaxiesOfStars.Add(Galaxies.Count - 1);
                }
                */
            }

            int m = findMajorityElement(GalaxiesOfStars);
            if (m == -1)
                Console.WriteLine("NO");
            else
                Console.WriteLine(Galaxies[m].numStars);

            Console.ReadKey();

        }

        public static int CheckGalaxies(List<Galaxy> sorted, int lo, int hi, long x, long y, long diameter)
        {
            bool found = false;
            int lastMid = -1;
            int mid = sorted.Count / 2;
            while(mid != lastMid && !found)
            {
                if(x >= sorted[mid].x_coord - diameter && x <= sorted[mid].x_coord + diameter)
                {

                }

                else
                {

                }
            }
        }

        public static int findMajorityElement(List<int> arr)
        {
            if (arr.Count == 0)
                return -1;
            else if (arr.Count == 1)
                return arr[0];
            else
            {
                List<int> arrPrime = new List<int>();
                for (int i = 0; i < arr.Count - 1; i += 2)
                {
                    if (arr[i] == arr[i + 1])
                        arrPrime.Add(arr[i]);
                }
                int x = findMajorityElement(arrPrime);
                if (x == -1)
                {
                    if (arr.Count % 2 == 1)
                    {
                        if (isMajority(arr[arr.Count - 1], arr))
                            return arr[arr.Count - 1];
                        else
                            return -1;
                    }
                    else
                        return -1;
                }

                else
                {
                    if (isMajority(x, arr))
                        return x;
                    else
                        return -1;
                }
            }
        }

        public static bool isMajority(int target, List<int> a)
        {
            int occurrences = 0;
            for (int i = 0; i < a.Count; i++)
            {
                if (a[i] == target)
                    occurrences++;
            }
            if (occurrences > a.Count / 2)
                return true;
            else
                return false;
        }
    }

    class Galaxy
    {
        public long x_coord;
        public long y_coord;
        public int index;
        public int numStars;

        public Galaxy(long x, long y, int i)
        {
            x_coord = x;
            y_coord = y;
            index = i;
            numStars = 1;
        }

        public bool checkStar(long star_x, long star_y, long diameter)
        {
            if ((star_x - x_coord) * (star_x - x_coord) + (star_y - y_coord) * (star_y - y_coord) <= diameter * diameter)
            {
                numStars++;
                return true;
            }
            return false;
        }
    }
}
