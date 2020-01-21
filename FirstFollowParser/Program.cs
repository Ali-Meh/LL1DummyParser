using System;

namespace FirstFollowParser
{
    class Program
    {
        static void Main(string[] args)
        {
            GrammarParser grammer = new GrammarParser(@"C:\Users\darkshot\source\repos\LL1Parser\ParserTests\Sample3.in");
            Console.WriteLine(string.Join(",", grammer.nonTerminals));
            Console.WriteLine(string.Join(",", grammer.terminals));
            Console.WriteLine(grammer.grammer.Count);
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            var fifo = new FirstFollowParser(grammer);
            fifo.First();
            fifo.printFirst();

        }
    }
}
