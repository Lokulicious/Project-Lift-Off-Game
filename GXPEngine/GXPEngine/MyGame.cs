using System;
using System.Drawing;
using GXPEngine;

public class MyGame : Game
{
    MainMenu mainMenu;
    Level level;
    Cursor cursor;
	
	Sound music;
    SoundChannel musicChannel;
	SoundChannel vfx;

    float musicVolume;
	float vfxVolume; //doesn't control jump sound volume yet


	public MyGame() : base(1920, 1080, false, false, -1, -1, true)
	{
        level = new Level();

        mainMenu = new MainMenu();
        AddChild(mainMenu);

        targetFps = 60;


        music = new Sound("music_powerup.mp3", true, false);
		music.Play(false, 0);

        musicChannel = new SoundChannel(1);
		vfx = new SoundChannel(2);

        musicVolume = 0.2f;
        vfxVolume = 0.2f;


    }

    void Update()
	{
        if (level.Lost())
        {
            level.Destroy();
            cursor.Destroy();
            level = new Level();
            AddChild(level);
            cursor = new Cursor();
            AddChild(cursor);
        }

        MusicController();
        startGame();


        Console.WriteLine(currentFps);
        }



    void startGame()
    {
        if (mainMenu.Start())
        {
            mainMenu.Destroy();
            level = new Level();
            AddChild(level);
            mainMenu.isStarting = false;
            cursor = new Cursor();
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
		vfx.Volume = vfxVolume;

    }

}