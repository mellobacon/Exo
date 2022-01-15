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
                ("1.5", 1.5f),
                ("0.6 + 0.1", 0.70000005f), // smh float point
                ("1 + 0.5", 1.5f),
                ("1_000", 1000),
                ("1_000 * 5", 5000),
                ("1 + 2 + 3", 6),
                ("1 + 2 * 3", 7),
                ("(1 + 2) * 3", 9),
                ("1 / 2", 0.5f),
                ("1 % 2", 1),
                ("1 < 2", true),
                ("9 > 1", true),
                ("1.5 > 3", false),
                ("0.6 < 0.7", true),
                ("5 <= 5", true),
                ("25 >= 63", false),
                ("(1 + 2) < 5", true),
                ("(1 + 3) * 5 > 2", true),
                ("1 == 2", false),
                ("(5 * 10) / 16 == 3.125", true),
                ("1 == 1", true),
                ("False == False", true),
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
            var compilation = new Compilation(tree);
            Result result = compilation.Evaluate();
            Assert.Equal(value, result.Value);
        }
    }
}