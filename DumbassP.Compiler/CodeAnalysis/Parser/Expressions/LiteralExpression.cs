using System.Collections.Generic;
using DumbassP.Compiler.CodeAnalysis.Lexer;

namespace DumbassP.Compiler.CodeAnalysis.Parser.Expressions
{
    public class LiteralExpression : ExpressionSyntax
    {
        public SyntaxToken Token;

        public LiteralExpression(SyntaxToken token)
        {
            Token = token;
        }

        public override SyntaxTokenType Type => SyntaxTokenType.LiteralExpression;
        public override IEnumerable<SyntaxNode> GetChildren()
        {
            yield return Token;
        }
    }
}