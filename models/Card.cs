namespace ProjectJack.Models { 
    public class Card {
        public int Value { get; set; }
        public ESuit Suit { get; set; }
        public string DisplayValue { get; set; }

        public Card(int value, ESuit suit)
        {
            this.Value = value;
            this.Suit = suit;

            this.DisplayValue = this.SetDisplayValue(value);
        }

        private string SetDisplayValue(int value)
        {
            if (value < 1)
                return "Joker";

            if (value == 1)
                return "A";

            if (value > 0 && value < 11)
                return value.ToString();

            if (value == 11)
                return "J";

            if (value == 12)
                return "Q";

            if (value == 13)
                return "K";

            return "Joker";
        }
    }
}