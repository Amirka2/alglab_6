// See https://aka.ms/new-console-template for more information
using alglab_6;

//CreateDataForChained();
//CreateDataForAddressed();
ShowWork();


void FillTable(HashTable<string> table, int count)
{
    Item<string>[] items = Generator.GenerateItems(count);
    foreach (var item in items)
    {
        table.AddItem(item);
    }
}

void CreateDataForAddressed()
{
    List<int> clusters = new List<int>();
    List<int> collissions = new List<int>();
    for (int i = 0; i < 10; i++)
    {
        AdressedHashHashTable<string> ht = new AdressedHashHashTable<string>();
        FillTable(ht, 3000);

        int cluster = ht.GetLargestClusterLength();
        int collission = ht.CollisionCount;
        clusters.Add(cluster);
        collissions.Add(collission);
    }

    int col = (int)collissions.Average();
    int cl = (int)clusters.Average();
    using var sw = new StreamWriter("addressed-data.txt", false);
    sw.WriteLine($"{col};{cl}");
}

void CreateDataForChained()
{
    int count = 100000;
    ChainedHashTable<string> ct = new ChainedHashTable<string>();
    FillTable(ct, count);
    var length = ct.GetCapacity();
    List<int> lengths = new List<int>();
    for (int i = 0; i < length; i++)
    {
        lengths.Add(ct.GetCapacity(i));
    }

    using StreamWriter sw = new StreamWriter("chained-data.txt", false);
    for (int i = 0; i < lengths.Count; i++)
    {
        sw.WriteLine($"{i};{lengths[i]}");
    }
    sw.WriteLine("максимальная длина цепочки: " + ct.GetMaxChainLength());
}

void ShowWork(int opt = 0)
{
    HashTable<int> at;
    if (opt == 0)
    {
        at = new AdressedHashHashTable<int>();
        Console.WriteLine("Addressed Hashtable is being used");
    }
    else
    {
        at = new ChainedHashTable<int>();
        Console.WriteLine("Chained Hashtable is being used");
    }

    Console.WriteLine();
    Console.WriteLine("added 0: " + at.Add("0", 0));
    Console.WriteLine("added 1: " + at.Add("1", 0));
    Console.WriteLine("added 2: " + at.Add("2", 0));
    Console.WriteLine("added 3: " + at.Add("3", 0));
    Console.WriteLine("added 4: " + at.Add("4", 0));
    Console.WriteLine("added 0: " + at.Add("0", 0));
    Console.WriteLine("added 3: " + at.Add("3", 0));

    Console.WriteLine();
    Console.WriteLine(at.Contains(new Item<int>("0", 0)) ? "contains 0" : "doesnt contain 0");
    Console.WriteLine(at.Contains(new Item<int>("1", 0)) ? "contains 1" : "doesnt contain 1");
    Console.WriteLine(at.Contains(new Item<int>("2", 0)) ? "contains 2" : "doesnt contain 2");
    Console.WriteLine(at.Contains(new Item<int>("3", 0)) ? "contains 3" : "doesnt contain 3");
    Console.WriteLine(at.Contains(new Item<int>("5", 0)) ? "contains 5" : "doesnt contain 5");

    Console.WriteLine();
    Console.WriteLine("removed 0: " + at.Remove(new Item<int>("0", 0)));
    Console.WriteLine("removed 1: " + at.Remove(new Item<int>("1", 0)));
    Console.WriteLine("removed 2: " + at.Remove(new Item<int>("2", 0)));
    Console.WriteLine("removed 3: " + at.Remove(new Item<int>("3", 0)));

    Console.WriteLine();
    Console.WriteLine(at.Contains(new Item<int>("0", 0)) ? "contains 0" : "doesnt contain 0");
    Console.WriteLine(at.Contains(new Item<int>("1", 0)) ? "contains 1" : "doesnt contain 1");
    Console.WriteLine(at.Contains(new Item<int>("2", 0)) ? "contains 2" : "doesnt contain 2");
    Console.WriteLine(at.Contains(new Item<int>("3", 0)) ? "contains 3" : "doesnt contain 3");
    Console.WriteLine(at.Contains(new Item<int>("5", 0)) ? "contains 5" : "doesnt contain 5");
}