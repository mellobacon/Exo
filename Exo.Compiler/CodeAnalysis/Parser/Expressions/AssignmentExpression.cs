using System.Collections.Generic;
using Exo.Compiler.CodeAnalysis.Lexer;

namespace Exo.Compiler.CodeAnalysis.Parser.Expressions
{
    public class AssignmentExpression : ExpressionSyntax
    {
        private SyntaxToken VariableToken;
        private SyntaxToken EqualsToken;
        private ExpressionSyntax Expression;
        public AssignmentExpression(SyntaxToken variableToken, SyntaxToken equalsToken, ExpressionSyntax expression)
        {
            VariableToken = variableToken;
            Expression = expression;
            EqualsToken = equalsToken;
        }

        public override SyntaxTokenType Type => SyntaxTokenType.AssignmentExpression;
        public override IEnumerable<SyntaxNode> GetChildren()
        {
            yield return VariableToken;
            yield return EqualsToken;
            yield return Expression;
        }
    }
}