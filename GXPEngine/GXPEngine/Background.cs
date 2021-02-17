using GXPEngine;
using System.Collections;
using System;


    public class Background : Sprite
    {
    private float scrollspeed;
    private bool startScroll;
    public bool looped = false;
   
    public Background(string filename, int yPos, float scrollspeed, bool startScroll, float a) : base(filename)
    {
        SetOrigin(width / 2, 0);
        x = game.width / 2;
        y = yPos;
        this.scrollspeed = scrollspeed;
        this.startScroll = startScroll;
        alpha = a;
    }
    public Background(string filename, float scrollspeed, bool startScroll, float a) : base(filename)
    {
        SetOrigin(width / 2, 0);
        x = game.width / 2;
        y = -height-20;
        this.scrollspeed = scrollspeed;
        this.startScroll = startScroll;
        alpha = a;
    }



    void Update()
    {
        if (startScroll)
        {
            y += scrollspeed;
        }
        if(y > game.height)
        {
            this.LateDestroy();
        }
    }
    public bool reachedEnd()
    {
        if (y > -50)
        {
            return true;
        }
        else 
            return false;
    }
    public void StartScroll()
    {
        startScroll = true;
    }
    public void fadein()
    {
        if (alpha < 0.99)
            alpha += 0.01f;
        else
            alpha = 1;
    }
    public void fadeout()
    {
        alpha -= 0.01f;
        if (alpha <= 0.01f)
        {
            LateDestroy();
        }
    }
    public void reset()
    {
        y = -height;
    }

}
