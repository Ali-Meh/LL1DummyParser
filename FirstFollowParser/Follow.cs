using System;
using System.Collections.Generic;
using System.Text;

namespace FirstFollowParser
{
	public class Follow
	{
		public string variable;
		public HashSet<string> setOfFollow = new HashSet<string>();

		public Follow(string variable, HashSet<string> setOfFollow)
		{
			this.variable = variable;
			this.setOfFollow = setOfFollow;
		}
		public Follow(string variable)
		{
			this.variable = variable;
		}

		public Follow()
		{
			// TODO Auto-generated constructor stub
		}

		public void print()
		{

			Console.WriteLine(this.ToString());
		}

		public override string ToString()
		{
			return $"Follow( { variable } ) : [ { string.Join(", ", setOfFollow) } ]";
		}

		public string printInFile()
		{
			return this.ToString();
		}
	}
}
