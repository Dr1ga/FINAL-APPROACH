using System;
using GXPEngine;
using System.Drawing;
using System.Collections.Generic;


public class Level : GameObject
{
    Ball _ball;
    Constructor _hud;
    EasyDraw _text;
    NLineSegment _lineSegment;
    int _lineIndex;
    int _trapmsIndex;
    int _magasIndex;
    int _shelvesIndex;
    int _collidersIndex;

    public Sprite magwf;
    public Sprite trampwf;
    public Sprite shlfwf;
    public Sprite fon;

    //public List<NLineSegment> _colliders;
    public List<NLineSegment> _lines;
    public List<Trampoline> _tramps;
    public List<Magnit> _magas;
    public List<Shelf> _shelves;

    public float TOI;
    public Vec2 POI;

    public float oldBallDistance;
    public float ballDistance;
    public float ballDistanceToTramp;
    public float ballDistanceToShelf;

    public bool magnetInConst;
    public bool trampInConst;
    public bool shelfInConst;
    public bool isGameStarted;

    public int level;

    public Vec2 ballStartPos;
    public Level thatlevel;
    int _startSceneNumber = 1;

    LevelChanger levelC;
    public Level(int numberOfLevel) : base()
    {

        fon = new Sprite("fon.png");
        AddChild(fon);
        level = numberOfLevel;
        thatlevel = this;
        magwf = new Sprite("magnetWireframe.png");
        trampwf = new Sprite("trampolineWireframe.png");
        shlfwf = new Sprite("shelfBig.png");
        levelC = new LevelChanger("bucket.png");
        _ball = new Ball(30, ballStartPos, thatlevel);
        AddChild(shlfwf);
        AddChild(magwf);
        AddChild(trampwf);
        AddChild(levelC);

        magwf.SetXY(-500, -500);
        trampwf.SetXY(-500, -500);
        shlfwf.SetXY(-500, -500);
        levelC.SetXY(-500, -500);
        _lines = new List<NLineSegment>();
        _tramps = new List<Trampoline>();
        _magas = new List<Magnit>();
        _shelves = new List<Shelf>();
        //_colliders = new List<NLineSegment>();
        //_ball = new Ball(30, ballStartPos);





        _hud = new Constructor();

        
        AddChild(_hud);
        _text = new EasyDraw(250, 25);
        _text.TextAlign(CenterMode.Min, CenterMode.Min);
        AddChild(_text);

        oldBallDistance = 0;

        //_lineSegment = new NLineSegment(new Vec2(500, 500), new Vec2(100, 200), 0xff00ff00, 3);
        //AddChild(_lineSegment);
        //_lineSegment = new NLineSegment(new Vec2(game.width - 60, game.height - 200), new Vec2(50, game.height - 20));
        //AddChild(_lineSegment);

        LoadScene(1);



    }


    void AddLine(Vec2 start, Vec2 end)
    {

        Wall lineAng = new Wall(start, end);
        AddChild(lineAng);
        _lines.Add(lineAng);

    }

    void AddTrampoline(Vec2 start, Vec2 end, float rotation)
    {

        Trampoline trampoline = new Trampoline(end, start, rotation, "trampoline.png");
        AddChild(trampoline);
        _tramps.Add(trampoline);

    }

    void AddMagnete(Vec2 start, Vec2 end, float rotation)
    {

        Magnit maga = new Magnit(start, end, rotation, "magnet.png");
        AddChild(maga);
        _magas.Add(maga);

    }


    void AddShelf(Vec2 start, Vec2 end, float rotation)
    {

        Shelf polka = new Shelf(start, end, rotation, "shelfBig.png");
        AddChild(polka);
        _lines.Add(polka);

    }


