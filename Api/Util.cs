using System;

namespace Api2
{
	public static class Util
	{	 
		public static decimal TruncateDecimal(decimal value, int precision)
		{
			decimal step = (decimal)Math.Pow(10, precision);
			decimal tmp = Math.Truncate(step * value);
			return tmp / step;
		}

		public class Git
		{
			public string Repositorio { get; set; }
			public string Url { get; set; }
		}


        
    }
}
