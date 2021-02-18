using GXPEngine;
using System.Collections;
using System;
using System.Collections.Generic;

namespace GXPEngine
{
    public class Level : GameObject
    {


        Player player = new Player();

        Background[] backgrounds = new Background[9];
        List<Background> loopPlanet = new List<Background>();
        List<Background> loopDust = new List<Background>();
        List<Background> loopStars = new List<Background>();
        List<GameObject> foregroundObjects = new List<GameObject>();

        GameObject toAdd = new Pivot();

        HUD shieldhud = new HUD("No shield", 550, 75,64);
        HUDimage shieldHudImg = new HUDimage("shieldIcon.png", 300, 25);

        HUD scorehud = new HUD("Score: 0", 1750, 25,32);

        HUD dashhud = new HUD("Dashes Available: 0", 550, 350,64);
        HUDimage dashHudImg = new HUDimage("dashIcon.png", 300,250);

        Wall wallLeft;
        Wall wallRight;


        Arrow arrow;
        ShieldParticle shieldParticle;

        float wallLength;
        float wallStartPositionY = 100;
        float wallPositionX = 600;
       
        bool firstDropperMade = false;
        bool secondDropperMade = false;
        bool thirdDropperMade = false;
        bool fourthDropperMade = false;
        bool shieldMade = false;
        bool scoreBallMade = false;
        bool scoreDoubleMade = false;
        bool dashMade = false;


        public bool lost = false;
        int dropperTimer;
        bool dropperTimed = false;

        float arrowRotation;


        bool shieldParticleMade = false;

        public int score;
        
        public Level()
        {
            GenerateLevel();
            wallLength = game.height + 50;
            wallPositionX = 452;
        }

        void GenerateLevel()
        {
            loadBackgrounds();

            for (int x = 0; x < 5; x++)
            {
                for (int y = 0; y < 9; y++)
                {
                    FilledWall filledwall = new FilledWall(player);
                    filledwall.x = x * filledwall.width;
                    filledwall.y = y * filledwall.height - 50;
                    AddChild(filledwall);
                }
            }

            for (int x = 0; x < 5; x++)
            {
                for (int y = 0; y < 9; y++)
                {
                    FilledWall filledwall = new FilledWall(player);
                    filledwall.x = game.width - x * filledwall.width - 50;
                    filledwall.y = y * filledwall.height - 50;
                    AddChild(filledwall);
                }
            }




            for (y = 0; y < 18; y++)
            {
                wallLeft = new Wall(player, 1);
                wallLeft.y = y * wallLeft.height - wallStartPositionY;
                wallLeft.x = wallPositionX;
                wallLeft.scaleX = -1;
                AddChild(wallLeft);
            }            
            
            for (y = 0; y < 18; y++)
            {
                wallRight = new Wall(player, 1);
                wallRight.y = y * wallRight.height - wallStartPositionY;
                wallRight.x = game.width - wallPositionX;
                AddChild(wallRight);
            }


            AddChild(player);



            AddChild(shieldhud);
            AddChild(scorehud);
            AddChild(dashhud);
            AddChild(dashHudImg);
            AddChild(shieldHudImg);

            arrow = new Arrow(player, player.x, player.y);
            AddChild(arrow);
        }

        void loadBackgrounds()
        {
            backgrounds[0] = new Background("mountain_art_3.png", -1100,0.5f, true,1);
            backgrounds[1] = new Background("mountain_art_2.png", -1500,1f,true,1);
            backgrounds[2] = new Background("mountain_art_1.png", -1100, 2,true,1);

            backgrounds[3] = new Background("cave_3.png", -1100, 0.5f,false,0);
            backgrounds[4] = new Background("cave_2.png", -1100, 0.6f, false,0);
            backgrounds[5] = new Background("cave_1.png", -1100,  0.7f, false,0);

            backgrounds[6] = new Background("space_1.png", -1100,  0.5f, false,0);
            backgrounds[7] = new Background("space_2.png", -1100,  2, false, 0);
            backgrounds[8] = new Background("space_3.png", -2500,  4, false, 0);
            


            foreach (Background b in backgrounds)
            {
                AddChild(b);
            }
        }




