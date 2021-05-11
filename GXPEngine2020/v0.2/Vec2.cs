using System;
using GXPEngine;    // For Mathf

public struct Vec2
{
	public float x;
	public float y;

	public Vec2(float pX = 0, float pY = 0)
	{
		x = pX;
		y = pY;
	}

	public override string ToString()
	{
		return String.Format("({0},{1})", x, y);
	}

	public void SetXY(float pX, float pY)
	{
		x = pX;
		y = pY;
	}

	public float Length()
	{
		return Mathf.Sqrt(x * x + y * y);
	}

	public void Normalize()
	{
		float len = Length();
		if (len > 0)
		{
			x /= len;
			y /= len;
		}
	}




	public Vec2 Normalized()
	{
		Vec2 result = new Vec2(x, y);
		result.Normalize();
		return result;
	}


	public Vec2 Normal()
	{
		// TODO: return a unit normal


		float newX = -y;
		float newY = x;
		Vec2 result = new Vec2(newX, newY);
		result.Normalize();
		return result;
	}

	public float Dot(Vec2 other)
	{
		Vec2 a = this;
		Vec2 b = other;
		float dotProduct;
		if (this.Length() < other.Length())
		{
			a.Normalize();
			dotProduct = (b.x * a.x) + (b.y * a.y);

			return dotProduct;

		}

		if (this.Length() > other.Length())
		{


			b.Normalize();
			dotProduct = (b.x * a.x) + (b.y * a.y);
			return dotProduct;

		}
		else
		{

			return 0;
		}
	}
	public static float Deg2Rad(float degrees)
	{
		float radians;
		return radians = (degrees * Mathf.PI / 180); ;


	}

	public static float Rad2Deg(float radians)
	{

		float degrees;

		return degrees = (radians * 180 / Mathf.PI);

	}

	public static Vec2 GetUnitVectorDeg(float degrees)
	{

		Vec2 angleVec;
		float radians;
		radians = Deg2Rad(degrees);

		angleVec.x = (float)Mathf.Cos(radians);
		angleVec.y = (float)Mathf.Sin(radians);

		if (Mathf.Abs(angleVec.x) < 0.00001f)
			angleVec.x = 0;
		if (Mathf.Abs(angleVec.y) < 0.00001f)
			angleVec.y = 0;
		return angleVec;
	}

	public static Vec2 GetUnitVectorRad(float radians)
	{

		Vec2 angleVec;

		angleVec.x = (float)Mathf.Cos(radians);
		angleVec.y = (float)Mathf.Sin(radians);

		if (Mathf.Abs(angleVec.x) < 0.00001f)
			angleVec.x = 0;
		if (Mathf.Abs(angleVec.y) < 0.00001f)
			angleVec.y = 0;



		return angleVec;
	}

	public static Vec2 GetRandomUnitVector()
	{

		Vec2 angleVec;
		float radians;
		radians = Deg2Rad(Utils.Random(0, 360));
		angleVec.x = (float)Mathf.Cos(radians);
		angleVec.y = (float)Mathf.Sin(radians);

		if (Mathf.Abs(angleVec.x) < 0.00001f)
			angleVec.x = 0;
		if (Mathf.Abs(angleVec.y) < 0.00001f)
			angleVec.y = 0;



		return angleVec;

	}

	public void SetAngleDegrees(float degrees)
	{
		float radians = Deg2Rad(degrees);
		float radius = Length();

		float cosinus = (float)Mathf.Cos(radians);
		float sinus = (float)Mathf.Sin(radians);

		if (Mathf.Abs(cosinus) < 0.00001f)
			cosinus = 0;
		if (Mathf.Abs(sinus) < 0.00001f)
			sinus = 0;

		SetXY(radius * cosinus, radius * sinus);
	}


	public void SetAngleRadians(float radians)
	{

		float radius = Length();

		float cosinus = (float)Mathf.Cos(radians);
		float sinus = (float)Mathf.Sin(radians);

		if (Mathf.Abs(cosinus) < 0.00001f)
			cosinus = 0;
		if (Mathf.Abs(sinus) < 0.00001f)
			sinus = 0;

		SetXY(radius * cosinus, radius * sinus);
	}


	public float GetAngleRadians()
	{

		float angle = Mathf.Atan2(y, x);

		return angle;
	}

	public float GetAngleDegrees()
	{

		float angle = Mathf.Atan2(y, x);

		return Rad2Deg(angle);
	}

	public void RotateDegrees(float degrees)
	{

		float cosinus = (float)Mathf.Cos(Deg2Rad(degrees));
		float sinus = (float)Mathf.Sin(Deg2Rad(degrees));
		this = new Vec2(x * cosinus - y * sinus, x * sinus + y * cosinus);

	}



	public void RotateRadians(float radians)
	{

		float cosinus = (float)Mathf.Cos(radians);
		float sinus = (float)Mathf.Sin(radians);
		this = new Vec2(x * cosinus - y * sinus, x * sinus + y * cosinus);

	}

	public void RotateAroundDegrees(float degrees, Vec2 targetPoint)
	{

		this -= targetPoint;
		RotateDegrees(degrees);
		this += targetPoint;
	}


	public void RotateAroundRadians(float radians, Vec2 targetPoint)
	{

		this -= targetPoint;
		RotateRadians(radians);
		this += targetPoint;
	}


	public void Reflect(Vec2 pnormal, float pBounciness)
	{
		x = x - (1 + pBounciness) * (x * pnormal.x) * pnormal.x;
		y = y - (1 + pBounciness) * (y * pnormal.y) * pnormal.y;


	}


	public void Bounce(Vec2 pnormal, float pBounciness)
	{
		x = (1 + pBounciness) * (4 * pnormal.x) * 2;
		y = (1 + pBounciness) * (4 * pnormal.y) * 2;


	}
	public static Vec2 operator +(Vec2 left, Vec2 right)
	{
		return new Vec2(left.x + right.x, left.y + right.y);
	}

	public static Vec2 operator -(Vec2 left, Vec2 right)
	{
		return new Vec2(left.x - right.x, left.y - right.y);
	}

	public static Vec2 operator *(Vec2 v, float scalar)
	{
		return new Vec2(v.x * scalar, v.y * scalar);
	}

	public static Vec2 operator *(float scalar, Vec2 v)
	{
		return new Vec2(v.x * scalar, v.y * scalar);
	}

	public static Vec2 operator /(Vec2 v, float scalar)
	{
		return new Vec2(v.x / scalar, v.y / scalar);
	}
}

