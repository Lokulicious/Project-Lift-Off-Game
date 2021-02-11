using GXPEngine;
using System.Collections;
using System;


public class Wall : Sprite
{

    private Player player;

    float moveSpeed;

    public Wall(Player player) : base("GrassWallRightSmall.png")
    {
        SetOrigin(width / 2, 0);
        moveSpeed = 3;

        this.player = player;
    }



    void Update()
    {
        WallMovement();

        //y += 5; //test for wall reset

    }





/*    void Sliding()
    {
        if (player.isTouchingWall == true)
        {
            y -= 1f;
        }


        if (player.isJumping == true)
        {
            player.isTouchingWall = false;
        }
    }*/



    void WallMovement()
    {

        y += moveSpeed;

        if (this.y > 1550)
        {
            this.y = y - (height * 17);
            player.gainHeight();
        }
        if (this.y < -626)
        {
            this.y = y + (height * 17);
        }


    }

    void OnCollision(GameObject other)
    {
        if (other is Player)
        {
            //Console.WriteLine("wall collison");
            Player player = other as Player;
            player.isTouchingWall = true;
            player.isJumping = false;
        }
    }


/*    void WallJumpMovement()
    {
        if (Input.GetKeyDown(Key.SPACE) && player.isJumping == false)
        {
            speedY = jumpForce; //give jump speed
            Console.WriteLine("jump start");
            Console.WriteLine(speedY);
        }


        if (player.isJumping)
        {
            speedY -= gravity; //apply gravity
            y += speedY; //apply movement
        }

    }
*/

}
