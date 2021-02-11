using System;
using System.Collections;
using GXPEngine;

namespace GXPEngine
{
    public class Cursor : Sprite
    {
        public Cursor() : base("player.png")
        {

        }

        void Update()
        {
            this.x = Input.mouseX;
            this.y = Input.mouseY;
        }

    }
}
