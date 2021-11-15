using DumbassP.Compiler;

namespace DumbassP
{
    internal static class Program
    {
        private static void Main()
        {
            var repl = new Repl()
            {
                Prompt = "» "
            };
            repl.Run();
        }
    }
}