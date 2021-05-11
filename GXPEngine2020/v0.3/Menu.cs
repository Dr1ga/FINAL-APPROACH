using System;

using GXPEngine;

public class Menu : GameObject
{
    private Sound _clickSound;

    private Sprite _startButton;
    private Sprite _startButtonActive;

    private Sprite _exitButton;
    private Sprite _exitButtonActive;

    private Sprite _optionsButton;
    private Sprite _optionsButtonActive;

    private Sprite _goBackButton;
    private Sprite _goBackButtonActive;

    private Sprite _plusButton;
    private Sprite _plusButtonActive;

    private Sprite _minusButton;
    private Sprite _minusButtonActive;

    private Sprite _fon;

    private int _numberOfLevel;

    private Sound backgroundMusic;
    private SoundChannel soundChannel;

    public float soundVolume = 0.1f;
    private TextBoard _sVolume;
    public Menu(int numberOfLevel) : base()
    {


        backgroundMusic = new Sound("music_for_menu.mp3", true, true);
        
        soundChannel = backgroundMusic.Play();
        _numberOfLevel = numberOfLevel;

        //enter filenames of menu buttons. try use buttons not much bigger than in sample.
        //_clickSoundFilename = "ping.wav";

        _fon = new Sprite("fon.png");

        _startButton = new Sprite("HUD/main_menu/start/1_start_deactiv.png");
        _startButton.SetOrigin(_startButton.width / 2f, _startButton.height / 2f);
        _startButton.SetXY(game.width / 2f, 300);

        _startButtonActive = new Sprite("HUD/main_menu/start/1_start_active.png");
        _startButtonActive.SetOrigin(_startButtonActive.width / 2f, _startButtonActive.height / 2f);
        _startButtonActive.SetXY(game.width / 2f - 10000, 300 - 10000);

        _optionsButton = new Sprite("HUD/main_menu/options/2_options_deactiv.png");
        _optionsButton.SetOrigin(_optionsButton.width / 2f, _optionsButton.height / 2f);
        _optionsButton.SetXY(game.width / 2f, 500);

        _optionsButtonActive = new Sprite("HUD/main_menu/options/2_options_active.png");
        _optionsButtonActive.SetOrigin(_optionsButtonActive.width / 2f, _optionsButtonActive.height / 2f);
        _optionsButtonActive.SetXY(game.width / 2f, 500 - 10000);

        _exitButton = new Sprite("HUD/main_menu/exit/3_exit_deactiv.png");
        _exitButton.SetOrigin(_exitButton.width / 2f, _exitButton.height / 2f);
        _exitButton.SetXY(game.width / 2f, 700);

        _exitButtonActive = new Sprite("HUD/main_menu/exit/3_exit_active.png");
        _exitButtonActive.SetOrigin(_exitButtonActive.width / 2f, _exitButtonActive.height / 2f);
        _exitButtonActive.SetXY(game.width / 2f, 700 - 10000);

        _minusButton = new Sprite("HUD/main_menu/options/minus.png");
        _minusButton.SetOrigin(_minusButton.width / 2f, _minusButton.height / 2f);
        _minusButton.SetXY(game.width / 2f - 200 + 10000, game.height / 2f + 100);

        _minusButtonActive = new Sprite("HUD/main_menu/options/minus_active.png");
        _minusButtonActive.SetOrigin(_minusButtonActive.width / 2f, _minusButtonActive.height / 2f);
        _minusButtonActive.SetXY(game.width / 2f - 200 + 10000, game.height / 2f + 100);

        _plusButton = new Sprite("HUD/main_menu/options/plus.png");
        _plusButton.SetOrigin(_plusButton.width / 2f, _plusButton.height / 2f);
        _plusButton.SetXY(game.width / 2f + 200 + 10000, game.height / 2f + 100);

        _plusButtonActive = new Sprite("HUD/main_menu/options/plus_active.png");
        _plusButtonActive.SetOrigin(_plusButtonActive.width / 2f, _plusButtonActive.height / 2f);
        _plusButtonActive.SetXY(game.width / 2f + 200 + 10000, game.height / 2f + 100);

        _goBackButton = new Sprite("HUD/main_menu/options/go_back.png");
        _goBackButton.SetOrigin(_goBackButton.width / 2f, _goBackButton.height / 2f);
        _goBackButton.SetXY(game.width / 2f + 10000, game.height / 2f + 300);

        _goBackButtonActive = new Sprite("HUD/main_menu/options/go_back_active.png");
        _goBackButtonActive.SetOrigin(_goBackButtonActive.width / 2f, _goBackButtonActive.height / 2f);
        _goBackButtonActive.SetXY(game.width / 2f + 10000, game.height / 2f + 300);

        _sVolume = new TextBoard(220, 200);
        _sVolume.x += -10000;
        _sVolume.y += game.height / 2f - 150;

        AddChild(_fon);
        AddChild(_sVolume);
        AddChild(_startButton);
        AddChild(_startButtonActive);
        AddChild(_optionsButton);
        AddChild(_optionsButtonActive);
        AddChild(_exitButton);
        AddChild(_exitButtonActive);
        AddChild(_goBackButton);
        AddChild(_goBackButtonActive);
        AddChild(_minusButton);
        AddChild(_minusButtonActive);
        AddChild(_plusButton);
        AddChild(_plusButtonActive);
        //_clickSound = new Sound(_clickSoundFilename);

    }

