using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;

    class MainMenu : GameObject
    {


    Player _player = new Player();
    MenuPlayer menuPlayer = new MenuPlayer();
    Background menuBG = new Background("bg_big.png", -20, 0, false, 1);
    StartText startText = new StartText(-15);
    StartTextShadow startTextShadow;
    MenuOverlay menuOverlay = new MenuOverlay();
    NudinoLogo nudinoLogo = new NudinoLogo(300);

    float wallPositionX = 600;
    float wallStartPositionY = 100;

    public bool isStarting = false;

    public MainMenu()
    {
        GenerateEnvironment();
        //AddChild(menuPlayer);
        AddChild(menuOverlay);
        startTextShadow = new StartTextShadow(_player);
        /*AddChild(startTextShadow);*/
        AddChild(startText);
        AddChild(nudinoLogo);
    }

    void Update()
    {
        checkIfStart();
        Start();
    }

    void GenerateEnvironment()
    {
        //add BG
        AddChild(menuBG);

        //Generate Solid Walls
        for (int x = 0; x < 5; x++)
        {
            for (int y = 0; y < 9; y++)
            {
                FilledWall filledwall = new FilledWall(_player);
                filledwall.x = x * filledwall.width;
                filledwall.y = y * filledwall.height - 50;
                AddChild(filledwall);
            }
        }

        for (int x = 0; x < 5; x++)
        {
            for (int y = 0; y < 9; y++)
            {
                FilledWall filledwall = new FilledWall(_player);
                filledwall.x = game.width - x * filledwall.width - 50;
                filledwall.y = y * filledwall.height - 50;
                AddChild(filledwall);
            }
        }


        //Generate Walls
        for (y = 0; y < 18; y++)
        {
            Wall wallLeft = new Wall(_player, 1);
            wallLeft.y = y * wallLeft.height - wallStartPositionY;
            wallLeft.x = wallPositionX;
            wallLeft.scaleX = -1;
            AddChild(wallLeft);
        }

        for (y = 0; y < 18; y++)
        {
            Wall wallRight = new Wall(_player, 1);
            wallRight.y = y * wallRight.height - wallStartPositionY;
            wallRight.x = game.width - wallPositionX;
            AddChild(wallRight);
            
        }


    }


    public void checkIfStart()
    {
        if (Input.GetKeyDown(Key.ENTER))
        {
            isStarting = true;
            Console.WriteLine("StartGame");
        }
    }


    void PlayerJumping()
    {

    }


    public bool Start()
    {
        return isStarting;
    }


}
