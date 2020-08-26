using System;

namespace DataStructure.Data_Structure_1
{
    public class Array
    {
        private object[] _items;
        private int _lastIndex = 0;

        public int Length => _items.Length;


        public Array(int length)
        {
            _items = new object[length];
        }

        public void Insert(object item)
        {
            if(item==null) throw new ArgumentNullException();

            IncreaseSizeIfExceed();

            _items[_lastIndex] = item;
            _lastIndex++;
        }

        private void IncreaseSizeIfExceed()
        {
            if (Length == _lastIndex)
            {
                var array = new object[Length + 1];
                for (var i = 0; i < array.Length - 1; i++)
                {
                    array[i] = _items[i];
                }

                _items = array;
            }
        }

        public void RemoveAt(int index)
        {
            if(index < 0 || _lastIndex<=index)
                throw new IndexOutOfRangeException();

            for (var i = index; i < _lastIndex -1; i++)
            {
                _items[i] = _items[i + 1];
            }
            _lastIndex--;
        }

        public int Count()
        {
            return _lastIndex;
        }

        public object[] GetItems()
        {
            return _items;
        }

        public int IndexOf(object item)
        {
            var type = item.GetType();
            
            var i = 0;
            foreach (var itemInArray in _items)
            {
                if (Convert.ChangeType(item,type).ToString() == Convert.ChangeType(itemInArray,type).ToString()) return i;

                i++;
            }

            return -1;
        }
    }
}
