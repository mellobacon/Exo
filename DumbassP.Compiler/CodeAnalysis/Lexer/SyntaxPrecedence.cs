namespace DumbassP.Compiler.CodeAnalysis.Lexer
{
    public static class SyntaxPrecedence
    {
        public static string GetText(SyntaxTokenType type)
        {
            return type switch
            {
                SyntaxTokenType.PlusToken => "+",
                SyntaxTokenType.MinusToken => "-",
                SyntaxTokenType.StarToken => "*",
                SyntaxTokenType.SlashToken => "/",
                SyntaxTokenType.OpenParenToken => "(",
                SyntaxTokenType.ClosedParenToken => ")",
                /*
                SyntaxTokenType.OpenBracketToken => "{",
                SyntaxTokenType.ClosedBracketToken => "}",
                SyntaxTokenType.SemicolonToken => ";",
                SyntaxTokenType.BangToken => "!",
                SyntaxTokenType.EqualsToken => "=",
                SyntaxTokenType.LessThanToken => "<",
                SyntaxTokenType.GreaterThanToken => ">",
                SyntaxTokenType.NotEqualsToken => "!=",
                SyntaxTokenType.EqualsEqualsToken => "==",
                SyntaxTokenType.LessEqualsToken => "<=",
                SyntaxTokenType.GreatEqualsToken => ">=",
                SyntaxTokenType.OrPipeToken => "||",
                SyntaxTokenType.AndAmpersandToken => "&&",
                SyntaxTokenType.ModuloToken => "%",
                SyntaxTokenType.StarStarToken => "**",
                SyntaxTokenType.PlusEqualsToken => "+=",
                SyntaxTokenType.MinusEqualsToken => "-=",
                SyntaxTokenType.SlashEqualsToken => "/=",
                SyntaxTokenType.StarEqualsToken => "*=",
                SyntaxTokenType.ModuloEqualsToken => "%=",
                */
                _ => null
            };
        }
    }
}