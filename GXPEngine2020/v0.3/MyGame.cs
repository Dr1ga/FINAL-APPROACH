using System;
using GXPEngine;
using System.Drawing;
using System.Collections.Generic;

public class MyGame : Game
{	

	static void Main() {
		new MyGame().Start();
	}

	

	
	public MyGame () : base(1920, 1080, true, false)
	{
		SetupMenu();
		//StartLevel();
	}

	void StartLevel() 
	{
		Level level = new Level(1, 0.1f);
		AddChild(level);
	}

	void SetupMenu()
	{
		Menu menu = new Menu(1, 0.1f);
		AddChild(menu);
	}

}

