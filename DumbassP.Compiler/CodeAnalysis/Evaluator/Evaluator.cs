using System;
using DumbassP.Compiler.CodeAnalysis.Binding;
using DumbassP.Compiler.CodeAnalysis.Binding.Expressions;

namespace DumbassP.Compiler.CodeAnalysis.Evaluator
{
    public class Evaluator
    {
        private readonly IBoundExpression _root;
        public Evaluator(IBoundExpression root)
        {
            _root = root;
        }

        public object Evaluate()
        {
            return EvaluateExpression(_root);
        }

        private object EvaluateExpression(IBoundExpression root)
        {
            return root.BoundType switch
            {
                BoundType.BinaryExpression => EvaluateBinaryExpression(root),
                BoundType.LiteralExpression => EvaluateLiteralExpression(root),
                _ => null
            };
        }

        private object EvaluateBinaryExpression(IBoundExpression root)
        {
            if (root is not BinaryBoundExpression b) return null;
            
            object left = EvaluateExpression(b.Left);
            BoundBinaryOperator op = b.Op;
            object right = EvaluateExpression(b.Right);

            if (left == null || right == null) return null;

            // If one of the numbers is a float convert both numbers to floats else make sure both are ints
            // (except division)
            if (left is float || right is float)
            {
                return op.BoundType switch
                {
                    BinaryOperatorType.Addition => Convert.ToSingle(left) + Convert.ToSingle(right),
                    BinaryOperatorType.Subtraction => Convert.ToSingle(left) - Convert.ToSingle(right),
                    BinaryOperatorType.Multiplication => Convert.ToSingle(left) * Convert.ToSingle(right),
                    BinaryOperatorType.Division => Convert.ToSingle(left) / Convert.ToSingle(right),
                    BinaryOperatorType.Modulo => Convert.ToSingle(left) % Convert.ToSingle(right),
                    BinaryOperatorType.LessThan => Convert.ToSingle(left) < Convert.ToSingle(right),
                    BinaryOperatorType.MoreThan => Convert.ToSingle(left) > Convert.ToSingle(right),
                    BinaryOperatorType.LessEqual => Convert.ToSingle(left) <= Convert.ToSingle(right),
                    BinaryOperatorType.MoreEqual => Convert.ToSingle(left) >= Convert.ToSingle(right),
                    BinaryOperatorType.Equal => Equals(left, right),
                    _ => throw new Exception($"Unexpected binary operator {b.Op}")
                };
            }

            return op.BoundType switch
            {
                BinaryOperatorType.Addition => (int)left + (int)right,
                BinaryOperatorType.Subtraction => (int)left - (int)right,
                BinaryOperatorType.Multiplication => (int)left * (int)right,
                BinaryOperatorType.Division => Convert.ToSingle(left) / Convert.ToSingle(right),
                BinaryOperatorType.Modulo => (int)left % (int)right,
                BinaryOperatorType.LogicalOr => (bool)left || (bool)right,
                BinaryOperatorType.LogicalAnd => (bool)left && (bool)right,
                BinaryOperatorType.LessThan => (int)left < (int)right,
                BinaryOperatorType.MoreThan => (int)left > (int)right,
                BinaryOperatorType.LessEqual => (int)left <= (int)right,
                BinaryOperatorType.MoreEqual => (int)left >= (int)right,
                BinaryOperatorType.Equal => Equals(left, right),
                _ => throw new Exception($"Unexpected binary operator {b.Op}")
            };
        }

        private object EvaluateLiteralExpression(IBoundExpression root)
        {
            return root is not LiteralBoundExpression n ? null : n.Value;
        }
    }
}