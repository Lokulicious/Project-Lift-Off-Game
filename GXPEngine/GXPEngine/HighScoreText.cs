using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
{
    class HighScoreText : Sprite
    {
        public HighScoreText(float textHeight) : base("HighScoreText.png")
        {
            SetOrigin(width / 2, height / 2);

            this.x = game.width / 2;
            this.y = (game.height / 2) - textHeight;
        }

    }
}
