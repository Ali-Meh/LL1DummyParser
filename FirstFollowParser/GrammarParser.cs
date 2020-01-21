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

                    nonTerminals.Add(rule.source);


                    foreach (var item in setOfProductions)
                    {
                        foreach (var item2 in item.Split(string.Join(",", nonTerminals).Split(","), StringSplitOptions.RemoveEmptyEntries))
                        {
                            if (!nonTerminals.Contains(item2)&&item2!="'")
                            {
                                terminals.Add(item2);
                            }
                        }
                    }
                    grammer.Add(rule);
                }
                foreach (var item in nonTerminals)
                {
                    if (terminals.Contains(item))
                    {
                        terminals.Remove(item);
                    }
                }
                foreach (var rule in grammer)
                {
                    foreach (var item in rule.production)
                    {
                        foreach (var item2 in item.Split(string.Join(",", nonTerminals).Split(","), StringSplitOptions.None))
                        {
                            if (nonTerminals.Contains(item2)||item2=="")
                            {
                                terminals.Remove(item);
                            }
                        }
                    }
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
