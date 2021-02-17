using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

namespace GXPEngine
{
    class Pickup : Sprite
    {
        private bool destroyed =false;
        public Pickup(string filename, int xPos) : base(filename)
        {
            x = xPos;
            y = 0 - height;
        }

        public void Update()
        {
            y += 3;
            

        }
        void OnCollision(GameObject other)
        {
            if (other is Player)
            {
                this.LateDestroy();
                Player player = other as Player;
                //player.gainPickupEffect();
            }
        }
        override protected void OnDestroy()
        {
            destroyed = true;
        }
        public bool Destroyed()
        {
            return destroyed;
        }

    }
}
