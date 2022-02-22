using Exo.Compiler.CodeAnalysis.Errors;
using Exo.Compiler.CodeAnalysis.Lexer;
using Exo.Compiler.CodeAnalysis.Parser.Expressions;

namespace Exo.Compiler.CodeAnalysis.Parser
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