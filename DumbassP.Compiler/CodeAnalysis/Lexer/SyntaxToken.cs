namespace DumbassP.Compiler.CodeAnalysis.Lexer
{
    public class SyntaxToken
    {
        public string Text;
        public object Value;
        public SyntaxTokenType Type;

        public SyntaxToken(string text, object value, SyntaxTokenType type)
        {
            Text = text;
            Value = value;
            Type = type;
        }
    }
}