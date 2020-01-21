using FirstFollowParser;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ParserTests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test()
        {
            var ext = new List<string> { "in" };
            var myFiles = Directory
                .EnumerateFiles(@"C:\Users\darkshot\source\repos\LL1Parser\ParserTests", "*.in", SearchOption.AllDirectories)
                .ToList();
            foreach (var input in myFiles)
            {
                GrammarParser grammer = new GrammarParser(input);
                Console.WriteLine(grammer.nonTerminals);
                Console.WriteLine(grammer.terminals);
                Console.WriteLine(grammer.grammer.Count);
                Console.WriteLine();
            }
            Assert.Pass();
        }
    }
}