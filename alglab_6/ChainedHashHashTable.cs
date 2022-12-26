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
    private bool Add(Item<U> elem)
    {
        CheckSize();
        var index = GetIndexByHash(elem.GetHashCode());
        foreach (var e in _lst)
        {
            if (e.Equals(elem))
            {
                return false;
            }
        }
        _lst[index].AddLast(elem);
        return true;
    }
    
    public override bool Remove(string key)
    {
        return Remove(new Item<string>(key, ""));
    }
    private bool Remove(Item<string> elem)
    {
        var index = GetIndexByHash(elem.GetHashCode());
        if (_lst[index] == null) return false;
        foreach (var el in _lst[index])
        {
            if (el.Equals(elem))  return _lst[index].Remove(el);
        }

        return false;
    }
    
    public override bool Contains(string key)
    {
        return Contains(new Item<string>(key, ""));
    }
    public bool Contains(Item<string> elem)
    {
        var index = GetIndexByHash(elem.GetHashCode());
        if (_lst[index] == null) return false;
        foreach (var el in _lst[index])
        {
            if (el.Equals(elem))  return true;
        }

        return false;
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
        
        return Math.Abs(sum % _lst.Length);    
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
            if (_lst[i] is null) continue;
            var index = GetIndexByHash(_lst[i].GetHashCode());

            lst[index] = _lst[i];
        }

        _lst = lst;
    }
}