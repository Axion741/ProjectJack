namespace ProjectJack.Models { 
    public class Deck {
        public List<Card> Cards { get; set; }

        public Deck(List<Card> cards)
        {
            this.Cards = cards;
        }

        public void Shuffle()
        {
            var r = new Random();

            for(int i = 0; i < this.Cards.Count - 1; i++)
            {
                int pos = r.Next(i, this.Cards.Count); 
                var temp = this.Cards[i];          
                this.Cards[i] = this.Cards[pos];
                this.Cards[pos] = temp;
            }
        }
    }
}