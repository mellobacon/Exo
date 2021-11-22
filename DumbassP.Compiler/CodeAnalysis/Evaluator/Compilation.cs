using DumbassP.Compiler.CodeAnalysis.Errors;
using DumbassP.Compiler.CodeAnalysis.Parser;
using DumbassP.Compiler.CodeAnalysis.Parser.Expressions;

namespace DumbassP.Compiler.CodeAnalysis.Evaluator
{
    public class Compilation
    {
        private object Value;
        private SyntaxTree _tree;
        public Compilation(SyntaxTree tree)
        {
            _tree = tree;
        }
        
        public Result Evaluate()
        {
            Evaluator evaluator = new Evaluator(_tree.Root);
            Value = evaluator.Evaluate();

            if (_tree.Errors.Any())
            {
                return new Result(_tree.Errors, null);
            }
            return new Result(_tree.Errors, Value);
        }
    }
}