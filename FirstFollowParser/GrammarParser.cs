using System;
using System.Collections.Generic;
using System.Text;

namespace FirstFollowParser
{
    public class GrammarParser
    {
        public List<Rule> grammer = new List<Rule>();
        public HashSet<string> nonTerminals = new HashSet<string>();
        public HashSet<string> terminals = new HashSet<string>();

        public GrammarParser()
        {

        }

        public GrammarParser(string fileName)
        {
            try
            {
                System.IO.StreamReader reader =
                    new System.IO.StreamReader(fileName);
                string text = "";
                string[] rawRule;
                int counter = 0;

                while ((text = reader.ReadLine()) != null)
                {
                    rawRule = text.Split("->");

                    Rule rule = new Rule();
                    rule.source = rawRule[0].Trim();
                    List<String> setOfProductions = new List<String>(rawRule[1].Split("|"));
                    rule.production = setOfProductions;
                    foreach (var item in setOfProductions)
                    {
                        foreach (var item2 in item)
                        {
                            if (!Char.IsUpper(item2))
                            {
                                terminals.Add(item2.ToString());
                            }
                        }
                    }
                    if (rule.source.Length == 1 && Char.IsUpper(rule.source[0]))
                    {
                        nonTerminals.Add(rule.source);
                    }
                    else
                    {
                        //# parse Error
                    }
                    grammer.Add(rule);

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void print()
        {
            Console.WriteLine(nonTerminals);
            Console.WriteLine(terminals);
            for (int m = 0; m < grammer.Count; m++)
            {
                Console.Write(grammer[m].source + " " + "->");
                Console.WriteLine(string.Join(" | ", grammer[m].production));
            }
            Console.WriteLine("******************************************************************");
        }
    }
}
