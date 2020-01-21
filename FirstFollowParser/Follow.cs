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
			variable = this.variable;
			setOfFollow = this.setOfFollow;
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
