using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

namespace GXPEngine
{
    class Pickup : AnimationSprite
    {
        public Pickup (string filename,int cols, int rows) : base(filename,cols,rows) { 

        }

        public void Update()
        {
            
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
