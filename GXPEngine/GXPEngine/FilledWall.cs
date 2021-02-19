using GXPEngine;
using System.Collections;
using System;

namespace GXPEngine
{
    public class FilledWall : AnimationSprite
    {
        private Player player;
        int currentHeight;
        int stage;


        public FilledWall(Player player) : base("Full_Tile_All.png", 3, 1)
        {
            this.player = player;
        }

        void Update()
        {
            UpdateEnv();
        }


        void UpdateEnv()
        {
            switch (stage)
            {
                case 1:
                    SetFrame(0);
                    break;
                case 2:
                    SetFrame(1);
                    break;
                case 3:
                    SetFrame(2);
                    break;
            }


            currentHeight = player.getHeightClimbed();


            if (currentHeight < 114)
            {
                stage = 1;
            }
            else if (currentHeight < 190)
            {
                stage = 2;
            }
            else if (currentHeight >= 190)
            {
                stage = 3;
            }

        }


    }
}