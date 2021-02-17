using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;

    class MenuOverlay : Sprite
    {

    public MenuOverlay() : base("menu_overlay.png")
    {
        SetOrigin(width / 2, height / 2);
        this.x = game.width / 2;
        this.y = game.height / 2 - 18;
        this.alpha = 0.6f;
    }

    }
