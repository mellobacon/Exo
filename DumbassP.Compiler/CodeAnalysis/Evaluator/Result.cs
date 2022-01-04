using DumbassP.Compiler.CodeAnalysis.Errors;
using DumbassP.Compiler.CodeAnalysis.Parser.Expressions;

namespace DumbassP.Compiler.CodeAnalysis.Evaluator
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