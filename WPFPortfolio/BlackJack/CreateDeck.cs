using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFPortfolio.BlackJack
{
    class CreateDeck
    {
        public CreateDeck(List<Card> deck)
        {
            List<Card> deckList = new List<Card>();
            for (int j = 2; j <= 14; j++)
            {
                for (int i = 0; i <= 3; i++)
                {
                    deck.Add(new Card((Suits)i, (Values)j));
                }
            }
        }
    }
}
