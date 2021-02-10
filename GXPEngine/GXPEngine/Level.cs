using GXPEngine;
using System.Collections;
using System;

namespace GXPEngine
{
    public class Level : GameObject
    {

        float wallLength;

        float wallStartPositionY = 100;

        float wallPositionX = 500;  //452



        public Level()
        {
            GenerateLevel();
            wallLength = game.height + 50;

            wallPositionX = 452;
            Console.WriteLine(wallPositionX);
        }

        void GenerateLevel()
        {
            for (y = 0; y < 13; y++)
            {
                Wall wallLeft = new Wall();
                wallLeft.y = y * wallLeft.height - wallStartPositionY;
                wallLeft.x = wallPositionX;
                wallLeft.scaleX = -1;
                AddChild(wallLeft);
            }            
            
            for (y = 0; y < 13; y++)
            {
                Wall wallRight = new Wall();
                wallRight.y = y * wallRight.height - wallStartPositionY;
                wallRight.x = game.width - wallPositionX;
                AddChild(wallRight);
            }
        }


        void OnCollision(GameObject other)
        {
            if (other is Player)
            {
                y -= 1f;
                Console.WriteLine("sliding");
            }
        }



        void Update()
        {

        }


    }
}