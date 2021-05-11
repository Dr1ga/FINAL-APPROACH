using System;
using GXPEngine;


public class Trampoline : NLineSegment
{

	public NLineSegment lineSegment;
	public Sprite trapmliiin;
	Vec2 _collisionZone;
	Vec2 _middle;
	public Trampoline(Vec2 nostart, Vec2 noend, float trampRotation, string filename) : base(nostart, noend, 0xff00ff00, 3)
	{


		end.RotateAroundDegrees(trampRotation, nostart);

		//start.RotateAroundDegrees(trampRotation, noend);

		trapmliiin = new Sprite(filename, false, false);


		_middle = (start + end) * 0.5f;

		

		AddChild(trapmliiin);
		trapmliiin.SetOrigin(trapmliiin.width / 2, 0);
		trapmliiin.rotation = trampRotation;
		trapmliiin.SetXY(_middle.x, _middle.y);
		
	}


	void Update() 
	{

		//Console.WriteLine(trapmliiin.rotation);

	}
}