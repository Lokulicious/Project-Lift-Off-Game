using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;


   class MenuPlayer : AnimationSprite
    {

    float jumpForce;
    float jumpForceX;
    float jumpForceY;

    float playerMoveSpeed;
    float passiveMoveSpeed;
    float slideSpeed;

    float gravity;

    public bool rightSide = false;
    public bool isJumping = false;
    public bool isTouchingWall = true;

    int jumpTimer;


    public MenuPlayer() : base("new_flip.png", 21, 1)
    {
        passiveMoveSpeed = 3;
        slideSpeed = 1;
        gravity = 10f;


        this.x = 655;
        this.y = game.height - 750;

        SetCycle(0, 1);


    }


    void Update()
    {
        moveDown();
        randomJump();
    }

    void randomJump()
    {
        if (jumpTimer > 160)
        {
            jumpForce = 15;
            isJumping = true;
            jumpTimer = 0;
        }


        if (!rightSide)
        {
            jumpForceX = jumpForce;
            jumpForceY = jumpForce * 1.5f;
        }
        else
        {
            jumpForceX = -jumpForce;
            jumpForceY = jumpForce * 1.5f;
        }



        if (isJumping)
        {
            jumpForceY = (jumpForce * 1.5f) - gravity;
            Console.WriteLine(jumpForceY);
        }
        else
        {
            jumpForce = 0;
        }

        x += jumpForceX;
        y -= jumpForceY;

        jumpTimer += 1;
        Console.WriteLine(isJumping);

    }


    void moveDown()
    {
        playerMoveSpeed = passiveMoveSpeed;

        if (isTouchingWall)
        {
            playerMoveSpeed = passiveMoveSpeed + slideSpeed;
        }

        y += playerMoveSpeed;
    }




}
