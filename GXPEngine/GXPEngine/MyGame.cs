using System;
using System.Drawing;
using GXPEngine;

public class MyGame : Game
{
	Level level;
	Cursor cursor;
	
	Sound music;
    SoundChannel musicChannel;
	SoundChannel vfx;

    float musicVolume;
	float vfxVolume;


	public MyGame() : base(1920, 1080, true, false, -1, -1, true)
	{
		targetFps = 60;
		level = new Level();
		AddChild(level);
		cursor = new Cursor();
		AddChild(cursor);

		

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