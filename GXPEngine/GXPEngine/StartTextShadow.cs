using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;


    class StartTextShadow : Sprite
    {

    private Sprite _spriteToAttachTo;
    private int _offsetX;
    private int _offsetY;

    public StartTextShadow(Sprite spriteToAttachTo, int offsetX = 4, int offsetY = 4) : base(spriteToAttachTo.name)
    {
        _spriteToAttachTo = spriteToAttachTo;
        _offsetX = offsetX;
        _offsetY = offsetY;

        SetOrigin(width / 2, height / 2);
        alpha = 0.3f;
        color = 0x00000000;

        adjustPosition();
    }

    private void adjustPosition()
    {
        if (null != _spriteToAttachTo)
        {
            x = _spriteToAttachTo.x + _offsetX;
            y = _spriteToAttachTo.y + _offsetX;
        }
    }


    }
