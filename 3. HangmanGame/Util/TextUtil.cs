using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Util
{
	public class TextUtil
	{
		public static bool isAplpha(char c)
		{
			return (c >= 'A' && c <= 'Z' || c >= 'a' && c <= 'z');
		}
	}
}
