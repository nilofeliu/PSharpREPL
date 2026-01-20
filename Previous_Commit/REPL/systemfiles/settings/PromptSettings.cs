namespace REPL.systemfiles.settings
{
    internal static class PromptSettings
    {
        private static bool _hasDate = false;
        private static bool _hasTime = false;
        private static bool _hasUserName = true;
        private static bool _hasEnvironment = true;
        private static bool _isColored = false;
        private static bool _isBold = false;
        private static bool _isItalic = false;
        private static bool _isUnderline = false;
        private static bool _isStrikeout = false;
        private static ConsoleColor _promptColor = ConsoleColor.White;
        private static ConsoleColor _backgroundColor = ConsoleColor.Black;
        private static ConsoleColor userNameColor = ConsoleColor.Cyan;
        private static ConsoleColor _envColor = ConsoleColor.Green;
        private static string _dateTimeFormat = "yyyy-MM-dd";

        internal static bool HasDate => _hasDate;
        internal static bool HasTime { get => _hasTime; set => _hasTime = value; }
        internal static bool HasUserName { get => _hasUserName; set => _hasUserName = value; }
        internal static bool HasEnvironment { get => _hasEnvironment; set => _hasEnvironment = value; }
        internal static bool IsColored { get => _isColored; set => _isColored = value; }
        internal static bool IsBold { get => _isBold; set => _isBold = value; }
        internal static bool IsItalic { get => _isItalic; set => _isItalic = value; }
        internal static bool IsUnderline { get => _isUnderline; set => _isUnderline = value; }
        internal static bool IsStrikeout { get => _isStrikeout; set => _isStrikeout = value; }
        internal static ConsoleColor PromptColor { get; private set; }
        internal static string DateTimeFormat => _dateTimeFormat;
        internal static ConsoleColor UserNameColor { get => userNameColor; set => userNameColor = value; }
        internal static ConsoleColor EnvColor { get => _envColor; set => _envColor = value; }

        internal static void SetDateTimeFormat(string format)
        {
            if (string.IsNullOrEmpty(format))
                return;

            try
            {
                // Test if format is valid
                DateTime.Now.ToString(format);
                _dateTimeFormat = format;
            }
            catch
            {
                Console.WriteLine($"Invalid date format: {format}\n", ConsoleColor.Red);
            }
        }

    }
}
