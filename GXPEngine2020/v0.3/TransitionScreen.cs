using System;
using GXPEngine;


public class Transit : GameObject
{

	public Sprite blackF;
	public int frameTimer;
	int _score;
	public TextBoard scoreBroad;
	public Transit(int score)
	{
		_score = score;
		blackF = new Sprite("blackpng.png");
		blackF.alpha = 0.8f;
		AddChild(blackF);

		scoreBroad = new TextBoard(600, 400);
		scoreBroad.x += game.width / 2f - 300;
		scoreBroad.y += game.height / 2f - 250;
		AddChild(scoreBroad);
	}

	void Update() {
		scoreBroad.SetText("Your score" + _score, 60, 255, 255, 255);
		frameTimer++;

		if (frameTimer % 180 == 0) 
		{
			LateDestroy();
		
		}



	}

}