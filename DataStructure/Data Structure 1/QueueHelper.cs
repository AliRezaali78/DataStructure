using System;
using System.Collections.Generic;
using System.Text;

namespace DataStructure.Data_Structure_1
{
    public static class QueueHelper
    {
        public static Queue<T> Reverse<T>(Queue<T> queue)
        {
            var stack = new Stack<T>();

            while (queue.Count != 0)
                stack.Push(queue.Dequeue());

            while (stack.Count !=0)
                queue.Enqueue(stack.Pop());

            return queue;
        }

    }

    public class PriorityQueue
    {
        private int?[] _array;
        private int _rear;
        private int _front;
        private int _size;
        public PriorityQueue(int size)
        {
            _array = new int?[size];
        }

        public void Enqueue(int item)
        {
            if(_size == _array.Length) throw new Exception();

            if (_size == 0)
            {
                Insert(item);
                return;
            }

            if (ShiftToRightIfSmaller(item)) return;

            Insert(item);
        }

        private bool ShiftToRightIfSmaller(int item)
        {
            var i = _front;
            while (_array[i] != null)
            {
                if (_array[i] < item)
                {
                    i++;
                    continue;
                }

                for (var j = _size; j > i; j--)
                {
                    _array[j] = _array[j - 1];
                    _array[j - 1] = item;
                }

                MoveRear();
                return true;
            }

            return false;
        }

        public void Dequeue()
        {
            if (_size == 0) throw new Exception();

            _array[_front] = null;
            _front = (_front + 1) % _array.Length;
            _size--;
        }
       
        private void Insert(int item)
        {
            _array[_rear] = item;
            MoveRear();
        }

        private void MoveRear()
        {
            _size++;
            _rear = (_rear + 1) % _array.Length;
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.Append("[");
            foreach (var i in _array)
            {
                builder.Append(i + ",");    
            }

            builder.Append("]");
            return builder.ToString();
        }
    }

}
