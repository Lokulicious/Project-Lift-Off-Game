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
        public Pickup(string filename, int yPos) : base(filename)
        {
            x = GetFallLane();
            y = (0 - height)-yPos;
        }

        public void Update()
        {
            y += 3;
            if (y > game.height)
            {
                this.Destroy();
            }

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
        public int GetFallLane()
        {
            Random rando = new Random();
            int selection = rando.Next(1, 5);

            switch (selection)
            {

                case 1: return 700;
                case 2: return 800;
                case 3: return 900;
                case 4: return 1000;

            }
            return 0;
        }

    }
}