    void Update()
    {
       oldBallDistance = ballDistance;

        _magasIndex++;
        if (_magasIndex >= _magas.Count)
        {
            _magasIndex = 0;
        }



        Vec2 differenceVectorMagnete = _magas[_magasIndex].end - _ball.position;
        Vec2 angledMagnete = _magas[_magasIndex].end - _magas[_magasIndex].start;
        Vec2 magneteNormal = angledMagnete.Normal();

        ballDistance = differenceVectorMagnete.Dot(magneteNormal.Normalized());

        if (Mathf.Abs(ballDistance) <= _ball.radius + 18)
        {
            Vec2 middle = (_magas[_magasIndex].end + _magas[_magasIndex].start) * 0.5f;
            Vec2 middletoBall = _ball.position - middle;

            if (middletoBall.Length() < (angledMagnete.Length() / 2) + _ball.radius + 18)
            {
                if (middletoBall.Length() > 0)
                {

                    //if (_magas[_magasIndex].end.y > _magas[_magasIndex].start.y)
                    //{

                    //    _ball.acceleration.y = -1.7f;
                    //}


                    //if (_magas[_magasIndex].end.y < _magas[_magasIndex].start.y)
                    //{

                    //    _ball.acceleration.y = 1.7f;
                    //}

                    Vec2 distance = _magas[_magasIndex].start - _ball.position;
                    _ball.velocity += distance.Normalized() *1.5f;

                }
            }

        }
        else
        if (Mathf.Abs(ballDistance) >= _ball.radius)
        {

            _ball.acceleration.y = 1;

        }




        if (Input.GetKeyDown(Key.ENTER))
        {

            isGameStarted = true;
            
        }

        if (isGameStarted == true) 
        {
            _ball.Step();

        }

   

        if (Input.GetMouseButtonDown(0) & _hud.magnetPNG.HitTestPoint(Input.mouseX, Input.mouseY) & trampInConst == false & shelfInConst == false)
        {

            
            magwf.SetOrigin(magwf.width/2, magwf.height/2);
            
            magwf.SetXY(_hud.bakingPosition.x , _hud.bakingPosition.y);
            _hud.isConstWork = true;
            
            magwf.rotation = 0;
            magnetInConst = true;
        }
        else
        if (Input.GetMouseButtonDown(0) & _hud.trampolinePNG.HitTestPoint(Input.mouseX, Input.mouseY) & magnetInConst == false & shelfInConst == false)
        {
            
            trampwf.SetOrigin(trampwf.width, 0);
            trampwf.SetXY(_hud.bakingPosition.x + 64, _hud.bakingPosition.y );
            _hud.isConstWork = true;
            trampwf.rotation = 0;
            trampInConst = true;
            
        }
        else
        if (Input.GetMouseButtonDown(0) & _hud.shelfPNG.HitTestPoint(Input.mouseX, Input.mouseY) & trampInConst == false & magnetInConst == false)
        {

            

            shlfwf.SetOrigin(shlfwf.width, 0);
            shlfwf.alpha = 0.4f;
            shlfwf.SetXY(_hud.bakingPosition.x + shlfwf.width/2, _hud.bakingPosition.y);
            _hud.isConstWork = true;
            
            shlfwf.rotation = 0;
            shelfInConst = true;
            

        }

        if (_hud.isConstWork == true)
        {


            if (magnetInConst == true)
            {

                if (Input.GetKey(Key.RIGHT))
                {

                    magwf.rotation++;

                }

                if (Input.GetKey(Key.LEFT))
                {
                    magwf.rotation--;

                }

                if (Input.GetKeyDown(Key.SPACE))
                {

                    magwf.SetXY(-500, -500);
                    AddMagnete(new Vec2(_hud.bakingPosition.x, _hud.bakingPosition.y), new Vec2(_hud.bakingPosition.x + 1, _hud.bakingPosition.y + 201), magwf.rotation);
                    _hud.isConstWork = false;
                    magnetInConst = false;

                }
            }

            if (trampInConst == true)
            {

                if (Input.GetKey(Key.RIGHT))
                {

                    
                    trampwf.rotation++;
                }

                if (Input.GetKey(Key.LEFT))
                {

                    
                    trampwf.rotation--;

                }

                if (Input.GetKeyDown(Key.SPACE))
                {

                    trampwf.SetXY(-500, -500);
                    AddTrampoline(new Vec2(_hud.bakingPosition.x - 64, _hud.bakingPosition.y), new Vec2(_hud.bakingPosition.x + 64, _hud.bakingPosition.y), trampwf.rotation);


                    _hud.isConstWork = false;
                    trampInConst = false;

                }
            }

            if (shelfInConst == true)
            {

                if (Input.GetKey(Key.RIGHT))
                {

                    //magwf.rotation++;
                    //trampwf.rotation++;
                    shlfwf.rotation++;

                }

                if (Input.GetKey(Key.LEFT))
                {
                    //magwf.rotation--;
                    //trampwf.rotation--;
                    shlfwf.rotation--;
                }

                if (Input.GetKeyDown(Key.SPACE))
                {
                    shlfwf.SetXY(-500, -500);
                    AddShelf (new Vec2(_hud.bakingPosition.x + shlfwf.width/2, _hud.bakingPosition.y), new Vec2(_hud.bakingPosition.x - shlfwf.width / 2, _hud.bakingPosition.y), shlfwf.rotation);


                    _hud.isConstWork = false;
                    shelfInConst = false;

                }

                
            }

            if (Input.GetKeyDown(Key.C))
            {

                shelfInConst = false;
                _hud.isConstWork = false;
                trampInConst = false;
                magnetInConst = false;
                shlfwf.SetXY(-500, -500);
                magwf.SetXY(-500, -500);
                trampwf.SetXY(-500, -500);
            }


        }


        if (Input.GetKey(Key.R))
        {

            //Level reslevel = new Level(level);
            //game.AddChild(reslevel);
            //this.LateDestroy();
            isGameStarted = false;
            LoadScene(_startSceneNumber);
        }

        if (levelC.HitTestPoint(_ball.position.x, _ball.position.y)) 
        {
            if (_startSceneNumber < 3)
            {
                _startSceneNumber++;
                LoadScene(_startSceneNumber);
            }
            else
            {

                _startSceneNumber = 0;


            }
        }

        //_text.Clear(Color.Transparent);
        //_text.Text("Distance to line: "+(ballDistance - _ball.radius), 0, 0);
        //_text.Text("point X: " + (Input.mouseX), 0, 0);
        //_text.Text("point Y: " + (Input.mouseY), 130, 0);

        
    }



