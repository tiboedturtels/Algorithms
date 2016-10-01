using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RumorMill
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, Student> StudentsDict = new Dictionary<string, Student>();

            //Take in students
            int numStudents = Convert.ToInt32(Console.ReadLine());
            for(int i = 0; i < numStudents; i++)
            {
                string sName = Console.ReadLine();
                Student s = new Student(sName);
                StudentsDict.Add(sName, s);
            }

            //Take in Friendships
            int numFreindships = Convert.ToInt32(Console.ReadLine());
            for (int i = 0; i < numFreindships; i++)
            {
                string[] line = Console.ReadLine().Split();

                Student s1 = StudentsDict[line[0]];
                Student s2 = StudentsDict[line[1]];

                s1.AddFriend(s2);
            }

            int numSearches = Convert.ToInt32(Console.ReadLine());
            List<string> searches = new List<string>();
            for (int i = 0; i < numSearches; i++)
            {
                searches.Add(Console.ReadLine());
            }

            for (int i = 0; i < searches.Count; i++)
            {
                Student start = StudentsDict[searches[i]];

                Dictionary<string, int> dist = new Dictionary<string, int>();
                Dictionary<string, Student> prev = new Dictionary<string, Student>();

                BFS(StudentsDict, start, dist, prev);

                Ouput(dist);
            }

            Console.ReadKey();
        }

        static void Ouput(Dictionary<string, int> dist)
        {
            List<string>[] order = new List<string>[dist.Count + 1];
            for(int i = 0; i < order.Length; i++)
            {
                order[i] = new List<string>();
            }

            foreach(string s in dist.Keys)
            {
                if(dist[s] == int.MaxValue)
                    order[order.Length - 1].Add(s);
                else
                    order[dist[s]].Add(s);
            }

            for(int i = 0; i < order.Length; i ++)
            {
                List<string> current = order[i];
                current.Sort();
                for(int j = 0; j < current.Count; j++)
                {
                    Console.Write(current[j] + " ");
                }
            }
            Console.WriteLine();
        }

        static void BFS(Dictionary<string, Student> Graph, Student start, Dictionary<string, int> dist, Dictionary<string, Student> prev)
        {
            //Clear out BFS structure
            foreach (Student s in Graph.Values)
            {
                dist.Add(s.Name, int.MaxValue);
                prev.Add(s.Name, null);
            }

            //Set up first 
            dist[start.Name] = 0;
            Queue<Student> Q = new Queue<Student>();
            Q.Enqueue(start);

            while(Q.Count > 0)
            {
                Student current = Q.Dequeue();
                for(int i = 0; i < current.Friends.Count; i++)
                {
                    if(dist[current.Friends[i].Name] == int.MaxValue)
                    {
                        Student next = current.Friends[i];
                        dist[next.Name] = dist[current.Name] + 1;
                        prev[next.Name] = current;
                        Q.Enqueue(next);
                    }
                }
            }
        }
    }

    class Student
    {
        public string Name;
        public List<Student> Friends;
        public Student(string n)
        {
            Name = n;
            Friends = new List<Student>();
        }

        public void AddFriend(Student other)
        {
            Friends.Add(other);
            other.Friends.Add(this);
        }
    }
}
