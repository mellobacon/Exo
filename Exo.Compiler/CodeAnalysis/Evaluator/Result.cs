using Exo.Compiler.CodeAnalysis.Errors;
using Exo.Compiler.CodeAnalysis.Parser.Expressions;

namespace Exo.Compiler.CodeAnalysis.Evaluator
{
    public class Result
    {
        public readonly ErrorList Errors;
        public readonly object Value;
        public Result(ErrorList errors, object value)
        {
            Errors = errors;
            Value = value;
        }
    }
}