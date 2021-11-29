# Expressions
This is where we define what expressions we are going to have in the evaluator

# Parser.cs
This is where we parse the tokens. For the parser, it's common to use a recursive decent technique to account for PEMDAS.

# SyntaxNode.cs
This is an abstract class for grouping the parts of the tree together. In the project there are different parts like binary expressions, number expressions, etc. To properly print the tree, we want something that all of them have in common.

# SyntaxTree.cs
This file holds the logic for the abstract syntax tree. It defines the root of the tree, and then parses each node, setting a seperate branch as needed which is set by the Parser.cs file...yknow the recursive thing.
