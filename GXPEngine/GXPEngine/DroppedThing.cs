﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

namespace GXPEngine
{
    class DroppedThing : AnimationSprite
    {
        private Player player;

        private float fallspeed;


        bool broken = false;

        int brokenTime;

        Sound breakSound;

        bool handled;

        

        public DroppedThing(float fallspeed, int xPos, Player player, string filename) : base(filename, 5, 1)
        {
            this.fallspeed = fallspeed;
            x = xPos;
            y = 0 - height;
            this.player = player;
            
            
        }
        public void Update()
        {
            y += fallspeed;
            if (y > game.height)
            {
                this.Destroy();
            }
            if (broken)
            {
                Animate();
                if (Time.now - brokenTime > 300)
                {
                    this.LateDestroy();
                }
            }
        }
        public bool Handled()
        {
            return handled;
        }
        public void Handle()
        {
            handled = true;
        }
        public void Break()
        {
            broken = true;

            breakSound = new Sound("destroy.mp3");
            breakSound.Play();
            SetCycle(1, 5);
            brokenTime = Time.now;
        }
        public bool Broken()
        {
            return broken;
        }


      

    }
}
