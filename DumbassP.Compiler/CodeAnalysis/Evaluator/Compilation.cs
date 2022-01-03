using DumbassP.Compiler.CodeAnalysis.Binding;
using DumbassP.Compiler.CodeAnalysis.Binding.Expressions;
using DumbassP.Compiler.CodeAnalysis.Errors;
using DumbassP.Compiler.CodeAnalysis.Parser;

namespace DumbassP.Compiler.CodeAnalysis.Evaluator
{
    public class Compilation
    {
        private object _value;
        private readonly SyntaxTree _tree;
        public Compilation(SyntaxTree tree)
        {
            _tree = tree;
        }
        
        public Result Evaluate()
        {
            var binder = new Binder();
            IBoundExpression expression = binder.BindExpression(_tree.Root);

            _tree.Errors.Concat(binder.Errors);
            ErrorList errors = _tree.Errors;
            var evaluator = new Evaluator(expression);
            _value = evaluator.Evaluate();

            if (errors.Any())
            {
                return new Result(errors, null);
            }
            return new Result(errors, _value);
        }
    }
}