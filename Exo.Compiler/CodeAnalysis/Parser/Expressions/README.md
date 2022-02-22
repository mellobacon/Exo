# BinaryExpression.cs
This defines what parts make up a binary expression

# ExpressionSyntax.cs
So why the empty class? This is just to group each expression together. The parser is not going to be able to account for each expression like that, so this class is just a way to group them together for ease of access

# GroupedExpression.cs and LiteralExpression.cs
Same explanation as the binary expression syntax, just fitted for the exression type
