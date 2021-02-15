using GXPEngine;
using System.Collections;
using System;

namespace GXPEngine
{
    public class Level : GameObject
    {


        Player player = new Player();
        Background background = new Background();
        HUD shieldhud = new HUD("No shield", 450, 25);
        HUD scorehud = new HUD("Score: 0", 1750, 25);
        Arrow arrow;
        Dust dust;

        float wallLength;
        float wallStartPositionY = 100;
        float wallPositionX = 600;
        bool firstDropperMade = false;
        bool secondDropperMade = false;
        bool thirdDropperMade = false;
        bool fourthDropperMade = false;
        bool shieldMade = false;
        bool lost = false;
        int dropperTimer;
        bool dropperTimed = false;

        float arrowRotation;
        
        public Level()
        {
            dust = new Dust(player);
            GenerateLevel();
            wallLength = game.height + 50;
            wallPositionX = 452;
            
        }

        void GenerateLevel()
        {
            AddChild(background);

            for (int x = 0; x < 5; x++)
            {
                for (int y = 0; y < 9; y++)
                {
                    FilledWall filledwall = new FilledWall();
                    filledwall.x = x * filledwall.width;
                    filledwall.y = y * filledwall.height - 50;
                    AddChild(filledwall);
                }
            }

            for (int x = 0; x < 5; x++)
            {
                for (int y = 0; y < 9; y++)
                {
                    FilledWall filledwall = new FilledWall();
                    filledwall.x = game.width - x * filledwall.width - 50;
                    filledwall.y = y * filledwall.height - 50;
                    AddChild(filledwall);
                }
            }




            for (y = 0; y < 18; y++)
            {
                Wall wallLeft = new Wall(player);
                wallLeft.y = y * wallLeft.height - wallStartPositionY;
                wallLeft.x = wallPositionX;
                wallLeft.scaleX = -1;
                AddChild(wallLeft);
            }            
            
            for (y = 0; y < 18; y++)
            {
                Wall wallRight = new Wall(player);
                wallRight.y = y * wallRight.height - wallStartPositionY;
                wallRight.x = game.width - wallPositionX;
                AddChild(wallRight);
            }


            AddChild(player);
            AddChild(dust);
            AddChild(shieldhud);
            AddChild(scorehud);

            arrow = new Arrow(player, player.x, player.y);
            AddChild(arrow);
        }



        
        void Update()
        {
            checkIfLost();
            DisplayHudItems();
            if (player.getHeightClimbed()  > 70 && !shieldMade)
            {
                AddChild(new Shield(game.width / 2));
                shieldMade = true;
            }
            if (player.getHeightClimbed() >= 15 && !firstDropperMade)
            {
                AddChild(new Dropper(2500, 4, player, false));
                firstDropperMade = true;
            }
            if (player.getHeightClimbed() >= 55 && !secondDropperMade)
            {
                AddChild(new Dropper(2000, 2, player, true));
                secondDropperMade = true;
            }
            if (player.getHeightClimbed() >= 75)
            {
                if (!dropperTimed)
                {
                    dropperTimer = Time.now;
                    dropperTimed = true;
                }

                if (!thirdDropperMade)
                {
                    AddChild(new Dropper(4000, 10, player, false));
                    thirdDropperMade = true;
                }
                if (!fourthDropperMade && Time.now - dropperTimer >= 2000)
                {
                    AddChild(new Dropper(4000, 10, player, true));
                    fourthDropperMade = true;
                }
            }
            Arrow(player);
        }

        public void DisplayHudItems()
        {
            if (player.HasShield())
            {
                shieldhud.updateMessage("SHIELD");
            }
            else
                shieldhud.updateMessage("No shield");

            scorehud.updateMessage("Score: " + (player.getHeightClimbed()/10)*10 );
        }
        public void checkIfLost()
        {
            if (player.hitRock())
            {
                lost = true;
            }
            if (!player.onMap())
            {
                lost = true;
            }
        }
        public bool Lost()
        {
            return lost;
        }

        void Arrow(Player player)
        {
            if (!player.isJumping)
            {
                arrow.alpha = 0.8f;
            } else
            {
                arrow.alpha = 0;
            }

            arrow.x = player.x;
            arrow.y = player.y;


            if (!player.rightSide)
            {
                if (player.angle > 0 && player.angle <= 90)
                {
                    arrowRotation = player.angle;
                }
                else if (player.angle < 0)
                {
                    arrowRotation = player.angle + 180;
                }
            } else
            {
                if (player.angle > 0 && player.angle <= 90)
                {
                    arrowRotation = player.angle + 180;
                }
                else if (player.angle < 0)
                {
                    arrowRotation = player.angle;
                }
            }

            arrow.rotation = arrowRotation;
        }


    }
}