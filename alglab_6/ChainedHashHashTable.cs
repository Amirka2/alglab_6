﻿namespace alglab_6;

public class ChainedHashHashTable<U> : HashTable<U>
{
    private LinkedList<Item<U>>[] lst;
    
    public ChainedHashHashTable() {}

    public ChainedHashHashTable(int capacity) : base(capacity)
    {
    }

    public ChainedHashHashTable(int capacity, float loadFactor) :base(capacity, loadFactor)
    {
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
        foreach (var e in lst)
        {
            if (e.Equals(elem))
            {
                return false;
            }
        }
        lst[index].AddLast(elem);
        return true;
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
        return this.lst[index].Remove(elem);
    }
    public bool Contains(Item<U> elem)
    {
        var index = GetIndexByHash(elem.GetHashCode());

        foreach (var e in lst[index])
        {
            if (elem.Equals(e)) return true;
        }
        return false;
    }
    public override bool ContainsItem(Item<U> item)
    {
        return Contains(item);
    }
    public override bool Contains(string key, U value)
    {
        return Contains(new Item<U>(key, value));
    }
}