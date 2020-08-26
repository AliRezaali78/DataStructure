using System;
using System.Collections;
using System.Collections.Generic;

namespace DataStructure.Data_Structure_1
{
    public class LinkedList<T> : IEnumerable<T>
    {
        public Node<T> First { get; private set; }

        public Node<T> Last { get; private set; }
        public int Size { get; private set; } = 0;

        public void AddFirst(T value)
        {
            var node = new Node<T> {Value = value};
            if (InitialIfEmpty(node)) return;

            node.Next = First;
            First = node;
            Size++;
        }

        public void AddLast(T value)
        {
            var node = new Node<T>() {Value = value};
            if (InitialIfEmpty(node)) return;

            Last.Next = node;
            Last = node;
            Size++;
        }

        private bool InitialIfEmpty(Node<T> node)
        {
            if (IsEmpty())
            {
                First = Last = node;
                Size++;
                return true;
            }

            return false;
        }

        public void DeleteFirst()
        {
            if (Size == 0) throw new Exception("List is empty");



            var oldFirst = First;
            First = First.Next;
            oldFirst.Next = null;

            Size--;
        }

        public void DeleteLast()
        {
            if (Size == 0) throw new Exception("List is empty");

            var currentNode = First;
            while (currentNode != null)
            {
                if (currentNode == Last)
                    First = Last = null;

                if (currentNode.Next == Last)
                {
                    currentNode.Next = null;
                    Last = currentNode;
                }

                currentNode = currentNode.Next;
            }

            Size--;
        }

        public bool Contains<T>(T value) where  T : IComparable
        {
            return IndexOf(value) != -1;
        }

        public int IndexOf<T>(T value) where T : IComparable
        {
            var index = 0;
            var currentNode = First;
            while (currentNode !=null)
            {
                if (value.CompareTo(currentNode.Value) == 0) 
                    return index;

                index++;
                currentNode = currentNode.Next;
            }

            return -1;
        }

        public T[] ToArray()
        {
            var array = new T[Size];

            var index = 0;
            var currentNode = First;
            while (currentNode != null)
            {
                array[index++] = currentNode.Value;
                currentNode = currentNode.Next;
            }

            return array;
        }

        private bool IsEmpty()
        {
            return First == null;
        }

        public void ReverseWithArray()
        {
            var array = ToArray();
            System.Array.Reverse(array);
            var index = 0;
            var currentNode = First;
            while (currentNode != null)
            {
                currentNode.Value = array[index++];
                currentNode = currentNode.Next;
            }
        }

        public void Reverse()
        {
            // [10 => 20 => 30]
            //  p     c     n

            var previous = First;
            var current = First.Next;

            while (current != null)
            {
                var next = current.Next;
                current.Next = previous;
                previous = current;
                current = next;
            }

            var temp = First;
            First = Last;
            Last = temp;
            Last.Next = null;
        }

        public T FindKthNodeValueFromEnd(int kth) 
        {
            if(IsEmpty()) throw new ArgumentNullException("List is empty");

            var first = First;
            var second = First;

            for (var i = 0; i < kth - 1; i++)
                second = second.Next ?? throw new ArgumentOutOfRangeException("kth is bigger than list size");

            while (second != Last)
            {
                first = first.Next;
                second = second.Next;
            }

            return first.Value;
        }

        public T FindKthNodeValue(int kth)
        {
            var current = First;
            for (int i = 0; i != kth; i++)
            {
                if (i == kth) return current.Value;
                current = current.Next;
            }

            return default;
        }

        public void FindMiddle(){
            // [1 => 2 => 3 => 4 => 5]

            var first = First;
            var second = First;
            var middle = 0d;
            var size = 0d;
            while (second != null)
            {
                size++;
                second = second.Next;
            }

            var isOdd = (size % 2) == 1;
            middle = Math.Ceiling(size/2);

            if (isOdd)
            {
                for (int i = 0; i < middle - 1; i++)
                    first = first.Next;
            }
            else
            {
                for (var i = 0; i < middle - 1; i++)
                    first = first.Next;
                var firstMid = first.Value;
                var secondMid = first.Next.Value;
                Console.WriteLine(firstMid+ ", " +secondMid);
                return;
            }

            Console.WriteLine(first.Value);
        }

        public bool HasLoop()
        {
            var slow = First;
            var fast = First;

            while (fast!=null)
            {
                slow = slow.Next;
                fast = fast.Next.Next;
                if (fast == slow) return true;
            }

            return false;
        }

        public IEnumerator<T> GetEnumerator()
        {
            var current = First;
            while (current != null)
            {
                yield return current.Value;

                current = current.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

    }

   
}
