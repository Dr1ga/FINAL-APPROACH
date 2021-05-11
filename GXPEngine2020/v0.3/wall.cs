using System;
using GXPEngine;


    public class Wall : NLineSegment
{

	public NLineSegment lineSegment;
	//public Sprite wallSprite;
	Vec2 _collisionZone;

	public Wall(Vec2 start, Vec2 end) : base(start, end)
	{

		//wallSprite = new Sprite(filename, false, false);
		lineSegment = new NLineSegment(start, end, 0xff00ff00, 3);
		//wallSprite.SetXY(start.x, start.y);
		//_collisionZone = lineSegment.start - lineSegment.end;
		
		//wallSprite.rotation = _collisionZone.GetAngleDegrees();
	}

}

