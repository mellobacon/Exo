using System.Collections.Generic;
using System.Linq;
using DumbassP.Compiler.CodeAnalysis.Parser;

namespace DumbassP.Compiler.CodeAnalysis.Lexer
{
    public class SyntaxToken : SyntaxNode
    {
        public string Text;
        public readonly object Value;
        public int Position;
        public TextSpan TextSpan => new(Position, Text.Length);
        public override SyntaxTokenType Type { get; }

        public SyntaxToken(string text, object value, SyntaxTokenType type, int position)
        {
            Text = text;
            Value = value;
            Type = type;
            Position = position;
        }
        
        public override IEnumerable<SyntaxNode> GetChildren()
        {
            return Enumerable.Empty<SyntaxNode>(); // Theres no node to return so fuck off
        }
    }
}