using System;
using System.Text;

namespace alglab_6
{
	public static class Generator<U>
	{
		public static string GenerateKey(U value)
		{
			if (value is null) throw new ArgumentNullException();
            int length = 0;

            if (value is string)
            {
                length = value.ToString().Length;
            } else if (value is int)
            {
                length = Int32.Parse(value.ToString());
            } else if (value is float)
            {
                length = Int32.Parse(value.ToString());
            }

            var rnd = new Random();
            var sb = new StringBuilder();
            for (var i = 0; i < length; i++)
                sb.Append((char)(rnd.Next(0, 26) + 'a'));
            return sb.ToString();
        }
	}
}

