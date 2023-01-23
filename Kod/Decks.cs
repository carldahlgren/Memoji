using System.Linq;
namespace Memoji
{
    abstract class Deck
    {
        public string DifficultyName { get; private set; }
        public int DeckSize { get; private set; }
        public (ICard, string)[] ShuffledDeck { get; set; }
        public List<ICard> UnshuffledDeck { get; set; }
        public Coordinates Coordinates { get; protected set; }

        public virtual List<ICard> BuildDeck(int emojiChoice)
        {
            IEmojiCollection emojiCollection = emojiChoice switch
            {
                0 => new AnimalsEmoji(),
                1 => new FoodEmoji(),
                2 => new FlowerEmoji(),
                _ => throw new ArgumentOutOfRangeException(nameof(emojiChoice), $"Not expected value: {emojiChoice}")
            };

            List<ICard> output = new List<ICard>();
            int j = 0;
            for (int i = 0; i < DeckSize; i++)
            {
                RegularCard card = new RegularCard(emojiCollection.Emojis[j]);
                output.Insert(i, card);
                i++;
                card = new RegularCard(emojiCollection.Emojis[j]);
                output.Insert(i, card);
                j++;
            }

            return output;
        }
        
        //Vi vill mappa vårt expression “difficulty” (konstant) till en sträng DifficultyName.
        private string DifficultyIntToString(int difficulty) => difficulty switch 
        {
            0 => "Easy",
            1 => "Medium",
            2 => "Hard",
            _ => throw new ArgumentOutOfRangeException(nameof(difficulty), $"Not expected value: {difficulty}")
        };
        //Vi vill mappa vårt expression “difficulty” (konstant) till en int DeckSize.
        public int DifficultyIntToDeckSizeInt(int difficulty) => difficulty switch
        {
            0 => 4,
            1 => 8,
            2 => 12,
            _ => throw new ArgumentOutOfRangeException(nameof(difficulty), $"Not expected value: {difficulty}")
        };
        public Deck(int difficulty, int emojiChoice)
        {
            DifficultyName = DifficultyIntToString(difficulty);
            DeckSize = DifficultyIntToDeckSizeInt(difficulty);
            UnshuffledDeck = BuildDeck(emojiChoice);
            Coordinates = new Coordinates(difficulty);
            ShuffledDeck = Shuffler(UnshuffledDeck);

        }

        public (ICard, string)[] Shuffler(List<ICard> deck)
        {
            Random shuffle = new Random();
            string[] shuffled_coords = Coordinates.CoordArr.OrderBy(x => shuffle.Next()).ToArray();
            //https://stackoverflow.com/questions/108819/best-way-to-randomize-an-array-with-net 
            (ICard, string)[] output = new (ICard, string)[deck.Count];
            for (int i = 0; i < deck.Count; i++)
            {
                output[i].Item1 = deck[i];
                output[i].Item2 = shuffled_coords[i];
            }
            return output;

        }


        public void OrderDeck()
        {//Lägger korten i sorterad ordning efter koordinater. (A1,A2... C1,C2)
            var linq_query = from letter in ShuffledDeck orderby letter.Item2 select letter;
            int i = 0;
            foreach (var card in linq_query)
            {
                ShuffledDeck[i] = card;
                i++;
            }
        }
    }
    class CardDeck : Deck
    {
        public CardDeck(int difficulty, int emojiChoice) : base(difficulty, emojiChoice) { }
    }

    class BombDeck : Deck
    {
        public BombDeck(int difficulty, int emojiChoice) : base(difficulty, emojiChoice) { }

        public override List<ICard> BuildDeck(int emojiChoice)
        {
            IEmojiCollection emojiCollection = emojiChoice switch
            {
                0 => new AnimalsEmoji(),
                1 => new FoodEmoji(),
                2 => new FlowerEmoji(),
                _ => throw new ArgumentOutOfRangeException(nameof(emojiChoice), $"Not expected value: {emojiChoice}")
            };

            List<ICard> output = new List<ICard>();
            int j = 0;

            for (int i = 0; i < DeckSize - 2; i++)
            {
                RegularCard card = new RegularCard(emojiCollection.Emojis[j]);
                output.Insert(i, card);
                i++;
                card = new RegularCard(emojiCollection.Emojis[j]);
                output.Insert(i, card);
                j++;
            }
            for (int i = DeckSize - 2; i < DeckSize; i++)//Adds two bombs!
            {
                output.Insert(i, new BombCard());
            }

            return output;

        }

    }
}