using FirstFollowParser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LL1Parser
{
    class ParseTable
    {
        public List<First> first = new List<First>();
        public List<Follow> follow = new List<Follow>();
        public string[][] table;
        GrammarParser grammar;

        public ParseTable(GrammarParser grammar)
        {
            first = grammar.FIFO.first;
            follow = grammar.FIFO.follow;
            this.grammar = grammar;
            this.filler();
        }

        public void filler()
        {
            var terminals = grammar.terminals.Where(e=>e!="!").ToArray();
            var nonTerminals = grammar.nonTerminals.ToArray();

            table =new string[nonTerminals.Length + 1][];
            for (int i = 0; i < table.Length; i++)
            {
                table[i] = new string[terminals.Length + 2];
            }


            for (int i = 0; i < table.Length; i++)
            {
                for (int j = 0; j < table[i].Length; j++)
                {
                    if (i == 0)
                    {
                        if (j == 0)
                            table[0][j] = "";
                        if (j > 0 && j - 1 < terminals.Length)
                            table[0][j] = terminals[j - 1];

                        if (j > 0 && j - 1 == terminals.Length)
                            table[0][j] = "$";

                    }
                    else if (i != 0)
                    {
                        if (j == 0)
                        {
                            table[i][j] = nonTerminals[i - 1];
                        }
                        else if (j != 0)
                        {
                            String src = nonTerminals[i - 1];
                            List<String> productions = getProductions(src, grammar);
                            if (productions != null)
                            {
                                if (containEpsilon(productions))
                                {
                                    List<String> followOf = FollowOf(src);
                                    foreach (String compare in followOf)
                                    {
                                        if (table[0][j] == compare)
                                        {
                                            table[i][j] = "!";
                                        }
                                    }

                                }
                                List<List<String>> splitProduction = SplitProduction(productions, grammar);

                                for (int k = 0; k < splitProduction.Count; k++)
                                {
                                    String var = splitProduction[k][0];
                                    var firstOf =new List<String>();
                                    if (terminals.Contains(var))
                                        firstOf.Add(var);
                                    else
                                        firstOf = FirstOf(var);

                                    var = "";
                                    for (int l = 0; l < splitProduction[k].Count; l++)
                                        var += splitProduction[k][l];

                                    if (firstOf != null)
                                        foreach (String compare in firstOf)
                                        {
                                            if (table[0][j] == compare)
                                            {
                                                table[i][j] = var;
                                            }
                                        }
                                }

                            }
                        }
                    }
                }

            }
        }
        private List<String> FollowOf(String src)
        {
            for (int i = 0; i < follow.Count; i++)
            {
                if (follow[i].variable == src)
                {
                    return follow[i].setOfFollow.ToList();
                }

            }
            return null;
        }

        private List<string> FirstOf(String src)
        {
            for (int i = 0; i < first.Count; i++)
            {
                if (first[i].variable == src)
                {
                    return first[i].setOfFirst.ToList();
                }
            }
            return null;
        }

        private static bool containEpsilon(List<String> x)
        {

            foreach (String str in x)
            {
                if (str == "!")
                {
                    return true;
                }
            }
            return false;
        }

        private static List<String> getProductions(String src, GrammarParser grammar)
        {
            for (int i = 0; i < grammar.grammer.Count; i++)
            {

                if (grammar.grammer[i].source == src)
                    return grammar.grammer[i].production;
            }
            return null;
        }

        private static List<List<String>> SplitProduction(List<String> production, GrammarParser grammer)
        {
            List<List<String>> output = new List<List<String>>();
            for (int i = 0; i < production.Count; i++)
            {
                String tempo = production[i];
                List<String> aProduction = new List<String>();
                String temp = "";
                for (int j = 0; j < tempo.Length; j++)
                {
                    temp += tempo[j];
                    if (j + 1 < tempo.Length && (tempo[j + 1] + "") == "'")
                    {
                        temp += "'";
                        j++;
                    }
                    if (grammer.nonTerminals.Contains(temp) || grammer.terminals.Contains(temp) || temp == "!")
                    {
                        aProduction.Add(temp);
                        temp = "";
                    }

                }
                output.Add(aProduction);
            }
            return output;
        }
        public static void printRow(String[] row)
        {
            foreach (String i in row)
            {
                Console.Write(i);
                Console.Write("\t");
            }
            Console.WriteLine();
        }
        public void printTable()
        {
            foreach (String[] row in table)
            {
                ParseTable.printRow(row);
            }
        }

    }
}
