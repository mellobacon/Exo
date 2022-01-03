using System;

namespace DumbassP.Compiler.CodeAnalysis.Binding
{
    public interface IBindNode
    {
        public BoundType BoundType { get; }
    }
}