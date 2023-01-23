
namespace Memoji
{
    class StartScreen
    {
        public void MainMenu()
        {
            bool quit = false;
            while (!quit)
            {
                string[] prompts = {
                "",
                @".___  ___.  _______ .___  ___.   ______          __   __  ",
                @"|   \/   | |   ____||   \/   |  /  __  \        |  | |  | ",
                @"|  \  /  | |  |__   |  \  /  | |  |  |  |       |  | |  | ",
                @"|  |\/|  | |   __|  |  |\/|  | |  |  |  | .--.  |  | |  | ",
                @"|  |  |  | |  |____ |  |  |  | |  `--'  | |  `--'  | |  | ",
                @"|__|  |__| |_______||__|  |__|  \______/   \______/  |__| ",
                "",
                "Welcome To Memoji!",
                "The objective is to match the similar elements.",
                "Select one of the options below:",
                ""};

                string[] options = { "Play", "High Score", "Change Theme", "Exit" };

                Menu MainMenu = new Menu(prompts, options);
                int choice = MainMenu.Run();

                switch (choice)
                {
                    case 0: new GameCustomize(); break;
                    case 1: new HighScore(); break;
                    case 2: Theme.SelectTheme(); break;
                    case 3:
                        Console.Clear();
                        Tools.CenterWrite("");
                        Tools.CenterWrite("Press any key to exit...");
                        Console.ReadKey(true);
                        Tools.CenterWrite("");
                        Tools.CenterWrite("Bye!! 👋");
                        Thread.Sleep(500);
                        quit = true;
                        break;
                }
            }

        }

        class GameCustomize
        {
            int gameModeChoice;
            string gameMode = "";
            int difficultyChoice;
            string difficulty = "";
            int emojiChoice;

            Player player;
            public GameCustomize()
            {
                gameModeChoice = SelectGamemode();
                difficultyChoice = SelectDifficulty();
                emojiChoice = SelectEmojis();
                player = new Player();
                BuildGame();

            }
            public int SelectGamemode()
            {
                string[] prompts = {
                "",
                "-| MEMOJI |-",
                "",
                "| Play |",
                "",
                "Select gamemode:",
                ""
                };
                string[] options = { "Classic", "Bomb" };
                Menu gameModeMenu = new Menu(prompts, options);

                return gameModeMenu.Run();

            }

            public int SelectDifficulty()
            {
                gameMode = gameModeChoice switch
                {
                    0 => "Classic",
                    1 => "Bomb",
                    _ => throw new ArgumentOutOfRangeException(nameof(gameModeChoice), $"Not expected value: {gameModeChoice}")
                };

                string[] prompts = {
                "",
                "-| MEMOJI |-",
                "",
                 $"| Play | {gameMode} |",
                "",
                "Select difficulty",
                ""
                };
                string[] options = { "Easy", "Medium", "Hard" };
                Menu difficultyMenu = new Menu(prompts, options);

                return difficultyMenu.Run();

            }
            public int SelectEmojis()
            {
                difficulty = difficultyChoice switch
                {
                    0 => "Easy",
                    1 => "Medium",
                    2 => "Hard",
                    _ => throw new ArgumentOutOfRangeException(nameof(difficultyChoice), $"Not expected value: {difficultyChoice}")
                };
                string[] prompts = {
                "",
                "-| MEMOJI |-",
                "",
                $"| Play | {gameMode} | {difficulty} |",
                "",
                "Select what emojis you would like to play with:",
                ""
                };
                string[] options = { "Animals 🐍", "Food 🍔", "Flowers 🌺" };
                Menu emojiMenu = new Menu(prompts, options);
                return emojiMenu.Run();
            
            }
            void BuildGame()
            {
                
                Deck deck = gameModeChoice switch
                {
                    0 => new CardDeck(difficultyChoice, emojiChoice),
                    1 => new BombDeck(difficultyChoice, emojiChoice),
                    _ => throw new ArgumentOutOfRangeException(nameof(gameModeChoice), $"Not expected value: {gameModeChoice}")
                };

                GameMode gameMode = gameModeChoice switch
                {
                    0 => new GMClassic(deck, player),
                    1 => new GMBomb(deck, player),
                    _ => throw new ArgumentOutOfRangeException(nameof(gameModeChoice), $"Not expected value: {gameModeChoice}")

                };

                Game game = new Game(gameMode);


            }
        
        }

    }

}



