using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

namespace GXPEngine
{
    class DroppedThing : AnimationSprite
    {
        private Player player;

        private float fallspeed;

        float speedY;
        float gravity;
        float jumpForce;

        int totalFrames;
        int startFrame;

        public DroppedThing(float fallspeed, int xPos, Player player, string filename) : base(filename, 16, 1)
        {
            this.fallspeed = fallspeed;
            x = xPos;
            y = 0 - height;

            jumpForce = 23f;
            gravity = 1f;

            this.player = player;

            this.totalFrames = totalFrames;
            this.startFrame = startFrame;

        }
        public void Update()
        {
            y += fallspeed;
            if (y > game.height)
            {
                this.Destroy();
            }
            Break();
        }

        public void Break()
        {
            SetCycle(0, 16, 1);
            if (player.hitRock())
            {
                Animate();
            }
        }


        /*        void DropJumpMovement()
                {
                    if (Input.GetKeyDown(Key.SPACE) && player.isJumping == false)
                    {
                        speedY = jumpForce; //give jump speed
                        Console.WriteLine("jump start");
                        Console.WriteLine(speedY);
                    }


                    if (player.isJumping)
                    {
                        speedY -= gravity; //apply gravity
                        y += speedY; //apply movement
                    }

                }*/

    }
}
