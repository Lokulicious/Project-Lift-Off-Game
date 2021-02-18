﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
{
    class GameOverMenu : GameObject
    {



        HighScoreText highScoreText = new HighScoreText(350);
        YourScoreText yourScoreText = new YourScoreText(450, 200);
        HighScores[] highScores = new HighScores[4];
        PlayerIcon playerIcon = new PlayerIcon(200);


        HUD scorehud;

        int _score = 10;

        public GameOverMenu(int score)
        {
            scorehud = new HUD("0", (game.width / 2) + 150, (game.height / 2) - 155, 72);


            _score = score;

            GenerateBackground();


            AddChild(highScoreText);
            AddChild(yourScoreText);
/*
            highScores[1] = new HighScores(100, "hs1.png");
            AddChild(highScores[1]);*/

            for (int i = 0; i < highScores.Length; i++)
            {
                string filename = "hs" + (i + 1) + ".png";

                float posY = 60 - i * 100;
                /*float posY = i * 100;*/
                highScores[i] = new HighScores(posY, filename);
                AddChild(highScores[i]);
            }

            AddChild(playerIcon);
            AddChild(scorehud);

        }


        void Update()
        {
            SetScore();
            Restart();
/*            Console.WriteLine(_score);*/
        }


        void SetScore()
        {
            scorehud.updateMessage(""+_score);
            Console.WriteLine(scorehud);
        }

        void GenerateBackground()
        {
            for (int x = 0; x < 20; x++)
            {
                for (int y = 0; y < 10; y++)
                {
                    FilledWall filledwall = new FilledWall();
                    filledwall.x = x * filledwall.width;
                    filledwall.y = y * filledwall.height - 50;
                    AddChild(filledwall);
                }
            }
        }


        public bool Restart()
        {
            if (Input.GetKeyDown(Key.SPACE))
            {
                return true;
            }
            return false;
        }


    }
}
