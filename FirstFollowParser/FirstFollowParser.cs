using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FirstFollowParser
{
    class FirstFollowParser
    {
        public List<First> first = new List<First>();
        public List<Follow> follow = new List<Follow>();
        GrammarParser grammer;
        public FirstFollowParser(GrammarParser grammer)
        {
            this.grammer=grammer;
        }

        public void First()
        {
            foreach (var item in grammer.nonTerminals)
            {
                FirstOf(item);
            }

        }

        public void printFirst()
        {
            for (int i = 0; i < first.Count; i++)
            {
                first[i].print();
            }
        }


        public HashSet<string> FirstOf(string source)
        {
            var oldfirst = first.FirstOrDefault(e => e.variable == source);
            if (oldfirst != null)
            {
                return oldfirst.setOfFirst;
            }
            var listOfFirsts = new HashSet<string>();
            foreach (var rule in grammer.grammer.Where(e => e.source == source))
            {
                foreach (var product in rule.production)
                {
                    if (grammer.terminals.Contains(product[0].ToString()))
                    {
                        listOfFirsts.Add(product[0].ToString());
                    }
                    else if (grammer.terminals.Contains(product.ToString()))
                    {
                        listOfFirsts.Add(product.ToString());
                    }
                    else
                    {
                        int i = 0;
                        var tempList = new HashSet<string>();
                        do
                        {
                            if (product[i].ToString() == source)
                                continue;
                            tempList = FirstOf(product[i].ToString());
                            listOfFirsts.UnionWith(tempList);
                            i++;
                        } while (tempList.Contains("!")&&i<product.Length);
                    }
                }
            }
            First fi = new First(source);
            fi.setOfFirst = listOfFirsts;
            first.Add(fi);
            return listOfFirsts;
        }




    }
}
