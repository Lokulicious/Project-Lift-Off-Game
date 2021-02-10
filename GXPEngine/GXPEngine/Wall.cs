using GXPEngine;
using System.Collections;

namespace GXPEngine
{
    public class Wall : Sprite
    {

        public Wall() : base("square.png")
        {
            SetOrigin(width / 2, height / 2);
        }

        void Update()
        {

        }
    }
}