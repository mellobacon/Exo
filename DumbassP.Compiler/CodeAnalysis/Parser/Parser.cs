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

                if (token.Type is SyntaxTokenType.WhiteSpaceToken or SyntaxTokenType.BadToken)
                {
                    continue;
                }
                _tokens.Add(token);
                
                if (token.Type == SyntaxTokenType.EofToken)
                {
                    break;
                }
            }
            // add errors to the errors list
            _errors.Concat(lexer.Errors);
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
                int currentPrecedence = SyntaxPrecedence.GetBinaryPrecedence(Current.Type);
                if (currentPrecedence.Equals(0) || currentPrecedence <= precedence)
                {
                    break;
                }

                var op = NextToken();
                var right = ParseBinaryExpression(currentPrecedence);
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
                    bool value = keywordtoken.Type == SyntaxTokenType.TrueKeyword;
                    return new LiteralExpression(keywordtoken, value);
                case SyntaxTokenType.VariableToken:
                    var variabletoken = MatchToken(SyntaxTokenType.VariableToken);
                    return new LiteralExpression(variabletoken, variabletoken.Value);
                default:
                    var number = MatchToken(SyntaxTokenType.NumberToken);
                    return new LiteralExpression(number, number.Value);
            }
        }

        private SyntaxToken Peek(int offset)
        {
            int index = _position + offset;
            return index >= _tokens.Count ? _tokens[^1] : _tokens[index];
        }

        private SyntaxToken MatchToken(SyntaxTokenType type)
        {
            if (Current.Type == type)
            {
                return NextToken();
            }
            _errors.ReportUnExpectedToken(Current.Text, Current.Type, type);
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
