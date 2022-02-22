using System;

namespace Exo.Compiler.CodeAnalysis.Binding.Expressions
{
    // Returns the value of the literal expression
    public class LiteralBoundExpression : IBoundExpression
    {
        public object Value { get; }

        public LiteralBoundExpression(object value)
        {
            Value = value;
        }

        public BoundType BoundType => BoundType.LiteralExpression;
        public Type Type => Value.GetType();
    }
}