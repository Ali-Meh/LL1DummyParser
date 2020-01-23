using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FirstFollowParser
{
    public class FirstFollow
    {
        public List<First> first = new List<First>();
        public List<Follow> follow = new List<Follow>();
        GrammarParser grammer;
        public FirstFollow(GrammarParser grammer)
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
        public void Follow()
        {
            foreach (var item in grammer.nonTerminals)
            {
                FollowOf(item);
            }
        }

        public void printFirst()
        {
            for (int i = 0; i < first.Count; i++)
            {
                first[i].print();
            }
        }
        public void PrintFollow()
        {
            for (int i = 0; i < follow.Count; i++)
            {
                follow[i].print();
            }
        }
        public HashSet<string> FollowOf(string source)
        {
            var oldFollow = follow.FirstOrDefault(e => e.variable == source);
            if (oldFollow != null)
            {
                return oldFollow.setOfFollow;
            }
            var listOfFollows = new HashSet<string>();

            for(int j=0;j<grammer.grammer.Count;j++)
            {
                var rule = grammer.grammer[j];
                if (j == 0&&source==rule.source)
                    listOfFollows.Add("$");
                foreach (var product in rule.production.Where(e=>e.Contains(source)))
                {
                    var foundIndexes = new List<int>();
                    for (int i = product.IndexOf(source); i > -1; i = product.IndexOf(source, i + 1))
                    {
                        foundIndexes.Add(i);
                    }
                    foreach (var index in foundIndexes)
                    {
                        if (index == product.Length - source.Length && source!=rule.source)
                        {
                            //the follow of rule owner
                            listOfFollows.UnionWith(FollowOf(rule.source));
                        }
                        else if(index + source.Length<product.Length&&grammer.nonTerminals.Contains(product[index+source.Length].ToString()))//found nonterminals
                        {
                            var firstof = FirstOf(product[index + 1].ToString());
                            if (product.Length>index+2&&product[index + 2].ToString() == "'")
                            {
                                firstof = FirstOf(product.Substring(index+1,2));
                            }
                            //
                            listOfFollows.UnionWith(firstof);
                            if (firstof.Contains("!"))
                            {
                                listOfFollows.UnionWith(FollowOf(rule.source));
                            }
                        }
                        var term = grammer.terminals.FirstOrDefault(product.Substring(index).Contains);
                        if (term != null)//found terminals
                        {
                            listOfFollows.Add(term);
                            //
                        }
                    }
                }
            }

            listOfFollows=replaceEpislon(listOfFollows);


            Follow fi = new Follow(source);
            fi.setOfFollow = listOfFollows;
            follow.Add(fi);
            return listOfFollows;
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


        public static HashSet<String> replaceEpislon(HashSet<String> x)
        {
            var t = x.ToArray();

            for (int i = 0; i < t.Length; i++)
            {
                if (t[i] == "!")
                {
                    t[i]="$";
                }

            }
            return new HashSet<string>(t);
        }

    }
}
