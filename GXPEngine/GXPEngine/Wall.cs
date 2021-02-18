using GXPEngine;
using GXPEngine.Core;
using System.Collections;
using System;


public class Wall : AnimationSprite
{

    private Player player;

    float moveSpeed;

    int stage;
    int currentHeight;


    public Wall(Player player, int stage) : base("wall_tiles.png", 3, 1)
    {
        SetOrigin(width / 2 - 32, 0);
        moveSpeed = 3;

        this.stage = stage;
        this.player = player;

        SetFrame(0);
    }



    void Update()
    {
        WallMovement();
        UpdateEnv();
        //y += 5; //test for wall reset
    }

    void UpdateEnv()
    {
        switch (stage)
        {
            case 1:
                SetFrame(0);
                break;
            case 2:
                SetFrame(1);
                break;
            case 3:
                SetFrame(2);
                break;
        }


        currentHeight = player.getHeightClimbed();


        if (currentHeight < 113)
        {
            stage = 1;
        }
        else if (currentHeight < 187)
        {
            stage = 2;
        }
        else if (currentHeight >= 187)
        {
            stage = 3;
        }

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
            if (player.x > game.width / 2)
            {
                player.rightSide = true;
            }
            else if (player.x < game.width/2)
            {
                player.rightSide = false;
            }
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
