using GXPEngine;
using System.Collections;
using System;


    public class Background : Sprite
    {
    public Background() : base("bg_big.png")
    {
        SetOrigin(width / 2, 0);
        x = game.width / 2;
        y = -20;
    }

    }
