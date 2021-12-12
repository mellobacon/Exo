using DumbassP.Compiler.CodeAnalysis.Errors;

namespace DumbassP.Compiler.CodeAnalysis.Lexer
{
    public class Lexer
    {
        private readonly string _text;
        private object _value;
        private SyntaxTokenType _type;

        // keeps track of where the lexer is lexing in the string
        private int _start;
        private int _position;
        private char _current => Peek(0);
        
        // keeps track of any errors during when lexing
        public readonly ErrorList Errors = new();

        public Lexer(string text)
        {
            _text = text;
        }

        // gets the current character getting lexed
        private char Peek(int offset)
        {
            var index = _position + offset;
            return index >=_text.Length ? '\0' : _text[index];
        }

        // increments the position in the string
        private void Advance(int amount)
        {
            _position += amount;
        }

        private void LexNumber()
        {
            var dotcount = 0;
            // continues getting the number until there isnt another one to read
            while (char.IsDigit(_current) || _current == '.')
            {
                if (_current == '.')
                {
                    if (dotcount == 1)
                    {
                        break;
                    }
                    dotcount++;
                }
                Advance(1);
            }

            var length = _position - _start;
            var text = _text.Substring(_start, length);
            _type = SyntaxTokenType.NumberToken;

            // if there is a decimal in the number its a float else its an int
            if (_text.Contains('.') && dotcount == 1)
            {
                if (!float.TryParse(text, out var value))
                {
                    Errors.ReportInvalidNumberConversion(text, typeof(float));
                }
                _value = value;   
            }
            else
            {
                if (!int.TryParse(text, out var value))
                {
                    Errors.ReportInvalidNumberConversion(text, typeof(int));
                }

                _value = value;
            }
        }

        private void LexWhiteSpace()
        {
            while (char.IsWhiteSpace(_current))
            {
                Advance(1);
            }
            _type = SyntaxTokenType.WhiteSpaceToken;
        }

        private void LexString()
        {
            // strings with either "" or '' works. might change later
            switch (_current)
            {
                case '"':
                {
                    Advance(1);
                    while (_current != '"')
                    {
                        Advance(1);
                    }

                    if (_current is '"')
                    {
                        Advance(1);
                        var length = _position - _start;
                        var text = _text.Substring(_start, length);
                        _type = SyntaxTokenType.StringToken;
                        _value = text;
                    }
                    break;
                }
                case '\'':
                {
                    Advance(1);
                    while (_current != '\'')
                    {
                        Advance(1);
                    }

                    if (_current is '\'')
                    {
                        Advance(1);
                        var length = _position - _start;
                        var text = _text.Substring(_start, length);
                        _type = SyntaxTokenType.StringToken;
                        _value = text;
                    }
                    break;
                }
            }
        }

        private void LexKeyword()
        {
            while (char.IsLetter(_current))
            {
                Advance(1);
            }

            var length = _position - _start;
            var text = _text.Substring(_start, length);
            _type = SyntaxPrecedence.GetKeywordType(text);
            
            if (_type == SyntaxTokenType.TrueKeyword) _value = true;
            if (_type == SyntaxTokenType.FalseKeyword) _value = false;
            
        }
        
        public SyntaxToken Lex()
        {
            _start = _position;
            _value = null;
            _type = SyntaxTokenType.BadToken;

            switch (_current)
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
                default:
                    if (char.IsDigit(_current))
                    {
                        LexNumber();
                    }
                    else if (char.IsWhiteSpace(_current))
                    {
                        LexWhiteSpace();
                    }
                    else if (char.IsLetter(_current))
                    {
                        LexKeyword();
                    }
                    else if (_current is '"' or '\'')
                    {
                        LexString();
                    }
                    else
                    {
                        Errors.ReportBadCharacter(_current);
                        Advance(1);
                    }

                    break;
            }
            
            var text = SyntaxPrecedence.GetText(_type);
            var length = _position - _start;
            if (text is null)
            {
                text = _text.Substring(_start, length);
            }

            return new SyntaxToken(text, _value, _type);
        }
    }
}
