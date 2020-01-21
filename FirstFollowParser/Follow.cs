using System;
using System.Collections.Generic;
using System.Text;

namespace FirstFollowParser
{
	public class Follow
	{
		public String variable;
		public HashSet<String> setOfFollow = new HashSet<String>();

		public Follow(String variable, HashSet<String> setOfFollow)
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
			return $"Follow( { variable } ) : [ { String.Join(", ", setOfFollow) } ]";
		}

		public String printInFile()
		{
			return this.ToString();
		}
	}
}
