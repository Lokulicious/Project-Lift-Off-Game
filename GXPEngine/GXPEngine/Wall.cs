using GXPEngine;
using System.Collections;

namespace GXPEngine
{
    public class Wall : Sprite
    {

        public Wall() : base("square.png")
        {
            SetOrigin(width / 2, 0);
        }

        void Update()
        {
            WallSliding();
        }

        void WallSliding()
        {
            this.y -= 0.5f;
        }

    }
}