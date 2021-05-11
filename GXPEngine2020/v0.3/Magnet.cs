using System;
using GXPEngine;


public class Magnit : NLineSegment
{

	public NLineSegment lineSegment;
	public Sprite magnet;
	Vec2 _collisionZone;

	Vec2 _realEnd;
	Vec2 _realStart;

	public Magnit(Vec2 noStart, Vec2 noEnd, float magnetRotation, string filename) : base(noStart, noEnd, 0xff00ff00, 3)
	{
		
		end.RotateAroundDegrees(magnetRotation, noStart);

		//Console.WriteLine(end);
		//Console.WriteLine(start);

		magnet = new Sprite(filename, false, false);
		//lineSegment = new NLineSegment(noStart, noEnd, 0xff00ff00, 3);
		magnet.SetXY(start.x, start.y);
		magnet.SetOrigin(magnet.width / 2, magnet.height / 2);

		//_collisionZone = lineSegment.start - lineSegment.end;
		

		

		AddChild(magnet);

		magnet.rotation = magnetRotation;

		Console.WriteLine(magnet.rotation);
		
	}

}