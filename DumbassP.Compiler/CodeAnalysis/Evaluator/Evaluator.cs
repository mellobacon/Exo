using System;
using DumbassP.Compiler.CodeAnalysis.Lexer;
using DumbassP.Compiler.CodeAnalysis.Parser.Expressions;

namespace DumbassP.Compiler.CodeAnalysis.Evaluator
{
    public class Evaluator
    {
        private readonly ExpressionSyntax _root;
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
            if (root is GroupedExpression g)
            {
                return EvaluateExpression(g.Expression);
            }
            
            return root.Type switch
            {
                SyntaxTokenType.BinaryExpression => EvaluateBinaryExpression(root),
                SyntaxTokenType.LiteralExpression => EvaluateLiteralExpression(root),
                _ => null
            };
        }

        private object EvaluateBinaryExpression(ExpressionSyntax root)
        {
            if (root is not BinaryExpression b) return null;
            
            var left = EvaluateExpression(b.Left);
            var op = b.Op;
            var right = EvaluateExpression(b.Right);
                
            // If one of the numbers is a float convert both numbers to floats else make sure both are ints
            // (except division)
            if (left is float || right is float)
            {
                return op.Type switch
                {
                    SyntaxTokenType.PlusToken => Convert.ToSingle(left) + Convert.ToSingle(right),
                    SyntaxTokenType.MinusToken => Convert.ToSingle(left) - Convert.ToSingle(right),
                    SyntaxTokenType.StarToken => Convert.ToSingle(left) * Convert.ToSingle(right),
                    SyntaxTokenType.SlashToken => Convert.ToSingle(left) / Convert.ToSingle(right),
                    _ => throw new Exception($"Unexpected binary operator {b.Op}")
                };
            }

            return op.Type switch
            {
                SyntaxTokenType.PlusToken => (int)left + (int)right,
                SyntaxTokenType.MinusToken => (int)left - (int)right,
                SyntaxTokenType.StarToken => (int)left * (int)right,
                SyntaxTokenType.SlashToken => Convert.ToSingle(left) / Convert.ToSingle(right),
                _ => throw new Exception($"Unexpected binary operator {b.Op}")
            };
        }

        private object EvaluateLiteralExpression(ExpressionSyntax root)
        {
            if (root is not LiteralExpression n) return null;
            if (n.Token.Text == null) return null; // got lazy and dont feel like figuring out how to deal with
                                                   // strings so here you go
            // Return float or int depending on which number it is...this is a bad comment isnt it...
            if (n.Token.Text.Contains('.'))
            {
                return (float)n.Token.Value;
            }
            return (int)n.Token.Value;

        }
    }
}