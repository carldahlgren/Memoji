
namespace Memoji
{
    class Game
    {
        Player Player;
        public Deck Deck;
        GameMode Mode;
        
        //Abstract Injected Object Composition
        public Game(GameMode mode)
        {
            Mode = mode;
            Player = mode.Player;
            Deck = mode.Deck;
            Deck.OrderDeck();
            Start();
            GameWon();
        }
        private void Start(){
            Mode.Play();
        }
        void GameWon()
        {
            Tools.CenterWrite("");
            Tools.CenterWrite($"🏆 CONGRATULATIONS {Player.name.ToUpper()} 🏆");
            Tools.CenterWrite("You completed the game!");
            Tools.CenterWrite("");
            Tools.CenterWrite("(Press any key to return to menu.)");
            Console.ReadKey();

            SaveScore();
        }
        void SaveScore()
        {
            Score highscore = new Score(Mode);
            
            Tracker<Score> tracker_score = new Tracker<Score>();
            tracker_score.SaveInfo(highscore, AppContext.BaseDirectory+"/HighScore");
        }

    }

}