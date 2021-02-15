using System;
using System.Drawing;
using GXPEngine;

public class MyGame : Game
{
	Level level;
	Cursor cursor;
	public MyGame() : base(1920, 1080, true, false, -1, -1, true)
	{
		level = new Level();
		AddChild(level);
		targetFps=60;
		cursor = new Cursor();
		AddChild(cursor);
		targetFps = 60;
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
	}

	static void Main()
	{
		new MyGame().Start();
	}
}