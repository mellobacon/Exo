# Lexer.cs
The lexer is where we lex each character in the input. Basically identifying each character and setting it as a token, which is just short for an identifier

# SyntaxPrecedence.cs
This file holds the logic for checking operator precedence in binary expressions, so PEMDAS basically, and the logic for setting the text for each character in the input for visual purposes (youll see this when you print the syntax tree or print what each character is lexed to)

# SyntaxToken.cs
This is where we define what a syntax token is. Basically which parts are required for the syntax token

# SyntaxTokenType.cs
This is where we define what tokens the characters can be identified as
