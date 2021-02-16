using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    class ShieldParticle : AnimationSprite
    {
        private Player player;
        public ShieldParticle(Player player) : base("ShieldSmall.png", 8, 1)
        {
            this.player = player;
            SetOrigin(width / 2, height / 2);
        }

        void Update()
        {
            this.x = player.x;
            this.y = player.y - 20;

            SetCycle(1, 8);
            Animate();
        }

    }
}
