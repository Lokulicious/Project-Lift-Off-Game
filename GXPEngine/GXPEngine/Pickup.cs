using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

namespace GXPEngine
{
    class Pickup : Sprite
    {
        public Pickup(string filename) : base(filename)
        {

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
    }
}
