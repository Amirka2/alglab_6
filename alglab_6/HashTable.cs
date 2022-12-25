using System;
using System.Collections;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace alglab_6
{
    public class HashTable<U> : Table<U>
    {
        public HashTable()
        {
        }
        public HashTable(int capacity) : base(capacity)
        {
        }
        public HashTable(int capacity, int loadFactor) : base(capacity, loadFactor)
        {
        }
        
        public override bool Add(string key, U data)
        {
            return Add(new Item<U>(key, data));
        }
        public override bool AddItem(Item<U> item)
        {
            return Add(item);
        }
        private bool Add(Item<U> item)
        {
            CheckSize();
            if (item.Value == null) throw new ArgumentNullException("item's value is null!");

            var index = GetIndexByHash(item.GetHashCode());

            for (int i = index; i < _items.Length; i++)
            {
                if (item.Equals(_items[i]))
                {
                    return false;
                }

                if (_items[i] == null) //если проверили все значения которые могли сместиться . заменить на проверку удаленных хэшей
                {
                    _items[index] = item;
                    _loaded++;
                    return true;                
                }
            }

            return false;
        }

        public override bool Remove(string key, U value)
        {
            return Remove(new Item<U>(key, value));
        }
        public override bool RemoveItem(Item<U> item)
        {
            return Remove(item);
        }
        private bool Remove(Item<U> item)//? key or value
        {
            var index = GetIndexByHash(item.GetHashCode());

            do {
                if (item.Equals(_items[index]))
                {
                    _items[index] = null;
                    _loaded--;
                    return true;
                }

                index++;
            } while (index < _loaded);

            return false;
        }

        public override bool Contains(string key, U value)
        {
            return Contains(new Item<U>(key, value));
        }
        public override bool ContainsItem(Item<U> item)
        {
            return Contains(item);
        }
        private bool Contains(Item<U> item)
        {
            var index = GetIndexByHash(item.GetHashCode());

            do {
                if (item.Equals(_items[index]))
                {
                    return true;
                }

                index++;
            } while (index < _loaded);

            return false;
        }

        public int GetLargestClusterLength()
        {
            int[] counts = new int[_items.Length];
            
            for (int i = 0; i < counts.Length; i++)
            {
                int j = i;
                while (_items[j] == null)
                {
                    j++;
                }
                Item<U> firstItem = _items[j];
                
                var index = GetIndexByHash(firstItem.GetHashCode());

                while (_items[index].Equals(_items[index + 1]))
                {
                    counts[i]++;
                    i++;
                }
            }

            int maxClusterLength = GetMax(counts);
            return maxClusterLength;
        }

        private int GetMax(int[] arr)
        {
            int max = arr[0];
            for (int i = 0; i < arr.Length; i++)
            {
                if (max < arr[i]) max = arr[i];
            }

            return max;
        }
    }
}

