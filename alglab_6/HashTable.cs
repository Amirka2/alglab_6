using System;
using System.Collections;
using System.Text;
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
            Items = new Item<U>[8];
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

        public void Add(string key, U data)
        {
            Add(new Item<U>(key, data));
            Console.WriteLine(data);
        }
        private int Add(Item<U> item)
        {
            double factor = (double)loaded / Items.Length;
            if (LoadFactor.CompareTo(factor) <= 0) ResizeTable();
            if (item.Value == null) throw new ArgumentNullException("item's value is null!");

            var index = GetIndexByHash(item.GetHashCode());

            for (int i = index; i < Items.Length; i++)
            { 
                if (Items[i] is not null)
                {
                    if (Items[i].Key == item.Key)
                    {
                        return -1;
                    }
                    continue;
                }
                Items[i] = item;
                loaded++;
                return index;
            } 

            return -1;
        }

        public bool Remove(string key)//? key or value
        {
            var i = 0;
            do {
                var index = CalculateHash(key);
                if (Items[index] == null || Items[i].Key != key) continue;
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
                if (Items[i] is null) continue;
                var index = GetIndexByHash(Items[i].GetHashCode());
                items[i] = Items[i];
            }

            Items = items;
        }

        private int GetIndexByHash(int hash)
        {
            return hash % Items.Length;
        }
        public static int CalculateHash(string key)
        {
            if (string.IsNullOrEmpty(key)) throw new ArgumentNullException();
            return key.Length;

            //switch (value)
            //{
            //    case string s:
            //        break;
            //    case int i:
            //        break;
            //    case byte b:
            //        break;
            //    case double d:
            //        break;
            //    default:
            //        tmp = value.ToString();
            //        break;
            //}
        }

    }
}

