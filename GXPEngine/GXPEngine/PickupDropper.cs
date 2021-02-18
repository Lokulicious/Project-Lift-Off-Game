using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
{
    class PickupDropper : GameObject
    {
        int timeOfLastDrop;
        bool safety = false;
        float frequency;
        int type;
        int endAfter;
        int drops=0;
        public PickupDropper(float frequency,int type, int endAfter ) : base( )
        {
            this.frequency = frequency;
            this.type = type;
            this.endAfter = endAfter;
            timeOfLastDrop = Time.now;
        }
        public void Update()
        {
            if (!Done())
            {
                
                dropPickups();
                checkSafety();
            }
        }

        void dropPickups()
        {
            if (TimeToDrop() && !safety)
            {
                if(type == 0)
                {
                    Pickup toAdd = new ScoreBall(0);
                    AddChild(toAdd);
                    game.SetChildIndex(toAdd, game.GetChildren().Count);
                }
                else if (type == 1)
                {
                    Pickup toAdd = new DoubleScore(0);
                    AddChild(toAdd);
                    game.SetChildIndex(toAdd, game.GetChildren().Count);
                }
                else if (type == 2)
                {
                    Pickup toAdd = new Shield(0);
                    AddChild(toAdd);
                    game.SetChildIndex(toAdd, game.GetChildren().Count);
                }
                else if (type == 3)
                {
                    Pickup toAdd = new Dash(0);
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
            
            if (drops - 1 >= endAfter)
            {
                return true;
            }
            else
                return false;
        }
    }
}
