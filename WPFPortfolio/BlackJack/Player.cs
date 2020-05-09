using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFPortfolio.BlackJack
{
    class Player : Dealer
    {
        public bool? Winner { get; set; }
        public double BankRoll { get; private set; }
        public string PlayerName { get; private set; }
        public string PlayerPass { get; private set; }
        public double PlayerBet { get; private set; }

        public Player(Random random)
            : base(random)
        {
            Random = random;
            Cards = new List<Card>();
            BankRoll = 1000;
            Winner = false;
        }

        public void Bet(double betAmount)
        {
            PlayerBet = betAmount;
        }

        public void WhoWon(int playerTotal, int dealerTotal)
        {
            if (playerTotal == dealerTotal)
            {
                Winner = null;
            }
            else if (playerTotal < dealerTotal)
                Winner = false;
            else
                Winner = true;

        }
        public void PayOut(double betAmount)
        {
            if (Winner == true)
                BankRoll += betAmount;
            else if (Winner == false)
                BankRoll -= betAmount;
        }
    }
}
