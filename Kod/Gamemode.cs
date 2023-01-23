using System.Threading;
namespace Memoji
{
    
    abstract class GameMode
    {
        public string GameModeName = "";
        public Deck Deck { get; private set; } //Bridge Pattern.
        public Player Player { get; private set; }
        public abstract void Play();
        public virtual bool AllCardsRemoved()
        {
            foreach (var card in Deck.ShuffledDeck)
            {
                if (card.Item1.Removed == false)
                    return false;
            }
            return true;
        }

        public GameMode(string gameModeName, Deck deck, Player player)//Abstract Injected Object Composition.
        {
            GameModeName = gameModeName;
            Deck = deck;
            Player = player;

        }

        public void DealDeck() 
        {//Fungerar som en refresh. Denna köra när något i deck har förändrats, och vi vill visa detta i consolsen.
            
            Console.Clear();
            int start = 0;
            int end = Deck.Coordinates.Grid.Item1;
            

            string alph = "ABCDE";

            Tools.CenterWrite("");
            Tools.CenterWrite("-| MEMOJI |-");
            Tools.CenterWrite("");

            string topline = "       ";
            for (int i = 1; i <= Deck.Coordinates.Grid.Item1; i++)
            {
                topline += $"{i}    ";
            }
            Tools.CenterWrite(topline);
            for (int i = 0; i < Deck.Coordinates.Grid.Item2; i++)
            {
                string line = "";
                line += $" {alph[i]}  ";            //Indexerar rad med Bokstav
                for (int j = start; j < end; j++)   // Bygger rader med kort. Symbol bestäms av värde på Flipped/Removed.
                {
                    if (Deck.ShuffledDeck[j].Item1.Removed == true)
                        line += Deck.ShuffledDeck[j].Item1.CardRemoved;
                    else if (Deck.ShuffledDeck[j].Item1.Flipped == true)
                        line += Deck.ShuffledDeck[j].Item1.CardFront;
                    else
                        line += Deck.ShuffledDeck[j].Item1.CardBack;

                }
                Tools.CenterWrite(line); //Skriver ut rad
                start += Deck.Coordinates.Grid.Item1;
                end += Deck.Coordinates.Grid.Item1;

            }
            Console.WriteLine();
            Console.WriteLine();
            Tools.CenterWrite("........................................");
            Tools.CenterWrite($"Player: {Player.name}    Points:{Player.score}    Moves:{Player.moves}");
            Tools.CenterWrite("........................................");
        }
        public string PickCard()
        {//Asks for input until the input is valid. Returns the input, flip-ready.
            string output = "";
            bool validinput = false;
            System.Console.WriteLine();
            while (!validinput)
            {
                System.Console.WriteLine("Enter a coordinate to pick a card.");
                string? input = Console.ReadLine();
                if (input is not null)
                {
                    DealDeck();
                    input = input.ToUpper();
                    validinput = Tools.ValidateCoord(input, Deck.ShuffledDeck);
                    if (!validinput)
                    {
                        System.Console.WriteLine("(Press any key to try again)");
                        Console.ReadKey();
                        DealDeck();
                    }
                    output = input;
                }

            }
            return output;
        }
        protected void MatchCards(string card1, string card2)
        {
            Player.moves++;
            (string, string) icons_card1_card2 = FindIcons(card1, card2);

            if (icons_card1_card2.Item1 == icons_card1_card2.Item2)
            {
                System.Console.WriteLine($"{card1} and {card2} matches! .");
                Console.WriteLine("(Press any key to continue)");
                Console.ReadKey();
                RemoveCard(card1);
                RemoveCard(card2);
                Player.score++;
            }
            else
            {
                System.Console.WriteLine($"Oh no! {card1} and {card2} does not match! .");
                Console.WriteLine("(Press any key to try again)");
                Console.ReadKey();
                FlipBack(card1);
                FlipBack(card2);
            }

            DealDeck();

            (string, string) FindIcons(string card1, string card2)
            {
                string card1_icon = "";
                string card2_icon = "";
                foreach (var item in Deck.ShuffledDeck)
                {
                    if (item.Item2 == card1)
                        card1_icon = item.Item1.CardFront;

                    if (item.Item2 == card2)
                        card2_icon = item.Item1.CardFront;

                }
                return (card1_icon, card2_icon);
            }

        }
        public void RemoveCard(string card)
        {
            FlipBack(card);
            foreach (var item in Deck.ShuffledDeck)
            {
                if (item.Item2 == card)
                    item.Item1.Removed = true;
            }
        }
        public void Flip(string input)
        {
            foreach (var item in Deck.ShuffledDeck)
            {
                if (item.Item2 == input)
                    item.Item1.Flipped = true;
            }
            // DealDeck();

        }
        public void FlipBack(string card)
        {
            foreach (var item in Deck.ShuffledDeck)
            {
                if (item.Item2 == card)
                    item.Item1.Flipped = false;

            }
        }
    }

