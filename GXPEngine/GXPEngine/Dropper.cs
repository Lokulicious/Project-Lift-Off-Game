using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

namespace GXPEngine
{
    class Dropper : GameObject
    {
        private float timeOfLastDrop = 0;

        private Player _player;

        float frequency;
        bool toDrop = false;
        int endAfter;
        int drops;
        public Dropper( float frequency, int endAfter, Player player) 
        {
            this.frequency = frequency;
            this.endAfter = endAfter;

            _player = player;
        }
        public void Update()
        {
            if (!Done())
            {
                if (Time.now % frequency == 0)
                {
                    AddChild(new DroppedThing(5, GetFallLane(),_player));
                    drops++;
                }
            }
           
        }

        public bool TimeToDrop()
        {
            if (Time.now - timeOfLastDrop == frequency) 
            {
                toDrop = true;
                timeOfLastDrop = Time.now;
                return toDrop;
            }
            else
            {
                toDrop = false;
                return toDrop;
            } 
        }
        public int GetFallLane()
        {
            Random rando = new Random();
            int selection = rando.Next(1, 6);

            switch(selection) {

                case 1: return 600;
                case 2: return 750;
                case 3: return 900;
                case 4: return 1050;
                case 5: return 1200;
            }
            return 0;
        }
        public bool Done()
        {
            if (drops > endAfter)
            {
                return true;
            }
            else
                return false;
        }
    }
}
