namespace alglab_6;

public class ChainedHashHashTable<U> : HashTable<U>
{
    private LinkedList<Item<U>>[] _lst;

    public ChainedHashHashTable()
    {
        _lst = new LinkedList<Item<U>>[8];
    }

    public ChainedHashHashTable(int capacity) : base(capacity)
    {
        _lst = new LinkedList<Item<U>>[capacity];
    }

    public ChainedHashHashTable(int capacity, float loadFactor) :base(capacity, loadFactor)
    {
        _lst = new LinkedList<Item<U>>[capacity];
    }

    public override bool Add(string key, U data)
    {
        return Add(new Item<U>(key, data));
    }
    public override bool AddItem(Item<U> item)
    {
        return Add(item);
    }
    private bool Add(Item<U> elem) //ПЕРЕДЕЛАТЬ
    {
        CheckSize();
        if (elem.Value == null) throw new ArgumentNullException("item's value is null!");

        var index = GetIndexByHash(elem.GetHash());

        for (int i = index; i < _lst.Length; i++)
        {
            if (i == _lst.Length - 1 && _lst[i] != null)
            {
                i = 0;
                continue;
            }
            if (elem.Equals(_lst[i]))
            {
                return false;
            }

            if (_lst[i] == null) //если проверили все значения которые могли сместиться(конец кластера) . заменить на проверку удаленных хэшей
            {
                _lst.SetValue(elem, i);
                var castedElem = (Item<U>)_lst.GetValue(i);
                Console.WriteLine( castedElem.Key+ ": el, " + i + ": index");
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
    private bool Remove(Item<U> elem)
    {
        var index = GetIndexByHash(elem.GetHashCode());
        return _lst[index].Remove(elem);
    }
    public bool Contains(Item<U> elem)
    {
        var index = GetIndexByHash(elem.GetHashCode());

        do
        {
            if (elem.Equals(_lst[index]))
            {
                return true;
            }

            index++;
        } while (index < _lst.Length);

        return false;
    }
    public override bool ContainsItem(Item<U> item)
    {
        return Contains(item);
    }
    protected override int GetIndexByHash(int hash)
    {
        return Math.Abs(hash % _lst.Length);
    }

    protected override int GetIndexByHash(byte[] hash)
    {
        int sum = 1;
        for (int i = 0; i < hash.Length; i++)
        {
            var convertedHash = BitConverter.ToInt32(hash);
            sum += convertedHash;
        }
        
        return Math.Abs(sum % _lst.Length) - 1;
    }

    public override bool Contains(string key, U value)
    {
        return Contains(new Item<U>(key, value));
    }

    protected override void CheckSize()
    {
        float factor = (float)_loaded / _lst.Length;
        if (_loadFactor <= factor) ResizeTable();
    }

    protected override void ResizeTable()
    {
        LinkedList<Item<U>>[] lst = new LinkedList<Item<U>>[_lst.Length * 2];
        for (int i = 0; i < _lst.Length; i++)
        {
            if (_lst[i] == null) continue;
            var index = GetIndexByHash(_lst[i].GetHashCode());
            for (int j = index; j < _lst.Length; j++)
            {
                if (lst[j] != null)
                {
                    continue;
                }
                else
                {
                    lst[i] = _lst[j];
                }
            }
        }

        _lst = lst;
    }
}