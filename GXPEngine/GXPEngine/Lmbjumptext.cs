using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;


    class Lmbjumptext : Sprite
    {

        public Lmbjumptext(float textX, float textY) : base("lmbtojump.png")
        {

        SetOrigin(width / 2, height / 2);
        
        this.x = (game.width / 2) - textX;
        this.y = (game.height / 2) - textY;

        }

    }
