using GXPEngine;
using System.Collections;
using System;

namespace GXPEngine
{
    public class Level : GameObject
    {


        Player player = new Player();
        Background background = new Background();

        float wallLength;
        float wallStartPositionY = 100;
        float wallPositionX = 600;
        bool firstDropperMade = false;
        bool secondDropperMade = false;

        public Level()
        {
            GenerateLevel();
            wallLength = game.height + 50;

            wallPositionX = 452;
        }

        void GenerateLevel()
        {
            AddChild(background);

            for (int x = 0; x < 5; x++)
            {
                for (int y = 0; y < 9; y++)
                {
                    FilledWall filledwall = new FilledWall();
                    filledwall.x = x * filledwall.width;
                    filledwall.y = y * filledwall.height - 50;
                    AddChild(filledwall);
                }
            }

            for (int x = 0; x < 5; x++)
            {
                for (int y = 0; y < 9; y++)
                {
                    FilledWall filledwall = new FilledWall();
                    filledwall.x = game.width - x * filledwall.width - 50;
                    filledwall.y = y * filledwall.height - 50;
                    AddChild(filledwall);
                }
            }




            for (y = 0; y < 18; y++)
            {
                Wall wallLeft = new Wall(player);
                wallLeft.y = y * wallLeft.height - wallStartPositionY;
                wallLeft.x = wallPositionX;
                wallLeft.scaleX = -1;
                AddChild(wallLeft);
            }            
            
            for (y = 0; y < 18; y++)
            {
                Wall wallRight = new Wall(player);
                wallRight.y = y * wallRight.height - wallStartPositionY;
                wallRight.x = game.width - wallPositionX;
                AddChild(wallRight);
            }


            AddChild(player);



        }



        
        void Update()
        {
           if(player.getHeightClimbed() >=10 && !firstDropperMade)
            {
                AddChild(new Dropper(2500,3,player));
                firstDropperMade = true;
            }
            if (player.getHeightClimbed() >= 60 && !secondDropperMade)
            {
                AddChild(new Dropper(1000, 6,player));
                secondDropperMade = true;
            }

        }


    }
}