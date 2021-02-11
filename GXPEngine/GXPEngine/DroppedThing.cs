using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

namespace GXPEngine
{
    class DroppedThing : Sprite
    {
        private float fallspeed;
        public DroppedThing(float fallspeed, int xPos) : base("circle.png")
        {
            this.fallspeed = fallspeed;
            x = xPos;
            y = 0 - height;
        }
        public void Update()
        {
            y += fallspeed;
            if (y > game.height)
            {
                this.Destroy();
            }
        }
    }
}
