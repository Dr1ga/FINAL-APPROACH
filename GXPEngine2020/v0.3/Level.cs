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

    private Sound _clickSound;

    public Sprite magwf;
    public Sprite trampwf;
    public Sprite shlfwf;
    public Sprite fon;
    public Sprite fonblack;
    //public List<NLineSegment> _colliders;
    public List<NLineSegment> _lines;
    public List<Trampoline> _tramps;
    public List<Magnit> _magas;
    public List<Shelf> _shelves;
    public List<Sprite> _boxes;

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
    public bool isLevelPassed = false;
    public bool isAnimStart = false;

    public int level;

    public int fpsTimer;

    public Vec2 ballStartPos;
    public Level thatlevel;
    int _startSceneNumber = 1;

    public Sprite scoreBoard;
    private Sprite _cursorIMG;

    public Sprite menuButton;

    public Sound levelSound;
    public SoundChannel soundChannel;

    public int score;
    public TextBoard boardWothScore;

    LevelChanger levelC;
    private int oldscore;
    public AnimationSprite crane;
    public Level(int numberOfLevel, float pSoundVolume) : base()
    {
        
        levelSound = new Sound("level_music_v2.mp3", true, true);
        soundChannel = levelSound.Play();
        soundChannel.Volume = pSoundVolume;

        _cursorIMG = new Sprite("HUD/cursor_PNG50.png");
        scoreBoard = new Sprite("HUD/score_board.png");

        fon = new Sprite("Level background hazy.png");
        fonblack = new Sprite("blackpng.png");
        AddChild(fon);
        level = numberOfLevel;
        thatlevel = this;
        magwf = new Sprite("magnet.png");
        trampwf = new Sprite("trampoline.png");
        shlfwf = new Sprite("shelfBig.png");
        levelC = new LevelChanger("bucket.png");

        _ball = new Ball(30, ballStartPos, thatlevel);

        crane = new AnimationSprite("spritesheet.png", 6, 1);

        magwf.SetXY(-500, -500);
        trampwf.SetXY(-500, -500);
        shlfwf.SetXY(-500, -500);
        levelC.SetXY(-500, -500);
        _lines = new List<NLineSegment>();
        _tramps = new List<Trampoline>();
        _magas = new List<Magnit>();
        _shelves = new List<Shelf>();
        _boxes = new List<Sprite>();
        boardWothScore = new TextBoard(220, 200);
        boardWothScore.x += game.width / 2 - 110;
        boardWothScore.y += game.height - 175;
        scoreBoard.SetXY(game.width/2 - scoreBoard.width/2, game.height - scoreBoard.height);

        menuButton = new Sprite("HUD/exit.png");
        menuButton.SetXY(game.width - menuButton.width * 1.5f, 15);



        _hud = new Constructor();

        

        oldBallDistance = 0;

        LoadScene(level);
        //LoadScene(4);


        AddChild(shlfwf);
        AddChild(magwf);
        AddChild(trampwf);
        
        AddChild(scoreBoard);
        AddChild(boardWothScore);
        AddChild(menuButton);
        
        AddChild(levelC);
        AddChild(crane);
        AddChild(_hud);
        //AddChild(fonblack);
        _clickSound = new Sound("1.mp3");
        crane.SetFrame(0);
        AddChild(_cursorIMG);
        //fonblack.alpha = 0.1f;
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

    void AddBox(Vec2 position)
    {
        Sprite korobka = new Sprite("box.png");
        korobka.SetXY(position.x, position.y);
        AddChild(korobka);
        _boxes.Add(korobka);
    }

    void Update()
    {

        fpsTimer++;
        boardWothScore.SetText("" + score, 40);

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

                    Vec2 distance = _magas[_magasIndex].start - _ball.position;
                    _ball.velocity += distance.Normalized() * 1.5f;

                }
            }

        }
        else
        if (Mathf.Abs(ballDistance) >= _ball.radius)
        {

            _ball.acceleration.y = 1;

        }
        
        

     if (Input.GetMouseButtonDown(0) & menuButton.HitTestPoint(Input.mouseX, Input.mouseY))
        {

            Menu menu = new Menu(level, soundChannel.Volume);
            game.AddChild(menu);
            LateDestroy();
            soundChannel.Stop();
            _clickSound.Play();

        }
        else
     if (Input.GetMouseButtonDown(0) & _hud.magnetPNG.HitTestPoint(Input.mouseX, Input.mouseY) & trampInConst == false & shelfInConst == false)
        {

            magwf.SetOrigin(magwf.width/2, magwf.height/2);
            magwf.SetXY(_hud.bakingPosition.x , _hud.bakingPosition.y);
            magwf.alpha = 0.4f;
            _hud.isConstWork = true;
            magwf.rotation = 0;
            magnetInConst = true;
            _clickSound.Play();

        }
        else
        if (Input.GetMouseButtonDown(0) & _hud.trampolinePNG.HitTestPoint(Input.mouseX, Input.mouseY) & magnetInConst == false & shelfInConst == false)
        {
            
            trampwf.SetOrigin(trampwf.width, 0);
            trampwf.SetXY(_hud.bakingPosition.x + 64, _hud.bakingPosition.y );
            trampwf.alpha = 0.4f;
            _hud.isConstWork = true;
            trampwf.rotation = 0;
            trampInConst = true;
            _clickSound.Play();

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
            _clickSound.Play();

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
                    score -= 20;
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
                    score -= 15;

                    _hud.isConstWork = false;
                    trampInConst = false;

                }
            }

            if (shelfInConst == true)
            {

                if (Input.GetKey(Key.RIGHT))
                {

                    shlfwf.rotation++;

                }

                if (Input.GetKey(Key.LEFT))
                {

                    shlfwf.rotation--;

                }

                if (Input.GetKeyDown(Key.SPACE))
                {

                    shlfwf.SetXY(-500, -500);
                    AddShelf (new Vec2(_hud.bakingPosition.x + shlfwf.width/2, _hud.bakingPosition.y), new Vec2(_hud.bakingPosition.x - shlfwf.width / 2, _hud.bakingPosition.y), shlfwf.rotation);
                    score -= 10;

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

            isGameStarted = false;
            LoadScene(_startSceneNumber);
            

        }

        if (Input.GetKey(Key.F))
        {
            isGameStarted = false;
            ResetBall(_startSceneNumber);
        }

        if (Input.GetKeyDown(Key.ENTER))
        {

            isAnimStart = true;
            //isGameStarted = true;

        }

        if (isAnimStart == true)
        {
            if (fpsTimer % 7.5 == 0)
            {
                crane.NextFrame();
            }

            if (crane.currentFrame >= 3)
            {
                
                isGameStarted = true;
                
            }
            if (crane.currentFrame >= 5)
            {
                isAnimStart = false;
                

            }
        }
        
        if (isGameStarted == true)
        {
           
            _ball.Step();
            
        }

        if (levelC.HitTestPoint(_ball.position.x, _ball.position.y)) 
        {
            
            
            //isLevelPassed = true;
            if (_startSceneNumber <= 4)
            {
                _startSceneNumber++;
                oldscore = score;

                LoadScene(_startSceneNumber);
                Transit transit = new Transit(oldscore);
                AddChild(transit);
            }


            else
            {


                _startSceneNumber = 1;
                LoadScene(_startSceneNumber);

            }


        }



       

                

            


            


        if (Input.mouseX >= 0 & Input.mouseX <= game.width & Input.mouseY >= 0 & Input.mouseY <= game.height)
        {
            _cursorIMG.SetXY(Input.mouseX, Input.mouseY);
        }
        
    }



    void LoadScene(int sceneNumber)
    {
        
        _startSceneNumber = sceneNumber;
        isGameStarted = false;
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

            foreach (Sprite korobka in _boxes)
            {
                korobka.Destroy();
            }
            _boxes.Clear();
        
        // boundary:
        

        AddTrampoline(new Vec2(213131, 12312312), new Vec2(12313123, 123131), 0);
        AddMagnete(new Vec2(12312, 123132), new Vec2(123123, 123123), 0);
        AddShelf(new Vec2(12312, 123132), new Vec2(123123, 123123), 0);
        levelC.LateDestroy();
        _ball.LateDestroy();
        _hud.LateDestroy();
        crane.LateDestroy();
        _cursorIMG.LateDestroy();
        //fonblack.LateDestroy();
        //scoreBoard.LateDestroy();
        //boardWothScore.LateDestroy();
        //menuButton.LateDestroy();
        switch (sceneNumber)
        {
            case 1: // level two
                AddBox(new Vec2(-5, -5));
                AddBox(new Vec2(-5, 125));
                AddBox(new Vec2(-5, 255));
                AddBox(new Vec2(-5, 385));
                AddBox(new Vec2(-5, 515));
                AddBox(new Vec2(-5, 645));
                AddBox(new Vec2(-5, 775));
                AddBox(new Vec2(-5, 905));
                AddBox(new Vec2(-5, 1035));

                //AddBox(new Vec2(game.width - 125, -5));
                AddBox(new Vec2(1870, 125));
                AddBox(new Vec2(1870, 255));
                AddBox(new Vec2(1870, 385));
                AddBox(new Vec2(1870, 515));
                AddBox(new Vec2(1870, 645));
                AddBox(new Vec2(1870, 775));
                AddBox(new Vec2(1870, 905));
                //AddBox(new Vec2(game.width - 125, 1035));

                AddBox(new Vec2(120, -5));
                AddBox(new Vec2(245, -5));
                AddBox(new Vec2(370, -5));
                AddBox(new Vec2(495, -5));
                AddBox(new Vec2(620, -5));
                AddBox(new Vec2(745, -5));
                AddBox(new Vec2(870, -5));
                AddBox(new Vec2(995, -5));
                AddBox(new Vec2(1120, -5));
                AddBox(new Vec2(1245, -5));
                AddBox(new Vec2(1370, -5));
                AddBox(new Vec2(1495, -5));
                AddBox(new Vec2(1620, -5));
                AddBox(new Vec2(1745, -5));
                AddBox(new Vec2(1870, -5));

                AddBox(new Vec2(120, 905));
                AddBox(new Vec2(245, 905));
                AddBox(new Vec2(370, 905));
                AddBox(new Vec2(495, 905));
                AddBox(new Vec2(620, 905));
                AddBox(new Vec2(745, 905));
                AddBox(new Vec2(870, 905));
                AddBox(new Vec2(995, 905));
                AddBox(new Vec2(1120, 905));
                AddBox(new Vec2(1245, 905));
                AddBox(new Vec2(1370, 905));
                AddBox(new Vec2(1495, 905));
                AddBox(new Vec2(1620, 905));
                AddBox(new Vec2(1745, 905));
                AddBox(new Vec2(1870, 905));

                AddBox(new Vec2(120, 1035));
                AddBox(new Vec2(245, 1035));
                AddBox(new Vec2(370, 1035));
                AddBox(new Vec2(495, 1035));
                AddBox(new Vec2(620, 1035));
                AddBox(new Vec2(745, 1035));
                AddBox(new Vec2(870, 1035));
                AddBox(new Vec2(995, 1035));
                AddBox(new Vec2(1120, 1035));
                AddBox(new Vec2(1245, 1035));
                AddBox(new Vec2(1370, 1035));
                AddBox(new Vec2(1495, 1035));
                AddBox(new Vec2(1620, 1035));
                AddBox(new Vec2(1745, 1035));
                AddBox(new Vec2(1870, 1035));


                

                AddLine(new Vec2(game.width - 50, game.height - 175), new Vec2(129, game.height - 175));
                AddLine(new Vec2(129, game.height - 175), new Vec2(129, 134));
                AddLine(new Vec2(129, 134), new Vec2(game.width - 50, 134));
                AddLine(new Vec2(game.width - 50, 134), new Vec2(game.width - 50, game.height - 175));

                //AddLine(new Vec2(1179, game.height / 2 - 100), new Vec2(800, game.height / 2 - 100));
                //AddLine(new Vec2(800, game.height / 2 + 34), new Vec2(1179, game.height / 2 + 34));
                //AddLine(new Vec2(800, game.height / 2 - 100), new Vec2(800, game.height / 2 + 34));
                //AddLine(new Vec2(1179, game.height / 2 + 34), new Vec2(1179, game.height / 2 - 100));
                _hud = new Constructor();
                levelC = new LevelChanger("bucket.png");
                levelC.SetXY(1750, 820);
                ballStartPos = new Vec2(290, 240);
                //crane = new AnimationSprite("spritesheet.png", 3, 2);
                crane = new AnimationSprite("spritesheet.png", 6, 1);
                crane.SetFrame(0);
                crane.SetXY(185, 30);
                _ball = new Ball(30, ballStartPos, thatlevel);
                AddChild(levelC);
                AddChild(_hud);
                AddChild(_ball);
                AddChild(crane);
                score = 1000;
                

                

                boardWothScore = new TextBoard(220, 200);
                boardWothScore.x += game.width / 2 - 110;
                boardWothScore.y += game.height - 175;
                scoreBoard.SetXY(game.width / 2 - scoreBoard.width / 2, game.height - scoreBoard.height);

                menuButton = new Sprite("HUD/exit.png");
                menuButton.SetXY(game.width - menuButton.width * 1.5f, 15);
                AddChild(scoreBoard);
                AddChild(boardWothScore);
                AddChild(menuButton);

                _cursorIMG = new Sprite("HUD/cursor_PNG50.png");
                AddChild(_cursorIMG);

                break;
            case 2: // level one

                AddBox(new Vec2(800, game.height / 2 - 100));
                AddBox(new Vec2(925, game.height / 2 - 100));
                AddBox(new Vec2(1050, game.height / 2 - 100));

                AddLine(new Vec2(1179, game.height / 2 - 100), new Vec2(800, game.height / 2 - 100));
                AddLine(new Vec2(800, game.height / 2 + 34), new Vec2(1179, game.height / 2 + 34));
                AddLine(new Vec2(800, game.height / 2 - 100), new Vec2(800, game.height / 2 + 34));
                AddLine(new Vec2(1179, game.height / 2 + 34), new Vec2(1179, game.height / 2 - 100));

                crane = new AnimationSprite("spritesheet.png", 6, 1);
                crane.SetFrame(0);
                crane.SetXY(785, 30);
                _hud = new Constructor();
                levelC = new LevelChanger("bucket.png");
                levelC.SetXY(960, 870);
                ballStartPos = new Vec2(890, 240);
                _ball = new Ball(30, ballStartPos, thatlevel);
                AddChild(levelC);
                AddChild(_hud);
                AddChild(_ball);
                AddChild(crane);
                score = 100;
                

                


                _cursorIMG = new Sprite("HUD/cursor_PNG50.png");
                AddChild(_cursorIMG);
                break;
            case 3: // level two
                AddBox(new Vec2(900, -5));
                AddBox(new Vec2(900, 125));
                AddBox(new Vec2(900, 255));
                
                

                AddLine(new Vec2(900, -5), new Vec2(900, 255 + 134));
                AddLine(new Vec2(1029, 255 + 134), new Vec2(1029, -5));
                AddLine(new Vec2(900, 255 + 134), new Vec2(1029, 255 + 134));
                AddLine(new Vec2(1029, -5), new Vec2(900, -5));


                levelC = new LevelChanger("bucket.png");
                AddBox(new Vec2(1735, 690));
                AddBox(new Vec2(1735, 820));
                AddBox(new Vec2(1735, 950));

                AddLine(new Vec2(1735 + 129, 690), new Vec2(1735, 690));
                AddLine(new Vec2(1735, 690), new Vec2(1735, 950 + 134));
                AddLine(new Vec2(1735 + 129, 950 + 134), new Vec2(1735 + 129, 690));

                crane = new AnimationSprite("spritesheet.png", 6, 1);
                crane.SetFrame(0);
                crane.SetXY(565, 30);
                _hud = new Constructor();
                levelC.SetXY(1800, 620);
                ballStartPos = new Vec2(670, 240);
                _ball = new Ball(30, ballStartPos, thatlevel);
                AddChild(levelC);
                AddChild(_hud);
                AddChild(_ball);
                AddChild(crane);
                score = 150;
                

                
                _cursorIMG = new Sprite("HUD/cursor_PNG50.png");
                AddChild(_cursorIMG);
                break;
            case 4: // level one
                AddBox(new Vec2(900, -5));
                AddBox(new Vec2(900, 125));
                AddBox(new Vec2(900, 255));

                AddLine(new Vec2(900, -5), new Vec2(900, 255 + 134));
                AddLine(new Vec2(1029, 255 + 134), new Vec2(1029, -5));
                AddLine(new Vec2(900, 255 + 134), new Vec2(1029, 255 + 134));
                AddLine(new Vec2(1029, -5), new Vec2(900, -5));

                AddBox(new Vec2(1235, -5));
                AddBox(new Vec2(1235, 125));
                AddBox(new Vec2(1235, 255));

                AddLine(new Vec2(1235, -5), new Vec2(1235, 255 + 134));
                AddLine(new Vec2(1235 + 129, 255 + 134), new Vec2(1235 + 129, -5));
                AddLine(new Vec2(1235, 255 + 134), new Vec2(1235 + 129, 255 + 134));
                AddLine(new Vec2(1235 + 129, -5), new Vec2(1235, -5));

                AddBox(new Vec2(900, 810));

                AddLine(new Vec2(900, 810), new Vec2(900, 810 + 134));
                AddLine(new Vec2(1029, 810 + 134), new Vec2(1029, 810));
                AddLine(new Vec2(900, 810 + 134), new Vec2(1029, 810 + 134));
                AddLine(new Vec2(1029, 810), new Vec2(900, 810));

                levelC = new LevelChanger("bucket.png");
                AddBox(new Vec2(1235, 690));
                AddBox(new Vec2(1235, 820));
                AddBox(new Vec2(1235, 950));

                AddLine(new Vec2(1235 + 129, 690), new Vec2(1235, 690));
                AddLine(new Vec2(1235, 690), new Vec2(1235, 950 + 134));
                AddLine(new Vec2(1235 + 129, 950 + 134), new Vec2(1235 + 129, 690));

                crane = new AnimationSprite("spritesheet.png", 6, 1);
                crane.SetFrame(0);
                crane.SetXY(1395, 60);

                //levelC.SetXY(1800, 620);
                levelC.SetXY(750, 920);
                AddBox(new Vec2(685, 990));
                ballStartPos = new Vec2(1500, 270);
                _ball = new Ball(30, ballStartPos, thatlevel);
                _hud = new Constructor();
                AddChild(levelC);
                AddChild(_hud);
                AddChild(_ball);
                AddChild(crane);
                score = 150;
                

                
                _cursorIMG = new Sprite("HUD/cursor_PNG50.png");
                AddChild(_cursorIMG);
                break;
            
            default: // 
                
                break;
        }
      
    }

    void ResetBall(int sceneNumber)
    {

        _startSceneNumber = sceneNumber;
        _ball.LateDestroy();
        crane.LateDestroy();
        _hud.LateDestroy();
        switch (sceneNumber)
        {

            case 1: // level one
                crane = new AnimationSprite("spritesheet.png", 6, 1);
                crane.SetFrame(0);
                crane.SetXY(185, 30);
                ballStartPos = new Vec2(290, 240);
                _ball = new Ball(30, ballStartPos, thatlevel);
                _hud = new Constructor();
                AddChild(_hud);
                AddChild(_ball);
                AddChild(crane);
                break;
            case 2: // level two
                crane = new AnimationSprite("spritesheet.png", 6, 1);
                crane.SetFrame(0);
                crane.SetXY(785, 30);
                ballStartPos = new Vec2(890, 240);
                _ball = new Ball(30, ballStartPos, thatlevel);
                _hud = new Constructor();
                AddChild(_hud);
                AddChild(_ball);
                AddChild(crane);
                break;
            case 3: // level tree
                crane = new AnimationSprite("spritesheet.png", 6, 1);
                crane.SetFrame(0);
                crane.SetXY(565, 30);
                ballStartPos = new Vec2(670, 240);
                _ball = new Ball(30, ballStartPos, thatlevel);
                _hud = new Constructor();
                AddChild(_hud);
                AddChild(_ball);
                AddChild(crane);
                break;
            case 4: // level 4
                crane = new AnimationSprite("spritesheet.png", 6, 1);
                crane.SetFrame(0);
                crane.SetXY(1395, 60);
                ballStartPos = new Vec2(1500, 270);
                _ball = new Ball(30, ballStartPos, thatlevel);
                _hud = new Constructor();
                AddChild(_hud);
                AddChild(_ball);
                AddChild(crane);
                break;
            default: // 

                break;
        }

    }


}