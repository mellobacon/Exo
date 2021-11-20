using System;
using DumbassP.Compiler.CodeAnalysis.Lexer;
using DumbassP.Compiler.CodeAnalysis.Parser.Expressions;

namespace DumbassP.Compiler.CodeAnalysis.Evaluator
{
    public class Evaluator
    {
        private ExpressionSyntax _root;
        public Evaluator(ExpressionSyntax root)
        {
            _root = root;
        }

        public object Evaluate()
        {
            return EvaluateExpression(_root);
        }

        private object EvaluateExpression(ExpressionSyntax root)
        {
            return root.Type switch
            {
                SyntaxTokenType.BinaryExpression => EvaluateBinaryExpression(root),
                SyntaxTokenType.LiteralExpression => EvaluateLiteralExpression(root),
                _ => null
            };
        }

        private object EvaluateBinaryExpression(ExpressionSyntax root)
        {
            if (root is BinaryExpression b)
            {
                var left = EvaluateExpression(b.Left);
                var op = b.Op;
                var right = EvaluateExpression(b.Right);
                
                // if one of the numbers is an int convert both numbers to ints (except division)
                switch (op.Type)
                {
                    case SyntaxTokenType.PlusToken:
                        return (int)left + (int)right;
                    case SyntaxTokenType.MinusToken:
                        return (int)left - (int)right;
                    case SyntaxTokenType.StarToken:
                        return (int)left * (int)right;
                    case SyntaxTokenType.SlashToken:
                        return (float)left / (float)right;
                    default:
                        throw new Exception($"Unexpected binary operator {b.Op}");
                }
            }

            return null;
        }

        private object EvaluateLiteralExpression(ExpressionSyntax root)
        {
            if (root is LiteralExpression n)
            {
                if (n.Token.Text.Contains('.'))
                {
                    return (float)n.Token.Value;
                }
                return (int)n.Token.Value;
            }

            return null;
        }
    }
}