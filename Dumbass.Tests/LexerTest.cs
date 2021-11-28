using System.Collections.Generic;
using DumbassP.Compiler.CodeAnalysis.Lexer;
using Xunit;

namespace Dumbass.Tests
{
    public static class LexerTest
    {
        [Theory]
        [InlineData("1", SyntaxTokenType.NumberToken)]
        [InlineData("123", SyntaxTokenType.NumberToken)]
        [InlineData("1.25", SyntaxTokenType.NumberToken)]
        [InlineData("+", SyntaxTokenType.PlusToken)]
        [InlineData("-", SyntaxTokenType.MinusToken)]
        [InlineData("/", SyntaxTokenType.SlashToken)]
        [InlineData("*", SyntaxTokenType.StarToken)]
        [InlineData("(", SyntaxTokenType.OpenParenToken)]
        [InlineData(")", SyntaxTokenType.ClosedParenToken)]
        public static void Lexer_Can_Lex(string text, SyntaxTokenType type)
        {
            var tokens = new List<SyntaxToken>();
            Lexer lexer = new (text);
            while (true)
            {
                var token = lexer.Lex();
                if (token.Type == SyntaxTokenType.EofToken)
                {
                    break;
                }
                tokens.Add(token);
            }
            var t = Assert.Single(tokens);
            Assert.Equal(text, t.Text);
            Assert.Equal(type, t.Type);
        }
    }
}