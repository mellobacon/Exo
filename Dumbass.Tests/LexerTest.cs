using System.Collections.Generic;
using DumbassP.Compiler.CodeAnalysis.Lexer;
using Xunit;

namespace Dumbass.Tests
{
    public static class LexerTest
    {
        [Theory]
        [MemberData(nameof(GetTokens))]
        public static void Lexer_Can_Lex(string text, SyntaxTokenType type)
        {
            var tokens = new List<SyntaxToken>();
            Lexer lexer = new (text);
            while (true)
            {
                SyntaxToken? token = lexer.Lex();
                if (token.Type == SyntaxTokenType.EofToken)
                {
                    break;
                }
                tokens.Add(token);
            }
            SyntaxToken t = Assert.Single(tokens);
            Assert.Equal(text, t.Text);
            Assert.Equal(type, t.Type);
        }

        private static IEnumerable<object[]> GetTokens()
        {
            var tokens = new (string text, SyntaxTokenType type)[]
            {
                ("1", SyntaxTokenType.NumberToken),
                ("123", SyntaxTokenType.NumberToken),
                ("1.23", SyntaxTokenType.NumberToken),
                ("1_000", SyntaxTokenType.NumberToken),
                ("100__000_______________000", SyntaxTokenType.NumberToken),
                ("+", SyntaxTokenType.PlusToken),
                ("-", SyntaxTokenType.MinusToken),
                ("/", SyntaxTokenType.SlashToken),
                ("*", SyntaxTokenType.StarToken),
                ("%", SyntaxTokenType.ModuloToken),
                ("<", SyntaxTokenType.LessThanToken),
                (">", SyntaxTokenType.MoreThanToken),
                ("<=", SyntaxTokenType.LessEqualsToken),
                (">=", SyntaxTokenType.MoreEqualsToken),
                ("(", SyntaxTokenType.OpenParenToken),
                (")", SyntaxTokenType.ClosedParenToken),
                ("||", SyntaxTokenType.DoublePipeToken),
                ("&&", SyntaxTokenType.DoubleAmpersandToken),
                ("==", SyntaxTokenType.EqualsEqualsToken),
                ("False", SyntaxTokenType.FalseKeyword),
                ("True", SyntaxTokenType.TrueKeyword),
                ("_Test", SyntaxTokenType.VariableToken),
                ("_1000", SyntaxTokenType.VariableToken),
                ("\"string\"", SyntaxTokenType.StringToken),
                ("\"str ing\"", SyntaxTokenType.StringToken),
                ("\"THE FITNESS GRAM PACER TEST IS A-\"", SyntaxTokenType.StringToken),
                ("'MULTI-STEP ACROBATIC TEST THAT INCREASES IN DIFF-'", SyntaxTokenType.StringToken),
                ("\")(**^%^&(*uyGYU5789324U3J\"", SyntaxTokenType.StringToken),
                ("')(**^%^&(*uyGYU5789324U3J'", SyntaxTokenType.StringToken),
                ("1.2.3", SyntaxTokenType.BadToken),
                ("1000_", SyntaxTokenType.BadToken)
            };
            foreach ((string text, SyntaxTokenType type) in tokens)
            {
                yield return new object[] {text, type};
            }
        }
    }
}