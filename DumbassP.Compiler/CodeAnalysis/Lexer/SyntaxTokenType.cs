namespace DumbassP.Compiler.CodeAnalysis.Lexer
{
    public enum SyntaxTokenType
    {
        NumberToken,
        StringToken,
        
        PlusToken,
        MinusToken,
        SlashToken,
        StarToken,
        OpenParenToken,
        ClosedParenToken,
        
        BinaryExpression,
        LiteralExpression,
        GroupedExpression,

        WhiteSpaceToken,
        BadToken,
        EofToken
    }
}