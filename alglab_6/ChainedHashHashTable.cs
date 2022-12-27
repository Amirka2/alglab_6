namespace alglab_6;

public class ChainedHashTable<U> : HashTable<U>
{
    private LinkedList<Item<U>>[] _lst;
    private int load = 200; // для корректного расширения таблицы допилить 

    public ChainedHashTable()
    {
        _lst = new LinkedList<Item<U>>[1000];
    }

    public ChainedHashTable(int capacity) : base(capacity)
    {
        _lst = new LinkedList<Item<U>>[capacity];
    }

    public ChainedHashTable(int capacity, float loadFactor) :base(capacity, loadFactor)
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
        var index = GetIndexByHash(elem.GetHash(), _lst.Length);
        if (_lst[index] == null)
        {
            _lst[index] = new LinkedList<Item<U>>();
            _lst[index].AddLast(elem);
            return true;
        } 
        if (_lst[index] != null)
        {
            foreach (var el in _lst[index])
            {
                if (el.Equals(elem)) return false;
            }
            _lst[index].AddLast(elem);
        }

        return true;

    }
    
    public override bool Remove(Item<U> elem)
    {
        var index = GetIndexByHash(elem.GetHash(), _lst.Length);
        if (_lst[index] == null) return false;
        foreach (var el in _lst[index])
        {
            if (el.Equals(elem))  return _lst[index].Remove(el);
        }

        return false;
    }
    
    public override bool Contains(Item<U> elem)
    {
        var index = GetIndexByHash(elem.GetHash(), _lst.Length);
        if (_lst[index] == null) return false;
        foreach (var el in _lst[index])
        {
            if (el.Equals(elem))  return true;
        }

        return false;
    }
    protected override int GetIndexByHash(int hash, int size)
    {
        return Math.Abs(hash % size);
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

    protected override void CheckSize()
    {
        float factor = (float)_loaded / _lst.Length;
        if (_loadFactor <= factor) ResizeTable();
    }

    protected override void ResizeTable() //подредачить
    {
        LinkedList<Item<U>>[] lst = new LinkedList<Item<U>>[_lst.Length * 2];
        for (int i = 0; i < _lst.Length; i++)
        {
            if (_lst[i] is null) continue;
            var index = GetIndexByHash(_lst[i].Last().GetHash(), lst.Length);

            if (lst[index] != null)
            {
                lst[index] = _lst[i];
            }
            if (lst[index] == null)
            {
                lst[index] = new LinkedList<Item<U>>();
                lst[index] = _lst[i];
            }
        }

        _lst = lst;
    }

    public int GetCapacity()
    {
        return _lst.Length;
    }

    public int GetCapacity(int dimension)
    {
        if (_lst[dimension] == null) return 0;
        return _lst[dimension].Count;
    }

    public int GetMaxChainLength()
    {
        int max;
        for (int i = 0;; i++)
        {
            if (_lst[i] != null)
            {
                max = _lst[i].Count;
                break;
            }
        }
        foreach (var chain in _lst)
        {
            if (chain == null) continue;

            if (chain.Count > max) max = chain.Count;
        }

        return max;
    }
}