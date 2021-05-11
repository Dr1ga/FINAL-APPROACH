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
    Level _levelinfo;
	int _radius;
	float _speed;
    private bool grounded = false;
    Sprite _skin;
    public Ball (int pRadius, Vec2 pPosition, Level level) : base (pRadius*2 + 1, pRadius*2 + 1)
	{
		_radius = pRadius;
		position = pPosition;
        //_speed = pSpeed;
        _levelinfo = level;

        _skin = new Sprite("ball.png");
        _skin.SetOrigin(_radius, _radius);

        acceleration = new Vec2(0, 1.5f);
		UpdateScreenPosition ();
		SetOrigin (_radius, _radius);
		velocity.SetXY(0, 0);
		Draw (255, 255, 255);
		oldPosition = new Vec2(0, 0);

        AddChild(_skin);
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
        if (grounded)
            grounded = false;
        else
            velocity += acceleration;
        position += velocity;
        CollisionInfo firstCollision = FindEarliestCollision();
        if (firstCollision != null)
        {
            ResolveCollision(firstCollision);
        }
        UpdateScreenPosition ();
	}





    CollisionInfo FindEarliestCollision()
    {
        if (velocity.Length() > 0)
        {

            //line segment collision
            CollisionInfo lineColl = GetFirstLineColl();
            //return first valid coll
            CollisionInfo trampColl = GetFirstTrampolineColl();

            //CollisionInfo magnetColl = GetFirstMagnetColl();

            

            if (trampColl != null)
            {
                if (lineColl != null)
                    return lineColl.timeOfImpact < trampColl.timeOfImpact ? lineColl : trampColl;
                else
                    return trampColl;

            }
            else if (lineColl != null)
                return lineColl;
        }
        return null;
    }

    void ResolveCollision(CollisionInfo info)
    {

        //reset to PoI

        if (info.other is Trampoline)
        {

            Vec2 POI = position + (info.ballDistance + radius) * info.normal;
            position = POI;
            velocity.Bounce(info.normal, 2f);

        }
        //position += velocity * info.timeOfImpact;
        //velocity.Reflect(info.normal, 0.8f);

        if (info.other is Wall )
        {
            Vec2 POI = position + (info.ballDistance + radius) * info.normal;
            position = POI;
            velocity.Reflect(info.normal, 0.6f);
        }

        if (info.other is Shelf)
        {
            Vec2 POI = position + (info.ballDistance + radius) * info.normal;
            position = POI;
            velocity.Reflect(info.normal, 0.6f);
        }

        grounded = true;
    }

    CollisionInfo GetFirstLineColl()
    {

        
        CollisionInfo firstColl = new CollisionInfo(new Vec2(), null, 2, 0);
        //line segment collision
        for (int i = 0; i < _levelinfo._lines.Count; i++)
        {
            if (_levelinfo._lines[i] is LineSegment)
            {

                LineSegment line = _levelinfo._lines[i] as LineSegment;
                //calc vars
                Vec2 normal = (line.end - line.start).Normal();

                Vec2 differenceVector = line.start - position;

                float ballDistance = differenceVector.Dot(normal.Normalized());

                float ToI = GetLineToI(line, normal);
                //test result
                if (ToI <= 1 && ToI < firstColl.timeOfImpact)
                {
                    Vec2 PoI = GetLinePoI(ToI);
                    if (IsValidPoI(PoI, line))
                        firstColl = new CollisionInfo(normal, line, ToI, ballDistance);
                }
            }
        }
        if (firstColl.timeOfImpact != 2)
            return firstColl;
        return null;
    }


    CollisionInfo GetFirstTrampolineColl()
    {

        CollisionInfo firstColl = new CollisionInfo(new Vec2(), null, 2, 0);
        //line segment collision
        for (int i = 0; i < _levelinfo._tramps.Count; i++)
        {
            if (_levelinfo._tramps[i] is Trampoline)
            {
                Trampoline tramp = _levelinfo._tramps[i] as Trampoline;
                //calc vars
                Vec2 normal = (tramp.end - tramp.start).Normal();

                Vec2 differenceVector = tramp.start - position;

                float ballDistance = differenceVector.Dot(normal.Normalized());

                float ToI = GetLineToI(tramp, normal);
                //test result
                if (ToI <= 1 && ToI < firstColl.timeOfImpact)
                {
                    Vec2 PoI = GetLinePoI(ToI);
                    if (IsValidPoI(PoI, tramp))
                        firstColl = new CollisionInfo(normal, tramp, ToI, ballDistance);
                }
            }
        }
        if (firstColl.timeOfImpact != 2)
            return firstColl;
        return null;
    }




    CollisionInfo GetFirstShelfColl()
    {

        CollisionInfo firstColl = new CollisionInfo(new Vec2(), null, 2, 0);
        //line segment collision
        for (int i = 0; i < _levelinfo._lines.Count; i++)
        {
            if (_levelinfo._lines[i] is Shelf)
            {
                Shelf shelf = _levelinfo._lines[i] as Shelf;
                //calc vars
                Vec2 normal = (shelf.end - shelf.start).Normal();

                Vec2 differenceVector = shelf.start - position;

                float ballDistance = differenceVector.Dot(normal.Normalized());

                float ToI = GetLineToI(shelf, normal);
                //test result
                if (ToI <= 1 && ToI < firstColl.timeOfImpact)
                {
                    Vec2 PoI = GetLinePoI(ToI);
                    if (IsValidPoI(PoI, shelf))
                        firstColl = new CollisionInfo(normal, shelf, ToI, ballDistance);
                }
            }
        }
        if (firstColl.timeOfImpact != 2)
            return firstColl;
        return null;
    }

    //CollisionInfo GetFirstMagnetColl()
    //{

    //    CollisionInfo firstColl = new CollisionInfo(new Vec2(), null, 2);
    //    //line segment collision
    //    for (int i = 0; i < _levelinfo._magas.Count; i++)
    //    {
    //        if (_levelinfo._magas[i] is Magnit)
    //        {
    //            Magnit magnit = _levelinfo._magas[i] as Magnit;
    //            //calc vars
    //            Vec2 normal = (magnit.end - magnit.start).Normal();
    //            float ToI = GetLineToI(magnit, normal);
    //            //test result
    //            if (ToI <= 1 && ToI < firstColl.timeOfImpact)
    //            {
    //                Vec2 PoI = GetLinePoI(ToI);
    //                if (IsValidPoI(PoI, magnit))
    //                    firstColl = new CollisionInfo(normal, magnit, ToI);
    //            }
    //        }
    //    }
    //    if (firstColl.timeOfImpact != 2)
    //        return firstColl;
    //    return null;
    //}

    float GetLineToI(LineSegment line, Vec2 normal)
    {
        float a = (oldPosition - line.start).Dot(normal) - radius;
        float b = -velocity.Dot(normal);
        if (b > 0)
        {
            if (a >= 0)
                return a / b;
            else if (a >= -radius)
                return 0;
        }
        return 2;
    }
    Vec2 GetLinePoI(float ToI)
    {
        return oldPosition + (ToI * velocity);
    }

    bool IsValidPoI(Vec2 PoI, LineSegment line)
    {
        Vec2 lineVectorDir = (line.end - line.start).Normalized();
        float dist = (PoI - line.start).Dot(lineVectorDir);
        if (dist >= 0 && dist <= (line.end - line.start).Length())
            return true;
        return false;
    }
}
