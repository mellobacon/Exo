using System.Collections.Generic;
using DumbassP.Compiler.CodeAnalysis.Lexer;

namespace DumbassP.Compiler.CodeAnalysis.Parser.Expressions
{
    public class BinaryExpression : ExpressionSyntax
    {
        public ExpressionSyntax Left;
        public SyntaxToken Op;
        public ExpressionSyntax Right;

        public BinaryExpression(ExpressionSyntax left, SyntaxToken op, ExpressionSyntax right)
        {
            Left = left;
            Op = op;
            Right = right;
        }

        public override SyntaxTokenType Type => SyntaxTokenType.BinaryExpression;
        public override IEnumerable<SyntaxNode> GetChildren()
        {
            yield return Left;
            yield return Op;
            yield return Right;
        }
    }
}