        void Update()
        {
            Console.WriteLine(player.getHeightClimbed());
            switchBG();
            checkIfLost();
            DisplayHudItems();
            adjustDrawOrder();


            if (player.getHeightClimbed() > 5 && !shieldMade)
            {
                toAdd = new Shield(game.width / 2);
                AddChild(toAdd);
                foregroundObjects.Add(toAdd);
                shieldMade = true;
            }
            if (player.getHeightClimbed() > 25 && !dashMade)
            {
                toAdd = new Dash(game.width / 2);
                AddChild(toAdd);
                foregroundObjects.Add(toAdd);
                dashMade = true;
            }
            if (player.getHeightClimbed() > 10 && !scoreBallMade)
            {
                AddChild(new ScoreBall(game.width / 2));
                scoreBallMade = true;
            }
            if (player.getHeightClimbed() > 15 && !scoreDoubleMade)
            {
                AddChild(new DoubleScore(game.width / 2));
                scoreDoubleMade = true;
            }
            if (player.getHeightClimbed() >= 15 && !firstDropperMade)
            {
                AddChild(new Dropper(3000, 4, player, false));
                firstDropperMade = true;
            }
            if (player.getHeightClimbed() >= 55 && !secondDropperMade)
            {
                AddChild(new Dropper(3000, 2, player, true));
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
                    AddChild(new Dropper(4000, 100, player, false));
                    thirdDropperMade = true;
                }
                if (!fourthDropperMade && Time.now - dropperTimer >= 2000)
                {
                    AddChild(new Dropper(4000, 100, player, true));
                    fourthDropperMade = true;
                }
            }

            Arrow(player);
            DisplayShieldParticle();



            score = player.checkScore()/10;
        }

        void switchBG()
        {
            if (backgrounds[0] != null && backgrounds[0].reachedEnd())
            {
                backgrounds[3].StartScroll();
                backgrounds[4].StartScroll();
                backgrounds[5].StartScroll();

                backgrounds[3].fadein();
                backgrounds[4].fadein();
                backgrounds[5].fadein();

                backgrounds[0].fadeout();
                backgrounds[1].fadeout();
                backgrounds[2].fadeout();
            }
            if (backgrounds[3] != null && backgrounds[5].reachedEnd())
            {
                backgrounds[6].StartScroll();
                backgrounds[7].StartScroll();
                backgrounds[8].StartScroll();

                backgrounds[6].fadein();
                backgrounds[7].fadein();
                backgrounds[8].fadein();

                backgrounds[3].fadeout();
                backgrounds[4].fadeout();
                backgrounds[5].fadeout();
                
            }

            //looping space

            if (backgrounds[8].reachedEnd() && !backgrounds[8].looped)
            {
                loopPlanet.Add(new Background("space_3.png", 4, true, 1));
                AddChild(loopPlanet[loopPlanet.Count - 1]);
                game.SetChildIndex(loopPlanet[loopPlanet.Count - 1], 2);
                backgrounds[8].looped = true;
            }

            if (loopPlanet.Count > 0)
            {
                if (loopPlanet[loopPlanet.Count - 1].reachedEnd() )
                {
                    
                    loopPlanet.Add(new Background("space_3.png", 4, true, 1));
                    AddChild(loopPlanet[loopPlanet.Count - 1]);
                    game.SetChildIndex(loopPlanet[loopPlanet.Count - 1], 2);

                }
            }



            if (backgrounds[7].reachedEnd() && !backgrounds[7].looped)
            {
                loopDust.Add(new Background("space_2.png", 2, true, 1));
                AddChild(loopDust[loopDust.Count - 1]);
                game.SetChildIndex(loopDust[loopDust.Count - 1], 1);
                backgrounds[7].looped = true;
            }

            if (loopDust.Count > 0)
            {
                if (loopDust[loopDust.Count - 1].reachedEnd())
                {

                    loopDust.Add(new Background("space_2.png", 2, true, 1));
                    AddChild(loopDust[loopDust.Count - 1]);
                    game.SetChildIndex(loopDust[loopDust.Count - 1], 1);

                }
            }


            if (backgrounds[6].reachedEnd() && !backgrounds[6].looped)
            {
                loopStars.Add(new Background("space_1.png", 0.5f, true, 1));
                AddChild(loopStars[loopStars.Count - 1]);
                game.SetChildIndex(loopStars[loopStars.Count - 1], 0);
                backgrounds[6].looped = true;
            }

            if (loopStars.Count > 0)
            {
                if (loopStars[loopStars.Count - 1].reachedEnd())
                {

                    loopStars.Add(new Background("space_1.png", 0.5f, true, 1));
                    AddChild(loopStars[loopStars.Count - 1]);
                    game.SetChildIndex(loopStars[loopStars.Count - 1], 0);

                }
            }






        }

        void adjustDrawOrder()
        {
            game.SetChildIndex(player, game.GetChildren().Count);
            game.SetChildIndex(arrow, game.GetChildren().Count);
        }
        public void DisplayHudItems()
        {

            shieldhud.updateMessage(" : " + player.getShields());

            scorehud.updateMessage("Score: " + player.checkScore()/10);

            dashhud.updateMessage(" : " + player.getDashes());
        }

        void DisplayShieldParticle()
        {
            if (player.HasShield() && !shieldParticleMade)
            {
                shieldParticle = new ShieldParticle(player);
                AddChild(shieldParticle);
                shieldParticleMade = true;
            }
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