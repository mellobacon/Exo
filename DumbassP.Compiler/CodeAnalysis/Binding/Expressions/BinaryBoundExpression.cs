using System;

namespace DumbassP.Compiler.CodeAnalysis.Binding.Expressions
{
    public class BinaryBoundExpression : IBoundExpression
    {
        public IBoundExpression Left { get; }
        public BoundBinaryOperator Op { get; }
        public IBoundExpression Right { get; }
        public BinaryBoundExpression(IBoundExpression left, BoundBinaryOperator op, IBoundExpression right)
        {
            Left = left;
            Op = op;
            Right = right;
        }

        public BoundType BoundType => BoundType.BinaryExpression;
        public Type Type => Op.ResultType;
    }
}