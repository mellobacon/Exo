using DumbassP.Compiler.CodeAnalysis.Errors;
using DumbassP.Compiler.CodeAnalysis.Lexer;
using DumbassP.Compiler.CodeAnalysis.Parser.Expressions;

namespace DumbassP.Compiler.CodeAnalysis.Parser
{
    public class SyntaxTree
    {
        public readonly ExpressionSyntax Root;
        private SyntaxToken _eofToken;
        public readonly ErrorList Errors;

        public SyntaxTree(ExpressionSyntax expression, SyntaxToken eofToken, ErrorList errors)
        {
            Root = expression;
            _eofToken = eofToken;
            Errors = errors;
        }

        public static SyntaxTree Parse(string text)
        {
            var parser = new Parser(text);
            return parser.Parse();
        }
    }
}