using System;
using System.Drawing;
using GXPEngine;


public class TextBoard : GameObject
{
    private EasyDraw _easyDraw;
    private Font riffic;
    public TextBoard(int width, int height)
    {

        riffic = new Font("RifficFree - Bold.ttf", 17);
        _easyDraw = new EasyDraw(width, height, false);
        _easyDraw.TextFont(riffic);

        _easyDraw.TextAlign(CenterMode.Center, CenterMode.Center);

        AddChild(_easyDraw);

    }



    /// <summary>Set the text. Int numbers mean color in RGB format
    /// </summary>
    public void SetText(string text, float fontsize, int a = 0, int b = 0, int c = 0)
    {

        _easyDraw.Clear(Color.Transparent);
        
        _easyDraw.TextSize(fontsize);
        _easyDraw.Text(text, _easyDraw.width / 2, _easyDraw.height / 2);
        _easyDraw.Fill(a, b, c);

    }

}