using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
{
    class GameData : GameObject
    {
        public int score;
        public int highScore;

        public GameData(int scoreInt, int highscoreInt)
        {
            score = scoreInt;
            highScore = highscoreInt;
        }


        void Update()
        {
            CheckHighscore();
        }


        void CheckHighscore()
        {
            if (score > highScore)
            {
                highScore = score;
            }
        }

    }
}
