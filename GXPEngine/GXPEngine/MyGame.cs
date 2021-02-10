using System;
using System.Drawing;
using GXPEngine;

public class MyGame : Game
{
	public MyGame() : base(1920, 1080, true)
	{
		Level level = new Level();
		AddChild(level);
    }

    void Update()
	{

	}

	static void Main()
	{
		new MyGame().Start();
	}
}