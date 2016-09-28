using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterStarQuest
{
    class Program
    {

        static void Main(string[] args)
        {
            string[] firstLine = (Console.ReadLine()).Split();
            long diameter = Convert.ToInt64(firstLine[0]);
            int totalStars = Convert.ToInt32(firstLine[1]);

            List<Star> stars = new List<Star>();
            inputStars(stars, totalStars);

            List<Star> Leftovers = new List<Star>();
            Star majority = findMajorityElement(stars, diameter);

            if (majority == null)
                Console.WriteLine("NO");

            else
                Console.WriteLine(CountOccurrences(majority, stars, diameter));

            //Console.ReadKey();
        }

        static void inputStars(List<Star> starList, int stars)
        {
            for(int i = 0; i < stars; i++)
            {
                string[] line = (Console.ReadLine()).Split();
                long x = Convert.ToInt64(line[0]);
                long y = Convert.ToInt64(line[1]);

                Star s = new Star(x, y);
                starList.Add(s);
            }
        }
        static Star findMajorityElement(List<Star> data, long diameter)
        {
            if (data.Count == 0)
                return null;
            if(data.Count == 1)
                return data[0];

            List<Star> dataPrime = new List<Star>();
            for(int i = 0; i < data.Count - 1; i += 2)
            {
                if(checkStars(data[i], data[i + 1], diameter))
                {
                    dataPrime.Add(data[i]);
                }
            }
            Star s = findMajorityElement(dataPrime, diameter);

            if (s == null)
            {
                if (data.Count % 2 == 1)
                {
                    if (CountOccurrences(data[data.Count - 1], data, diameter) > data.Count / 2)
                        return data[data.Count - 1];
                    else
                        return null;
                }
            }
            else
            {
                if (CountOccurrences(s, data, diameter) > data.Count / 2)
                    return s;
                else
                    return null;
            }
            return null;

        }

        static int CountOccurrences(Star s, List<Star> stars, long diameter)
        {
            int occurrences = 0;
            for(int i = 0; i < stars.Count; i++)
            {
                if (checkStars(s, stars[i], diameter))
                    occurrences++;
            }
            return occurrences;
        }

        static bool checkStars(Star s1, Star s2, long diameter)
        {
            if ((s1.x_coord - s2.x_coord) * (s1.x_coord - s2.x_coord) + (s1.y_coord - s2.y_coord) * (s1.y_coord - s2.y_coord) <= diameter * diameter)
            {
                return true;
            }
            return false;
        }
    }

    class Star
    {
        public long x_coord;
        public long y_coord;

        public Star(long x, long y)
        {
            x_coord = x;
            y_coord = y;
        }
    }
}
