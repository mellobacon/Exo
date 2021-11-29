using System;
using System.Collections;
using System.Collections.Generic;
using DumbassP.Compiler.CodeAnalysis.Lexer;
using DumbassP.Compiler.CodeAnalysis.Parser;
using DumbassP.Compiler.CodeAnalysis.Parser.Expressions;
using Xunit;

namespace Dumbass.Tests
{
    public class ParserTest
    {
        private static IEnumerable<SyntaxTokenType> BinaryOpTypes()
        {
            var types = (SyntaxTokenType[]) Enum.GetValues(typeof(SyntaxTokenType));
            foreach (var type in types)
            {
                if (SyntaxPrecedence.GetBinaryPrecedence(type) > 0)
                {
                    yield return type;
                }
            }
        }

        private static IEnumerable BinaryOpData()
        {
            foreach (var op1 in BinaryOpTypes())
            {
                foreach (var op2 in BinaryOpTypes())
                {
                    yield return new object[] { op1, op2 };
                }
            }
        }

        [Theory]
        [MemberData(nameof(BinaryOpData))]
        public static void Parser_Test()
        {
            
        }
    }
}