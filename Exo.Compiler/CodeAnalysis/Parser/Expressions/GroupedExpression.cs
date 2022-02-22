using System.Collections.Generic;
using Exo.Compiler.CodeAnalysis.Lexer;

namespace Exo.Compiler.CodeAnalysis.Parser.Expressions
{
    public class GroupedExpression : ExpressionSyntax
    {
        private readonly SyntaxToken LeftOp;
        public readonly ExpressionSyntax Expression;
        private readonly SyntaxToken RightOp;
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