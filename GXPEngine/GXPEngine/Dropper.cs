﻿using System;
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
        int drops = 0;
        bool safety = false;
        bool bigRocks;
        public Dropper(float frequency, int endAfter, Player player, bool bigRocks)
        {
            this.frequency = frequency;
            this.endAfter = endAfter;
            this.bigRocks = bigRocks;
            timeOfLastDrop = Time.now;
            _player = player;
            Console.WriteLine("---------------------------------------------------------------------------------------");
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
            Console.WriteLine(drops);
            if (TimeToDrop() && !safety)
            {
                if (bigRocks)
                    AddChild(new DroppedThing(5, GetFallLane(), _player, "Big_Rock_Break.png"));
                else
                    AddChild(new DroppedThing(5, GetFallLane(), _player, "Small_Rock_Break.png"));

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
            if (drops > endAfter)
            {
                return true;
            }
            else
                return false;
        }
    }
}
