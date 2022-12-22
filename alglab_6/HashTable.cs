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
        private bool Add(Item<U> item)
        {
            CheckSize();
            if (item.Value == null) throw new ArgumentNullException("item's value is null!");

            var index = GetIndexByHash(item.GetHashCode());

            for (int i = index; i < _items.Length; i++)
            { 
                if (_items[i] is not null)
                {
                    if (_items[i].Key == item.Key)
                    {
                        return false;
                    }
                    continue;
                }
                _items[i] = item;
                _loaded++;
                return true;
            } 

            return false;
        }

        public override bool Remove(Item<U> item)//? key or value
        {
            var i = 0;
            do {
                var index = GetIndexByHash(item.GetHashCode());
                if (_items[index] == null || _items[i].Key != item.Key) continue;
                _items[index] = null;
                _loaded--;
                return true;
            } while (i < _loaded);

            return false;
        }

        
            
    }
}

