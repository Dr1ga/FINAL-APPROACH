using System;
using System.Drawing;
using GXPEngine;


public class TextBoard : GameObject
{
    private EasyDraw _easyDraw;
    
    public TextBoard(int width, int height)
    {

        
        _easyDraw = new EasyDraw(width, height, false);
        

        _easyDraw.TextAlign(CenterMode.Center, CenterMode.Center);


        AddChild(_easyDraw);

    }



    /// <summary>Set the text. Int numbers mean color in RGB format
    /// </summary>
    public void SetText(string text, int red, int green, int blue)
    {

        _easyDraw.Clear(red, green, blue);


        _easyDraw.Text(text, _easyDraw.width / 2, _easyDraw.height / 2);


    }

}