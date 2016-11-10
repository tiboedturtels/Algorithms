using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankKattis
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] first = Console.ReadLine().Split();

            int numPeople = Convert.ToInt32(first[0]);
            int numMinutes = Convert.ToInt32(first[1]);

            int totalMoney = 0;
            
            List<int>[] Deposites = new List<int>[numMinutes];
            for(int i = 0; i < Deposites.Length; i++)
            {
                Deposites[i] = new List<int>();
            }

            //Take in every person
            for (int i = 0; i < numPeople; i++)
            {
                string[] line = Console.ReadLine().Split();

                int money = Convert.ToInt32(line[0]);
                int wait = Convert.ToInt32(line[1]);

                Deposites[wait].Add(money);
            }

            List<int> Candidates = new List<int>();
            for (int i = numMinutes - 1; i >= 0; i--)
            {
                List<int> current = Deposites[i];
                if (current.Count != 0)
                {
                    for (int k = 0; k < current.Count; k++)
                    {
                        Candidates.Add(current[k]);
                    }

                    int max = Candidates.Max();
                    totalMoney += max;
                    Candidates.Remove(max);
                }
            }

            Console.WriteLine(totalMoney);

            Console.Read();
        }
    }
}