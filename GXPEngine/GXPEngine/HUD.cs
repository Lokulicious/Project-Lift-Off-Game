using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace GXPEngine
{
    class HUD : Canvas
    {
        Font Messagefont;
        string message;
        StringFormat drawFormat = new StringFormat();
        int xPos;
        int yPos;
        public HUD(string s, int x, int y) : base(2000,1080,true)
        {
            Messagefont = new Font("Arial", 32f);
            message = s;
            drawFormat.Alignment = StringAlignment.Near;
            scale = 0.8F;
            this.xPos = x;
            this.yPos = y;
        }
        public void Update()
        {
            graphics.Clear(Color.Empty);
            graphics.DrawString(message, Messagefont, Brushes.White, xPos, yPos, drawFormat);
        }
        public void updateMessage(string s)
        {
            message = s;
        }
    }
}
