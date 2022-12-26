using System;
using System.ComponentModel;
using System.Security.Cryptography;
using System.Text;

namespace alglab_6
{
	public class Item<U>
	{
		public string Key { get; private set; }
		public U Value { get; private set; }
		private const double GOLDEN_RATIO = 1.61803398;
		public int Index { get; set; }
		private const double C1 = 1;
		private const double C2 = 2;

		public Item(string key, U value)
        {
	        if (value == null) throw new ArgumentNullException();
			Key = key;
			Value = value;
		}

        public Item(string key, U value, int index)
        {
	        if (value == null) throw new ArgumentNullException();
	        Key = key;
	        Value = value;
	        Index = index;
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
	        // var x1 = HashF(); //двойное хэширование
	        // var x2 = GetOwnHash(0);
	        // for (int i = 0; i < Math.Min(x1.Length, x2.Length); i++)
	        // {
		       //  x1[i] += x2[i];
	        // }
	        //
	        // return x1;
	        
	        // return HashF(); //линейное

	        var x1 = BitConverter.GetBytes(Index * C1 + Math.Pow(Index, 2) * C2); //квадратичное
	        var x2 = HashF();
	        for (int i = 0; i < Math.Min(x1.Length, x2.Length); i++)
	        {
		        x1[i] += x2[i];
	        }

	        return x1;
        }

        private byte[] HashF()
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

        private byte[] GetOwnHash(int option)
        {
	        switch (option)
	        {
		        case 0:
			        return BitConverter.GetBytes(Math.Abs(10000 * (Key.GetHashCode() * GOLDEN_RATIO % 1))); //сделать переменную размер таблмцы для скринов
			        break;
		        case 1:
			        return BitConverter.GetBytes(Math.Abs(Key.GetHashCode() % 10000));
			        break;
		        default:
			        break;
	        }

	        return null;
        }
    }
}

