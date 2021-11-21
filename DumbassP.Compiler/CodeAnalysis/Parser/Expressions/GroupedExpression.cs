using System.Collections.Generic;
using DumbassP.Compiler.CodeAnalysis.Lexer;

namespace DumbassP.Compiler.CodeAnalysis.Parser.Expressions
{
    public class GroupedExpression : ExpressionSyntax
    {
        private SyntaxToken LeftOp;
        public ExpressionSyntax Expression;
        private SyntaxToken RightOp;
        public GroupedExpression(SyntaxToken leftOp, ExpressionSyntax expression, SyntaxToken rightOp)
        {
            LeftOp = leftOp;
            Expression = expression;
            RightOp = rightOp;
        }

        public override SyntaxTokenType Type => SyntaxTokenType.GroupedExpression;
        public override IEnumerable<SyntaxNode> GetChildren()
        {
            yield return LeftOp;
            yield return Expression;
            yield return RightOp;
        }
    }
}