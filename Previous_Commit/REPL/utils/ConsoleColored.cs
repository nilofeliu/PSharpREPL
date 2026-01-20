namespace REPL.utils
{
    internal static class ConsoleColored
    {

        const ConsoleColor _default = ConsoleColor.White;

        public static void Write(string text, ConsoleColor color = _default)
        {
            Console.ForegroundColor = color;
            Console.Write(text);
            Console.ResetColor();
        }

        public static void WriteLine(string text, ConsoleColor color)
        {           
            Write(text, color);           
            Console.Write("\n");
        }

        public static void WriteLine(string text1, ConsoleColor color1, string text2, ConsoleColor color2)
        {
            Write(text1, color1);
            Write(text2, color2);
            Console.Write("\n");
        }

        public static void WriteLine(string text1, ConsoleColor color1,
                                     string text2, ConsoleColor color2,
                                     string text3, ConsoleColor color3)
        {
            Write(text1, color1);
            Write(text2, color2);
            Write(text3, color3);
            Console.Write("\n");
        }

        public static void WriteLine(string text1, ConsoleColor color1,
                                     string text2, ConsoleColor color2,
                                     string text3, ConsoleColor color3,
                                     string text4, ConsoleColor color4)
        {
            Write(text1, color1);
            Write(text2, color2);
            Write(text3, color3);
            Write(text4, color4);
            Console.Write("\n");
        }



    }

}
