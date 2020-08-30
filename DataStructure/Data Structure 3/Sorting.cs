using System.Collections.Generic;
using System.Linq;

namespace DataStructure.Data_Structure_3
{
    public static class Sorting
    {
        public static IList<int> BubbleSort(this IList<int> list)
        {
            var isSorted = true;
            for (var i = 0; i < list.Count; i++)
            {
                for (var j = 1; j < list.Count - i; j++)
                    if (list[j] < list[j - 1])
                    {
                        Swap(list, j, j-1);
                        isSorted = false;
                    }

                if (isSorted) return list;
            }

            return list;
        }

        public static IList<int> SelectionSort(this IList<int> list)
        {
            for (int i = 0; i < list.Count; i++)
                for (int j = i + 1; j < list.Count; j++)
                    if(list[j] < list[i])
                        Swap(list, i, j);

            return list;
        }

        public static IList<int> InsertionSort(this IList<int> list)
        {
            for (var i = 1; i < list.Count; i++)
            {
                var current = list[i];
                var j = i - 1;
                while (j >= 0 && list[j] > current)
                {
                    list[j + 1] = list[j];
                    j--;
                }

                list[j + 1] = current;
            }   

            return list;
        }

        public static IList<int> MergeSort(this IList<int> list)
        {
            if (list.Count < 2) return list; // base condition : single item 

            var mid = list.Count / 2;

            var left = new int[mid];
            for (int i = 0; i < mid; i++)
                left[i] = list[i];

            var right = new int[list.Count - mid];
            for (int i = mid; i < list.Count; i++)
                right[i - mid] = list[i];

            MergeSort(left);
            MergeSort(right);

            Merge(left, right, list);

            return list;
        }
        private static void Merge(int[] left, int[] right, IList<int> result)
        {
            int i = 0, j = 0, k = 0;

            while (i < left.Length && j < right.Length)
            {
                if (left[i] <= right[j])
                    result[k++] = left[i++];
                else
                    result[k++] = right[j++];
            }

            while (i < left.Length)
                result[k++] = left[i++];
            
            while (j < right.Length)
                result[k++] = right[j++];

        }

        public static IList<int> QuickSort(this IList<int> list)
        {
           QuickSort(list, 0 , list.Count - 1);

           return list;
        }
        private static void QuickSort(IList<int> list, int start, int end)
        {
            if (start >= end) return;
            var boundary = Partition(list,start,end);

            QuickSort(list, start, boundary - 1); // left

            QuickSort(list, boundary + 1, end); // right
        }
        private static int Partition(IList<int> list, int start, int end)

        {
            var boundary = start - 1;
            var pivot = list[end];
            for (var i = start; i <= end; i++)
                if (list[i] <= pivot)
                    Swap(list, i, ++boundary);

            return boundary;
        }

        public static IList<int> CountSort(this IList<int> list)
        {
            var max = list.Max();

            var array = new int[max + 1];

            foreach (var item in list)
                array[item]++;

            int k = 0;
            for (int i = 0; i < array.Length; i++)
                for (int j = 0; j < array[i]; j++)
                    list[k++] = i;

            return list;
        }

        private static void Swap(IList<int> list, int index1, int index2)
        {
            var temp = list[index2];
            list[index2] = list[index1];
            list[index1] = temp;
        }
    }
}
