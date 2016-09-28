using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSink
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, City> Cities = new Dictionary<string, City>();

            //Get Cities
            int numCities = Convert.ToInt32(Console.ReadLine());
            for(int i = 0; i < numCities; i++)
            {
                string[] line = Console.ReadLine().Split();

                string name = line[0];
                int toll = Convert.ToInt32(line[1]);

                Cities.Add(name, new City(name, toll));
            }

            //Get Highways
            int numHighways = Convert.ToInt32(Console.ReadLine());
            for(int i = 0; i < numHighways; i++)
            {
                string[] line = Console.ReadLine().Split();

                string first = line[0];
                string second = line[1];

                Cities[first].ConnectedCities.Add(second);
            }

            //Sort
            List<City> sorted = TopologicalSort(Cities);
            
            //Get Trips
            int numTrips = Convert.ToInt32(Console.ReadLine());
            List<string> trips = new List<string>();
            for (int i = 0; i < numTrips; i++)
            {
                trips.Add(Console.ReadLine());
            }
            
            //Check Paths
            for (int i = 0; i < numTrips; i++)
            {
                string[] line = trips[i].Split();

                string fromCity = line[0];
                string toCity = line[1];

                int fromIndex = 0;
                int toIndex = 0;

                for(int j = 0; j < sorted.Count; j++)
                {
                    if (sorted[j].Name == fromCity)
                        fromIndex = j;
                    if (sorted[j].Name == toCity)
                        toIndex = j;
                }

                if (fromIndex == toIndex)
                    Console.WriteLine("0");

                else if (fromIndex > toIndex)
                    Console.WriteLine("NO");

                else
                {
                    //Get sublist
                    List<City> subSorted = sorted.GetRange(fromIndex, toIndex - fromIndex + 1);
                    BellmanFord(subSorted, Cities);
                }
            }
            Console.ReadKey();
        }

        static void BellmanFord(List<City> subSorted, Dictionary<string, City> dict)
        {
            //Set up dictionaries
            Dictionary<string, int> dist = new Dictionary<string, int>();
            Dictionary<string, City> prev = new Dictionary<string, City>();

            dist.Add(subSorted[0].Name, 0);

            bool first = true;
            for (int j = 0; j < subSorted.Count; j++)
            {
                City currentCity = subSorted[j];

                if (prev.ContainsKey(currentCity.Name) || first)
                {
                    for (int k = 0; k < currentCity.ConnectedCities.Count; k++)
                    {
                        City otherCity = dict[currentCity.ConnectedCities[k]];

                        if (!dist.ContainsKey(otherCity.Name))
                        {
                            dist.Add(otherCity.Name, dist[currentCity.Name] + otherCity.Toll);
                            prev.Add(otherCity.Name, currentCity);
                        }

                        else if (dist[otherCity.Name] > dist[currentCity.Name] + otherCity.Toll)
                        {
                            dist[otherCity.Name] = dist[currentCity.Name] + otherCity.Toll;
                            prev[otherCity.Name] = currentCity;
                        }
                    }
                }
                first = false;
            }

            string endCity = subSorted[subSorted.Count - 1].Name;
            if (!dist.ContainsKey(endCity))
                Console.WriteLine("NO");

            else
                Console.WriteLine(dist[endCity]);

        }

        static List<City> TopologicalSort(Dictionary<string, City> cities)
        {
            List<City> ret = new List<City>();

            foreach(City c in cities.Values)
                TopologicalSort(cities, ret, c);

            return ret;
        }

        static void TopologicalSort(Dictionary<string, City> cities, List<City> ret, City currentCity)
        {
            if (!ret.Contains(currentCity))
            {
                foreach(string s in currentCity.ConnectedCities)
                {
                    TopologicalSort(cities, ret, cities[s]);
                }
                ret.Insert(0, currentCity);
            }
        }
    }

    class City
    {
        public string Name;
        public int Toll;

        public List<string> ConnectedCities;

        public City(string n, int t)
        {
            Name = n;
            Toll = t;
            ConnectedCities = new List<string>();
        }
    }
}
