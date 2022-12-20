using System;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace alglab_6
{
    public class HashTable<U>
    {
        private Item<U>[] Items;
        private readonly double LoadFactor = 0.75F;
        private int loaded = 0;

        public HashTable()
        {
            Items = new Item<U>[255];
        }
        public HashTable(int capacity)
        {
            Items = new Item<U>[capacity];
        }
        public HashTable(int capacity, float loadFactor)
        {
            LoadFactor = loadFactor;
            Items = new Item<U>[capacity];
        }

        public void Add(U data)
        {
            Add(new Item<U>(data));
        }
        private int Add(Item<U> item)
        {
            double factor = (double)loaded / Items.Length;
            if (LoadFactor.CompareTo(factor) <= 0) ResizeTable();
            if (item.Value == null) throw new ArgumentNullException("item's value is null!");

            var i = 0;
            do {
                var index = CalculateHash(item.Key, i++);
                if (Items[index] is not null) continue;
                Items[index] = item;
                loaded++;
                return index;
            } while (i < loaded);

            return -1;
        }

        public bool Remove(Item<U> item)
        {
            var i = 0;
            do {
                var index = CalculateHash(item.Key, i++);
                if (Items[index] == null || Items[i].Key != item.Key) continue;
                Items[index] = null;
                loaded--;
                return true;
            } while (i < loaded);

            return false;
        }
        //public bool Contains(U value)
        //{
        //    // Item<U> item = new Item<U>(value);
        //    var i = 0;
        //    do {
        //        var index = CalculateHash(item.Key, i++);
        //        if (Items[index] == null || Items[index].Key != item.Key) continue;
        //        return true;
        //    } while (i < loaded);

        //    return false;
        //}
        //public bool ContainsValue(T value)
        //{
        //          var hash = GetHashCode(key);
        //          if (Items[hash] == null) { return false; }      //если нет элемента по хэшу 
        //          else                                            //если есть элемент смотрим все элементы по хэшу
        //          {
        //              var current = Items[hash];
        //              while (current != null)
        //              {
        //                  if (current.Key.Equals(key)) { return true; }
        //                  current = current.Next;
        //              }
        //          }
        //          return false;
        //      }
        private void ResizeTable()
        {
            Item<U>[] items = new Item<U>[Items.Length * 2];
            for (int i = 0; i < Items.Length; i++)
            {
                
            }
            Items = new Item<U>[Items.Length * 2];
            foreach (var item in items)
            {
                this.Add(item);
            }
        }
        private static int CalculateHash(string key, int i)
        {
            return key[0] - 'a' + i;
        }
    }
}

