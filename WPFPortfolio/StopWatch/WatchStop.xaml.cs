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
using System.Diagnostics;
using System.Windows.Threading;

namespace WPFPortfolio.StopWatch
{
    /// <summary>
    /// Interaction logic for WatchStop.xaml
    /// </summary>
    public partial class WatchStop : UserControl
    {
        Stopwatch stopwatch;
        TimeSpan ts;
        DispatcherTimer timer1;
        bool start = false;

        public WatchStop()
        {
            InitializeComponent();
            startButton.Focusable = false;
            clearButton.Focusable = false;

            TimeSpan timespan = new TimeSpan(00, 00, 00);
            timer1 = new DispatcherTimer();
            stopwatch = new Stopwatch();
            timer1.Tick += timer1_Tick;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            ts = stopwatch.Elapsed;
            labelStopWatch.Text = String.Format("{0:00}:{1:00}:{2:00}",
            ts.Hours, ts.Minutes, ts.Seconds);
            labelMiliSeconds.Text = String.Format(".{0:000}", ts.Milliseconds);
        }

        private void startButton_Click(object sender, RoutedEventArgs e)
        {
            
            if (!start)
            {
                start = true;
                timer1.Start();
                stopwatch.Start();
                startButton.Content = "Pause";
            }
            else
            {
                start = false;
                timer1.Stop();
                stopwatch.Stop();
                startButton.Content = "Cont.";
            }
        }

        private void clearButton_Click(object sender, RoutedEventArgs e)
        {
            startButton.Content = "Start";
            stopwatch.Reset();
            timer1.Stop();
            labelStopWatch.Text = "00:00:00";
            labelMiliSeconds.Text = ".000";
            start = false;
        }
    }
}
