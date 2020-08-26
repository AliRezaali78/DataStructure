using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataStructure.Data_Structure_1
{
    public static class StackHelper
    {
        public static void ReverseString(string value)
        {
            var stack = new Stack<char>();
            foreach (var c in value)
            {
                stack.Push(c);
            }

            var stringBuilder = new StringBuilder();
            while (stack.Count !=0)
            {
                stringBuilder.Append(stack.Pop());
            }
            
            Console.WriteLine(stringBuilder.ToString());
        }

        public static void Compile(string syntax)
        {
            if (!CheckSyntax(syntax)) throw new Exception("Syntax error");

            Console.WriteLine("Successfully compiled.");
        }

        private static bool CheckSyntax(string syntax)
        {
            var stack = new Stack<char>();
            var allowedOpeningSyntaxes = new []{'(','[','<','{'};
            var allowedClosingSyntaxes = new []{')',']','>','}'};
            foreach (var c in syntax.Where(c => allowedClosingSyntaxes.Contains(c) || allowedOpeningSyntaxes.Contains(c)))
            {
                if (allowedOpeningSyntaxes.Contains(c))
                {
                    stack.Push(c);
                    continue;
                }

                if (stack.Count == 0) return false;
                var openingSyntax = stack.Pop();
                if (!CheckIfMatch(openingSyntax, c)) return false;
            }

            return stack.Count == 0;
        }

        private static bool CheckIfMatch(char openingSyntax, char closingSyntax)
        {
            var matchingList = new Dictionary<char, char>()
            {
                ['('] = ')',
                ['<'] = '>',
                ['['] = ']',
                ['{'] = '}',
            };

            return matchingList[openingSyntax] == closingSyntax;
        }
    }

    public class ArrayStack<T>
    {
        private T[] _array;
        private int _size = 0;
        private int _length = 1;

        public ArrayStack()
        {
            _array = new T[_length];
        }

        public bool IsEmpty()
        {
            return _size == 0;
        }

        public void Push(T item)
        {
            if (_length == _size) IncreaseSize();
            _array[_size++] = item;
        }

        private void IncreaseSize()
        {
            _length *= 2;
            var newArray = new T[_length];

            for (var i = 0; i < _size; i++)
                newArray[i] = _array[i];

            _array = newArray;
        }

        public T Pull()
        {
            if (IsEmpty()) throw new StackIsEmptyException();
            var lastItem = _array[_size - 1];
            _array[_size - 1] = default;
            _size--;

            return lastItem;
        }

        public T Peek()
        {
            if(IsEmpty()) throw  new StackIsEmptyException();
            return _array[_size-1];
        }
    }

    public class LinkedStack<T>
    {
        private readonly LinkedList<T> _linkedList = new LinkedList<T>();

        public bool IsEmpty()
        {
            return _linkedList.Size == 0;
        }

        public void Push(T item)
        {
            _linkedList.AddFirst(item);
        }

        public T Pull()
        {
            if (IsEmpty()) 
                throw new StackIsEmptyException();

            var topItem = _linkedList.First.Value;
            _linkedList.DeleteFirst();

            return topItem;
        }

        public T Peek()
        {
            if(IsEmpty()) throw new StackIsEmptyException();
            return _linkedList.First.Value;
        }
    }

    public class StackIsEmptyException : Exception
    {
        public StackIsEmptyException(string exception="Stack is empty"): base(exception)
        {
        }
    }
}
