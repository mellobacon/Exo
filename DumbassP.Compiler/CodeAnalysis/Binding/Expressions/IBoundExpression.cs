using System;

namespace DumbassP.Compiler.CodeAnalysis.Binding.Expressions
{
    public interface IBoundExpression : IBindNode
    {
        public Type Type { get; }
    }
}