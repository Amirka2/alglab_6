// See https://aka.ms/new-console-template for more information
using alglab_6;

CreateDataForChained();


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
    ChainedHashHashTable<string> ct = new ChainedHashHashTable<string>();
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
    
