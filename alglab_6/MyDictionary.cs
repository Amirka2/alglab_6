namespace alglab_6;

public class MyDictionary<U> : Table<U>
{
    private LinkedList<Item<U>>[] lst;

    public override bool Add(string key, U data)
    {
        return AddItem(new Item<U>(key, data));
    }

    private bool AddItem(Item<U> elem)
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

    public override bool Remove(Item<U> elem)
    {
        var index = GetIndexByHash(elem.GetHashCode());
        return this.lst[index].Remove(elem);
    }

    public override bool Contains(Item<U> elem)
    {
        foreach (var e in lst)
        {
            if (e.Contains(elem))
            {
                return true;
            }
        }

        return false;
    }
}