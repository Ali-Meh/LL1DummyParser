using System;
using System.Collections.Generic;
using System.Text;

namespace FirstFollowParser
{
	public class Rule
	{
		public string source;
		public List<string> production = new List<string>();

		public Rule(string source, List<string> destinationList)
		{
			source = this.source;
			destinationList = this.production;
		}
		public Rule()
		{
			// TODO Auto-generated constructor stub
		}
	}
}
