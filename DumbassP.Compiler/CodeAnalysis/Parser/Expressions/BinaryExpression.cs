using System.Collections.Generic;
using DumbassP.Compiler.CodeAnalysis.Lexer;

namespace DumbassP.Compiler.CodeAnalysis.Parser.Expressions
{
    public class BinaryExpression : ExpressionSyntax
    {
        private ExpressionSyntax left;
        private SyntaxToken op;
        private ExpressionSyntax right;

        public BinaryExpression(ExpressionSyntax left, SyntaxToken op, ExpressionSyntax right)
        {
            this.left = left;
            this.op = op;
            this.right = right;
        }

        public override SyntaxTokenType Type => SyntaxTokenType.BinaryExpression;
        public override IEnumerable<SyntaxNode> GetChildren()
        {
            yield return left;
            yield return op;
            yield return right;
        }
    }
}