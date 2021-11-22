using DumbassP.Compiler.CodeAnalysis.Errors;
using DumbassP.Compiler.CodeAnalysis.Parser.Expressions;

namespace DumbassP.Compiler.CodeAnalysis.Evaluator
{
    public class Result
    {
        public ErrorList Errors;
        public object Value;
        public Result(ErrorList errors, object value)
        {
            Errors = errors;
            Value = value;
        }
    }
}