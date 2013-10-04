using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorting_Algorithm
{
    using System.Diagnostics;

    using NUnit.Framework;

    [TestFixture]
    class Sorting_Tests
    {
        
        #region UtilityTests

        [TestCase(new int[] {1,2,3,4}, true)]
        [TestCase(new int[] { 2, 1, 3, 4 }, false)]
        public void ArrayIsInOrder_Works(int[] input, bool expectedOutput)
        {
            Utilities u = new Utilities();

            var output = u.ArrayIsInOrder(input);

            Assert.AreEqual(expectedOutput, output);
        }


        [Test]
        public void IComparerDoesWhatIThinkItDoes()
        {
            int output = getCompare<string>("Brian", "Walsh");
            Assert.AreEqual(-1, output);

            output = getCompare<int>(1, 2);
            Assert.AreEqual(-1, output);

            output = getCompare<string>("Brian", "Brian");
            Assert.AreEqual(0, output);

            output = getCompare<int>(1, 1);
            Assert.AreEqual(0, output);

            output = getCompare<string>("Walsh", "Brian");
            Assert.AreEqual(1, output);

            output = getCompare<int>(2, 1);
            Assert.AreEqual(1, output);
        }

        public int getCompare<T>(T first, T second) where T : IComparable
        {
            return first.CompareTo(second);
        }


        [Test]
        public void GetRandomSet_ActuallyWorks()
        {
            Utilities u = new Utilities();

            var output = u.GetRandomSet(100);
        }

        #endregion

        #region BubbleSortTests

        [Test]
        public void BubbleSort_ActuallyWorks()
        {
            Utilities u = new Utilities();
            Sorting s = new Sorting();


            int[] inputArray = u.GetRandomSet(1000);
            s.BubbleSort(inputArray);

            var output = u.ArrayIsInOrder(inputArray);

            Assert.IsTrue(output);
        }

        [Test]
        public void BubbleSort_RunSpeedTest()
        {
            Utilities u = new Utilities();
            Sorting s = new Sorting();

            int bottom =   1000;
            int interval = 1000;
            int top =     10000;

            string absPath = @"C:\Users\Brian\Dropbox\.5 - Inbox\PerfGraph\BubbleSort_SortTimes.txt";

            u.getSortSpeed(absPath, (i) => u.GetRandomSet(i), (j) => s.BubbleSort(j), bottom, interval, top);
        }


        [Test]
        public void ListVsMySort()
        {
            Utilities u = new Utilities();
            Sorting s = new Sorting();

            int bottom = 1000;
            int interval = 1000;
            int top = 10000;



            string listSortAbsPath = @"C:\Users\bwalsh\Dropbox\.5 - Inbox\PerfGraph\ListVsMySortTimes.txt";

            using (System.IO.StreamWriter file = new System.IO.StreamWriter(listSortAbsPath))
            {
                string line = "Elements" + "\t" + "CustomIsFaster" + "\t" + "CustomSortTime" + "\t" + "ListSortTime";

                file.WriteLine(line);

                for (int i = bottom; i < top; i = i + interval)
                {
                    int[] stuff1 = u.GetRandomSet(i);
                    int[] stuff2 = new int[i];
                    Array.Copy(stuff1, stuff2, i);
                    List<int> list = stuff2.ToList();

                    TimeTracker tt = new TimeTracker();

                    tt.Elements = i;

                    Stopwatch sw = new Stopwatch();

                    sw.Start();

                    list.Sort();

                    sw.Stop();

                    tt.ListSortTime = sw.ElapsedMilliseconds;

                    sw.Reset();

                    sw.Start();

                    s.BubbleSort(stuff1);

                    sw.Stop();

                    tt.CustomSortTime = sw.ElapsedMilliseconds;
                    
                    tt.CustomIsFaster = tt.CustomSortTime < tt.ListSortTime;

                    line = tt.Elements + "\t" + tt.CustomIsFaster + "\t" + tt.CustomSortTime + "\t" + tt.ListSortTime;

                    file.WriteLine(line);

                    Console.WriteLine("List Sort Complete");
                }
            }
        }

        public class TimeTracker
        {
            public int Elements;
            public long CustomSortTime;
            public long ListSortTime;
            public bool CustomIsFaster;
        }

#endregion

        
        [Test]
        public void Partition_PartitionWorks()
        {


            int upperLimit = 1000;
            Sorting s = new Sorting();
            Utilities u = new Utilities();

            
            for (int z = 1; z < upperLimit; z++)
            {
                Console.WriteLine(z);

                int[] input = u.GetRandomSet(z);

                int leftPosition = 0;
                int rightPosition = input.Count() - 1;

                int pivotPosition = leftPosition + ((rightPosition - leftPosition) / 2);
                int pivotValue = input[pivotPosition];
                s.QuickSort(ref input);
                
                pivotPosition = input.ToList().IndexOf(pivotValue);

                int[] firstHalf = input.Take(pivotPosition).ToArray();

                int[] secondHalf = input.Skip(pivotPosition).ToArray();

                bool firstHalfIsSmaller = true;
                bool secondHalfIsLarger = true;

                foreach (var i in firstHalf)
                {
                    if (pivotValue < i)
                        firstHalfIsSmaller = false;
                }

                foreach (var i in secondHalf)
                {
                    if (i < pivotValue)
                        secondHalfIsLarger = false;
                }

                Assert.IsTrue(firstHalfIsSmaller);
                Assert.IsTrue(secondHalfIsLarger);
            }
        }


        [Test]
        public void QuickSort_SortingIsComplete()
        {
            int elements = 5;
            Sorting s = new Sorting();
            Utilities u = new Utilities();

            int[] input = u.GetRandomSet(elements);

            int[] expectedOutput = new int[elements];

            Array.Copy(input, expectedOutput, elements);

            s.BubbleSort(expectedOutput);

            s.QuickSort(ref input);

            bool sequenceEqual = input.SequenceEqual(expectedOutput);

            if (!sequenceEqual)
            {
                Assert.AreSame(expectedOutput, input);
            }

            Assert.IsTrue(sequenceEqual);

        }

        [Test]
        public void SkipAndTake()
        {
            Utilities u = new Utilities();
            Sorting s = new Sorting();
            int[] input = u.GetRandomSet(10);



            int _pivotPosition = 2;


            int[] arrayHead = input.Take(_pivotPosition + 1).ToArray();

            s.BubbleSort(arrayHead);

            int[] headRemoved = input.Skip(_pivotPosition + 1).ToArray();


            s.BubbleSort(headRemoved);
            
            input = arrayHead.Concat(headRemoved).ToArray();

        }

    }

    

    



}
