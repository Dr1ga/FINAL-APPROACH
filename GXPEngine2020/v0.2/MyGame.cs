using System;
using GXPEngine;
using System.Drawing;
using System.Collections.Generic;

public class MyGame : Game
{	

	static void Main() {
		new MyGame().Start();
	}

	

	
	public MyGame () : base(1920, 1080, false,false)
	{
		StartLevel();
	}

	void StartLevel() 
	{
		Level level = new Level(1);
		AddChild(level);
	}

	
}
