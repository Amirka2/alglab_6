using System;
using System.Text;

namespace alglab_6
{
	public static class Generator
	{
		public static Item<string>[] GenerateItems(int count)
		{
			Item<string>[] items = new Item<string>[count];
			for (int i = 0; i < items.Length; i++)
			{
				string key = GenerateKey(i);
				items[i] = new Item<string>(key, key);
			}

			return items;
		}

		private static string GenerateKey(int index)
		{
			Random rnd = new Random();
			int x = rnd.Next(0, 10000);
			
			return (x.ToString() + index.ToString());
		}
	}
}

