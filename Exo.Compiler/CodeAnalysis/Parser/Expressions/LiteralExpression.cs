using System.Collections.Generic;
using Exo.Compiler.CodeAnalysis.Lexer;

namespace Exo.Compiler.CodeAnalysis.Parser.Expressions
{
    public class LiteralExpression : ExpressionSyntax
    {
        private readonly SyntaxToken _token;
        public readonly object Value;

        public LiteralExpression(SyntaxToken token, object value)
        {
            _token = token;
            Value = value;
        }

        public override SyntaxTokenType Type => SyntaxTokenType.LiteralExpression;
        public override IEnumerable<SyntaxNode> GetChildren()
        {
            yield return _token;
        }
    }
}