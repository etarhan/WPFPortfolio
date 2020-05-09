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
using WPFPortfolio.TextEditor;
using WPFPortfolio.StopWatch;
using WPFPortfolio.TypingGame;
using WPFPortfolio.BlackJack;

namespace WPFPortfolio
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer clockTimer;

        public MainWindow()
        {
            InitializeComponent();
            clockTimer = new DispatcherTimer();
            clockTimer.Start();
            clockTimer.Tick += clockTimer_Tick;
        }

        void clockTimer_Tick(object sender, EventArgs e)
        {
            currentTime.Text = DateTime.Now.ToLongTimeString();
        }

        private void WatchStop_Selected(object sender, RoutedEventArgs e)
        {
            WatchStop watchStop = new WatchStop();
            viewArea.Children.Clear();
            viewArea.Children.Add(watchStop);
            
        }

        private void Home_Selected(object sender, RoutedEventArgs e)
        {
            viewArea.Children.Clear();
        }

        private void topBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void TextEditor_Selected(object sender, RoutedEventArgs e)
        {
            SimpleTextEditor simpleTextEditor = new SimpleTextEditor();
            viewArea.Children.Clear();
            viewArea.Children.Add(simpleTextEditor);


        }

        private void TypingGame_Selected(object sender, RoutedEventArgs e)
        {
            WPFPortfolio.TypingGame.TypingGame typingGame = new WPFPortfolio.TypingGame.TypingGame();
            viewArea.Children.Clear();
            viewArea.Children.Add(typingGame);
        }

        private void Calculator_Selected(object sender, RoutedEventArgs e)
        {
            Calculator.Calculator calculator = new Calculator.Calculator();
            viewArea.Children.Clear();
            viewArea.Children.Add(calculator);
        }

        private void BlackJack_Selected(object sender, RoutedEventArgs e)
        {
            WPFPortfolio.BlackJack.BlackJack blackJack = new WPFPortfolio.BlackJack.BlackJack();
            viewArea.Children.Clear();
            viewArea.Children.Add(blackJack);
        }



    }
}
