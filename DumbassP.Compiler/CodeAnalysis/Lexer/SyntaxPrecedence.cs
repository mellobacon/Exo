namespace DumbassP.Compiler.CodeAnalysis.Lexer
{
    public static class SyntaxPrecedence
    {
        public static int GetBinaryPrecedence(SyntaxTokenType type)
        {
            // follows pemdas
            return type switch
            {
                SyntaxTokenType.SlashToken => 4,
                SyntaxTokenType.StarToken => 4,
                SyntaxTokenType.PlusToken => 3,
                SyntaxTokenType.MinusToken => 3,
                SyntaxTokenType.ModuloToken => 3,
                SyntaxTokenType.DoubleAmpersandToken => 2,
                SyntaxTokenType.DoublePipeToken => 1,
                _ => 0
            };
        }

        public static SyntaxTokenType GetKeywordType(string text)
        {
            return text switch
            {
                "True" => SyntaxTokenType.TrueKeyword,
                "False" => SyntaxTokenType.FalseKeyword,
                _ => SyntaxTokenType.BadToken
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
                SyntaxTokenType.DoublePipeToken => "||",
                SyntaxTokenType.DoubleAmpersandToken => "&&",
                SyntaxTokenType.ModuloToken => "%",
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