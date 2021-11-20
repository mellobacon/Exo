using DumbassP.Compiler.CodeAnalysis.Lexer;
using DumbassP.Compiler.CodeAnalysis.Parser.Expressions;

namespace DumbassP.Compiler.CodeAnalysis.Parser
{
    public class SyntaxTree
    {
        public ExpressionSyntax Root;
        private SyntaxToken _eofToken;

        public SyntaxTree(ExpressionSyntax expression, SyntaxToken eofToken)
        {
            Root = expression;
            _eofToken = eofToken;
        }

        public static SyntaxTree Parse(string text)
        {
            Parser parser = new Parser(text);
            return parser.Parse();
        }
    }
}