namespace DumbassP.Compiler.CodeAnalysis.Errors
{
    public class Error
    {
        private string _message;
        public Error(string message)
        {
            _message = message;
        }

        public override string ToString()
        {
            return _message;
        }
    }
}