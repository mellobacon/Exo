﻿namespace DumbassP.Compiler.CodeAnalysis.Lexer
{
    public enum SyntaxTokenType
    {
        NumberToken,
        StringToken,
        
        PlusToken,
        MinusToken,
        SlashToken,
        StarToken,
        ModuloToken,
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