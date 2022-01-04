namespace DumbassP.Compiler.CodeAnalysis
{
    public struct TextSpan
    {
        public int Start;
        public int Length;
        public int End => Start + Length;
        public TextSpan(int start, int length)
        {
            Start = start;
            Length = length;
        }
    }
}