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
        ShieldParticle sp;

        private int score;

        int noOfShields = 0;
        bool hasDoubleScore = false;
        int startDoubleScore;
        int noOfDashes = 0;
        bool dashStart = false;



        public bool rightSide = false;
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


        Sound jump;
        SoundChannel vfx;


        public Player() : base("new_flip.png",21,1)
        {
            passiveMoveSpeed = 3;
            SetOrigin(width / 2, height / 2);
            this.x = 690;
            this.y = game.height - 550;
            SetCycle(0, 1);

            slideSpeed = 1;

            gravity = 1f;
            jumpForceMultiplier = 0.7f;

            dashForceMultiplier = 1.2f;

            jump = new Sound("jump.mp3", false, false);
            vfx = new SoundChannel(1);

        }



        void Update()
        {
            collisions = GetCollisions();
            getJumpDirection();
            Jump();
            moveDown();
            checkPickups();
            AnimateFlipAndWallHang();
            Animate();
            CollisionReset();
            IsMouseRight();
            checkDash();
            UpdateScore();

            vfx.Volume = 0.1f;
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

        void checkDash()
        {
            if (noOfDashes > 0)
            {
                Dash();
            }
            else
                dash = false;
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

            if (0 < jumpAngle && jumpAngle < 15)
            {
                jumpAngle = 15;
            }
            else if (0 > jumpAngle && jumpAngle > -15)
            {
                jumpAngle = -15;
            }

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
                jump.Play(false, 0);
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
            else
            {
                jumpSpeedX = 0;
                jumpSpeedY = 0;
            }

            if (y < 0)
            {
                y = 0;
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
                dashStart = true;
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

            if (!isJumping)
            {
                dash = false;
                dashSpeedX = 0f;
                dashSpeedY = 0f;
            }

            if (dashTimer == 6)
            {
                dashSpeedX = dashSpeedX / 1.7;
                dashSpeedY = dashSpeedY / 1.7;
            }
            /*if (dashTimer > 4)
            {
                dashSpeedX = dashSpeedX * 0.6;
                dashSpeedY = dashSpeedY * 0.6;
            }*/
            /*else
            {
                dashDrag = 0f;
            }*/


            if (dash && isJumping)
            {
                x += (float)dashSpeedX;
                y -= (float)dashSpeedY;
                dashTimer++;
            }

        }


        public void UpdateScore()
        {
            if (hasDoubleScore)
                score += (1) *5;
            else if (!hasDoubleScore)
                score += (1) ;

            if(Time.now - startDoubleScore > 3000)
            {
                hasDoubleScore = false;
            }
        }
        public int checkScore()
        {
            return score;
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
                SetCycle(4, 12,2);
                if (Time.now - startFlipTime > 100)
                {
                    startFlip = false;
                }
            }
            else if (onWallRight())
            {
                SetCycle(21, 1);
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
                if (i is DroppedThing && noOfShields == 0)
                {
                    if(!((DroppedThing)i).Broken())
                    return true;
                }
                else if (i is DroppedThing && noOfShields > 0)
                {
                    
                    ((DroppedThing)i).Break();
                    noOfShields--;
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
        public void checkPickups()
        {
            foreach (GameObject i in collisions)
            {
                if (i is Shield)
                {
                    if (noOfShields < 3)
                    {
                        
                        i.LateDestroy();
                        noOfShields ++;
                        
                    }
                }
                else if (i is ScoreBall)
                {
                    i.LateDestroy();
                    score += 500;
                }
                else if(i is DoubleScore)
                {
                    i.LateDestroy();
                    hasDoubleScore = true;
                    startDoubleScore = Time.now;
                }
                else if(i is Dash)
                {
                    if (noOfDashes < 6)
                    {
                        i.LateDestroy();
                        noOfDashes += 2;
                    }
                }
                else if(i is Wall && dashStart)
                {
                    dashStart = false;
                    noOfDashes--;
                }
            }
        }
        public bool HasShield()
        {
            if (noOfShields > 0)
            {
                return true;
            }
            else
                return false;
        }
        public int getShields()
        {
            return noOfShields;
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
        public int getDashes()
        {
            return noOfDashes;
        }
       

    }
}