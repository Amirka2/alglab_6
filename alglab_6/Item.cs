using System;
namespace alglab_6
{
	public class Item<U>
	{
		public string Key { get; private set; }
		public U Value { get; private set; }

        public Item(U value)
		{
			Key = Generator<U>.GenerateKey(value);
			Value = value;
		}
    }
}

