using System.Collections.Generic;
using System.Linq;
using DumbassP.Compiler.CodeAnalysis.Lexer;

namespace DumbassP.Compiler.CodeAnalysis.Errors
{
    public class ErrorList
    {
        private readonly List<Error> _errors = new();
        
        public void Concat(ErrorList errors)
        {
            _errors.AddRange(errors._errors);
        }

        public bool Any()
        {
            return _errors.ToArray().Any();
        }

        public Error[] ToArray()
        {
            return _errors.ToArray();
        }
        public void ReportInvalidNumberConversion(string num, object type)
        {
            string message = $"Heehoo invalid number: Cannot convert {num} to {type}";
            _errors.Add(new Error(message));
        }

        public void ReportBadCharacter(char character)
        {
            string message = $"Heehoo bad character: {character} is not a valid character";
            _errors.Add(new Error(message));
        }

        public void ReportUnExpectedToken(string token, SyntaxTokenType result, SyntaxTokenType expected)
        {
            string message = $"Heehoo unexpected token {token}: got {result} not {expected}";
            _errors.Add(new Error(message));
        }
    }
}