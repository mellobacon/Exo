using System;
using System.IO;
using System.Text;
using DumbassP.Compiler.CodeAnalysis.Lexer;

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

        // function  Run(file). just runs cmd if theres no file specified
        public void Run(string path = null)
        {
            // runs the lang. probably going to end up just being the lexer ig. compiler...eh maybe not? idk we will see
            var textbuilder = new StringBuilder();
            while (true)
            {
                var input = "";
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
                    using var file = new StreamReader(path);
                    input = file.ReadLine();
                }

                var isblank = string.IsNullOrWhiteSpace(input);
                if (isblank && textbuilder.Length == 0)
                {
                    break;
                }
                textbuilder.AppendLine(input);
                var text = textbuilder.ToString();
                
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
            }
        }
    }
}
