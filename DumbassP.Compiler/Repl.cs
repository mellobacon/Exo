using System;
using System.IO;
using System.Linq;
using System.Text;
using DumbassP.Compiler.CodeAnalysis.Errors;
using DumbassP.Compiler.CodeAnalysis.Evaluator;
using DumbassP.Compiler.CodeAnalysis.Lexer;
using DumbassP.Compiler.CodeAnalysis.Parser;

namespace DumbassP.Compiler
{
    public class Repl
    {
        // prompt
        public string Prompt = ">";

        // multiline prompt (for cmd only)
        public string MultilinePrompt = "#";
        // prompt color
        // multiline prompt color
        // command prompt // (for cmd only)
        // is colored? (cmd only)

        // just runs cmd if theres no file specified
        public void Run(string path = null)
        {
            var textbuilder = new StringBuilder();
            StreamReader file = null;
            if (path != null)
            {
                file = new StreamReader(path);
            }
            while (true)
            {
                // Get the input depending if its from the cmd or a file
                string input;
                if (path is null)
                {
                    if (textbuilder.Length == 0)
                    {
                        Console.Write(Prompt);
                        input = Console.ReadLine();
                    }
                    else
                    {
                        Console.Write(MultilinePrompt);
                        input = Console.ReadLine();
                    }
                }
                else
                {
                    input = file.ReadLine();
                    Console.WriteLine(input);
                }

                // Process the input
                var isblank = string.IsNullOrWhiteSpace(input);
                if (isblank && textbuilder.Length == 0)
                {
                    break;
                }
                textbuilder.AppendLine(input);
                var text = textbuilder.ToString();
                
                /*
                // lex
                var lexer = new Lexer(text);
                while (true)
                {
                    var token = lexer.Lex();
                    if (token.Type == SyntaxTokenType.EofToken)
                    {
                        textbuilder.Clear();
                        Console.WriteLine();
                        break;
                    }
                    if (token.Type != SyntaxTokenType.WhiteSpaceToken && token.Value != null)
                    {
                        Console.Write($"[{token.Type}:{token.Value}]");   
                    }
                    else if (token.Type != SyntaxTokenType.WhiteSpaceToken)
                    {
                        Console.Write($"[{token.Type}]");
                    }
                }
                */
                
                SyntaxTree tree = SyntaxTree.Parse(text);
                if (!isblank && tree.Errors.Any())
                {
                    continue;
                }
                ShowTree(tree.Root);
                
                Compilation compilation = new Compilation(tree);
                Result result = compilation.Evaluate();

                if (result.Value != null)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(result.Value);
                    Console.ResetColor();
                }

                foreach (Error error in result.Errors.ToArray())
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(error);
                    Console.ResetColor();
                }

                textbuilder.Clear();
            }
        }

        private static void ShowTree(SyntaxNode node, string indent = "", bool isLast = true)
        {
            var marker = isLast ? "└──" : "├──";
            Console.Write(indent);
            Console.Write(marker);

            switch (node.Type)
            {
                case SyntaxTokenType.BinaryExpression:
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    Console.Write(node.Type);
                    Console.ResetColor();
                    break;
                case SyntaxTokenType.LiteralExpression:
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.Write(node.Type);
                    Console.ResetColor();
                    break;
                case SyntaxTokenType.StarToken:
                case SyntaxTokenType.SlashToken:
                case SyntaxTokenType.PlusToken:
                case SyntaxTokenType.MinusToken:
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.Write(node.Type);
                    Console.ResetColor();
                    break;
                default:
                    Console.ResetColor();
                    Console.Write(node.Type);
                    break;
            }

            // Basically for printing numbers
            if (node is SyntaxToken token && token.Value != null)
            {
                Console.Write(" ");
                Console.Write(token.Value);
            }

            Console.WriteLine();
            indent += isLast ? "   " : "│  ";
            var last = node.GetChildren().LastOrDefault();
            foreach (var child in node.GetChildren())
            {
                ShowTree(child, indent, child == last);
            }
        }
    }
}
