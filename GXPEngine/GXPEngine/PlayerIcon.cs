using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;


    class PlayerIcon : Sprite
    {

        public PlayerIcon(float x) : base("bigger_man.png")
        {

        SetOrigin(width / 2, height);
        SetScaleXY(4);
        
        this.x = game.width - x;
        this.y = game.height;

        }

    }
