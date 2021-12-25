using System.Collections.Generic;
using DumbassP.Compiler.CodeAnalysis.Evaluator;
using DumbassP.Compiler.CodeAnalysis.Parser;
using Xunit;

namespace Dumbass.Tests
{
    public static class EvaluatorTest
    {
        private static IEnumerable<object[]> GetEvaluations()
        {
            var evals = new (string text, object value)[]
            {
                ("1", 1),
                ("1_000", 1000),
                ("1_000 * 5", 5000),
                ("1 + 2 + 3", 6),
                ("1 + 2 * 3", 7),
                ("(1 + 2) * 3", 9),
                ("1 / 2", 0.5f),
                ("1 % 2", 1),
                ("False", false),
                ("True", true),
                ("False || True", true),
                ("True && True", true),
                ("True && False", false)
            };
            foreach ((string text, object value) in evals)
            {
                yield return new[] { text, value };
            }
        }
        [Theory]
        [MemberData(nameof(GetEvaluations))]
        public static void Evaluator_Outputs_Correct_Value(string text, object value)
        {
            SyntaxTree tree = SyntaxTree.Parse(text);
            Compilation compilation = new Compilation(tree);
            Result result = compilation.Evaluate();
            Assert.Equal(value, result.Value);
        }
    }
}