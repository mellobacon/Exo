using System.Collections.Generic;
using DumbassP.Compiler.CodeAnalysis.Lexer;

namespace DumbassP.Compiler.CodeAnalysis.Parser.Expressions
{
    public class LiteralExpression : ExpressionSyntax
    {
        public readonly SyntaxToken Token;
        public object Value;

        public LiteralExpression(SyntaxToken token)
        {
            Token = token;
        }

        public LiteralExpression(SyntaxToken token, object value)
        {
            Token = token;
            Value = value;
        }

        public override SyntaxTokenType Type => SyntaxTokenType.LiteralExpression;
        public override IEnumerable<SyntaxNode> GetChildren()
        {
            yield return Token;
        }
    }
}