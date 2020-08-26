using System;
using System.Collections;
using System.Collections.Generic;

namespace DataStructure.Data_Structure_2
{
    public class PriorityHeap<T>: IEnumerable<T> 
    {
        private readonly List<T> _list;

        public int Size { get; private set; } = 0;

        private readonly Func<T,T,bool> _compareFunc;

        public PriorityHeap(Func<T,T,bool> compareFunc)
        {
            _list = new List<T>();
            _compareFunc = compareFunc;
        }

        public void Enqueue(T item)
        {
            if(item == null)
                throw new ArgumentNullException(nameof(item));

            _list.Add(item);
            
            BubbleUp(Size, GetRootIndex(Size));
            
            Size++;
        }

        private void BubbleUp(int valueIndex,int rootIndex)
        {
            // _list[rootIndex].CompareTo(_list[valueIndex])
            if (valueIndex == 0 ||  _compareFunc(_list[rootIndex],_list[valueIndex]))
                return;
           
            Swap(rootIndex, valueIndex);

            BubbleUp(rootIndex, GetRootIndex(rootIndex));
        }

        private int GetRootIndex(int index)
        {
            double exp = index - 1;
            return Size == 0 ? 0 : Convert.ToInt32(Math.Floor(exp / 2));
        }

        public T Dequeue()
        {
            if(Size == 0 ) throw new ArgumentNullException("Heap is empty");

            var root = _list[0];
            _list[0] = _list[Size -1];
            Size--;

            BubbleDown(0);

            _list.RemoveAt(Size);

            return root;
        }

        private void BubbleDown(int index)
        {
            if (IsValidRoot(index))
                return;

            var largerChildIndex = PriorityChildIndex(index);
            Swap(index, largerChildIndex);

            BubbleDown(largerChildIndex);
        }

        private int PriorityChildIndex(int index)
        {
            if (!HasLeftChild(index))
                return index;
            
            if (!HasRightChild(index))
                return LeftChildIndex(index);

            return _compareFunc(LeftChild(index),RightChild(index))?
                LeftChildIndex(index) : RightChildIndex(index);

        }

        private bool IsValidRoot(int rootIndex)
        {
            if (!HasLeftChild(rootIndex))
                return true;

            var isValid = _compareFunc(_list[rootIndex],LeftChild(rootIndex));

            if (HasRightChild(rootIndex))
                isValid &= _compareFunc(_list[rootIndex],RightChild(rootIndex));

            return isValid;
        }

        private bool HasLeftChild(int index)
        {
            return LeftChildIndex(index) <= Size;
        }
      
        private bool HasRightChild(int index)
        {
            return RightChildIndex(index) <= Size;
        }

        private static int RightChildIndex(int rootIndex)
        {
            return rootIndex * 2 + 2;
        }
        private static int LeftChildIndex(int rootIndex)
        {
            return rootIndex * 2 + 1;
        }
        private T LeftChild(int index)
        {
            var leftChildIndex = LeftChildIndex(index);

            return _list[leftChildIndex];
        } 
        private T RightChild(int index)
        {
            var rightChildIndex = RightChildIndex(index);

            return _list[rightChildIndex];
        }
        private void Swap(int first, int second)
        {
            var temp = _list[first];
            _list[first] = _list[second];
            _list[second] = temp;
        }

        public T this[int index] => _list[index];

        public IEnumerator<T> GetEnumerator()
        {
            return _list.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    
    }

}