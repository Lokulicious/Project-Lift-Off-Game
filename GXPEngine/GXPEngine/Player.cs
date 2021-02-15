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
        GameObject[] collisions = new GameObject[0];

        public bool rightSide = false;
        bool hasShield = false;
        float jumpforce = 15f;
        int heightClimbed = -1;
        int startFlipTime;

        float mouseX;
        float mouseY;

        float doubleX;
        float doubleY;

        
        double tan;
        double jumpRads;
        public double jumpAngle;
        public float angle;

        double jumpSpeedX;
        double jumpSpeedY;
        float gravity;
        float jumpForceMultiplier;

        float playerMoveSpeed;
        float slideSpeed;
        public float passiveMoveSpeed;

        float dashForceMultiplier;
        float dashDrag;

        double dashSpeedX;
        double dashSpeedY;
        public bool dash = false;
        bool mouseRight;
        int dashCooldown;
        int dashTimer;
        

        public Player() : base("jump_animation.png",10,1)
        {
            passiveMoveSpeed = 3;
            SetOrigin(width / 2, height / 2);
            this.x = 690;
            this.y = game.height - 550;
            SetCycle(0, 1);

            slideSpeed = 1;

            gravity = 1f;
            jumpForceMultiplier = 0.7f;


            dashForceMultiplier = 1.3f;

        }



        void Update()
        {
            collisions = GetCollisions();
            getJumpDirection();
            Jump();
            moveDown();
            checkShield();
            AnimateFlipAndWallHang();
            Animate();
            CollisionReset();
            IsMouseRight();
            Dash();
        }



        void moveDown()
        {
            playerMoveSpeed = passiveMoveSpeed;

            if (isTouchingWall)
            {
                playerMoveSpeed = passiveMoveSpeed + slideSpeed;
            }

            y += playerMoveSpeed;
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
            angle = (float)jumpAngle;

           

        }


        void Jump()
        {
            if (Input.GetMouseButtonDown(0) && isJumping == false)
            {
                if (jumpAngle > 0 && !rightSide)
                {
                    jumpSpeedY = (90 - jumpAngle) * jumpForceMultiplier;
                    jumpSpeedX = jumpAngle * jumpForceMultiplier;


                }
                else if (jumpAngle < 0 && !rightSide)
                {
                    jumpSpeedY = (-90 - jumpAngle) * jumpForceMultiplier;
                    jumpSpeedX = -jumpAngle * jumpForceMultiplier;
                }
                else if (jumpAngle < 0 && rightSide)
                {
                    jumpSpeedY = (90 + jumpAngle) * jumpForceMultiplier;
                    jumpSpeedX = jumpAngle * jumpForceMultiplier;
                }
                else if (jumpAngle > 0 && rightSide)
                {
                    jumpSpeedX = -jumpAngle * jumpForceMultiplier;
                    jumpSpeedY = (-90 + jumpAngle) * jumpForceMultiplier;
                }



                /* if (jumpAngle > 0)
                 {
                     if (!rightSide)
                     {
                         jumpSpeedY = (90 - jumpAngle) * jumpForceMultiplier;
                     }
                     else
                     {
                         jumpSpeedY = (90 + jumpAngle) * jumpForceMultiplier;
                     }
                     jumpSpeedX = jumpAngle * jumpForceMultiplier;
                 }
                 else
                 {
                     if (!rightSide)
                     {
                         jumpSpeedY = (-90 - jumpAngle) * jumpForceMultiplier;
                     }
                     else
                     {
                         jumpSpeedY = (90 + jumpAngle) * jumpForceMultiplier;
                     }
                     jumpSpeedX = -jumpAngle * jumpForceMultiplier;
                 }*/
                /*                jumpSpeedX = jumpAngle * jumpForceMultiplier;*/

                isJumping = true;

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


            if (!dash)
            {
                x += (float)jumpSpeedX;
                y -= (float)jumpSpeedY;
            }



        }


        void CollisionReset()
        {
            if (rightSide && !isJumping && this.x > 1220)
            {
                this.x = 1220;
            }
            else if (!rightSide && !isJumping && this.x < 700)
            {
                this.x = 700;
            }
        }

        public bool IsMouseRight()
        {
            if (this.x > Input.mouseX)
            {
                return false;
            }
            return true;
        }



        void Dash()
        {
            if (Input.GetKeyDown(Key.SPACE) && isJumping && dashCooldown == 0)
            {
                if (jumpAngle > 0 && IsMouseRight())
                {
                    dashSpeedY = (90 - jumpAngle) * dashForceMultiplier;
                    dashSpeedX = jumpAngle * dashForceMultiplier;
                }
                else if (jumpAngle < 0 && IsMouseRight())
                {
                    dashSpeedY = (-90 - jumpAngle) * dashForceMultiplier;
                    dashSpeedX = -jumpAngle * dashForceMultiplier;
                }
                else if (jumpAngle < 0 && !IsMouseRight())
                {
                    dashSpeedY = (90 + jumpAngle) * dashForceMultiplier;
                    dashSpeedX = jumpAngle * dashForceMultiplier;
                }
                else if (jumpAngle > 0 && !IsMouseRight())
                {
                    dashSpeedX = -jumpAngle * dashForceMultiplier;
                    dashSpeedY = (-90 + jumpAngle) * dashForceMultiplier;
                }
                dash = true;
                dashTimer = 0;
                dashCooldown = 120;
            }


            dashCooldown--;
            if (dashCooldown < 0)
            {
                dashCooldown = 0;
            }

            if (dashTimer > 30 || !isJumping)
            {
                dash = false;
                dashSpeedX = 0f;
                dashSpeedY = 0f;
            }


            if (dash && isJumping)
            {
                x += (float)dashSpeedX;
                y -= (float)dashSpeedY;
                dashTimer++;
            }
        }




        public void AnimateFlipAndWallHang()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startFlip = true;
            startFlipTime = Time.now;
        }
        
            if (startFlip)
            {
                SetCycle(1, 8,5);
                if (Time.now - startFlipTime > 100)
                {
                    startFlip = false;
                }
            }
            else if (onWallRight())
            {
                SetCycle(9, 1);
            }
            else if (onWallLeft())
            {
                SetCycle(0, 1);
            }
        
    }
    
    public void gainHeight()
    {
        heightClimbed++;
    }
    public int getHeightClimbed()
    {
        return heightClimbed;
    }
        public bool hitRock()
        {
            foreach (GameObject i in collisions)
            {
                if (i is DroppedThing && !hasShield)
                {
                    return true;
                }
                else if (i is DroppedThing && hasShield)
                {
                    i.Destroy();
                    hasShield = false;
                }
            }

            return false;
        }
        public bool onMap()
        {
            if(y > game.height + height / 2)
            {
                return false;
            }
            return true;
        }
        public void checkShield()
        {
            foreach (GameObject i in collisions)
            {
                if (i is Shield)
                {
                    i.LateDestroy();
                    hasShield = true;
                }
            }
        }
        public bool HasShield()
        {
            return hasShield;
        }

        public bool onWallRight()
        {
            foreach (GameObject i in collisions)
            {
                if(i is Wall && rightSide)
                {
                    return true;
                }
            }
            return false;
        }
        public bool onWallLeft()
        {
            foreach (GameObject i in collisions)
            {
                if (i is Wall && !rightSide)
                {
                    return true;
                }
            }
            return false;
        }

    }
}