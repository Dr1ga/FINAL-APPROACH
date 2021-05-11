using System;
using GXPEngine;
using System.IO;

public class Constructor : GameObject
{
    
    public Sprite magnetPNG;
    public Sprite trampolinePNG;
    public Sprite shelfPNG;
    public Sprite point;

    public bool isConstOpen = false;
    public bool isConstWork = false;

    public Vec2 bakingPosition;

    public Constructor()
    {
        point = new Sprite("ball.png");
        
        magnetPNG = new Sprite("magnetPNG.png");
        trampolinePNG = new Sprite("trampoline.png");
        shelfPNG = new Sprite("shelf.png");

        AddChild(magnetPNG);
        AddChild(trampolinePNG);
        AddChild(shelfPNG);
        AddChild(point);

        bakingPosition.SetXY(50021, 100 + 10000);
        magnetPNG.SetXY(500, 100 + 10000);
        trampolinePNG.SetXY(800, 100 + 10000);
        shelfPNG.SetXY(1100, 100 + 10000);

    }

    public void Update()
    {

        if (Input.GetMouseButton(0) & isConstOpen == false)
        {

            BakeCursor();
            OpenConstructor();
            isConstOpen = true;

        }
        else

        if (Input.GetMouseButton(1) & isConstOpen == true & isConstWork == false)
        {

            CloseConstructor();
            isConstOpen = false;

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

    }

    public void CloseConstructor()
    {

        magnetPNG.y += 10000;
        trampolinePNG.y += 10000;
        shelfPNG.y += 10000;

    }

}