    public void Update()
    {

        soundChannel.Volume = soundVolume;
        //--------------------------------------------------------------------
        //                             Buttons Active
        //--------------------------------------------------------------------

        if (_startButton.HitTestPoint(Input.mouseX, Input.mouseY))
        {

            _startButtonActive.SetXY(game.width / 2f, 300);



        }
        else 
        {
            _startButtonActive.SetXY(game.width / 2f - 10000, 200 - 10000);
        }

        if (_optionsButton.HitTestPoint(Input.mouseX, Input.mouseY))
        {
            
            _optionsButtonActive.SetXY(game.width / 2f, 500);



        }
        else
        {
            _optionsButtonActive.SetXY(game.width / 2f, 400 - 10000);
        }

        if (_exitButton.HitTestPoint(Input.mouseX, Input.mouseY))
        {

            _exitButtonActive.SetXY(game.width / 2f, 700);



        }
        else
        {
            _exitButtonActive.SetXY(game.width / 2f, 600 - 10000);
        }

        if (_goBackButton.HitTestPoint(Input.mouseX, Input.mouseY))
        {

            _goBackButtonActive.SetXY(game.width / 2f, game.height / 2f + 300);



        }
        else
        {
            _goBackButtonActive.SetXY(game.width / 2f + 10000, game.height / 2f + 300);
        }

        if (_minusButton.HitTestPoint(Input.mouseX, Input.mouseY))
        {

            _minusButtonActive.SetXY(game.width / 2f- 200, game.height / 2f + 100);



        }
        else
        {
            _minusButtonActive.SetXY(game.width / 2f, game.height / 2f + 100 - 10000);
        }

        if (_plusButton.HitTestPoint(Input.mouseX, Input.mouseY))
        {

            _plusButtonActive.SetXY(game.width / 2f +200, game.height / 2f + 100);



        }
        else
        {
            _plusButtonActive.SetXY(game.width / 2f, game.height / 2f + 100 - 10000);
        }

        //--------------------------------------------------------------------
        //                             Buttons
        //--------------------------------------------------------------------

        if (Input.GetMouseButtonDown(0))
        {
            if (_startButton.HitTestPoint(Input.mouseX, Input.mouseY))
            {
                StartGame();
                //clickSound.Play();
            }
            if (_optionsButton.HitTestPoint(Input.mouseX, Input.mouseY))
            {

                _startButton.x += 10000;
                _exitButton.x += 10000;
                _optionsButton.x += 10000;
                _goBackButton.x -= 10000;
                _minusButton.x -= 10000;
                _plusButton.x -= 10000;
                _sVolume.x = game.width / 2f - 110;
            }
            if (_exitButton.HitTestPoint(Input.mouseX, Input.mouseY))
            {
                game.Destroy();
                //clickSound.Play();
            }
            if (_goBackButton.HitTestPoint(Input.mouseX, Input.mouseY))
            {

                _goBackButton.x += 10000;
                _minusButton.x += 10000;
                _plusButton.x += 10000;
                _startButton.x -= 10000;
                _exitButton.x -= 10000;
                _optionsButton.x -= 10000;
                _sVolume.x = 10000;
                //clickSound.Play();
            }

            if (_minusButton.HitTestPoint(Input.mouseX, Input.mouseY) & soundVolume >= 0.01)
            {

                soundVolume -= 0.01f;

                //clickSound.Play();
            }
            if (_plusButton.HitTestPoint(Input.mouseX, Input.mouseY) & soundVolume <= 0.99)
            {

                soundVolume += 0.01f;

                //clickSound.Play();
            }

        }


        _sVolume.SetText(""+ Math.Round(soundVolume * 100), 100);

    }




    //--------------------------------------------------------------------
    //                             Setup Game
    //--------------------------------------------------------------------
    private void StartGame()
    {
        Level level = new Level(1);
        game.AddChild(level);
        LateDestroy();
    }
}
