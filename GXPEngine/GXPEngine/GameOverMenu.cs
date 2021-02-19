using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
{
    class GameOverMenu : GameObject
    {
        private Player player = new Player();
        private Level _level;

        HighScoreText highScoreText = new HighScoreText(350);
        YourScoreText yourScoreText = new YourScoreText(0, 100);
        /*HighScores[] highScores = new HighScores[4];*/
        PlayerIcon playerIcon = new PlayerIcon(200);
        Spacetocontinue spacetocontinue = new Spacetocontinue(0, 80);
        Gameovertext gameovertext = new Gameovertext(0, 400);
        
        HUD scorehud;

        int _score = 10;
        public bool _gameOver = false;


        public GameOverMenu(int score, Level level, bool gameOver)
        {
            scorehud = new HUD("0", (game.width / 2) + 130, (game.height / 2) + 100, 72);

            _level = level;
            _score = score;
            _gameOver = gameOver;

            GenerateBackground();


/*            AddChild(highScoreText);*/
            AddChild(yourScoreText);
/*
            highScores[1] = new HighScores(100, "hs1.png");
            AddChild(highScores[1]);*/

/*            for (int i = 0; i < highScores.Length; i++)
            {
                string filename = "hs" + (i + 1) + ".png";

                float posY = 40 - i * 60;
                highScores[i] = new HighScores(posY, filename);
                AddChild(highScores[i]);
            }*/

            AddChild(playerIcon);
            AddChild(scorehud);
            AddChild(spacetocontinue);
            LateAddChild(gameovertext);


        }


        void Update()
        {
            SetScore();
            Restart();

        }


        void SetScore()
        {
            scorehud.updateMessage(""+_score);
        }

        void GenerateBackground()
        {
            for (int x = 0; x < 20; x++)
            {
                for (int y = 0; y < 10; y++)
                {
                    FilledWall filledwall = new FilledWall(player);
                    filledwall.x = x * filledwall.width;
                    filledwall.y = y * filledwall.height - 50;
                    AddChild(filledwall);
                }
            }
        }


        public bool Restart()
        {
            if (Input.GetKeyDown(Key.SPACE) && _gameOver)
            {
                return true;
            }
            return false;
        }


    }
}
