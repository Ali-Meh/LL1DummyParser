using System;

namespace FirstFollowParser
{
    class Program
    {
        static void Main(string[] args)
        {
            GrammarParser grammer = new GrammarParser(@"C:\Users\darkshot\source\repos\LL1Parser\ParserTests\Sample5.in");
            Console.WriteLine(string.Join(",", grammer.nonTerminals));
            Console.WriteLine(string.Join(",", grammer.terminals));
            Console.WriteLine(grammer.grammer.Count);
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            var fifo = new FirstFollow(grammer);
            fifo.First();
            fifo.printFirst();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            fifo.Follow();
            fifo.PrintFollow();

        }
    }
}
