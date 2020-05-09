using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFPortfolio.TypingGame
{
    class Stats
    {

        public int Total { get; private set; }
        public int Missed { get; private set; }
        public int Correct { get; private set; }
        public int Accuracy { get; private set; }

        public Stats()
        {
            Total = 0;
            Missed = 0;
            Correct = 0;
            Accuracy = 0;
        }

        public void Update(bool correctKey)
        {
            Total++;

            if (!correctKey)
            {
                Missed++;
            }
            else
            {
                Correct++;
            }

            Total = Missed + Correct;
            Accuracy = 100 * Correct / Total;
        }

        public void ClearStats()
        {
            Total = 0;
            Missed = 0;
            Correct = 0;
            Accuracy = 0;
        }
    }
}
