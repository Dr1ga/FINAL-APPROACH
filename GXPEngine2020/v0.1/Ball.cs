using System;
using GXPEngine;

public class Ball : EasyDraw
{
	public int radius {
		get {
			return _radius;
		}
	}

	public Vec2 velocity;
	public Vec2 position;
	public Vec2 oldPosition;
	public Vec2 acceleration;
	int _radius;
	float _speed;

	public Ball (int pRadius, Vec2 pPosition, float pSpeed=5) : base (pRadius*2 + 1, pRadius*2 + 1)
	{
		_radius = pRadius;
		position = pPosition;
		_speed = pSpeed;
		acceleration = new Vec2(0, 0.2f);
		UpdateScreenPosition ();
		SetOrigin (_radius, _radius);
		velocity.SetXY(0, 0);
		Draw (255, 255, 255);
		oldPosition = new Vec2(0, 0);
	}

	void Draw(byte red, byte green, byte blue) {
		Fill (red, green, blue);
		Stroke (red, green, blue);
		Ellipse (_radius, _radius, 2*_radius, 2*_radius);
	}

	

	void UpdateScreenPosition() {
		x = position.x;
		y = position.y;
	}

	public void Step () {
		oldPosition = position;
		
		velocity += acceleration;

		position += velocity;

		UpdateScreenPosition ();
	}
}
