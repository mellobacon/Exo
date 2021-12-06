using System.Collections.Generic;
using DumbassP.Compiler.CodeAnalysis.Errors;
using DumbassP.Compiler.CodeAnalysis.Lexer;
using DumbassP.Compiler.CodeAnalysis.Parser.Expressions;

namespace DumbassP.Compiler.CodeAnalysis.Parser
{
    public class Parser
    {
        // keep track of the tokens we parse
        private int _position;
        private readonly List<SyntaxToken> _tokens = new();
        private SyntaxToken Current => Peek(0);
        private readonly ErrorList _errors = new();

        public Parser(string text)
        {
            // Lex the tokens and add them to the tokens list for parsing
            Lexer.Lexer lexer = new Lexer.Lexer(text);
            while (true)
            {
                var token = lexer.Lex();
                if (token.Type == SyntaxTokenType.EofToken)
                {
                    break;
                }
                
                if (token.Type is SyntaxTokenType.WhiteSpaceToken or SyntaxTokenType.BadToken)
                {
                    continue;
                }
                _tokens.Add(token);
            }
            // add errors to the errors list
            _errors.Concat(lexer.Errors);
            _tokens.ToArray();
        }

        public SyntaxTree Parse()
        {
            // recursive decent parser
            var expression = ParseBinaryExpression();
            var eof_token = MatchToken(SyntaxTokenType.EofToken);
            return new SyntaxTree(expression, eof_token, _errors);
        }

        private ExpressionSyntax ParseBinaryExpression(int precedence = 0)
        {
            var left = ParseLiteralExpression();
            while (true)
            {
                int current_precedence = SyntaxPrecedence.GetBinaryPrecedence(Current.Type);
                if (current_precedence.Equals(0) || current_precedence <= precedence)
                {
                    break;
                }

                var op = NextToken();
                var right = ParseBinaryExpression(current_precedence);
                left = new BinaryExpression(left, op, right);
            }

            return left;
        }

        private ExpressionSyntax ParseLiteralExpression()
        {
            switch (Current.Type)
            {
                case SyntaxTokenType.OpenParenToken:
                    var openparen = MatchToken(SyntaxTokenType.OpenParenToken);
                    var expression = ParseBinaryExpression();
                    var closedparen = MatchToken(SyntaxTokenType.ClosedParenToken);
                    return new GroupedExpression(openparen, expression, closedparen);
                case SyntaxTokenType.FalseKeyword:
                case SyntaxTokenType.TrueKeyword:
                    var keywordtoken = NextToken();
                    var value = keywordtoken.Type == SyntaxTokenType.TrueKeyword;
                    return new LiteralExpression(keywordtoken, value);
                default:
                    var number = MatchToken(SyntaxTokenType.NumberToken);
                    return new LiteralExpression(number);
            }
        }

        private SyntaxToken Peek(int offset)
        {
            var index = _position + offset;
            return index >= _tokens.Count ? _tokens[^1] : _tokens[index];
        }

        private SyntaxToken MatchToken(SyntaxTokenType type)
        {
            if (Current.Type == type)
            {
                return NextToken();
            }

            return new SyntaxToken(null, null, type);
        }

        private SyntaxToken NextToken()
        {
            var current = Current;
            _position++;
            return current;
        }
    }
}
