using System;
using System.Collections.Generic;
using System.Text;

namespace FirstFollowParser
{
	public class First
	{
		public String variable;
		public HashSet<String> setOfFirst = new HashSet<String>();

		public First(String variable, HashSet<String> setOfFirst)
		{
			variable = this.variable;
			setOfFirst = this.setOfFirst;
		}

		public First()
		{
			// TODO Auto-generated constructor stub
		}

		public void print()
		{

			Console.WriteLine(this.ToString());
		}

		public override string ToString()
		{
			return $"First( { variable } ) : [ { String.Join(", ", setOfFirst) } ]";
		}

		public String printInFile()
		{
			return this.ToString();
		}
	}
}
