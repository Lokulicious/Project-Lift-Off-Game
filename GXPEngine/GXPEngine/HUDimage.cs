using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
{
    class HUDimage : Sprite
    {
        public HUDimage(string filename, int xPos, int yPos): base(filename)
        {
            x = xPos;
            y = yPos;
        }
    }
}
