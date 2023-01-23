namespace Memoji
{
    public static class Theme
    {
        public static string? ThemeName { get; set; }
        public static ConsoleColor Background { get; set; }
        public static ConsoleColor Foreground { get; set; }

        public static void SelectTheme()
        {

            string[] prompts = {
                "",
                "|| Theme ||",
                "",
                "Pick a theme:",
                ""
            };
            string[] options = { "Black & Yellow", "Matrix", "Clouds","Pink", "Back" };

            Menu themeMenu = new Menu(prompts, options);
            int choice = themeMenu.Run();
            switch (choice)
            {
                case 0:
                    ThemeName = "Black/Yellow";
                    Background = ConsoleColor.Yellow;
                    Foreground = ConsoleColor.Black;
                    ChangeTheme();
                    break;
                case 1:
                    ThemeName = "Matrix";
                    Background = ConsoleColor.Black;
                    Foreground = ConsoleColor.Green;
                    ChangeTheme();
                    break;

                case 2:
                    ThemeName = "Clouds";
                    Background = ConsoleColor.Blue;
                    Foreground = ConsoleColor.Yellow;
                    ChangeTheme();
                    break;
                case 3:
                    ThemeName = "Pink";
                    Background = ConsoleColor.DarkMagenta;
                    Foreground = ConsoleColor.Yellow;
                    ChangeTheme();
                    break;
                case 4:
                    //back
                    break;

            }
        }

        public static void ChangeTheme()
        {
            Console.BackgroundColor = Background;
            Console.ForegroundColor = Foreground;
            Console.Clear();
        }
    }






}