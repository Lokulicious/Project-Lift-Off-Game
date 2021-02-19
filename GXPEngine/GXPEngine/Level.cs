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

        Dropper currentDropper = new Dropper();

        GameObject toAdd = new Pivot();

        HUD shieldhud = new HUD("No shield", 390, 15,48);
        HUDimage shieldHudImg = new HUDimage("shieldIcon.png", 225, 10, 0.6f);

        HUD scorehud = new HUD(" ", 2200, 15,38);
        HUDimage scoreHudImg = new HUDimage("scoreHud.png", 1550, 10, 0.5f);

        HUD dashhud = new HUD("Dashes Available: 0", 140, 20,48);
        HUDimage dashHudImg = new HUDimage("dashIcon.png", 25,10,0.6f);

        Wall wallLeft;
        Wall wallRight;

        Cursor cursor;

        Arrow arrow;
        ShieldParticle shieldParticle;

        float wallLength;
        float wallStartPositionY = 100;
        float wallPositionX = 600;
       
        bool firstDropperMade = false;
        bool secondDropperMade = false;
        bool thirdDropperMade = false;
        bool fourthDropperMade = false;
        bool fifthDropperMade = false;

        bool shieldMade = false;
        bool scoreBallMade = false;
        bool scoreDoubleMade = false;
        bool dashMade = false;
        bool loopedPickupMade = true;
        bool firstLooper = false;
        bool canset = false;
        bool shieldParticleMade = false;



        public bool lost = false;

        int pickupTime = 0;
        int levelStartTime = 0;

        float arrowRotation;

        public int score;
        
        public Level()
        {
            GenerateLevel();
            wallLength = game.height + 50;
            wallPositionX = 452;
            shieldParticle = new ShieldParticle(player);
            loadDroppers();
            levelStartTime = Time.now;
        }

        int getLevelTime()
        {
            return Time.now - levelStartTime;
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

            cursor = new Cursor();
            AddChild(cursor);

            AddChild(player);

            AddChild(shieldhud);
            AddChild(scorehud);
            AddChild(dashhud);
            AddChild(dashHudImg);
            AddChild(shieldHudImg);
            AddChild(scoreHudImg);

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


        void loadDroppers()
        {
            AddChild(new PickupDropper(5000, 0, 500000));
            AddChild(new PickupDropper(18000, 1, 500000));
            AddChild(new PickupDropper(24000, 2, 500000));
            AddChild(new PickupDropper(31500, 3, 500000));
        }

        void Update()
        {
            switchBG();
            checkIfLost();
            DisplayHudItems();
            adjustDrawOrder();
            DisplayShieldParticle();

            

            
            if (!firstDropperMade)
            {
                firstDropperMade = true;
                AddChild(new Dropper(3000, 8, 4, player, false, false));
            }
            if (getLevelTime() >= 24900 && getLevelTime() <= 25100 && !secondDropperMade)
            {
                secondDropperMade = true;
                AddChild(new Dropper(3600, 13, 5, player, false, false));
                AddChild(new Dropper(8100, 6, 3, player, true, false));
            }
            if (getLevelTime() >= 73400 && getLevelTime() <= 73600 && !thirdDropperMade)
            {
                thirdDropperMade = true;
                player.updateSlideSpeed(3);
                AddChild(new Dropper(3600, 7, 5, player, false, false));
                AddChild(new Dropper(8100, 3, 3, player, true, false));

            }
            if (getLevelTime() >= 99100 && getLevelTime() <= 100100 && !fourthDropperMade)
            {
                fourthDropperMade = true;
                currentDropper = new Dropper(3500, 7, 7, player, false, false);
                AddChild(new Dropper(6000, 4, 5, player, true, false));
                AddChild(currentDropper);
            }
            if (getLevelTime() >= 125900 && getLevelTime() <= 126100 && !fifthDropperMade)
            {
                fifthDropperMade = true;
                player.updateSlideSpeed(3.5f);
            }

            loopSpaceLevel();

            Arrow(player);
            score = player.checkScore()/10;
        }
       
        
        public void loopSpaceLevel()
        {
            if(player.getHeightClimbed()> 0)
            {
                
                if (firstLooper || (currentDropper.Done()  && Time.now - pickupTime > 2000))
                {
                    canset = true;
                    int chosenrocks = ChooseSpace();
                    if (chosenrocks == 1)
                    {
                        AddChild(new Dropper(3000, 7, 8, player, false, false));
                        currentDropper = new Dropper(5300, 4, 6, player, true, false);
                        AddChild(currentDropper);
                        Console.WriteLine("normal");
                    }
                    else if (chosenrocks == 2)
                    {
                        AddChild(new Dropper(2500, 4, 10, player, false, false));
                        currentDropper = new Dropper(3700, 2, 8, player, true, false);
                        AddChild(currentDropper);
                        Console.WriteLine("burst");
                    }
                    else if (chosenrocks == 3)
                    {
                        AddChild(new Dropper(2700, 6, 7, player, false, false));
                        currentDropper = new Dropper(5000, 4, 5, player, true, false);
                        AddChild(currentDropper);
                        Console.WriteLine("chill");
                    }
                    firstLooper = false;
                }

               
                
            }
        }
        public int Choose()
        {
            Random rando = new Random();
            int selection = rando.Next(1, 5);
            return selection;
        }
        public int ChooseSpace()
        {
            Random rando = new Random();
            int selection = rando.Next(1, 4);
            return selection;
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
                game.SetChildIndex(loopPlanet[loopPlanet.Count - 1], 4);
                backgrounds[8].looped = true;
            }

            if (loopPlanet.Count > 0)
            {
                if (loopPlanet[loopPlanet.Count - 1].reachedEnd() )
                {
                    
                    loopPlanet.Add(new Background("space_3.png", 4, true, 1));
                    AddChild(loopPlanet[loopPlanet.Count - 1]);
                    game.SetChildIndex(loopPlanet[loopPlanet.Count - 1], 4);

                }
            }

            if (backgrounds[7].reachedEnd() && !backgrounds[7].looped)
            {
                loopDust.Add(new Background("space_2.png", 2, true, 1));
                AddChild(loopDust[loopDust.Count - 1]);
                game.SetChildIndex(loopDust[loopDust.Count - 1], 2);
                backgrounds[7].looped = true;
            }

            if (loopDust.Count > 0)
            {
                if (loopDust[loopDust.Count - 1].reachedEnd())
                {

                    loopDust.Add(new Background("space_2.png", 2, true, 1));
                    AddChild(loopDust[loopDust.Count - 1]);
                    game.SetChildIndex(loopDust[loopDust.Count - 1], 2);

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
            game.SetChildIndex(cursor, GetChildren().Count - 1);
        }
        public void DisplayHudItems()
        {

            shieldhud.updateMessage(" : " + player.getShields());

            scorehud.updateMessage("" + player.checkScore()/10);

            dashhud.updateMessage(" : " + player.getDashes());
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

        void DisplayShieldParticle()
        {
            if (player.HasShield() && !shieldParticleMade)
            {
                shieldParticle = new ShieldParticle(player);
                AddChild(shieldParticle);
                shieldParticleMade = true;
            }
            else if (!player.HasShield())
            {
                shieldParticle.Destroy();
                shieldParticleMade = false;
            }
            
           
        }


    }
}