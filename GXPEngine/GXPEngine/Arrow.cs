using GXPEngine;
using System;
using System.Collections;


namespace GXPEngine
{
    class Arrow : Sprite
    {

        private Player player;

        public Arrow(Player player, float X, float Y) : base("arrow_long.png")
        {
            this.player = player;
            this.x = X;
            this.y = Y;

            SetOrigin(width/2, height);
        }


    }
}
