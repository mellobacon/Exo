using Exo.Compiler;

namespace Exo
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
            repl.Run();
        }
    }
}