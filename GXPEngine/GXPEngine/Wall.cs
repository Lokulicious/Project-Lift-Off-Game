using GXPEngine;
using System.Collections;

namespace GXPEngine
{
    public class Wall : Sprite
    {

        float gravity;
        float jumpForce;

        public Wall() : base("GrassWallRightSmall.png")
        {
            SetOrigin(width / 2, 0);
            jumpForce = 0f;
        }



        void Update()
        {
            WallReset();
            WallJumpMovement();
            y += 5;

        }






        void WallReset()
        {
            if (this.y > 1250)
            {
                this.y = y - (height * 12);
            }
        }

        void WallJumpMovement()
        {
            if (Input.GetKeyDown(Key.SPACE))
            {
                jumpForce = 5f;
            }
        }

    }
}