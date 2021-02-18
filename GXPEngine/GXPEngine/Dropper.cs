using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

namespace GXPEngine
{
    class Dropper : GameObject
    {
        private float timeOfLastDrop;
        private Player _player;
        float frequency;
        int endAfter;
        int drops = 1;
        bool safety = false;
        bool bigRocks;
        int currentheight;
        bool doubleDrop;
        bool current = false;
        GameObject toAdd = new Pivot();
        public Dropper(float frequency, int endAfter, Player player, bool bigRocks,bool doubledropper)
        {
            this.frequency = frequency;
            this.endAfter = endAfter;
            this.bigRocks = bigRocks;
            timeOfLastDrop = Time.now;
            _player = player;
            doubleDrop = doubledropper;
        }
        public Dropper()
        {
            current = true;
        }
        public void Update()
        {

            if (!Done())
            {
                dropRocks();
                checkSafety();
            }

        }
        public void dropRocks()
        {
            currentheight = _player.getHeightClimbed();

            if (TimeToDrop() && !safety)
            {
                if(doubleDrop && currentheight < 113 && drops % 2 != 0)
                {
                    toAdd = new DroppedThing(5, GetFallLane(), _player, "MRBbreak.png");
                    AddChild(toAdd);
                    game.SetChildIndex(toAdd, game.GetChildren().Count);
                }
                else if (doubleDrop && currentheight < 113 && drops % 2 == 0)
                {
                    toAdd = new DroppedThing(5, GetFallLane(), _player, "MRSbreak.png");
                    AddChild(toAdd);
                    game.SetChildIndex(toAdd, game.GetChildren().Count);
                }
                else if (doubleDrop && currentheight < 187 && drops % 2 != 0)
                {
                    toAdd = new DroppedThing(5, GetFallLane(), _player, "CRBbreak.png");
                    AddChild(toAdd);
                    game.SetChildIndex(toAdd, game.GetChildren().Count);
                }
                else if (doubleDrop && currentheight < 187 && drops % 2 == 0)
                {
                    toAdd = new DroppedThing(5, GetFallLane(), _player, "CRSbreak.png");
                    AddChild(toAdd);
                    game.SetChildIndex(toAdd, game.GetChildren().Count);
                }
                else if (doubleDrop && currentheight >= 187 && drops % 2 != 0)
                {
                    toAdd = new DroppedThing(5, GetFallLane(), _player, "SRBbreak.png");
                    AddChild(toAdd);
                    game.SetChildIndex(toAdd, game.GetChildren().Count);
                }
                else if (doubleDrop && currentheight >= 187 && drops % 2 == 0)
                {
                    toAdd = new DroppedThing(5, GetFallLane(), _player, "SRSbreak.png");
                    AddChild(toAdd);
                    game.SetChildIndex(toAdd, game.GetChildren().Count);
                }
                else if (bigRocks && currentheight < 113)
                {
                    toAdd = new DroppedThing(5, GetFallLane(), _player, "MRBbreak.png");
                    AddChild(toAdd);
                    game.SetChildIndex(toAdd, game.GetChildren().Count);
                }

                else if (!bigRocks && currentheight < 113)
                {
                    toAdd = new DroppedThing(5, GetFallLane(), _player, "MRSbreak.png");
                    AddChild(toAdd);
                    game.SetChildIndex(toAdd, game.GetChildren().Count);
                }
                else if (bigRocks && currentheight < 187)
                {
                    toAdd = new DroppedThing(5, GetFallLane(), _player, "CRBbreak.png");
                    AddChild(toAdd);
                    game.SetChildIndex(toAdd, game.GetChildren().Count);
                }
                else if (!bigRocks && currentheight < 187)
                {
                    toAdd = new DroppedThing(5, GetFallLane(), _player, "CRSbreak.png");
                    AddChild(toAdd);
                    game.SetChildIndex(toAdd, game.GetChildren().Count);
                }
                else if (bigRocks && currentheight >= 187)
                {
                    toAdd = new DroppedThing(5, GetFallLane(), _player, "SRBbreak.png");
                    AddChild(toAdd);
                    game.SetChildIndex(toAdd, game.GetChildren().Count);
                }
                else if (!bigRocks && currentheight >= 187)
                {
                    toAdd = new DroppedThing(5, GetFallLane(), _player, "SRSbreak.png");
                    AddChild(toAdd);
                    game.SetChildIndex(toAdd, game.GetChildren().Count);
                }

                drops++;
            }
        }
        public void checkSafety() //checks if last drop was less than a tenth of a second ago to avoid double dropping
        {
            if (Time.now - timeOfLastDrop < 100)
            {
                safety = true;
            }
            else
            {
                safety = false;
            }
        }

        public bool TimeToDrop()
        {
            if (Time.now - timeOfLastDrop >= frequency - 100 && Time.now - timeOfLastDrop <= frequency + 100)
            {
                timeOfLastDrop = Time.now;
                return true;
            }
            else
            {
                return false;
            }
        }
        public int GetFallLane()
        {
            Random rando = new Random();
            int selection = rando.Next(1, 5);

            switch (selection)
            {

                case 1: return 700;
                case 2: return 800;
                case 3: return 900;
                case 4: return 1000;

            }
            return 0;
        }
        public bool Done()
        {
            if (current)
            {
                return false;
            }
            else if (drops-1 >= endAfter)
            {
                return true;
            }
            else
                return false;
        }
    }
}
