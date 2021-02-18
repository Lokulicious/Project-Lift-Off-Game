using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;

    class StartText : Sprite
    {

    int blinkCounter;

    float alphaValue;

    public StartText(float textHeight) : base("Start_Text_White.png")
    {
        SetOrigin(width / 2, height / 2);
        this.x = game.width / 2;
        this.y = (game.height / 2) - textHeight;

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
