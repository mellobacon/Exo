using System;
using DumbassP.Compiler.CodeAnalysis.Lexer;

namespace DumbassP.Compiler.CodeAnalysis.Binding
{
    public class BoundBinaryOperator
    {
        public BinaryOperatorType BoundType { get; }
        private SyntaxTokenType SyntaxTokenType { get; }
        private Type LeftType { get; }
        private Type RightType { get; }
        public Type ResultType { get; }

        private BoundBinaryOperator(BinaryOperatorType boundType, SyntaxTokenType syntaxTokenType, Type leftType, Type rightType, Type resultType)
        {
            BoundType = boundType;
            SyntaxTokenType = syntaxTokenType;
            LeftType = leftType;
            RightType = rightType;
            ResultType = resultType;
        }

        private BoundBinaryOperator(BinaryOperatorType boundType, SyntaxTokenType syntaxTokenType, Type type) : this(
            boundType, syntaxTokenType, type, type, type) {}

        // defines what counts as a valid binary expression...i think
        private static readonly BoundBinaryOperator[] _operations =
        {
            new(BinaryOperatorType.Addition, SyntaxTokenType.PlusToken, typeof(int)),
            new(BinaryOperatorType.Addition, SyntaxTokenType.PlusToken, typeof(float)),
            new(BinaryOperatorType.Addition, SyntaxTokenType.PlusToken, typeof(int), typeof(float), typeof(float)),
            new(BinaryOperatorType.Addition, SyntaxTokenType.PlusToken, typeof(float), typeof(int), typeof(float)),
            
            new(BinaryOperatorType.Subtraction, SyntaxTokenType.MinusToken, typeof(int)),
            new(BinaryOperatorType.Subtraction, SyntaxTokenType.MinusToken, typeof(float)),
            new(BinaryOperatorType.Subtraction, SyntaxTokenType.MinusToken, typeof(int), typeof(float), typeof(float)),
            new(BinaryOperatorType.Subtraction, SyntaxTokenType.MinusToken, typeof(float), typeof(int), typeof(float)),
            
            new(BinaryOperatorType.Multiplication, SyntaxTokenType.StarToken, typeof(int)),
            new(BinaryOperatorType.Multiplication, SyntaxTokenType.StarToken, typeof(float)),
            new(BinaryOperatorType.Multiplication, SyntaxTokenType.StarToken, typeof(int), typeof(float), typeof(float)),
            new(BinaryOperatorType.Multiplication, SyntaxTokenType.StarToken, typeof(float), typeof(int), typeof(float)),
            
            new(BinaryOperatorType.Division, SyntaxTokenType.SlashToken, typeof(int)),
            new(BinaryOperatorType.Division, SyntaxTokenType.SlashToken, typeof(float)),
            new(BinaryOperatorType.Division, SyntaxTokenType.SlashToken, typeof(int), typeof(float), typeof(float)),
            new(BinaryOperatorType.Division, SyntaxTokenType.SlashToken, typeof(float), typeof(int), typeof(float)),
            
            new(BinaryOperatorType.Modulo, SyntaxTokenType.ModuloToken, typeof(int)),
            new(BinaryOperatorType.Modulo, SyntaxTokenType.ModuloToken, typeof(float)),
            
            new(BinaryOperatorType.LessThan, SyntaxTokenType.LessThanToken, typeof(int)),
            new(BinaryOperatorType.LessThan, SyntaxTokenType.LessThanToken, typeof(float)),
            new(BinaryOperatorType.LessThan, SyntaxTokenType.LessThanToken, typeof(int), typeof(float), typeof(float)),
            new(BinaryOperatorType.LessThan, SyntaxTokenType.LessThanToken, typeof(float), typeof(int), typeof(float)),
            
            new(BinaryOperatorType.MoreThan, SyntaxTokenType.MoreThanToken, typeof(int)),
            new(BinaryOperatorType.MoreThan, SyntaxTokenType.MoreThanToken, typeof(float)),
            new(BinaryOperatorType.MoreThan, SyntaxTokenType.MoreThanToken, typeof(int), typeof(float), typeof(float)),
            new(BinaryOperatorType.MoreThan, SyntaxTokenType.MoreThanToken, typeof(float), typeof(int), typeof(float)),

            new(BinaryOperatorType.LessEqual, SyntaxTokenType.LessEqualsToken, typeof(int)),
            new(BinaryOperatorType.LessEqual, SyntaxTokenType.LessEqualsToken, typeof(float)),
            new(BinaryOperatorType.LessEqual, SyntaxTokenType.LessEqualsToken, typeof(int), typeof(float), typeof(float)),
            new(BinaryOperatorType.LessEqual, SyntaxTokenType.LessEqualsToken, typeof(float), typeof(int), typeof(float)),
            
            new(BinaryOperatorType.MoreEqual, SyntaxTokenType.MoreEqualsToken, typeof(int)),
            new(BinaryOperatorType.MoreEqual, SyntaxTokenType.MoreEqualsToken, typeof(float)),
            new(BinaryOperatorType.MoreEqual, SyntaxTokenType.MoreEqualsToken, typeof(int), typeof(float), typeof(float)),
            new(BinaryOperatorType.MoreEqual, SyntaxTokenType.MoreEqualsToken, typeof(float), typeof(int), typeof(float)),
            
            new(BinaryOperatorType.LogicalAnd, SyntaxTokenType.DoubleAmpersandToken, typeof(bool)),
            new(BinaryOperatorType.LogicalOr, SyntaxTokenType.DoublePipeToken, typeof(bool))
        };

        // Get operator based on types supplied
        public static BoundBinaryOperator GetOp(Type left, SyntaxTokenType type, Type right)
        {
            foreach (BoundBinaryOperator op in _operations)
            {
                if (op.LeftType == left && op.SyntaxTokenType == type && op.RightType == right)
                {
                    return op;
                }
            }

            return null;
        }
    }
}