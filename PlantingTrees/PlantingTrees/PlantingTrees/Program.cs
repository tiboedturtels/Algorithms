using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace PlantingTrees
{
    class Program
    {
        public const int DURATION = 1000;
        static void Main(string[] args)
        {

            // Report the average time required to do a linear search for various sizes
            // of arrays.
            int size = 32;
            Console.WriteLine("\nSize\tTime (msec)\tRatio (msec)");
            double previousTime = 0;
            for (int i = 0; i <= 17; i++)
            {
                size = size * 2;
                double currentTime = TimePlantingTrees(size - 1);
                Console.Write((size - 1) + "\t" + currentTime.ToString("G3"));
                if (i > 0)
                {
                    Console.WriteLine("   \t" + (currentTime / previousTime).ToString("G3"));
                }
                else
                {
                    Console.WriteLine();
                }
                previousTime = currentTime;
            }

        }

        static int PlantingTrees(int NumTrees, int[] TreeTimes)
        {
            int DaysLeft = 0;

            Array.Sort(TreeTimes);

            for (int i = NumTrees - 1; i >= 0; i--)
            {
                DaysLeft--;
                if (TreeTimes[i] > DaysLeft)
                    DaysLeft = TreeTimes[i];
            }

            return DaysLeft + NumTrees + 1;
        }

        public static double TimePlantingTrees(int size)
        {
            Random r = new Random();

            // Construct a sorted array
            int[] data = new int[size];
            for (int i = 0; i < size; i++)
            {
                data[i] = r.Next(1000001);
            }

            // Get the process
            Process p = Process.GetCurrentProcess();

            // Keep increasing the number of repetitions until one second elapses.
            double elapsed = 0;
            long repetitions = 1;
            do
            {
                repetitions *= 2;
                TimeSpan start = p.TotalProcessorTime;
                for (int i = 0; i < repetitions; i++)
                {
                    for (int d = 0; d < size; d++)
                    {
                        PlantingTrees(data.Length, data);
                    }
                }
                TimeSpan stop = p.TotalProcessorTime;
                elapsed = stop.TotalMilliseconds - start.TotalMilliseconds;
            } while (elapsed < DURATION);
            double totalAverage = elapsed / repetitions / size;

            // Keep increasing the number of repetitions until one second elapses.
            elapsed = 0;
            repetitions = 1;
            do
            {
                repetitions *= 2;
                TimeSpan start = p.TotalProcessorTime;
                for (int i = 0; i < repetitions; i++)
                {
                    for (int d = 0; d < size; d++)
                    {
                        //LinearSearch(data, d);
                    }
                }
                TimeSpan stop = p.TotalProcessorTime;
                elapsed = stop.TotalMilliseconds - start.TotalMilliseconds;
            } while (elapsed < DURATION);
            double overheadAverage = elapsed / repetitions / size;

            // Return the difference
            return totalAverage - overheadAverage;
        }
    }
}
