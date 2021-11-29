This is where all the logic is.

# Code analysis
This holds each piece of the evalutor. From lexing, to parsing, and then evaluating. Also error handling and any other classes that it needs.

# Repl.cs
This holds the implementation of the compiler for use in other programs. In the file, there is a function called `Run()`. This can either be run with no arguments or with one argument. With no arguments, it will run the program via the commandline. The commandline prompt can be customized. If the function has an argument, it will have to be a file or filepath. Then it will run the program on the input in that file. Refer to the Program.cs file in the DumbassP folder for an example.
