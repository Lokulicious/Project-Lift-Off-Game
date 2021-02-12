using System;
using System.Drawing;
using GXPEngine;

public class MyGame : Game
{
	Level level;
	Cursor cursor;
	public MyGame() : base(1920, 1080, true)
	{
		level = new Level();
		AddChild(level);

		cursor = new Cursor();
		AddChild(cursor);
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