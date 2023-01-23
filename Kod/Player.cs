namespace Memoji
{
    class Player
    {
        public string name = "";
        public int score = 0;
        public int moves = 0;

        public Player()
        {
            Console.WriteLine("Enter your name:");
            this.name = Tools.ReadString();
            Console.Clear();
            Tools.CenterWrite("");
            Tools.CenterWrite("-| MEMOJI |-");
            Tools.CenterWrite("");
            Tools.CenterWrite("");
            Tools.CenterWrite("");
            Tools.CenterWrite($"🎉 Welcome {name}! 🎉");
            Tools.CenterWrite("🎲  LET'S PLAY MEMOJI!! 🎲");
            Thread.Sleep(1000);
        }
    }
}