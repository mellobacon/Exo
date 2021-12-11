using System;
using System.Collections.Generic;
using System.Linq;
using DumbassP.Compiler.CodeAnalysis.Lexer;
using DumbassP.Compiler.CodeAnalysis.Parser;
using Xunit;

namespace Dumbass.Tests
{
    public class AssertingNumerator : IDisposable
    {
        readonly IEnumerator<SyntaxNode> _Enumerator;
        private bool HasErrors;

        public AssertingNumerator(SyntaxNode node)
        {
            // Get the list of nodes
            _Enumerator = Flatten(node).GetEnumerator();
        }

        private bool MarkFailed()
        {
            HasErrors = true;
            return false;
        }

        // This gets all the nodes from the tree
        private static IEnumerable<SyntaxNode> Flatten(SyntaxNode node)
        {
            var stack = new Stack<SyntaxNode>();
            stack.Push(node); // push the root to the stack

            while (stack.Count > 0)
            {
                var n = stack.Pop(); // the root or subroot node
                yield return n;
                // get each child of each root. reversed because yes. what am i smart to you?
                foreach (var child in n.GetChildren().Reverse())
                {
                    stack.Push(child);   
                }
            }
        }

        public void AssertNode(SyntaxTokenType type)
        {
            try
            {
                Assert.True(_Enumerator.MoveNext());
                if (_Enumerator.Current != null)
                {
                    Assert.Equal(type, _Enumerator.Current.Type);
                    Assert.IsNotType<SyntaxToken>(_Enumerator.Current);
                }
            }
            catch when (MarkFailed())
            {
                throw;
            }
        }
        
        public void AssertToken(SyntaxTokenType type, string text)
        {
            try
            {
                Assert.True(_Enumerator.MoveNext());
                if (_Enumerator.Current != null)
                {
                    Assert.Equal(type, _Enumerator.Current.Type);
                    var token = Assert.IsType<SyntaxToken>(_Enumerator.Current);
                    Assert.Equal(text, token.Text);
                }
            }
            catch when (MarkFailed())
            {
                throw;
            }
        }

        public void Dispose()
        {
            if (!HasErrors)
            {
                Assert.False(_Enumerator.MoveNext());   
            }
            _Enumerator.Dispose();
        }
    }
}