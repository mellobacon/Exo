namespace Exo.Compiler.CodeAnalysis.Lexer
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
        HatToken,
        LessThanToken,
        MoreThanToken,
        LessEqualsToken,
        MoreEqualsToken,
        EqualsEqualsToken,
        OpenParenToken,
        ClosedParenToken,

        DoublePipeToken,
        DoubleAmpersandToken,
        
        EqualsToken,
        
        TrueKeyword,
        FalseKeyword,
        VariableKeyword,
        
        BinaryExpression,
        LiteralExpression,
        GroupedExpression,
        AssignmentExpression,

        WhiteSpaceToken,
        BadToken,
        EofToken
    }
}