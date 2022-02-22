using System;

namespace Exo.Compiler.CodeAnalysis.Binding
{
    public interface IBindNode
    {
        public BoundType BoundType { get; }
    }
}