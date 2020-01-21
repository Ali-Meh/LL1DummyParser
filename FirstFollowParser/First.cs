using System;
using System.Collections.Generic;
using System.Text;

namespace FirstFollowParser
{
	public class First
	{
		public string variable;
		public HashSet<string> setOfFirst = new HashSet<string>();

		public First(string variable, HashSet<string> setOfFirst)
		{
			this.variable = variable;
			this.setOfFirst = setOfFirst;
		}
		public First(string variable)
		{
			this.variable=variable ;
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
			return $"First( { variable } ) : [ { string.Join(", ", setOfFirst) } ]";
		}

		public string printInFile()
		{
			return this.ToString();
		}
	}
}
