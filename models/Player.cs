namespace ProjectJack.Models { 
    public class Player {
        public Deck Deck { get; set; }
        public string Name { get; set; }
        public bool HasWon { get; set; }

        public Player(string name, Deck deck)
        {
            this.Name = name;
            this.Deck = deck;
            this.HasWon = false;
        }
    }
}