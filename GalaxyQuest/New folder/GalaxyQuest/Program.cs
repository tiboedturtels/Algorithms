using System;
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

            List<Galaxy> Galaxies = new List<Galaxy>();
            List<int> GalaxiesOfStars = new List<int>();

            int nextGalaxyInd = 0;

            string[] fline = (Console.ReadLine()).Split();
            long fx = Convert.ToInt64(fline[0]);
            long fy = Convert.ToInt64(fline[1]);

            Galaxy first = new Galaxy(fx, fy);
            Galaxies.Add(first);
            GalaxiesOfStars.Add(0);
            nextGalaxyInd++;
            Quadrant whole = new Quadrant(0, 1000000000, 0, 1000000000, first);

            for(int i = 1; i < totalStars; i++)
            {
                string[] line = (Console.ReadLine()).Split();
                long x = Convert.ToInt64(line[0]);
                long y = Convert.ToInt64(line[1]);

                whole.placeStar(x, y, diameter, ref nextGalaxyInd, GalaxiesOfStars, Galaxies);

            }

            int m = findMajorityElement(GalaxiesOfStars);
            if (m == -1)
                Console.WriteLine("NO");
            else
                Console.WriteLine(Galaxies[m].numStars);

            Console.ReadKey();
            
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
                for(int i = 0; i < arr.Count - 1; i += 2)
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
        long x_coord;
        long y_coord;

        public int numStars;

        public int index;

        public Galaxy(long x, long y, int i)
        {
            x_coord = x;
            y_coord = y;
            index = i;
            numStars = 1;
        }

        public bool checkStar(long star_x, long star_y, long diameter)
        {
            if((star_x - x_coord) * (star_x - x_coord) + (star_y - y_coord) * (star_y - y_coord) <= diameter * diameter)
            {
                numStars++;
                return true;
            }
            return false;
        }
    }

    class Quadrant
    {
        long xlo;
        long xhi;
        long ylo;
        long yhi;

        Galaxy galaxy;
        Quadrant[] subQuadrants;

        bool isEnd;

        public Quadrant(long xl, long xh, long yl, long yh, Galaxy g)
        {
            xlo = xl;
            xhi = xh;
            ylo = yl;
            yhi = yh;

            galaxy = g;
            subQuadrants = new Quadrant[4];

            isEnd = true;
        }

        public bool placeStar(long x, long y, long diameter, ref int nextGalInd, List<int> gos, List<Galaxy> galaxies)
        {
            if(galaxy.checkStar(x, y, diameter))
            {
                gos.Add(galaxy.index);
                return true;
            }

            long mid = (xhi + xlo) / 2;
            int subIndex;
            if(x < mid)
            {
                if (y < mid)
                    subIndex = 2;
                else
                    subIndex = 0;
            }
            else
            {
                if (y < mid)
                    subIndex = 3;
                else
                    subIndex = 1;
            }

            bool x_edge = false;
            bool y_edge = false;
            bool center = false;
            if ((mid - x) * (mid - x) <= diameter * diameter)
                x_edge = true;
            if((mid - y) * (mid - y) <= diameter * diameter)
                y_edge = true;
            if ((mid - y) * (mid - y) + (mid - x) * (mid - x) <= diameter * diameter)
                center = true;

            List<int> QuadsToChk = new List<int>();
            if (subIndex == 0)
            {
                if (x_edge)
                    QuadsToChk.Add(1);
                if (y_edge)
                    QuadsToChk.Add(2);
                if (center)
                    QuadsToChk.Add(3);
            }
            else if (subIndex == 1)
            {
                if (x_edge)
                    QuadsToChk.Add(0);
                if (y_edge)
                    QuadsToChk.Add(3);
                if (center)
                    QuadsToChk.Add(2);
            }
            else if (subIndex == 2)
            {
                if (x_edge)
                    QuadsToChk.Add(3);
                if (y_edge)
                    QuadsToChk.Add(0);
                if (center)
                    QuadsToChk.Add(1);
            }
            else
            {
                if (x_edge)
                    QuadsToChk.Add(2);
                if (y_edge)
                    QuadsToChk.Add(1);
                if (center)
                    QuadsToChk.Add(0);
            }
            bool done = false;
            for (int i = 0; i < 4 && !done; i++)
            {
                done = placeStar
            }

            
        }
    }
}
