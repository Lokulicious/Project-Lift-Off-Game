using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
    

    public class NudinoLogo : Sprite
    {

    public NudinoLogo(float logoHeight) : base("Nudino_Logo.png")
    {
        SetOrigin(width / 2, height / 2);
        this.x = game.width / 2;
        this.y = (game.height / 2) - logoHeight;
    }
        

    }
