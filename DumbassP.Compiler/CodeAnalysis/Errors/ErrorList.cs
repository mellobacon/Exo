using System.Collections.Generic;
using System.Linq;

namespace DumbassP.Compiler.CodeAnalysis.Errors
{
    public class ErrorList
    {
        private readonly List<Error> Errors = new();
        
        public void Concat(ErrorList errors)
        {
            Errors.AddRange(errors.Errors);
        }

        public bool Any()
        {
            return Errors.ToArray().Any();
        }

        public Error[] ToArray()
        {
            return Errors.ToArray();
        }
        public void ReportInvalidNumberConversion(string num, object type)
        {
            string message = $"Heehoo invalid number: Cannot convert {num} to {type}";
            Errors.Add(new Error(message));
        }

        public void ReportBadCharacter(char character)
        {
            string message = $"Heehoo bad character: {character} is not a valid character";
            Errors.Add(new Error(message));
        }
    }
}