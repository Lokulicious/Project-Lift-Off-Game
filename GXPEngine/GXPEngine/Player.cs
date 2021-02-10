using GXPEngine;
using System.Collections;
using System;


namespace GXPEngine
{
    public class Player : Sprite
    {

        public bool isTouchingWall = false;
        public bool isJumping = false;

        bool rightSide = false;
        float jumpforce = 15f;

        public Player() : base("player_big.png")
        {
            SetOrigin(width / 2, height / 2);
            this.x = game.width / 2;
            this.y = game.height / 2;
        }



        void Update()
        {
            Jump();
        }


        void Jump()
        {
            if (Input.GetKeyDown(Key.SPACE) && isJumping == false)
            {
                isJumping = true;
                if (x < (game.width / 2))
                {
                    rightSide = false;
                }
                else if (x > (game.width / 2))
                {
                    rightSide = true;
                }
            }

            if (isJumping)
            {
                if (rightSide == false)
                {
                    MoveUntilCollision(jumpforce, 0);
                    x = x + 10;
                }
                else if (rightSide == true)
                {
                    MoveUntilCollision(-jumpforce, 0);
                    x = x - 10;
                }
            }

        }

    }
}