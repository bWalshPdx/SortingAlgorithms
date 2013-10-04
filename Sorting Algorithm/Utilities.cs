using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorting_Algorithm
{
    public class Utilities
    {
        //Verify the list is in order:
        public bool ArrayIsInOrder(int[] input)
        {
            return input.SequenceEqual(input.OrderBy(i => i));
        }

        //Generate an array that is a random set:

        public int[] GetRandomSet(int length)
        {
            List<int> output = new List<int>(length);
            List<int> orderedList = new List<int>(length);

            for (int i = 0; i < length; i++)
                orderedList.Add(i);


            while (orderedList.Count() != 0)
            {
                Random rnd = new Random(orderedList.Count() - 1);

                int random = rnd.Next(orderedList.Count() - 1);

                output.Add(orderedList[random]);
                orderedList.RemoveAt(random);
            }

            return output.ToArray();
        }

        //Test Speed for the sorting algorithm:


        //http://www.dotnetperls.com/action


        public void getSortSpeed<T>(string absPathOfFile, Func<int, T[]> randomArrayGenerator, Action<T[]> SortAlg, int bottom, int interval, int top)
        {


            using (System.IO.StreamWriter file = new System.IO.StreamWriter(absPathOfFile))
            {
                string line = "SortTime";

                file.WriteLine(line);

                for (int i = bottom; i < top; i = i + interval)
                {
                    T[] inputArray = randomArrayGenerator(i);

                    Stopwatch sw = new Stopwatch();

                    sw.Start();

                    SortAlg.Invoke(inputArray);

                    sw.Stop();
                    line = sw.ElapsedMilliseconds.ToString();

                    file.WriteLine(line);

                    Console.WriteLine("Iteration Complete");
                }
            }
        }
    }
}
