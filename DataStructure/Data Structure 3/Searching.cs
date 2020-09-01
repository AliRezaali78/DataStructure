using System;
using System.Collections.Generic;

namespace DataStructure.Data_Structure_3
{
    public static class Searching
    {
        public static int LinearSearch(this IList<int> list, int search)
        {
            for (var i = 0; i < list.Count; i++)
            {
                if (list[i] == search)
                    return i;
            }

            return -1;
        }

        public static int BinarySearch(this IList<int> list, int search)
        {
            var left = 0;
            var right = list.Count - 1;

            if (list[left] == search)
                return left;
            if (list[right] == search)
                return right;

            while (left <= right)
            {
                var mid = (left + right) / 2;

                if (list[mid] == search)
                    return mid;

                if (list[mid] > search)
                    right -= 1;
                else
                    left += 1;

            }

            return -1;
        }

        private static int BinarySearch(IList<int> list, int leftRange, int rightRange, int search)
        {
            var left = leftRange;
            var right = rightRange;

            if (list[left] == search)
                return left;
            if (list[right] == search)
                return right;

            while (left <= right)
            {
                var mid = (left + right) / 2;

                if (list[mid] == search)
                    return mid;

                if (list[mid] > search)
                    right -= 1;
                else
                    left += 1;

            }

            return -1;
        }

        public static int JumpSearch(this IList<int> list, int search)
        {
            var blockSize = (int) Math.Sqrt(list.Count - 1);

            var start = 0;
            var next = blockSize;
            while (start < list.Count && list[next - 1] < search)
            {
                start = next;
                next += blockSize;

                if (next > list.Count)
                    next = list.Count;
            }

            for (var i = start; i < next; i++)
                if (list[i] == search)
                    return i;

            return -1;
        }

        public static int ExponentialSearch(this IList<int> list, int search)
        {
            var bound = 1;

            while (bound < list.Count && list[bound] < search)
                bound *= 2;

            return BinarySearch(list, bound/2,Math.Min(bound,list.Count - 1), search);
        }

    }
}
