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
    /**
     * Runs the compiler based on input
     */
    public class Repl
    {
        /**
         * The commandline prompt
         */
        public string Prompt = ">";

        /**
         * The commandline prompt for multilines
         */
        public string MultilinePrompt = "#";

        /**
         * <summary>Runs the compiler. If there is no argument specified, it will run via inputs from the cmd.</summary>
         * <param name="path">The path to a text file</param>
         */
        public void Run(string path = null)
        {
            var textbuilder = new StringBuilder();
            
            // Set the file for getting input from it
            StreamReader file = null;
            if (path != null)
            {
                file = new StreamReader(path);
            }
            
            while (true)
            {
                // Get the input depending if its from the cmd or a file
                string input;
                if (path is null) // Get input from the cmd
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
                else // Get input from the file
                {
                    input = file.ReadLine();
                    Console.WriteLine(input);
                }

                // Process the input
                bool isblank = string.IsNullOrWhiteSpace(input);
                if (isblank && textbuilder.Length == 0)
                {
                    break;
                }
                // Collect every line of input to be evaluated
                textbuilder.AppendLine(input);
                var text = textbuilder.ToString();

                // Get and print the syntax tree. This is for transparency when parsing.
                SyntaxTree tree = SyntaxTree.Parse(text);
                if (!isblank && tree.Errors.Any())
                {
                    continue;
                }
                ShowTree(tree.Root);
                
                // Evaluate the expression and print the output, along with any errors
                Compilation compilation = new Compilation(tree);
                Result result = compilation.Evaluate();

                if (!result.Errors.Any())
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(result.Value);
                    Console.ResetColor();
                }
                
                foreach (Error error in result.Errors.ToArray())
                {
                    if (error.ToString().Contains("Eof")) break;
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(error);
                    Console.ResetColor();

                    string prefix = text[..error.TextSpan.Start];
                    string e = text.Substring(error.TextSpan.Start, error.TextSpan.Length);
                    string suffix = text[error.TextSpan.End..];
                    
                    Console.Write(prefix);
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.Write(e);
                    Console.ResetColor();
                    Console.Write(suffix.TrimEnd());
                    Console.WriteLine();
                    
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    for (var _ = 0; _ < prefix.Length; _++)
                    {
                        Console.Write(" ");
                    }

                    for (var _ = 0; _ < e.Length; _++)
                    {
                        Console.Write("^");
                    }
                    Console.WriteLine();
                    Console.ResetColor();
                }

                textbuilder.Clear();
            }
        }

        /**
         * <summary>Prints the syntax tree</summary>
         * <param name="node">The node to put in the tree</param>
         * <param name="indent">The string to indent with. Defaults to an empty string</param>
         * <param name="isLast">Defines if the node is the last in the branch. Defaults to true</param>
         */
        private static void ShowTree(SyntaxNode node, string indent = "", bool isLast = true)
        {
            string marker = isLast ? "└──" : "├──";
            Console.Write(indent);
            Console.Write(marker);

            // Print the node
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
            SyntaxNode last = node.GetChildren().LastOrDefault();
            foreach (SyntaxNode child in node.GetChildren())
            {
                ShowTree(child, indent, child == last);
            }
        }
    }
}
