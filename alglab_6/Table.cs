namespace alglab_6;

public abstract class Table<U>
{
    protected Item<U>[] _items;
    protected readonly double _loadFactor = 0.75F;
    protected int _loaded = 0;

    public Table()
    {
        _items = new Item<U>[8];
    }
    public Table(int capacity)
    {
        _items = new Item<U>[capacity];
    }
    public Table(int capacity, float loadFactor)
    {
        _loadFactor = loadFactor;
        _items = new Item<U>[capacity];
    }

    public abstract bool Add(string key, U data);
    public abstract bool Remove(Item<U> item);
    
    protected virtual void ResizeTable()
    {
        Item<U>[] items = new Item<U>[_items.Length * 2];
        for (int i = 0; i < _items.Length; i++)
        {
            if (_items[i] is null) continue;
            var index = GetIndexByHash(_items[i].GetHashCode());
            items[i] = _items[i];
        }

        _items = items;
    }
    protected int GetIndexByHash(int hash)
    {
        return Math.Abs(hash % _items.Length);
    }

    protected void CheckSize()
    {
        double factor = (double)_loaded / _items.Length;
        if (_loadFactor.CompareTo(factor) <= 0) ResizeTable();
    }
}