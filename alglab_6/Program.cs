// See https://aka.ms/new-console-template for more information
using alglab_6;

AdressedHashHashTable<string> ht = new AdressedHashHashTable<string>();
ChainedHashHashTable<string> ct = new ChainedHashHashTable<string>();
//MyDictionary<string> at = new MyDictionary<string>();
//FillTable(ht, 100);
    //FillTable(at, 10000);
FillTable(ct, 100);

int cluster = ht.GetLargestClusterLength();
Console.WriteLine(cluster);




void FillTable(HashTable<string> table, int count)
{
    Item<string>[] items = Generator.GenerateItems(count);
    int c = 0;
    foreach (var item in items)
    {
        if (table.AddItem(item))
        {
            Console.WriteLine(item.Key + " / " + c);
            c++;
        }
    }

    // foreach (var item in items)
    // {
    //     Console.WriteLine(table.Contains(item.Key, item.Value));
    // }
}