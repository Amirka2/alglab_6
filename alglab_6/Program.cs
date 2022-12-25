// See https://aka.ms/new-console-template for more information
using alglab_6;

HashTable<string> ht = new HashTable<string>();
MyDictionary<string> at = new MyDictionary<string>();
FillTable(ht, 10000);
FillTable(at, 10000);







void FillTable(Table<string> table, int count)
{
    Item<string>[] items = Generator.GenerateItems(count);
    foreach (var item in items)
    {
        table.AddItem(item);
    }
}