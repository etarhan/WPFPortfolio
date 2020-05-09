using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFPortfolio.TypingGame
{

    enum DifficultyLevel
    {
        Easy,
        Normal,
        Hard,
    }
    class Difficulty
    {
        public TimeSpan Stage1 { get; private set; }
        public TimeSpan Stage2 { get; private set; }
        public TimeSpan Stage3 { get; private set; }
        public void SetDifficulty(DifficultyLevel difLevel)
        {
            if (difLevel == DifficultyLevel.Easy)
            {
                Stage1 = new TimeSpan(0, 0, 0, 0, 8);
                Stage2 = new TimeSpan(0, 0, 0, 0, 5);
                Stage3 = new TimeSpan(0, 0, 0, 0, 1);
            }
            else if (difLevel == DifficultyLevel.Normal)
            {
                Stage1 = new TimeSpan(0, 0, 0, 0, 10);
                Stage2 = new TimeSpan(0, 0, 0, 0, 7);
                Stage3 = new TimeSpan(0, 0, 0, 0, 2);
            }
            else if (difLevel == DifficultyLevel.Hard)
            {
                Stage1 = new TimeSpan(0, 0, 0, 0, 13);
                Stage2 = new TimeSpan(0, 0, 0, 0, 8);
                Stage3 = new TimeSpan(0, 0, 0, 0, 3);
            }
        }
    }
}
