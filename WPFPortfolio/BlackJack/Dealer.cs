using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace WPFPortfolio.BlackJack
{
    class Dealer
    {
        public List<Card> Cards { get; protected set; }
        public Random Random { get; protected set; }
        public int Total { get; protected set; }
        
        public Dealer(Random random)
        {
            Random = random;
            Cards = new List<Card>();
        }

        public void ResetTotal()
        {
            Total = 0;
        }
        public void DealCard(int cardsToDeal, List<Card> Deck)
        {
            for (int i = 1; i <= cardsToDeal; i++)
            {
                int randomNumber = Random.Next(0, Deck.Count - 1);
                Cards.Add(Deck[randomNumber]);
                Deck.Remove(Deck[randomNumber]);
                CalcTotal();
                CheckAces();
            }

        }
        public void CheckAces()
        {
            if (Total > 21)
            {
                List<Card> aces = Cards.FindAll(x => x.Value == Values.AceEleven);
                foreach (Card aceEleven in aces)
                {
                    if (Total > 21)
                    {
                        aceEleven.Value = Values.AceOne;
                        CalcTotal();
                    }
                }
            }
        }


        private void CalcTotal()
        {
            Total = 0;
            foreach (Card card in Cards)
            {
                if (card.Value == Values.Jack || card.Value == Values.Queen || card.Value == Values.King)
                {
                    Total += 10;
                }
                else
                {
                    Total += (int)card.Value;
                }
            }
        }

        public void DrawCardImages(StackPanel panel)
        {
            panel.Children.Clear();
            DispatcherTimer timer = new DispatcherTimer();
            foreach (Card card in this.Cards)
            {
                if (card.Value == Values.AceEleven || card.Value == Values.AceOne)
                {
                    BitmapImage cardImage = new BitmapImage(new Uri(String.Format("pack://siteoforigin:,,,/CardImages/Ace_of_{0}.png", card.Suit.ToString())));
                    Image imageCard = new Image();
                    imageCard.Source = cardImage;
                    imageCard.Margin = new Thickness(0, 0, 5, 0);
                    imageCard.VerticalAlignment = VerticalAlignment.Top;
                    imageCard.HorizontalAlignment = HorizontalAlignment.Center;
                    panel.Children.Add(imageCard);
                }
                else
                {
                    BitmapImage cardImage = new BitmapImage(new Uri(String.Format("pack://siteoforigin:,,,/CardImages/{0}_of_{1}.png", card.Value.ToString(), card.Suit.ToString())));
                    Image imageCard = new Image();
                    imageCard.Source = cardImage;
                    imageCard.Margin = new Thickness(0, 0, 5, 0);
                    imageCard.VerticalAlignment = VerticalAlignment.Top;
                    imageCard.HorizontalAlignment = HorizontalAlignment.Center;
                    panel.Children.Add(imageCard);
                }
            }
        }

    }
}
