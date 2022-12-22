using System;
using System.Collections;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace alglab_6
{
    public class HashTable<U> : Table<U>
    {
        public HashTable() : base()
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
            double factor = (double)_loaded / _items.Length;
            if (_loadFactor.CompareTo(factor) <= 0) ResizeTable();
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

        public override bool Remove(string key)//? key or value
        {
            var i = 0;
            do {
                var index = CalculateHash(key);
                if (_items[index] == null || _items[i].Key != key) continue;
                _items[index] = null;
                _loaded--;
                return true;
            } while (i < _loaded);

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

