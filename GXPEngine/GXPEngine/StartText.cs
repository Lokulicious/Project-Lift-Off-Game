using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
    class StartText : Sprite
    {
    
    public StartText(float textHeight) : base("Start_Text_White.png")
    {
        SetOrigin(width / 2, height / 2);
        this.x = game.width / 2;
        this.y = (game.height / 2) - textHeight;

    }

    }
