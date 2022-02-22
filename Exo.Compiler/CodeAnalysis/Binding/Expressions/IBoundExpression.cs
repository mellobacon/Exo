using System;

namespace Exo.Compiler.CodeAnalysis.Binding.Expressions
{
    public interface IBoundExpression : IBindNode
    {
        public Type Type { get; }
    }
}