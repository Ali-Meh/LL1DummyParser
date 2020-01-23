using FirstFollowParser;
using System;

namespace LL1Parser
{
    class Program
    {
        static void Main(string[] args)
        {
            GrammarParser grammar = new GrammarParser(@"C:\Users\darkshot\source\repos\LL1Parser\ParserTests\Sample5.in");
            grammar.parse();
            var table = new ParseTable(grammar);
            table.printTable();


            InputLexer n = new InputLexer(@"C:\Users\darkshot\source\repos\LL1\FirstAndFollow\Input2.in", grammar);

            Console.WriteLine("__________________________________________________________");
            Console.WriteLine(string.Join(",", n.elements));
            var output=Parser.parser(grammar, n.elements);
            Parser.printOutput(output);
            Console.WriteLine();
        }
    }
}
