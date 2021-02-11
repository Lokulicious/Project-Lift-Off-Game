using System;
using System.Drawing;
using GXPEngine;

public class MyGame : Game
{
	Level level;
	public MyGame() : base(1920, 1080, true)
	{
		level = new Level();
		AddChild(level);
    }

    void Update()
	{
        if (level.Lost())
        {
			level.Destroy();
			level = new Level();
			AddChild(level);
		}
	}

	static void Main()
	{
		new MyGame().Start();
	}
}