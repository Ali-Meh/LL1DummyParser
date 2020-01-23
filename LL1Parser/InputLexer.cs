using FirstFollowParser;
using System;
using System.Collections.Generic;
using System.Text;

namespace LL1Parser
{
    class InputLexer
    {
		public List<String> elements;

		public InputLexer()
		{
			// TODO Auto-generated constructor stub
		}

		public InputLexer(String fileName, GrammarParser grammer)
		{
			try
			{
				System.IO.StreamReader reader =
					new System.IO.StreamReader(fileName);
				String text = "";
				String var = "";
				while ((text = reader.ReadLine()) != null)
				{
					var = "";

					int i = 0;
					List<String> output = new List<String>();
					while (i < text.Length)
					{
						var = "";
						while (true)
						{
							if (i >= text.Length)
								break;

							var += text[i];
							i++;
							if (grammer.terminals.Contains(var))
							{

								break;
							}

						}
						if (!string.IsNullOrEmpty(var))
						{
							output.Add(var);
						}

					}

					elements = output;
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}

		}
	}
}
