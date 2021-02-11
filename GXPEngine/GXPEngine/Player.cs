using GXPEngine;
using GXPEngine.Core;
using System.Collections;
using System;
using System.Collections.Generic;

namespace GXPEngine
{
    public class Player : AnimationSprite
    {

        public bool isTouchingWall = false;
        public bool isJumping = false;
        bool startFlip = false;
        GameObject[] collisions;

        bool rightSide = false;
        float jumpforce = 15f;
        int heightClimbed = 0;
        int startFlipTime;

        float mouseX = Input.mouseX;
        float mouseY = Input.mouseY;

        Vector2 mousePos;
        Vector2 playerPos;

        public float passiveMoveSpeed;


        public Player() : base("flip.png",6,1)
        {
            passiveMoveSpeed = 3;
            SetOrigin(width / 2, height / 2);
            this.x = 690;
            this.y = game.height / 2;
            SetCycle(0, 1);

            mousePos = TransformPoint(Input.mouseX, Input.mouseY);
            playerPos = TransformPoint(this.x, this.y);

        }



        void Update()
        {
            collisions = GetCollisions();
            Jump();
            moveDown();
        }



        void moveDown()
        {
            y += passiveMoveSpeed;
        }



         void getJumpDirection()
        {
       //     Vector2 lookDir = ((Vector2)mousePos) - playerPos;
       
        }


        void Jump()
        {
/*            if (Input.GetKeyDown(Key.SPACE) && isJumping == false)
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
            }*/

        }
    /*public void AnimateFlipAndWallHang()
    {
        if (Input.GetKey(Key.SPACE))
        {
            startFlip = true;
            startFlipTime = Time.now;
        }
        foreach (GameObject i in collisions)
        {
            if (startFlip)
            {
                SetCycle(1, 4,20);
                if (Time.now - startFlipTime > 100)
                {
                    startFlip = false;
                }
            }
            else if (i is Wall && rightSide)
            {
                SetCycle(5, 1);
            }
            else if (i is Wall && !rightSide)
            {
                SetCycle(0, 1);
            }
        }
    }*/
    public void gainHeight()
    {
        heightClimbed++;
    }
    public int getHeightClimbed()
    {
        return heightClimbed;
    }


}
}