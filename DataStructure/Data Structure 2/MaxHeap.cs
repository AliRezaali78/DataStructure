using System;
using System.Collections;
using System.Collections.Generic;

namespace DataStructure.Data_Structure_2
{
    public class MaxHeap : IEnumerable
    {
        public int Length { get; }
        private int[] _array;

        public int Size { get; private set; } = 0;

        public MaxHeap(int length = 16)
        {
            Length = length;
            _array = new int[Length];
        }

        public void Insert(int value)
        {
            if(Size == Length) throw new IndexOutOfRangeException("Heap is full");

            _array[Size] = value;
            
            BubbleUp(Size, GetRootIndex(Size));

            Size++;
        }

        private void BubbleUp(int valueIndex,int rootIndex)
        {
            if (valueIndex == 0 || _array[rootIndex] >= _array[valueIndex] )
                return;
           
            Swap(rootIndex, valueIndex);

            BubbleUp(rootIndex, GetRootIndex(rootIndex));
        }

        private int GetRootIndex(int index)
        {
            double exp = index - 1;
            return Size == 0 ? 0 : Convert.ToInt32(Math.Floor(exp / 2));
        }

        public int Remove()
        {
            if(Size == 0 ) throw new ArgumentNullException("Heap is empty");

            var root = _array[0];
            _array[0] = _array[Size -1];
            Size--;

            BubbleDown(0);

            return root;
        }

        private void BubbleDown(int index)
        {
            if (IsValidRoot(index))
                return;

            var largerChildIndex = LargerChildIndex(index);
            Swap(index, largerChildIndex);

            BubbleDown(largerChildIndex);
        }

        private int LargerChildIndex(int index)
        {
            if (!HasLeftChild(index))
                return index;
            
            if (!HasRightChild(index))
                return LeftChildIndex(index);

            return LeftChild(index) > RightChild(index) ?
                LeftChildIndex(index) : RightChildIndex(index);

        }
        private int LargerChildIndex(int index, int[] array)
        {
            if (!HasLeftChild(index,array))
                return index;
            
            if (!HasRightChild(index, array))
                return LeftChildIndex(index);

            return LeftChild(index, array) > RightChild(index, array) ?
                LeftChildIndex(index) : RightChildIndex(index);

        }

        private bool IsValidRoot(int index)
        {
            if (!HasLeftChild(index))
                return true;

            var isValid = _array[index] >= LeftChild(index);

            if (HasRightChild(index))
                isValid &= _array[index] >= RightChild(index);

            return isValid;
        }
        private bool IsValidRoot(int index, IReadOnlyList<int> array)
        {
            if (!HasLeftChild(index, array))
                return true;

            var isValid = array[index] >= LeftChild(index, array);

            if (HasRightChild(index, array))
                isValid &= array[index] >= RightChild(index, array);

            return isValid;
        }

        private bool HasLeftChild(int index)
        {
            return LeftChildIndex(index) <= Size;
        }
        private bool HasLeftChild(int index, IReadOnlyCollection<int> array)
        {
            return LeftChildIndex(index) <= array.Count;
        }
      
        private bool HasRightChild(int index)
        {
            return RightChildIndex(index) <= Size;
        }
        private bool HasRightChild(int index, IReadOnlyCollection<int> array)
        {
            return RightChildIndex(index) <= array.Count -1 ;
        }

        private int RightChildIndex(int rootIndex)
        {
            return rootIndex * 2 + 2;
        }
        private int LeftChildIndex(int rootIndex)
        {
            return rootIndex * 2 + 1;
        }

        private int LeftChild(int index)
        {
            var leftChildIndex = LeftChildIndex(index);

            return _array[leftChildIndex];
        } 
        private int LeftChild(int index, IReadOnlyList<int> array)
        {
            var leftChildIndex = LeftChildIndex(index);

            return array[leftChildIndex];
        }
    
        private int RightChild(int index)
        {
            var rightChildIndex = RightChildIndex(index);

            return _array[rightChildIndex];
        }
        private int RightChild(int index, IReadOnlyList<int> array)
        {
            var rightChildIndex = RightChildIndex(index);

            return array[rightChildIndex];
        }

        private void Swap(int first, int second)
        {
            var temp = _array[first];
            _array[first] = _array[second];
            _array[second] = temp;
        }
        private void Swap(IList<int> array, int first, int second)
        {
            var temp = array[first];
            array[first] = array[second];
            array[second] = temp;
        }

        public  void Heapify(int[] array)
        {
            for (int i = 0; i < array.Length / 2; i++)
            {
                for (int j = 0; j < array.Length / 2; j++)
                {
                    
                    if(IsValidRoot(j, array)) continue;

                    var largerChildIndex = LargerChildIndex(j, array);

                    Swap(array, j, largerChildIndex);
                }
            }
        }

        public static int GetKthLargest(int[] array, int kth)
        {
            if(kth < 1 || kth > array.Length) throw new ArgumentNullException();

            var heap = new MaxHeap(array.Length);

            foreach (var i in array)
                heap.Insert(i);

            for (int i = 0; i < kth - 1; i++)
                heap.Remove();

            return heap[0];
        }


        public int this[int index] => _array[index];

        public IEnumerator GetEnumerator()
        {
            return _array.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public bool IsMaxHeap(IReadOnlyList<int> array)
        {
            return IsMaxHeap(0, array);
        }
        private bool IsMaxHeap(int index, IReadOnlyList<int> array)
        {
            if (index >= array.Count) return true;

            if (!IsValidRoot(index, array))
                return false;

            return IsMaxHeap(LeftChildIndex(index), array) && IsMaxHeap(RightChildIndex(index), array);

        }
    }
}
