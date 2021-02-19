using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;


    class Gameovertext : Sprite
    {

        public Gameovertext(float textX, float textY) : base("gameover.png")
        {

        SetOrigin(width / 2, height / 2);
        
        this.x = (game.width / 2) - textX;
        this.y = (game.height / 2) - textY;

        SetScaleXY(4);

        }

    }
