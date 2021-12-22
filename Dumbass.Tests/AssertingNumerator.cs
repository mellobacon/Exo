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
        private readonly IEnumerator<SyntaxNode> _enumerator;
        private bool _hasErrors;

        public AssertingNumerator(SyntaxNode node)
        {
            // Get the list of nodes
            _enumerator = Flatten(node).GetEnumerator();
        }

        private bool MarkFailed()
        {
            _hasErrors = true;
            return false;
        }

        // This gets all the nodes from the tree
        private static IEnumerable<SyntaxNode> Flatten(SyntaxNode node)
        {
            var stack = new Stack<SyntaxNode>();
            stack.Push(node); // push the root to the stack

            while (stack.Count > 0)
            {
                SyntaxNode n = stack.Pop(); // the root or subroot node
                yield return n;
                // get each child of each root. reversed because yes. what am i smart to you?
                foreach (SyntaxNode? child in n.GetChildren().Reverse())
                {
                    stack.Push(child);   
                }
            }
        }

        public void AssertNode(SyntaxTokenType type)
        {
            try
            {
                Assert.True(_enumerator.MoveNext());
                if (_enumerator.Current != null)
                {
                    Assert.Equal(type, _enumerator.Current.Type);
                    Assert.IsNotType<SyntaxToken>(_enumerator.Current);
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
                Assert.True(_enumerator.MoveNext());
                if (_enumerator.Current != null)
                {
                    Assert.Equal(type, _enumerator.Current.Type);
                    var token = Assert.IsType<SyntaxToken>(_enumerator.Current);
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
            if (!_hasErrors)
            {
                Assert.False(_enumerator.MoveNext());   
            }
            _enumerator.Dispose();
        }
    }
}