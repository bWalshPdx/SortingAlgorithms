using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorting_Algorithm
{
    public class Sorting
    {
        

        //THE SORTING ALGORITHMS THAT ARE N^2 OR BETTER AND HAVE TO BE GENERIC:

        //QuickSort
        //In Place Merge Sort
        //Heap Sort
        //Insertion Sort
        //Intro Sort

        public void BubbleSort<T>(T[] inputArray) where T : IComparable
        {
            T first;
            T second;

            if (inputArray.Count() <= 1) 
                return;

            for (int i = inputArray.Count() - 1; i != 0; i--)
            {
                for (int j = 0; j < i - 1; j++)
                {
                    if (inputArray[j].CompareTo(inputArray[j + 1]) == 1)
                    {
                        first = inputArray[j];
                        second = inputArray[j + 1];

                        inputArray[j] = second;
                        inputArray[j + 1] = first;
                    }
                }
            }
        }

        

        private int Swap<T>(T[] array, int left, int right, int pivot)
        {
            if (pivot == left)
            {
                pivot = right;
            }
            else if (pivot == right)
            {
                pivot = left;
            }
                

            var temp = array[right];
            array[right] = array[left];
            array[left] = temp;

            return pivot;
        }

        public void QuickSort<T>(ref T[] inputArray) where T : IComparable
        {

            int leftPosition = 0;

            int rightPosition = inputArray.Count() - 1;

            int pivotPosition = leftPosition + ((rightPosition - leftPosition) / 2);

            T pivotValue = inputArray[pivotPosition];

            if (inputArray.Count() < 2) 
                return;


            while (leftPosition < rightPosition)
            {

                int leftToPivotComparison = inputArray[leftPosition].CompareTo(pivotValue);
                int rightToPivotComparison = inputArray[rightPosition].CompareTo(pivotValue);

                //The element is in the correct position:
                if (leftToPivotComparison == -1) 
                    leftPosition++;

                if (rightToPivotComparison == 1)
                    rightPosition--;

                //Both elements are in the wrong Position:
                bool leftPosBad = (leftToPivotComparison == 1 || leftToPivotComparison == 0);
                bool rightPosBad = (rightToPivotComparison == -1 || rightToPivotComparison == 0);

                if (leftPosBad && rightPosBad)
                    pivotPosition =  Swap(inputArray, leftPosition, rightPosition, pivotPosition);

            }




            //Recurse on left and right partitions:
            if (0 < pivotPosition)
            {
                T[] arrayHead = inputArray.Take(pivotPosition + 1).ToArray();

                this.QuickSort<T>(ref arrayHead);

                T[] headRemoved = inputArray.Skip(pivotPosition + 1).ToArray();

                inputArray = arrayHead.Concat(headRemoved).ToArray();
            }

            if (pivotPosition <= inputArray.Count() - 3)
            {
                T[] arrayTail = inputArray.Skip(pivotPosition + 1).ToArray();

                this.QuickSort<T>(ref arrayTail);

                T[] tailRemoved = inputArray.Take(pivotPosition + 1).ToArray();

                inputArray = tailRemoved.Concat(arrayTail).ToArray();
            }
        }
    }
}
