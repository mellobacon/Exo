using DumbassP.Compiler;

namespace DumbassP
{
    internal static class Program
    {
        private static void Main()
        {
            var repl = new Repl()
            {
                Prompt = "» ",
                MultilinePrompt = "→ "
            };
            //repl.Run(@"../../../TestCode.txt");
            repl.Run();
        }
    }
}