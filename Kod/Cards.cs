namespace Memoji
{
    interface ICard
    {
        string Tag { get; set; }
        string CardBack { get; set; }
        string CardFront { get; set; }
        string CardRemoved { get; set; }
        bool Flipped { get; set; }
        bool Removed { get; set; }

    }
    class RegularCard : ICard
    {
        public string Tag { get; set; }
        public string CardBack { get; set; }
        public string CardFront { get; set; }
        public string CardRemoved { get; set; }
        public bool Flipped { get; set; }
        public bool Removed { get; set; }

        public RegularCard(string symbol)
        {
            Tag = "Regular";
            CardBack = "[🎴] ";
            CardFront = $"[{symbol}] ";
            CardRemoved = " 🔳  ";
            Flipped = false;
            Removed = false;

        }
    }

    class BombCard : ICard
    {
        public string Tag { get; set; }
        public string CardBack { get; set; }
        public string CardFront { get; set; }
        public string CardRemoved { get; set; }
        public bool Flipped { get; set; }
        public bool Removed { get; set; }

        public BombCard()
        {
            Tag = "Bomb";
            CardBack = "[🎴] ";
            CardFront = "[💣] ";
            CardRemoved = "[💥] ";
            Flipped = false;
            Removed = false;
        }
    }

    interface IEmojiCollection
    {
        string[] Emojis { get; set; }
    }
    class AnimalsEmoji : IEmojiCollection
    {
        public string[] Emojis { get; set; }
        public AnimalsEmoji()
        {
            Emojis = new string[] { "🐍", "🐖", "🐅", "🦋", "🐀", "🦆" };
        }
    }
    class FoodEmoji : IEmojiCollection
    {
        public string[] Emojis { get; set; }
        public FoodEmoji()
        {
            Emojis = new string[] { "🍔", "🍕", "🥬", "🍓", "🍆", "🥑", };
        }
    }
    class FlowerEmoji : IEmojiCollection
    {
        public string[] Emojis { get; set; }
        public FlowerEmoji()
        {
            Emojis = new string[] { "🌼", "🌸", "🥀", "🌹", "🌺", "💐" };
        }
    }
}