using System;
using System.Globalization;
using System.Linq;

namespace DataStructure.Data_Structure_1
{
    public class HashTable<TKey,TValue> where TKey : IConvertible
    {
        private int _length;
        private readonly LinkedList<KeyValuePair<TKey, TValue>>[] list;
        public int Size { get; set; }

        public HashTable(int length=5)
        {
            _length = length;
            list = new LinkedList<KeyValuePair<TKey,TValue>>[_length];
        }

        public void Put(TKey key, TValue value)
        {
            var index = Hash(key);
            var bucket =list[index] ??= new LinkedList<KeyValuePair<TKey, TValue>>();

            var keyValuePair = bucket.TryFindKey(key);
            if (keyValuePair != null)
            {
                keyValuePair.Value = value;
                return;
            }

            list[index].AddLast(new KeyValuePair<TKey, TValue>(key,value));
            Size++;
        }

        public TValue Get(TKey key)
        {
            if (Size == 0) throw new ArgumentNullException("Empty List");

            var index = Hash(key);
            var bucket = list[index];

            if (bucket == null)
                return default;

            var keyValuePair = bucket.TryFindKey(key);

            return keyValuePair==null ? default : keyValuePair.Value;
        }

        public void Remove(TKey key)
        {
            var index = Hash(key);
            var bucket = list[index];

            if(bucket==null)
                throw new ArgumentNullException("Empty Bucket");

            bucket.Remove(key);
            Size--;
        }

        private int Hash(TKey key)
        {
            int index = 0;
            if (key is int)
            {
                var converted = key.ToInt32(new NumberFormatInfo());
                converted = Math.Abs(converted);
                index = converted % _length;
            }

            return index;
        }
        
    }
    public static class LinkedListExtensions
    {
        public static KeyValuePair<TKey, TValue> TryFindKey<TKey,TValue>(this LinkedList<KeyValuePair<TKey, TValue>> bucket,TKey key)
        {
            return bucket.SingleOrDefault(keyValuePair => keyValuePair.Key.Equals(key));
        }

        public static void Remove<TKey, TValue>(this LinkedList<KeyValuePair<TKey, TValue>> bucket, TKey key)
        {
            var previous = bucket.First;
            var current = previous.Next;

            if (bucket.First.Value.Key.Equals(key))
            {
                bucket.DeleteFirst();
                return;
            }

            if (bucket.Last.Value.Key.Equals(key))
            {
                bucket.DeleteLast();
                return;
            }

            while (current != null)
            {
                if (current.Value.Key.Equals(key))
                {
                    previous.Next = current.Next;
                    current.Next = null;
                    break;
                }

                previous = previous.Next;
                current = current.Next;
            }

        }
    }
    public class KeyValuePair<TKey,TValue>
    {
        public TKey Key { get;  }

        public TValue Value { get; set; }

        public KeyValuePair(TKey key, TValue value)
        {
            Value = value;
            Key = key;
        }
    }
}
