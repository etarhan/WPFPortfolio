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

namespace WPFPortfolio.TypingGame
{
    /// <summary>
    /// Interaction logic for TypingGame.xaml
    /// </summary>
    public partial class TypingGame : UserControl
    {
        DispatcherTimer gameTimer;
        DispatcherTimer survivedTimer;
        Random random = new Random();
        Stats stats = new Stats();
        DifficultyLevel currentLevel;
        Difficulty difficulty = new Difficulty();
        int secondsSurvived = 0;


        public TypingGame()
        {
            InitializeComponent();
            gameTimer = new DispatcherTimer();
            gameTimer.Interval = new TimeSpan(0, 0, 0, 0, 800);
            survivedTimer = new DispatcherTimer();
            survivedTimer.Interval = new TimeSpan(0, 0, 1);
            gameTimer.Tick += gameTimer_Tick;
            survivedTimer.Tick += survivedTimer_Tick;
            gameArea.Items.Clear();
            gameArea.Items.Add("Start Game!");
            currentLevel = DifficultyLevel.Normal;
            normalMenu.IsChecked = true;

        }

        void survivedTimer_Tick(object sender, EventArgs e)
        {
            secondsSurvived++;
        }

        void gameTimer_Tick(object sender, EventArgs e)
        {
            gameArea.Items.Add((Key)random.Next(44, 69));
            if (gameArea.Items.Count > 7)
            {
                gameArea.Items.Clear();
                gameArea.Items.Add("Game Over");
                gameTimer.Stop();
                survivedTimer.Stop();
                MessageBox.Show("You survived " + secondsSurvived + " seconds!");
                secondsSurvived = 0;
            }
        }

        private void endMenu_Click(object sender, RoutedEventArgs e)
        {
            gameArea.Items.Clear();
            gameArea.Items.Add("Game Over");
            gameTimer.Stop();
        }

        private void newMenu_Click(object sender, RoutedEventArgs e)
        {
            gameTimer.Stop();

            difficulty.SetDifficulty(currentLevel);
            gameArea.Items.Clear();
            stats.ClearStats();
            gameTimer.Interval = new TimeSpan(0, 0, 0, 0, 800);
            difficultyBar.Value = 0;
            difficultyBar.Maximum = 800;
            gameTimer.Start();
            survivedTimer.Start();
            gameArea.Focus();
        }

        private void gameArea_KeyDown(object sender, KeyEventArgs e)
        {

            if (gameArea.Items.Contains(e.Key))
            {
                gameArea.Items.Remove(e.Key);
                gameArea.Items.Refresh();
                if (gameTimer.Interval > new TimeSpan(0, 0, 0, 0, 400))
                    gameTimer.Interval -= difficulty.Stage1;
                else if (gameTimer.Interval > new TimeSpan(0, 0, 0, 0, 250))
                    gameTimer.Interval -= difficulty.Stage2;
                else if (gameTimer.Interval > new TimeSpan(0, 0, 0, 0, 100))
                    gameTimer.Interval -= difficulty.Stage3;
                difficultyBar.Value = 800 - gameTimer.Interval.Milliseconds;

                stats.Update(true);
            }
            else
            {
                stats.Update(false);
            }

            correctLabel.Content = "Correct: " + stats.Correct;
            missedLabel.Content = "Missed: " + stats.Missed;
            totalLabel.Content = "Total: " + stats.Total;
            accuracyLabel.Content = "Accuracy: " + stats.Accuracy + "%";
        }

        private void easyMenu_Click(object sender, RoutedEventArgs e)
        {
            easyMenu.IsChecked = true;
            normalMenu.IsChecked = false;
            hardMenu.IsChecked = false;
            currentLevel = DifficultyLevel.Easy;
        }

        private void normalMenu_Click(object sender, RoutedEventArgs e)
        {
            easyMenu.IsChecked = false;
            normalMenu.IsChecked = true;
            hardMenu.IsChecked = false;
            currentLevel = DifficultyLevel.Normal;
        }

        private void hardMenu_Click(object sender, RoutedEventArgs e)
        {
            easyMenu.IsChecked = false;
            normalMenu.IsChecked = false;
            hardMenu.IsChecked = true;
            currentLevel = DifficultyLevel.Hard;
        }
    }
}
