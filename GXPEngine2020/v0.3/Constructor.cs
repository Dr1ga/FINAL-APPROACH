using System;
using GXPEngine;
using System.IO;

public class Constructor : GameObject
{
    private Sound _clickSound;
    public Sprite magnetPNG;
    public Sprite trampolinePNG;
    public Sprite shelfPNG;
    public Sprite itemBoard;
    //public Sprite point;

    public Cursor point;

    public bool isConstOpen = false;
    public bool isConstWork = false;

    public Vec2 bakingPosition;

    public Constructor()
    {
        //point = new Sprite("ball.png");
        point = new Cursor(0, 0, "cursor.png");
        magnetPNG = new Sprite("magnetPNG.png");
        trampolinePNG = new Sprite("trampoline.png");
        shelfPNG = new Sprite("shelf.png");
        itemBoard = new Sprite("HUD/top_items.png");

        AddChild(itemBoard);
        AddChild(magnetPNG);
        AddChild(trampolinePNG);
        AddChild(shelfPNG);
        AddChild(point);

        bakingPosition.SetXY(50021, 100 + 10000);
        magnetPNG.SetXY(game.width / 2 - itemBoard.width / 2 + 100, 20 + 10000);
        trampolinePNG.SetXY(game.width / 2 - trampolinePNG.width/2, 30 + 10000);
        shelfPNG.SetXY(game.width / 2 - itemBoard.width / 2 + 500, 35 + 10000);
        itemBoard.SetXY(game.width/2 - itemBoard.width/2, 10000);

        _clickSound = new Sound("1.mp3");

    }

    public void Update()
    {

        if (Input.GetMouseButton(0) & isConstOpen == false)
        {

            BakeCursor();
            OpenConstructor();
            isConstOpen = true;
            _clickSound.Play();
        }
        else

        if (Input.GetMouseButton(1) & isConstOpen == true & isConstWork == false)
        {

            CloseConstructor();
            isConstOpen = false;
            _clickSound.Play();
        }


        if (Input.GetKey(Key.A))
        {

            bakingPosition.x--;

        }
        if (Input.GetKey(Key.D))
        {

            bakingPosition.x++;

        }
        if (Input.GetKey(Key.W))
        {

            bakingPosition.y--;

        }
        if (Input.GetKey(Key.S))
        {

            bakingPosition.y++;

        }

        Point();
    }

    public void BakeCursor() 
    {

        bakingPosition = new Vec2(Input.mouseX, Input.mouseY);
        
    }

    public void Point() 
    {

        
        point.SetXY(bakingPosition.x, bakingPosition.y);

    }

    public void OpenConstructor() 
    {

        magnetPNG.y -= 10000;
        trampolinePNG.y -= 10000;
        shelfPNG.y -= 10000;
        itemBoard.y -= 10000;
    }

    public void CloseConstructor()
    {

        magnetPNG.y += 10000;
        trampolinePNG.y += 10000;
        shelfPNG.y += 10000;
        itemBoard.y += 10000;

    }

}