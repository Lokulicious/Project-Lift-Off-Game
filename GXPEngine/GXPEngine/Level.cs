using GXPEngine;
using System.Collections;
using System;

namespace GXPEngine
{
    public class Level : GameObject
    {


        Player player = new Player();

        float wallLength;
        float wallStartPositionY = 100;
        float wallPositionX = 500;  //452
        bool firstDropperMade = false;
        bool secondDropperMade = false;

        public Level()
        {
            GenerateLevel();
            wallLength = game.height + 50;

            wallPositionX = 452;
            Console.WriteLine(wallPositionX);
        }

        void GenerateLevel()
        {
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