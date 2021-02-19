using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;

namespace GXPEngine
{
    class Spacetocontinue : Sprite
    {

        int blinkCounter;
        float alphaValue;

        public Spacetocontinue(float textX, float textY) : base("space_to_continue.png")
        {
            SetOrigin(width / 2, height / 2);

            this.x = (game.width / 2) - textX;
            this.y = game.height - textY;

            alphaValue = 1;
        }



        void Update()
        {
            alpha = alphaValue;

            blinkCounter += 1;

            if (blinkCounter == 20)
            {
                alphaValue = alphaValue * -1;
                blinkCounter = 0;
            }
        }
    }
}