    class GMClassic : GameMode
    {
        public GMClassic(Deck deck, Player player) : base("Classic", deck, player) { }
        public override void Play()
        {
            string card1 = "";
            string card2 = "";
            DealDeck();

            while (!AllCardsRemoved())
            {

                card1 = PickCard();
                Flip(card1);
                DealDeck();
                card2 = PickCard();
                Flip(card2);
                DealDeck();
                MatchCards(card1, card2);

            }

        }
    }

    class GMBomb : GameMode
    {
        bool _bombExploded = false;

        public override void Play()
        {
            string card1 = "";
            string card2 = "";
            DealDeck();

            while (!AllCardsRemoved())
            {
                card1 = PickCardOrBomb();
                card2 = PickCardOrBomb();
                if (_bombExploded)
                {
                    card1 = CorrectFirstPick(card2);
                    _bombExploded = false;
                }
                MatchCards(card1, card2);

            }
            RevealDeck();
            DealDeck();

        }
        public override bool AllCardsRemoved()
        {
            foreach (var card in Deck.ShuffledDeck)
            {
                if (card.Item1.Removed == false && card.Item1.Tag == "Regular")
                    return false;
            }

            return true;
        }
        public GMBomb(Deck deck, Player player) : base("Bomb", deck, player) { }

        public string PickCardOrBomb()
        {
            string card = "";
            bool bomb = true;
            while (bomb)
            {

                card = PickCard();
                Flip(card);
                if (IsItBomb(card))
                {

                    Player.moves++;
                    BombAnimation();
                    RemoveCard(card);
                    Deck.ShuffledDeck = Deck.Shuffler(Deck.UnshuffledDeck);
                    Deck.OrderDeck();
                    _bombExploded = true;

                    bomb = true;
                }
                else
                    bomb = false;

                DealDeck();
            }

            return card;
        }
        public bool IsItBomb(string input)
        {
            foreach (var card in Deck.ShuffledDeck)
            {
                if (card.Item2 == input)
                    if (card.Item1.Tag == "Bomb")
                        return true;
            }
            return false;
        }
        public void BombAnimation()
        {
            DealDeck();
            Console.WriteLine();
            Console.Write("Good job, you found .");
            Thread.Sleep(200);
            Console.Write(".");
            Thread.Sleep(200);
            Console.Write(".");
            Thread.Sleep(200);
            Console.Write(".");
            Thread.Sleep(700);
            Console.Write("a bomb??!! 💢 😱 💢");
            Thread.Sleep(1000);
            Console.WriteLine();
            Console.WriteLine("3");
            Thread.Sleep(1000);
            Console.WriteLine("2");
            Thread.Sleep(1000);
            Console.WriteLine("1");
            Thread.Sleep(2000);
            DealDeck();
            Console.WriteLine("💥💥*BOOOOOOM*💥💥");
            Thread.Sleep(1500);
            Console.WriteLine("Oh no! The explosion scrambled all the cards...");
            Thread.Sleep(2000);
        }
        //Om en bomb smäller så blir coordinaten på det först valda kortet inkorrekt. Denna metod rättar till detta fel.
        public string CorrectFirstPick(string card2)
        {
            string output = "";
            foreach (var card in Deck.ShuffledDeck)
            {
                if (card.Item1.Flipped == true && card.Item2 != card2)
                    output = card.Item2;
            }
            return output;
        }
        public void RevealDeck()
        {
            foreach (var card in Deck.ShuffledDeck)
            {
                card.Item1.Flipped = true;
            }
        }
    }


}