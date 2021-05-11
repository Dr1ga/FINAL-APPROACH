using GXPEngine; 

public class CollisionInfo
{
	public readonly Vec2 normal;
	public readonly GameObject other;
	public readonly float timeOfImpact;
	public readonly float ballDistance;
	public CollisionInfo(Vec2 pNormal, GameObject pOther, float pTimeOfImpact, float pBallDistance)
	{
		normal = pNormal;
		other = pOther;
		timeOfImpact = pTimeOfImpact;
		ballDistance = pBallDistance;
	}
}
