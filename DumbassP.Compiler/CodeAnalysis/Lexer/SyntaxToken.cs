using System.Collections.Generic;
using System.Linq;
using DumbassP.Compiler.CodeAnalysis.Parser;

namespace DumbassP.Compiler.CodeAnalysis.Lexer
{
    public class SyntaxToken : SyntaxNode
    {
        public string Text;
        public object Value;
        public override SyntaxTokenType Type { get; }
        public override IEnumerable<SyntaxNode> GetChildren()
        {
            return Enumerable.Empty<SyntaxNode>(); // Theres no node to return so fuck off
        }

        public SyntaxToken(string text, object value, SyntaxTokenType type)
        {
            Text = text;
            Value = value;
            Type = type;
        }
    }
}