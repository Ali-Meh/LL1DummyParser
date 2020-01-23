using FirstFollowParser;
using System;

namespace LL1Parser
{
    class Program
    {
        static void Main(string[] args)
        {
            GrammarParser grammer = new GrammarParser(@"C:\Users\darkshot\source\repos\LL1Parser\ParserTests\Sample5.in");
            grammer.parse();
            var table = new ParseTable(grammer);
            table.printTable();
        }
    }
}