    void LoadScene(int sceneNumber)
    {
        _startSceneNumber = sceneNumber;
        // remove previous scene:
        foreach (NLineSegment lineAng in _lines)
        {
            lineAng.Destroy();
        }
        _lines.Clear();

        foreach (Trampoline trampoline in _tramps)
        {
            trampoline.Destroy();
        }
        _tramps.Clear();

        foreach (Magnit maga in _magas)
        {
            maga.Destroy();
        }
        _magas.Clear();

        foreach (Shelf polka in _shelves)
        {
            polka.Destroy();
        }
        _shelves.Clear();
        

        // boundary:
        AddLine(new Vec2(game.width - 50, game.height - 50), new Vec2(50, game.height - 50));
        AddLine(new Vec2(50, game.height - 50), new Vec2(50, 50));
        AddLine(new Vec2(50, 50), new Vec2(game.width - 50, 50));
        AddLine(new Vec2(game.width - 50, 50), new Vec2(game.width - 50, game.height - 50));

        AddTrampoline(new Vec2(213131, 12312312), new Vec2(12313123, 123131), 0);
        AddMagnete(new Vec2(12312, 123132), new Vec2(123123, 123123), 0);
        AddShelf(new Vec2(12312, 123132), new Vec2(123123, 123123), 0);
        levelC.LateDestroy();
        _ball.LateDestroy();
        switch (sceneNumber)
        {
           
            case 1: // level one
                AddLine(new Vec2(1347, 636), new Vec2(1006, 705));
                levelC = new LevelChanger("bucket.png");
                levelC.SetXY(600, 920);
                ballStartPos = new Vec2(1600, 240);
                _ball = new Ball(30, ballStartPos, thatlevel);
                AddChild(levelC);
                AddChild(_ball);
                break;
            case 2: // level two


                AddLine(new Vec2(445, 437), new Vec2(295, 363));
                AddLine(new Vec2(583, 583), new Vec2(429, 497));
                levelC = new LevelChanger("bucket.png");
                levelC.SetXY(1600, 920);
                ballStartPos = new Vec2(370, 240);
                _ball = new Ball(30, ballStartPos, thatlevel);
                AddChild(levelC);
                AddChild(_ball);
                break;
            default: // 
                
                break;
        }
      
    }

    


}