namespace Memoji
{
    class Init
    {
        public Init()
        {
            Theme.Background = ConsoleColor.Blue;
            Theme.Foreground = ConsoleColor.Yellow;
            Console.BackgroundColor = Theme.Background;
            Console.ForegroundColor = Theme.Foreground;
            Console.Clear();

            StartScreen start = new StartScreen();
            start.MainMenu();

        }

    }

}