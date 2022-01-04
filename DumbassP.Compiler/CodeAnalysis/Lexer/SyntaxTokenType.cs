namespace DumbassP.Compiler.CodeAnalysis.Lexer
{
    public enum SyntaxTokenType
    {
        NumberToken,
        StringToken,
        VariableToken,
        
        PlusToken,
        MinusToken,
        SlashToken,
        StarToken,
        ModuloToken,
        LessThanToken,
        MoreThanToken,
        LessEqualsToken,
        MoreEqualsToken,
        OpenParenToken,
        ClosedParenToken,
        
        DoublePipeToken,
        DoubleAmpersandToken,
        
        TrueKeyword,
        FalseKeyword,
        
        BinaryExpression,
        LiteralExpression,
        GroupedExpression,

        WhiteSpaceToken,
        BadToken,
        EofToken
    }
}