using System;
namespace alglab_6
{
	public class Item<U>
	{
		public string Key { get; private set; }
		public U Value { get; private set; }

        public Item(string key, U value)
		{
			Key = key;
			Value = value;
		}
        public override bool Equals(object? obj)
        {
            var item = (Item<U>)obj;
            if (this == item) return true;
            if (this.GetHashCode() == item?.GetHashCode()) return true;
            return false;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}

