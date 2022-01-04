namespace DumbassP.Compiler.CodeAnalysis.Errors
{
    public class Error
    {
        public TextSpan TextSpan;
        private string _message;
        public Error(TextSpan span, string message)
        {
            TextSpan = span;
            _message = message;
        }

        public override string ToString()
        {
            return _message;
        }
    }
}