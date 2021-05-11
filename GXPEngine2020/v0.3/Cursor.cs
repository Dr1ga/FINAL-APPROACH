using System;
using GXPEngine;


public class Cursor : GameObject
{

	public Sprite sprite;

	public Cursor(int x, int y, string filename)
	{

		sprite = new Sprite(filename);
		sprite.SetOrigin(sprite.width / 2, sprite.height / 2);
		AddChild(sprite);

	}

}
