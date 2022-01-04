using System;
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
        public void ReportInvalidNumberConversion(TextSpan span, string num, object type)
        {
            string message = $"Heehoo invalid number: Cannot convert {num} to {type}";
            _errors.Add(new Error(span, message));
        }

        public void ReportBadCharacter(char character, int position)
        {
            TextSpan span = new(position, 1);
            //string message = $"Heehoo bad character: {character} is not a valid character";
            string message = "you dumb fuck";
            _errors.Add(new Error(span, message));
        }

        public void ReportUnExpectedToken(TextSpan span, string token, SyntaxTokenType result, SyntaxTokenType expected)
        {
            string message = $"Heehoo unexpected token <{token}>: got {result} not {expected}";
            _errors.Add(new Error(span, message));
        }

        public void ReportUndefinedBinaryOperator(TextSpan span, Type left, string op, Type right)
        {
            string message = $"Heehoo bad binary operator {op} cant be applied to {left} and {right}";
            _errors.Add(new Error(span, message));
        }
    }
}