using System;
using GXPEngine;


public class Shelf : NLineSegment
{

	public NLineSegment lineSegment;
	public Sprite shelf;
	Vec2 _collisionZone;
	Vec2 _middle;
	public Shelf(Vec2 nostart, Vec2 noend, float shelfRotation, string filename) : base(nostart, noend, 0xff00ff00, 3)
	{


		end.RotateAroundDegrees(shelfRotation, nostart);

		//start.RotateAroundDegrees(trampRotation, noend);

		shelf = new Sprite(filename, false, false);


		_middle = (start + end) * 0.5f;



		AddChild(shelf);
		shelf.SetOrigin(shelf.width / 2, shelf.height / 2);
		shelf.rotation = shelfRotation;
		shelf.SetXY(_middle.x, _middle.y);

	}


	void Update()
	{

		Console.WriteLine(shelf.rotation);

	}
}