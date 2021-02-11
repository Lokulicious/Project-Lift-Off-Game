using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

namespace GXPEngine
{
    class DroppedThing : Sprite
    {
        private Player player;

        private float fallspeed;

        float speedY;
        float gravity;
        float jumpForce;

        public DroppedThing(float fallspeed, int xPos, Player player, string filename) : base(filename)
        {
            this.fallspeed = fallspeed;
            x = xPos;
            y = 0 - height;

            jumpForce = 23f;
            gravity = 1f;

            this.player = player;
        }
        public void Update()
        {
            y += fallspeed;
            if (y > game.height)
            {
                this.Destroy();
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
