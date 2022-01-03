#nullable enable
using DumbassP.Compiler.CodeAnalysis.Errors;

namespace DumbassP.Compiler.CodeAnalysis.Lexer
{
    public class Lexer
    {
        private readonly string _text;
        private object? _value;
        private SyntaxTokenType _type;

        // keeps track of where the lexer is lexing in the string
        private int _start;
        private int _position;
        private char Current => Peek(0);
        
        // keeps track of any errors during when lexing
        public ErrorList Errors { get; } = new();

        public Lexer(string text)
        {
            _text = text;
        }

        // gets the current character getting lexed
        private char Peek(int offset)
        {
            int index = _position + offset;
            return index >=_text.Length ? '\0' : _text[index];
        }

        // increments the position in the string
        private void Advance(int amount)
        {
            _position += amount;
        }

        private void LexNumber()
        {
            object? value = null;
            // continues getting the number until there isnt another one to read
            while (char.IsDigit(Current) || Current is '.' or '_')
            {
                Advance(1);
            }

            int length = _position - _start;
            string text = _text.Substring(_start, length);
            
            // Handle numeric separators
            if (text.Contains('_'))
            {
                if (text.EndsWith('_'))
                {
                    return;
                }
                text = text.Replace("_", "");
            }
            // if there is a decimal in the number its a float else its an int
            if (text.Contains('.')) 
            {
                if (float.TryParse(text, out float f))
                {
                    value = f;
                }
                else if (double.TryParse(text, out double d))
                {
                    value = d;
                }
                else
                {
                    Errors.ReportInvalidNumberConversion(text, typeof(float));
                }
            }
            else
            {
                if (int.TryParse(text, out int i))
                {
                    value = i;
                }
                else
                {
                    Errors.ReportInvalidNumberConversion(text, typeof(int));
                }
            }

            _value = value;

            _type = _value == null ? SyntaxTokenType.BadToken : SyntaxTokenType.NumberToken;
        }

        private void LexWhiteSpace()
        {
            while (char.IsWhiteSpace(Current))
            {
                Advance(1);
            }
            _type = SyntaxTokenType.WhiteSpaceToken;
        }

        private void LexString()
        {
            // strings with either "" or '' works. might change later
            switch (Current)
            {
                case '"':
                {
                    Advance(1);
                    while (Current != '"')
                    {
                        Advance(1);
                    }

                    if (Current is '"')
                    {
                        Advance(1);
                        int length = _position - _start;
                        string text = _text.Substring(_start, length);
                        _type = SyntaxTokenType.StringToken;
                        _value = text;
                    }
                    break;
                }
                case '\'':
                {
                    Advance(1);
                    while (Current != '\'')
                    {
                        Advance(1);
                    }

                    if (Current is '\'')
                    {
                        Advance(1);
                        int length = _position - _start;
                        string text = _text.Substring(_start, length);
                        _type = SyntaxTokenType.StringToken;
                        _value = text;
                    }
                    break;
                }
            }
        }

        private void LexKeyword()
        {
            while (char.IsLetter(Current) || Current == '_' || char.IsDigit(Current))
            {
                Advance(1);
            }

            int length = _position - _start;
            string text = _text.Substring(_start, length);
            _type = SyntaxPrecedence.GetKeywordType(text);

            _value = _type switch
            {
                SyntaxTokenType.TrueKeyword => true,
                SyntaxTokenType.FalseKeyword => false,
                _ => _value
            };
        }
        
        public SyntaxToken Lex()
        {
            _start = _position;
            _value = null;
            _type = SyntaxTokenType.BadToken;

            switch (Current)
            {
                case '\0':
                    _type = SyntaxTokenType.EofToken;
                    break;
                case '+':
                    _type = SyntaxTokenType.PlusToken;
                    Advance(1);
                    break;
                case '-':
                    _type = SyntaxTokenType.MinusToken;
                    Advance(1);
                    break;
                case '/':
                    _type = SyntaxTokenType.SlashToken;
                    Advance(1);
                    break;
                case '*':
                    _type = SyntaxTokenType.StarToken;
                    Advance(1);
                    break;
                case '%':
                    _type = SyntaxTokenType.ModuloToken;
                    Advance(1);
                    break;
                case '|':
                    if (Peek(1) == '|')
                    {
                        _type = SyntaxTokenType.DoublePipeToken;
                    }
                    Advance(2);
                    break;
                case '&':
                    if (Peek(1) == '&')
                    {
                        _type = SyntaxTokenType.DoubleAmpersandToken;
                    }
                    Advance(2);
                    break;
                case '(':
                    _type = SyntaxTokenType.OpenParenToken;
                    Advance(1);
                    break;
                case ')':
                    _type = SyntaxTokenType.ClosedParenToken;
                    Advance(1);
                    break;
                case var _ when char.IsDigit(Current) || Current is '.':
                    LexNumber();
                    break;
                case var _ when char.IsWhiteSpace(Current):
                    LexWhiteSpace();
                    break;
                case var _ when char.IsLetter(Current) || Current is '_':
                    LexKeyword();
                    break;
                case var _ when Current is '"' or '\'':
                    LexString();
                    break;
                default:
                    Errors.ReportBadCharacter(Current);
                    Advance(1);
                    break;
            }
            
            string text = SyntaxPrecedence.GetText(_type);
            int length = _position - _start;
            if (text is null)
            {
                text = _text.Substring(_start, length);
            }

            return new SyntaxToken(text, _value, _type);
        }
    }
}
