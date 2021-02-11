using GXPEngine;
using System.Collections;
using System;

namespace GXPEngine
{
    public class Level : GameObject
    {


        Player player = new Player();

        float wallLength;
        float wallStartPositionY = 100;
        float wallPositionX = 600;  //452
        bool firstDropperMade = false;
        bool secondDropperMade = false;
        bool thirdDropperMade = false;
        bool fourthDropperMade = false;
        bool shieldMade = false;
        bool lost = false;
        int dropperTimer;
        bool dropperTimed = false;
        HUD shieldhud = new HUD("No shield", 450, 25);
        HUD scorehud = new HUD("Score: 0", 1750, 25);
        Background bg = new Background();
        public Level()
        {
            GenerateLevel();
            wallLength = game.height + 50;
            wallPositionX = 452;
            Console.WriteLine(wallPositionX);
        }
        void GenerateLevel()
        {
            AddChild(bg);
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
            AddChild(shieldhud);
            AddChild(scorehud);
       
        }

        void Update()
        {
            checkIfLost();
            DisplayHudItems();
            if (player.getHeightClimbed() >= 1500 && !shieldMade)
            {
                AddChild(new Shield(game.width / 2));
                shieldMade = true;
            }
            if (player.getHeightClimbed() >=25 && !firstDropperMade)
            {
                AddChild(new Dropper(2500,4,player,false));
                firstDropperMade = true;
            }
            if (player.getHeightClimbed() >= 1000 && !secondDropperMade)
            {
                AddChild(new Dropper(2000, 2,player,true));
                secondDropperMade = true;
            }
            
            if (player.getHeightClimbed() >= 2000)
            {
                if (!dropperTimed)
                {
                    dropperTimer = Time.now;
                    dropperTimed = true;
                }

                if (!thirdDropperMade)
                {
                    AddChild(new Dropper(2000, 3, player, false));
                    thirdDropperMade = true;
                }
                if (!fourthDropperMade && Time.now - dropperTimer >= 1000)
                {
                    AddChild(new Dropper(2000, 3, player, true));
                    fourthDropperMade = true;
                } 
            }
            
        }


        public void DisplayHudItems()
        {
            if (player.HasShield())
            {
                shieldhud.updateMessage("SHIELD");
            }
            else
                shieldhud.updateMessage("No shield");

            scorehud.updateMessage("Score: " + (player.getClimbedHeight() / 100)*10);
        }

        public void checkIfLost()
        {
            if (player.hitRock())
            {
                lost = true;
            }
        }
        public bool Lost()
        {
            return lost;
        }


    }
}