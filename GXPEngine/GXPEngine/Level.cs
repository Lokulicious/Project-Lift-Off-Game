using GXPEngine;
using System.Collections;

namespace GXPEngine
{
    public class Level : GameObject
    {

        float wallLength;

        float wallStartPositionY = 100;
        float wallPositionX = 600;

        float gravity;
        float jumpforce;

        public Level()
        {
            GenerateLevel();
            wallLength = game.height + 50;
            wallPositionX= 600;
        }

        void GenerateLevel()
        {
            for (y = 0; y < 25; y++)
            {
                Wall wallLeft = new Wall();
                wallLeft.y = y * wallLeft.height - wallStartPositionY;
                wallLeft.x = wallPositionX;
                AddChild(wallLeft);
            }            
            
            for (y = 0; y < 25; y++)
            {
                Wall wallRight = new Wall();
                wallRight.y = y * wallRight.height - wallStartPositionY;
                wallRight.x = game.width - wallPositionX;
                AddChild(wallRight);
            }
        }


        void WallJump()
        {
            if (Input.GetKeyDown(Key.SPACE))
            {

            }
        }


        void Update()
        {

        }


    }
}