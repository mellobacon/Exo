using System.Collections.Generic;
using Exo.Compiler.CodeAnalysis.Lexer;

namespace Exo.Compiler.CodeAnalysis.Parser
{
    // This is here to group everything i need to print the syntax tree. The type is there because i need to access it
    // for the tree too. The GetChildren method is there because i need a way to collect all the nodes for the tree
    public abstract class SyntaxNode
    {
        public abstract SyntaxTokenType Type { get; }

        public abstract IEnumerable<SyntaxNode> GetChildren();
    }
}