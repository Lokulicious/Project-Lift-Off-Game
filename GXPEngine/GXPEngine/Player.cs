using GXPEngine;
using GXPEngine.Core;
using System.Collections;
using System.Collections.Generic;
using System;



namespace GXPEngine
{
    public class Player : AnimationSprite
    {

        public bool isTouchingWall = false;
        public bool isJumping = false;
        bool startFlip = false;
        GameObject[] collisions;

        public bool rightSide = false;
        float jumpforce = 15f;
        int heightClimbed = 0;
        int startFlipTime;

        float mouseX;
        float mouseY;

        float doubleX;
        float doubleY;

        
        double tan;
        double jumpRads;
        double jumpAngle;
        float angle;

        double jumpSpeedX;
        double jumpSpeedY;
        float gravity;
        float jumpForceMultiplier;


        public float passiveMoveSpeed;


        public Player() : base("flip.png",6,1)
        {
            passiveMoveSpeed = 0;
            SetOrigin(width / 2, height / 2);
            this.x = game.width - 690;
            this.y = game.height - 150;
            SetCycle(0, 1);

            gravity = 1;
            jumpForceMultiplier = 0.7f;

        }



        void Update()
        {
            collisions = GetCollisions();
            getJumpDirection();
            Jump();
            moveDown();
        }



        void moveDown()
        {
            y += passiveMoveSpeed;
        }



         void getJumpDirection()
        {
            mouseX = Input.mouseX;
            mouseY = Input.mouseY;

            doubleX = this.x - mouseX;
            doubleY = this.y - mouseY;

            tan = doubleX / doubleY;

            jumpRads = Math.Atan(tan);
            jumpAngle = (jumpRads * (180/Math.PI)) * -1;


            Console.WriteLine(jumpAngle);

        }


        void Jump()
        {
            if (Input.GetMouseButtonDown(0) && isJumping == false)
            {
                if (!rightSide)
                {
                    jumpSpeedX = jumpAngle * jumpForceMultiplier;
                    jumpSpeedY = (90 - jumpAngle) * 0.6;
                    isJumping = true;
                }
                else
                {
                    jumpSpeedX = jumpAngle * jumpForceMultiplier;
                    jumpSpeedY = (90 + jumpAngle) * 0.6;
                    isJumping = true;
                }

            }
            if (isJumping)
            {
                jumpSpeedY -= gravity;
            }
            if (isJumping == false)
            {
                jumpSpeedX = 0f;
                jumpSpeedY = 0f;
            }

            x += (float)jumpSpeedX;
            y -= (float)jumpSpeedY;
            



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