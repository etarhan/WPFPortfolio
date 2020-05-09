using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using Xceed.Wpf.Toolkit;

namespace WPFPortfolio.BlackJack
{
    /// <summary>
    /// Interaction logic for BlackJack.xaml
    /// </summary>
    public partial class BlackJack : UserControl
    {

        CreateDeck newDeck;
        List<Card> deck;
        Player player;
        Dealer dealer;
        Random random;
        public BlackJack()
        {
            InitializeComponent();
            
            random = new Random();
            player = new Player(random);
            dealer = new Dealer(random);
            numUpBet.Text = String.Format("{0}", 0);
            numUpBet.Maximum = player.BankRoll;
            totalCash.Text = player.BankRoll.ToString();
            deck = new List<Card>();
            newDeck = new CreateDeck(deck);
            UpdateLists(deck, listBox1);
        }
        private void UpdateLists(List<Card> deck, ListBox listBox)
        {
            listBox.Items.Clear();
            foreach (Card card in deck)
            {
                if (card.Value == Values.AceEleven || card.Value == Values.AceOne)
                    listBox.Items.Add(String.Format("Ace of {0}", card.Suit.ToString()));
                else
                    listBox.Items.Add(card);
            }
        }

        private void buttonDeal_Click(object sender, EventArgs e)
        {
            if (player.PlayerBet > 0)
            {
                buttonDeal.IsEnabled = false;
                numUpBet.IsEnabled = false;
                buttonHit.IsEnabled = true;
                buttonStay.IsEnabled = true;
                player.DealCard(2, deck);
                dealer.DealCard(1, deck);
                UpdateLists(deck, listBox1);
                UpdateLists(player.Cards, lbxPlayerCards);
                UpdateLists(dealer.Cards, lbxDealerCards);

                labelStatus.Text = String.Format("Player Total: {0}", player.Total);
                labelDealerTotal.Text = String.Format("Dealer Total: {0}", dealer.Total);

                player.DrawCardImages(playerCardsPanel);
                dealer.DrawCardImages(dealerCardsPanel);
                BitmapImage cardImage = new BitmapImage(new Uri("pack://siteoforigin:,,,/CardImages/cardBack.png"));
                Image imageCard = new Image();
                imageCard.Source = cardImage;
                imageCard.Margin = new Thickness(0, 0, 5, 0);
                imageCard.VerticalAlignment = VerticalAlignment.Top;
                imageCard.HorizontalAlignment = HorizontalAlignment.Center;
                dealerCardsPanel.Children.Add(imageCard);
            }
            else
                pleaseBet.Visibility = Visibility.Visible;
                //System.Windows.MessageBox.Show("Please enter a bet!");
            
            if (player.Total == 21)
            {
                YouWin("Black Jack!");
            }

        }


        private void buttonHit_Click(object sender, EventArgs e)
        {
            player.DealCard(1, deck);
            UpdateLists(deck, listBox1);
            UpdateLists(player.Cards, lbxPlayerCards);
            labelStatus.Text = String.Format("Player Total: {0}", player.Total);
            player.DrawCardImages(playerCardsPanel);
            
            if (player.Total > 21)
            {
                YouLose("BUST!");
            }
            
        }


        private void buttonStay_Click(object sender, RoutedEventArgs e)
        {
            while (dealer.Total < player.Total && dealer.Total <= 21)
            {
                dealer.DealCard(1, deck);
            }
            dealer.DrawCardImages(dealerCardsPanel);

            if (player.Total > dealer.Total || dealer.Total > 21)
            {
                YouWin("You Win!");
            }
            else if (dealer.Total == player.Total)
            {
                labelStatus.Text = "Push, all bets returned...";
                totalCash.Text = player.BankRoll.ToString();
                buttonPanel.Children.Clear();
                buttonPanel.Children.Add(buttonNewHand);
                buttonNewHand.Visibility = Visibility.Visible;
            }
            else
            {
                YouLose("House Wins!");
            }

            UpdateLists(deck, listBox1);
            UpdateLists(dealer.Cards, lbxDealerCards);
            
        }

        private void YouWin(string message)
        {
            labelStatus.Text = message;
            player.Winner = true;
            player.PayOut(player.PlayerBet);
            totalCash.Text = player.BankRoll.ToString();
            buttonPanel.Children.Clear();
            buttonPanel.Children.Add(buttonNewHand);
            buttonNewHand.Visibility = Visibility.Visible;
        }
        private void YouLose(string message)
        {
            labelStatus.Text = message;
            player.Winner = false;
            player.PayOut(player.PlayerBet);
            totalCash.Text = player.BankRoll.ToString();
            buttonPanel.Children.Clear();
            buttonPanel.Children.Add(buttonNewHand);
            buttonNewHand.Visibility = Visibility.Visible;
        }

        private void numUpBet_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (numUpBet.Value != null)
                player.Bet((double)numUpBet.Value);
            else
                player.Bet(0);
            pleaseBet.Visibility = Visibility.Hidden;
            totalCash.Text = (player.BankRoll-player.PlayerBet).ToString();
        }

        private void buttonNewHand_Click(object sender, RoutedEventArgs e)
        {
            playerCardsPanel.Children.Clear();
            player.Cards.Clear();
            deck.Clear();
            player.ResetTotal();
            newDeck = new CreateDeck(deck);
            dealerCardsPanel.Children.Clear();
            dealer.Cards.Clear();
            labelStatus.Text = "PLACE BET";
            buttonPanel.Children.Remove(buttonNewHand);
            buttonPanel.Children.Add(buttonDeal);
            buttonPanel.Children.Add(buttonHit);
            buttonPanel.Children.Add(buttonStay);
            buttonDeal.IsEnabled = true;
            buttonHit.IsEnabled = false;
            buttonStay.IsEnabled = false;
            if (player.BankRoll != 0)
            {
                numUpBet.IsEnabled = true;
                numUpBet.Value = player.PlayerBet;     
            }
            else
                labelStatus.Text = "You are broke!";

            if (player.BankRoll <= player.PlayerBet)
            {
                numUpBet.IsEnabled = true;
                numUpBet.Value = player.BankRoll;
            }

            numUpBet.Maximum = player.BankRoll;
            totalCash.Text = (player.BankRoll - player.PlayerBet).ToString();
           
        }


    }
}
