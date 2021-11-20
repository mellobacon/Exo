using System.Collections.Generic;
using DumbassP.Compiler.CodeAnalysis.Lexer;

namespace DumbassP.Compiler.CodeAnalysis.Parser.Expressions
{
    public class LiteralExpression : ExpressionSyntax
    {
        private SyntaxToken token;

        public LiteralExpression(SyntaxToken token)
        {
            this.token = token;
        }

        public override SyntaxTokenType Type => SyntaxTokenType.LiteralExpression;
        public override IEnumerable<SyntaxNode> GetChildren()
        {
            yield return token;
        }
    }
}