using System;
using System.Text;

namespace alglab_6
{
	public static class Generator<U>
	{
		public static string GenerateHash(U value)
		{
            var tmp = "";
			if (value is null) throw new ArgumentNullException();
            int length = 0;

            switch (value){ 
                case string s:
                    break;
                case int i:
                    break;
                case byte b:
                    break;
                case double d:
                    break;
                default:
                    tmp = value.ToString();
                    break;
            }
            


            var rnd = new Random();
            var sb = new StringBuilder();
            for (var i = 0; i < length; i++)
                sb.Append((char)(rnd.Next(0, 26) + 'a'));
            return sb.ToString();
        }
	}
}

