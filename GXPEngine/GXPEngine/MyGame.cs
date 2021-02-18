using System;
using System.Collections.Generic;
using System.Drawing;
using GXPEngine;

public class MyGame : Game
{
    MainMenu mainMenu;
    Level level;
    GameOverMenu gameOverMenu;

    Cursor cursor;

	
	Sound music;
    SoundChannel musicChannel;
	SoundChannel vfx;

    float musicVolume;
	float vfxVolume; //doesn't control jump sound volume yet
    List<GameObject> children = new List<GameObject>();
    int score;


	public MyGame() : base(1920, 1080, false, false, -1, -1, true)
	{
        level = new Level();

        mainMenu = new MainMenu();

        gameOverMenu = new GameOverMenu(score, level, false);
        /*AddChild(gameOverMenu);*/

        AddChild(mainMenu);

        targetFps = 30;


        music = new Sound("music_powerup.mp3", true, false);
		music.Play(false, 0);

        musicChannel = new SoundChannel(1);
		vfx = new SoundChannel(2);

        musicVolume = 0.2f;
        vfxVolume = 0.2f;


    }

    void Update()
	{
        children = level.GetChildren();
        
        if (level.Lost())
        {
            gameOverMenu = new GameOverMenu(score, level, true);
            foreach (GameObject child in GetChildren())
            {
                child.LateDestroy();
            }

            AddChild(gameOverMenu);   
            
            level.lost = false;
        }
        if (gameOverMenu.Restart())
        {
            gameOverMenu.Destroy();

            mainMenu = new MainMenu();
            AddChild(mainMenu);


        }
        MusicController();
        startGame();
        getScore();
        targetFps = 30;
        
    }



    void startGame()
    {
        if (mainMenu.Start())
        {
            mainMenu.Destroy();

            level = new Level();
            cursor = new Cursor();

            mainMenu.isStarting = false;

            AddChild(level);
            AddChild(cursor);
        }
    }


    static void Main()
	{
		new MyGame().Start();
	}

	void MusicController()
    {
        musicChannel.Volume = musicVolume;
		vfx.Volume = vfxVolume; //does not control anything right now

    }

    void getScore()
    {
        score = level.score;
    }

}