namespace Snake.Extensions
{
    public static class StringExtension
    {
        public static int HalfLength(this string str)
            => str.Length / 2;

        public static int HalfLength(this string[] str)
            => str.Length / 2;
    }
}
