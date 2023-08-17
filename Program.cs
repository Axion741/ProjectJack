using ProjectJack.Models;

namespace ProjectJack
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Project Jack!");

            var p = new Program();

            var deck = p.GenerateDeck();

            Console.WriteLine(string.Concat(deck.Select(s => s.DisplayValue)) + " " + deck.Count.ToString());
        }

        private List<Card> GenerateDeck()
        {
            var deck = new List<Card>();

            foreach (var suit in Enum.GetValues<ESuit>())
            {
                for (int i = 1; i < 14; i++)
                {
                    var card = new Card(i, suit);
                    deck.Add(card);
                }
            }

            return deck;
        }
    }
}