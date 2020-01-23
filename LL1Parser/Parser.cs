using FirstFollowParser;
using System;
using System.Collections.Generic;
using System.Text;

namespace LL1Parser
{
    class Parser
    {
        public static List<Derivations> output = new List<Derivations>();

        public static List<Derivations> parser(GrammarParser grammar, List<String> input)
        {
            var table = new ParseTable(grammar).table;
            Stack<String> pda = new Stack<String>();
            bool error = false;
            bool first = true;
            for (int a = 0; a < input.Count; a++)
            {

                bool done = false;
                while (!done)
                {
                    if (error)
                        break;

                    for (int i = 0; i < table.Length; i++)
                    {
                        for (int j = 0; j < table[i].Length; j++)
                        {
                            String var = input[a];
                            if (table[0][j] == var)
                            {

                                if (!(table[i][0] == ""))
                                {
                                    if (table[i][j] != null && first)
                                    {
                                        pda.Push(table[i][0]);
                                        first = false;
                                    }

                                    if (table[i][j] != null && pda.Count > 0 && !first && !(table[i][j] == var)
                                            && table[i][0] == pda.Peek())
                                    {
                                        Derivations d = new Derivations();
                                        d.input = var;
                                        d.topOfStack = pda.Pop();
                                        d.output = table[i][j];

                                        List<String> temp = new List<String>();
                                        temp.Add(d.output);
                                        List<List<String>> productions = splitProduction(temp, grammar);

                                        for (int k = productions[0].Count - 1; k >= 0; k--)
                                        {
                                            if (productions[0][k] != "!")
                                                pda.Push(productions[0][k]);

                                        }

                                        output.Add(d);

                                    }
                                    if (table[i][j] != null && pda.Count > 0 && (!first && table[i][j] == var)
                                            && table[i][0] == pda.Peek())
                                    {
                                        Derivations d = new Derivations();
                                        d.input = var;
                                        d.output = table[i][j];
                                        d.topOfStack = pda.Pop();

                                        List<String> temp = new List<String>();
                                        temp.Add(d.output);
                                        List<List<String>> productions = splitProduction(temp, grammar);
                                        for (int k = productions[0].Count - 1; k >= 0; k--)
                                        {
                                            if (productions[0][k] != "!")
                                                pda.Push(productions[0][k]);

                                        }

                                        output.Add(d);

                                    }
                                    if (table[i][j] != null && grammar.terminals.Contains(pda.Peek())
                                            && pda.Peek() == var)
                                    {
                                        Derivations d = new Derivations();
                                        d.input = "";
                                        d.output = pda.Peek();
                                        d.topOfStack = pda.Pop();
                                        done = true;
                                        output.Add(d);

                                    }
                                    if (table[i][j] == null && table[0][j] == var && table[i][0] == pda.Peek()
                                            && !done)
                                    {
                                        error = true;
                                    }
                                }

                            }
                        }

                    }

                }

            }

            if (!error)
                dollarPrsing(pda, grammar,table);
            if (error)
            {
                output = null;
            }
            return output;
        }

        public static bool haveNonEpislon(String[][] table, String var)
        {
            for (int i = 0; i < table.Length; i++)
            {
                for (int j = 0; j < table[i].Length; j++)
                {
                    if (table[0][j] == var)
                    {
                        if (table[i][j] != null && (table[i][j] != "!") && i > 0)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public static void dollarPrsing(Stack<String> pda, GrammarParser grammar, string[][] table)
        {
            while (pda.Count > 0)
            {

                for (int i = 0; i < table.Length; i++)
                {
                    for (int j = 0; j < table[i].Length; j++)
                    {
                        if (table[0][j] == "$")
                        {
                            if (table[i][j] != null && table[i][0] != "")
                            {

                                if (pda.Count > 0 && table[i][0] == pda.Peek())
                                {
                                    Derivations d = new Derivations();
                                    d.input = "$";
                                    d.topOfStack = pda.Pop();
                                    d.output = table[i][j];

                                    List<String> temp = new List<String>();
                                    temp.Add(d.output);
                                    List<List<String>> productions = splitProduction(temp, grammar);

                                    for (int k = productions[0].Count - 1; k >= 0; k--)
                                    {
                                        if (productions[0][k] != "!")
                                            pda.Push(productions[0][k]);
                                    }
                                    output.Add(d);

                                }

                            }
                        }
                    }
                }
            }
        }

        public static List<List<String>> splitProduction(List<String> production, GrammarParser grammer)
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

        public static void printOutput(List<Derivations> output)
        {
            if (output != null)
                for (int i = 0; i < output.Count; i++)
                {
                    Console.Write(output[i].topOfStack);
                    if (!string.IsNullOrEmpty(output[i].input))
                        Console.Write("[" + output[i].input + "]" + "  -->  " + output[i].output);

                    else
                        Console.Write("  -->  " + output[i].output);

                    Console.WriteLine();
                }
            else
                Console.WriteLine("Parse error");
        }
    }
}
