using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
{
    class Dust : AnimationSprite
    {
        Player player;
        public Dust(Player player) : base("dustnew.png",9,1)
        {
            this.player = player;
        }
        public void Update()
        {
            if (player.onWallLeft())
            {
                this.x = player.x - 55;
                this.y = player.y-55;
                SetCycle(1, 4);
                Animate();
            }
            else if (player.onWallRight())
            {
                this.x = player.x +20;
                this.y = player.y-55;
                SetCycle(5, 4);
                Animate();
            }
            else
                SetCycle(0, 1);
            
        }
        

    }
}
