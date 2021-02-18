using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
{
    class HighScores : Sprite
    {
        public HighScores(float posY, string filename) : base(filename)
        {
            SetOrigin(width / 2, height / 2);

            this.x = game.width / 2;
            this.y = (game.height / 2) - posY;

            scale = scale / 2;
        }

    }
}
