using System;
namespace alglab_6
{
	public class Item<U>
	{
		public string Key { get; private set; }
		public U Value { get; private set; }

        public Item(string key, U value)
        {
	        if (value == null) throw new ArgumentNullException();
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
	        switch (Value)
	        {
		        case string s:
			        return Key.GetHashCode() + Value.ToString().GetHashCode();
			        break;
		        case int i:
			        return Key.GetHashCode() + Convert.ToInt32(Value).GetHashCode();
			        break;
		        case byte b:
			        return Key.GetHashCode() + Convert.ToByte(Value).GetHashCode();
			        break;
		        case double d:
			        return Key.GetHashCode() + Convert.ToDouble(Value).GetHashCode();
			        break;
		        case char a:
			        return Key.GetHashCode() + Convert.ToChar(Value).GetHashCode();
			        break;
		        default:
			        break;
	        }
            return String.GetHashCode(Key);
        }
    }
}

