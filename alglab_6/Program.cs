// See https://aka.ms/new-console-template for more information
using alglab_6;

AdressedHashHashTable<string> ht = new AdressedHashHashTable<string>();
//MyDictionary<string> at = new MyDictionary<string>();
FillTable(ht, 100);
    //FillTable(at, 10000);

int cluster = ht.GetLargestClusterLength();
Console.WriteLine(cluster);




void FillTable(HashTable<string> table, int count)
{
    Item<string>[] items = Generator.GenerateItems(count);
    foreach (var item in items)
    {
        table.AddItem(item);
    }
}