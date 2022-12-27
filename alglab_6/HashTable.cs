namespace alglab_6;

public abstract class HashTable<U>
{
    protected readonly double _loadFactor = 0.75F;
    protected int _loaded = 0;

    public HashTable()
    {
    }
    public HashTable(int capacity)
    {
    }
    public HashTable(int capacity, float loadFactor)
    {
        _loadFactor = loadFactor;
    }

    public abstract bool Add(string key, U value);
    public abstract bool AddItem(Item<U> item);
    public abstract bool Remove(Item<U> item);

    public abstract bool Contains(Item<U> item);

    protected abstract void CheckSize();
    protected abstract void ResizeTable();
    
    protected abstract int GetIndexByHash(int hash, int size);
    protected abstract int GetIndexByHash(byte[] hash, int size);
}