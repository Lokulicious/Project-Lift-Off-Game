using GXPEngine;
using System.Collections;
using System;


namespace GXPEngine
{
    public class Player : Sprite
    {

        public bool isTouchingWall = false;

        public Player() : base("player_big.png")
        {
            SetOrigin(width / 2, height / 2);
            this.x = game.width / 2;
            this.y = game.height / 2;
        }



        void Update()
        {
            //temoprary movement for testing

            if (Input.GetKey(Key.A))
            {
                x -= 5;
            }
            
            if (Input.GetKey(Key.D))
            {
                x += 5;
            }

            Console.WriteLine(isTouchingWall);
        }
    }
}