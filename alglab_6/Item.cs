using System;
using System.Security.Cryptography;
using System.Text;

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
            if (this.GetHash() == item?.GetHash()) return true;
            if (item != null)
            {
	            if (item.GetHash().SequenceEqual(this.GetHash())) return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
	        return base.GetHashCode();
        }

        public byte[] GetHash()
        {
	        byte[] res;
	        // Creates an instance of the default implementation of the MD5 hash algorithm.
	        using (var md5Hash = MD5.Create())
	        {
		        // Byte array representation of source string
		        var sourceBytes = Encoding.UTF8.GetBytes(Key);

		        // Generate hash value(Byte Array) for input data
		        var hashBytes = md5Hash.ComputeHash(sourceBytes);
		        res = hashBytes;
	        }

	        return res;
        }
    }
}

