using ProjectJack.Models;

namespace ProjectJack
{
    internal class Program
    {
        private List<Player> Players = new List<Player>();
        private List<Card> Discard = new List<Card>();

        static void Main(string[] args)
        {
            Console.WriteLine("Project Jack!");

            var p = new Program();

            var fullDeck = p.GenerateDeck();

            fullDeck.Shuffle();

            Console.WriteLine(string.Concat(fullDeck.Cards.Select(s => s.DisplayValue)) + " " + fullDeck.Cards.Count.ToString());

            var splitDecks = p.SplitDecks(fullDeck);

            p.CreatePlayers(splitDecks);

            var currentPlayer = p.Players.First();

            while (p.Players.Any(a => a.HasWon) == false)
            {
                p.TakeTurn(currentPlayer, p.Discard);
                currentPlayer = p.Players.First(f => f != currentPlayer);
            }

            Console.WriteLine(p.Players.First(f => f.HasWon).Name + " Wins!");
        }

        private Deck GenerateDeck()
        {
            var cards = new List<Card>();

            foreach (var suit in Enum.GetValues<ESuit>())
            {
                for (int i = 1; i < 14; i++)
                {
                    var card = new Card(i, suit);
                    cards.Add(card);
                }
            }

            return new Deck(cards);
        }

        private Deck[] SplitDecks(Deck deck)
        {
            var cards1 = new List<Card>();
            var cards2 = new List<Card>();

            for (int i = 0; i < deck.Cards.Count; i = i+2)
            {
                cards1.Add(deck.Cards[i]);
                cards2.Add(deck.Cards[i+1]);
            }

            var deck1 = new Deck(cards1);
            var deck2 = new Deck(cards2);

            Deck[] decks = new Deck[] {deck1, deck2}; 
            return decks;
        }

        private void CreatePlayers(Deck[] decks)
        {
            var player1 = new Player("Kyle", decks[0]);
            var player2 = new Player("Emily", decks[1]);

            this.Players.Add(player1);
            this.Players.Add(player2);
        }

        private void TakeTurn(Player player, List<Card> discard)
        {
            if (discard.Count == 0)
            {
                this.PlayCard(player, discard, 1);
                return;
            }

            var lastCard = discard.Last();

            switch (lastCard.Value)
            {
                case 1:
                    this.PlayCard(player, discard, 4, true);
                    break;

                case 11:
                    this.PlayCard(player, discard, 1, true);
                    break;

                case 12:
                    this.PlayCard(player, discard, 2, true);
                    break;

                case 13:
                    this.PlayCard(player, discard, 3, true);
                    break;

                default:
                    this.PlayCard(player, discard, 1);
                    break;
            }

            return;
        }

        public void PlayCard(Player player, List<Card> discard, int cardsToPlay, bool pickup = false)
        {
            var faceCardValues = new int[] {1, 11, 12, 13};

            for (int i = 0; i < cardsToPlay; i++)
            {
                if (player.Deck.Cards.Count == 0)
                {
                    player.HasWon = true;
                    return;
                }

                var card = player.Deck.Cards[0];
                discard.Add(card);
                player.Deck.Cards.Remove(card);
                Console.WriteLine($"{card.DisplaySuit} {card.DisplayValue} ({player.Name})");

                if (faceCardValues.Contains(card.Value))
                    continue;
            }            

            if (faceCardValues.Contains(discard.Last().Value) == false && pickup)
            {
                discard.Reverse();
                player.Deck.Cards.AddRange(discard);
                Console.WriteLine($"{player.Name} picked up {discard.Count}");
                discard.Clear();
            }
        }
    }
}