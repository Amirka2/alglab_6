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
    public abstract bool Remove(string key, U value);
    public abstract bool RemoveItem(Item<U> item);

    public abstract bool Contains(string key, U value);
    public abstract bool ContainsItem(Item<U> item);


    protected abstract void ResizeTable();
    protected abstract int GetIndexByHash(int hash);

    protected abstract int GetIndexByHash(byte[] hash);

    protected abstract void CheckSize();
}