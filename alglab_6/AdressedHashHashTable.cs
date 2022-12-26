using System;
using System.Collections;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace alglab_6
{
    public class AdressedHashHashTable<U> : HashTable<U>
    {
        private Item<U>[] _items;
        private List<int> _clusterCounts = new List<int>();
        public int CollisionCount = 0;

        public AdressedHashHashTable()
        {
            _items = new Item<U>[10000];
        }
        public AdressedHashHashTable(int capacity) : base(capacity)
        {
            _items = new Item<U>[capacity];
        }
        public AdressedHashHashTable(int capacity, float loadFactor) : base(capacity, loadFactor)
        {
            _items = new Item<U>[capacity];
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

            var index = GetIndexByHash(item.GetHash(), _items.Length);
            if (_items[index] != null) CollisionCount++; //подсчет коллизий
            for (int i = index; i < _items.Length; i++)
            {
                if (i == _items.Length - 1 && _items[i] != null)
                {
                    i = 0;
                    continue;
                }
                _clusterCounts.Add(0);
                if (item.Equals(_items[i]))
                {
                    return false;
                }

                if (_items[i] == null) //если проверили все значения которые могли сместиться(конец кластера) . заменить на проверку удаленных хэшей
                {
                    _items[i] = new Item<U>(item.Key, item.Value, i);
                    _loaded++;
                    return true;                
                }

                _clusterCounts[_loaded]++; //инкремент счетчика кластеров
            }

            return false;
        }

        public override bool Remove(string key)
        {
            return Remove(new Item<string>(key, ""));
        }
        private bool Remove(Item<string> item)
        {
            var index = GetIndexByHash(item.GetHash(), _items.Length);

            do {
                if (item.Equals(_items[index]))
                {
                    _items[index] = null;
                    _loaded--;
                    return true;
                }

                index++;
            } while (index < _items.Length);

            return false;
        }

        public override bool Contains(string key)
        {
            return Contains(new Item<string>(key, ""));
        }
        private bool Contains(Item<string> item)
        {
            var index = GetIndexByHash(item.GetHash(), _items.Length);

            do {
                if (item.Equals(_items[index]))
                {
                    return true;
                }

                index++;
            } while (index < _items.Length);

            return false;
        }

        protected override void ResizeTable()
        {
            Item<U>[] items = new Item<U>[_items.Length * 2];
            for (int i = 0; i < _items.Length; i++)
            {
                if (_items[i] == null) continue;
                var index = GetIndexByHash(_items[i].GetHash(), _items.Length);
                for (int j = index; j < _items.Length; j++)
                {
                    if (items[j] != null)
                    {
                        continue;
                    }
                    else
                    {
                        items[j] = new Item<U>(_items[i].Key, _items[i].Value, j);
                        break;
                    }
                }
            }

            _items = items;
        }

        protected override void CheckSize()
        {
            double factor = (double)_loaded / _items.Length;
            if (_loadFactor.CompareTo(factor) <= 0) ResizeTable();
        }

        protected override int GetIndexByHash(byte[] hash, int size)
        {
            int sum = 1;
            for (int i = 0; i < hash.Length; i++)
            {
                var convertedHash = BitConverter.ToInt32(hash);
                sum += convertedHash;
            }
        
            return Math.Abs(sum % size);
        }

        protected override int GetIndexByHash(int hash, int size)
        {
            return Math.Abs(hash % size);
        }

        public int GetLargestClusterLength()
        {
            return GetMax(_clusterCounts.ToArray());
        }

        private static int GetMax(int[] arr)
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

