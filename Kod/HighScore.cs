using System.Linq;

namespace Memoji
{
    class HighScore
    {//HighScore och GameCustomize delar nästan all funktionalitet. Skulle kunna abstrahera?
        int gameModeChoice;
        string gameMode = "";
        int difficultyChoice;
        string difficulty = "";

        public HighScore()
        {
            gameModeChoice = SelectModeHS();
            difficultyChoice = SelectDifficultyHS();
            PrintHighScore();

        }

        public int SelectModeHS()
        {
            string[] prompts = {
                "",
                "-| MEMOJI |-",
                "",
                "| High Score |",
                "",
                "Select Mode:",
                ""

            };
            string[] options = { "Classic", "Bomb" };
            Menu modeHS = new Menu(prompts, options);
            return modeHS.Run();
        }
        private int SelectDifficultyHS()
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
                $"| High Score | {gameMode} |",
                "",
                "Select difficulty:",
                ""
            };
            string[] options = { "Easy", "Medium", "Hard" };
            Menu difficultyHS = new Menu(prompts, options);
            return difficultyHS.Run();

        }
        private void PrintHighScore()
        {
            difficulty = difficultyChoice switch
            {
                0 => "Easy",
                1 => "Medium",
                2 => "Hard",
                _ => throw new ArgumentOutOfRangeException(nameof(difficultyChoice), $"Not expected value: {difficultyChoice}")
            };
            Console.Clear();
            Tools.CenterWrite("");
            Tools.CenterWrite("-| MEMOJI |-");
            Tools.CenterWrite("");
            Tools.CenterWrite($"| High Score | {gameMode} | {difficulty} |");
            Tools.CenterWrite("");
            SortScore();

        }
        private void SortScore()
        {
            
            Tracker<Score> trackerScore = new Tracker<Score>();
            List<Score> highScores = trackerScore.GetInfo(AppContext.BaseDirectory+"/HighScore");

            highScores = highScores.OrderBy(x => x.moves).ToList();   //Sorterar listan efter moves

            Tools.CenterWrite("Rnk.      Name    Moves");
            Tools.CenterWrite("-----------------------");
            bool anyHighScores = false;
            int i = 1;
            foreach (var score in highScores)
            {
                if (score.gamemodeName == gameMode && score.difficultyName == difficulty)
                {
                    anyHighScores = true;
                    if (i == 6)
                        break;
                    Tools.CenterWrite($"{i}.     {score.playerName}     {score.moves} ");
                    i++;
                }

            }
            if (!anyHighScores)
                Tools.CenterWrite("(There are no high scores for this mode and difficulty. )");
            Console.ReadKey(true);

        }
    }
}