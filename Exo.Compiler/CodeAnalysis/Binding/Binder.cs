using System;
using Exo.Compiler.CodeAnalysis.Binding.Expressions;
using Exo.Compiler.CodeAnalysis.Errors;
using Exo.Compiler.CodeAnalysis.Lexer;
using Exo.Compiler.CodeAnalysis.Parser.Expressions;

namespace Exo.Compiler.CodeAnalysis.Binding
{
    public class Binder
    {
        public ErrorList Errors { get; } = new();

        public IBoundExpression BindExpression(ExpressionSyntax syntax)
        {
            return syntax.Type switch
            {
                SyntaxTokenType.LiteralExpression => BindLiteralExpression((LiteralExpression)syntax),
                SyntaxTokenType.BinaryExpression => BindBinaryExpression((BinaryExpression)syntax),
                SyntaxTokenType.GroupedExpression => BindGroupedExpression((GroupedExpression)syntax),
                _ => throw new Exception($"Unexpected syntax {syntax.Type}")
            };
        }

        private static IBoundExpression BindLiteralExpression(LiteralExpression syntax)
        {
            object value = syntax.Value ?? 0;
            return new LiteralBoundExpression(value);
        }

        private IBoundExpression BindBinaryExpression(BinaryExpression syntax)
        {
            IBoundExpression left = BindExpression(syntax.Left);
            IBoundExpression right = BindExpression(syntax.Right);
            BoundBinaryOperator op = BoundBinaryOperator.GetOp(left.Type, syntax.Op.Type, right.Type);
            if (op is null)
            {
                Errors.ReportUndefinedBinaryOperator(syntax.Op.TextSpan ,left.Type, syntax.Op.Text, right.Type);
                return left;
            }
            return new BinaryBoundExpression(left, op, right);
        }

        private IBoundExpression BindGroupedExpression(GroupedExpression syntax)
        {
            return BindExpression(syntax.Expression);
        }
    }
}