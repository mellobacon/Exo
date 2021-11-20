namespace DumbassP.Compiler.CodeAnalysis.Lexer
{
    public static class SyntaxPrecedence
    {
        
        public static int GetBinaryPrecedence(SyntaxTokenType type)
        {
            return type switch
            {
                SyntaxTokenType.StarToken => 4,
                SyntaxTokenType.SlashToken => 3,
                SyntaxTokenType.PlusToken => 2,
                SyntaxTokenType.MinusToken => 1,
                _ => 0
            };
        }
        
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