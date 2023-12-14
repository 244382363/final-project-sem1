using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace final_project_sem1
{
    public class Game1 : Game
    {
        enum GameStates
        {
            St_screen,
            Skin_select,
            Htp_screen,
            cut_scene1,
            pause_screen,
            Gameplayscreen_level1,
            Gameplayscreen_level2,
            Gameplayscreen_level3,
            Gameplayscreen_level4,
            Gameplayscreen_level5

        }
        

        Point MAXSHAKE = new Point(8, 8);


        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private MouseState _mouseState;
        KeyboardState kb, oldkb;
        RenderTarget2D DrawCanvas;
        Vector2 CanvasLocation;
        int bouncesLeft;
        SoundEffect bgm;


        public static Texture2D pixel;
        public int  bricks_destroyed;

        public static readonly Random RNG = new Random();
        GamePadState padcurr;

        GameStates _currState;
        
        SpriteFont debugFont, GuideFont;
        background bgd3, bgd1, bgd2, cut_scene1;
        List<Bricks> bricks_htp, bricks_lv1, bricks_lv2, bricks_lv3, bricks_lv4, bricks_lv5;
        List<ball> balls, balls_skin_select, balls_brick_spawn;
        ball _balls;
        Bat bat;
        buttons st_button, bk_button, sk_button, nxt_button, htp_button;
        



        int BOUNCECOUNT = 3;
        const int NOOFSC_BACKGROUNDS = 3;

        struct scrollingBG
        {
            public Rectangle _rect;
            public Texture2D _txr;
        }
        scrollingBG[] bgds;

        scrollingBG visibleScreen;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;


            _graphics.PreferredBackBufferWidth = 1024;
            _graphics.PreferredBackBufferHeight = 990;
        }



        protected override void Initialize()
        {
            
            _currState = GameStates.St_screen;
            bricks_htp = new List<Bricks>();
            bricks_lv1 = new List<Bricks>();
            bricks_lv2 = new List<Bricks>();
            bricks_lv3 = new List<Bricks>();
            bricks_lv4 = new List<Bricks>();
            bricks_lv5 = new List<Bricks>();
            balls = new List<ball>();
            balls_brick_spawn = new List<ball>();
            balls_skin_select = new List<ball>();

            
            bricks_destroyed = 0;


            bgds = new scrollingBG[NOOFSC_BACKGROUNDS];
            DrawCanvas = new RenderTarget2D(_graphics.GraphicsDevice, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
            CanvasLocation = Vector2.Zero;

            for (int i = 0; i < NOOFSC_BACKGROUNDS; i++)
            {
                // Set their X, Y, Height and Width
                bgds[i]._rect = new Rectangle(0,i*990,1024,990);
            }

            // Set up the viewable overlay
            visibleScreen._rect = new Rectangle(300, 25, 200, 150);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            GuideFont = Content.Load<SpriteFont>("Ariel07");
            //testInput = new InputBox(Content.Load<Texture2D>("TextboxUI"), Content.Load<SpriteFont>("UIFont"));
            debugFont = Content.Load<SpriteFont>("Ariel07");
            // Go through the backgrounds one by one and set up their textures
            for (int i = 0; i < NOOFSC_BACKGROUNDS; i++)
            {
                bgds[i]._txr = Content.Load<Texture2D>("sc_bgd" + i);
            }

            // Load the visible area overlay
            //visibleScreen._txr = Content.Load<Texture2D>("viewable");

            pixel = Content.Load<Texture2D>("pixel");

            //bricks for display in how to play screen
            bricks_htp = new List<Bricks>();
            bricks_htp.Add(new Bricks(Content.Load<Texture2D>("brick"), 190, 250, 0));
            bricks_htp.Add(new Bricks(Content.Load<Texture2D>("brick_ball_spawn"), 190, 350, 0));
            bricks_htp.Add(new Bricks(Content.Load<Texture2D>("brick_fortify"), 190, 450, 0));
            bricks_htp.Add(new Bricks(Content.Load<Texture2D>("brick_glass"), 190, 550, 0));
            bricks_htp.Add(new Bricks(Content.Load<Texture2D>("extra_life_brick"), 190, 650, 0));

            //all the bricks for lv1
            #region bricks_lv1 
            bricks_lv1 = new List<Bricks>();
            bricks_lv1.Add(new Bricks(Content.Load<Texture2D>("brick"), 80, 70, 0));
            bricks_lv1.Add(new Bricks(Content.Load<Texture2D>("brick"), 130, 70, 0));
            bricks_lv1.Add(new Bricks(Content.Load<Texture2D>("brick"), 180, 70, 0));
            bricks_lv1.Add(new Bricks(Content.Load<Texture2D>("brick"), 230, 70, 0));
            bricks_lv1.Add(new Bricks(Content.Load<Texture2D>("brick"), 280, 70, 0));
            bricks_lv1.Add(new Bricks(Content.Load<Texture2D>("brick"), 330, 70, 0));
            bricks_lv1.Add(new Bricks(Content.Load<Texture2D>("brick"), 380, 70, 0));
            bricks_lv1.Add(new Bricks(Content.Load<Texture2D>("brick"), 430, 70, 0));
            bricks_lv1.Add(new Bricks(Content.Load<Texture2D>("brick"), 480, 70, 0));
            bricks_lv1.Add(new Bricks(Content.Load<Texture2D>("brick"), 530, 70, 0));
            bricks_lv1.Add(new Bricks(Content.Load<Texture2D>("brick"), 580, 70, 0));
            bricks_lv1.Add(new Bricks(Content.Load<Texture2D>("brick"), 630, 70, 0));
            bricks_lv1.Add(new Bricks(Content.Load<Texture2D>("brick"), 680, 70, 0));
            bricks_lv1.Add(new Bricks(Content.Load<Texture2D>("brick"), 730, 70, 0));
            bricks_lv1.Add(new Bricks(Content.Load<Texture2D>("brick"), 780, 70, 0));
            bricks_lv1.Add(new Bricks(Content.Load<Texture2D>("brick"), 830, 70, 0));
            bricks_lv1.Add(new Bricks(Content.Load<Texture2D>("brick"), 880, 70, 0));
            bricks_lv1.Add(new Bricks(Content.Load<Texture2D>("brick"), 80, 95, 0));
            bricks_lv1.Add(new Bricks(Content.Load<Texture2D>("brick"), 130, 120, 0));
            bricks_lv1.Add(new Bricks(Content.Load<Texture2D>("brick"), 180, 145, 0));
            bricks_lv1.Add(new Bricks(Content.Load<Texture2D>("brick"), 230, 170, 0));
            bricks_lv1.Add(new Bricks(Content.Load<Texture2D>("brick"), 280, 195, 0));
            bricks_lv1.Add(new Bricks(Content.Load<Texture2D>("brick"), 330, 220, 0));
            bricks_lv1.Add(new Bricks(Content.Load<Texture2D>("brick"), 380, 245, 0));
            bricks_lv1.Add(new Bricks(Content.Load<Texture2D>("brick"), 430, 270, 0));
            bricks_lv1.Add(new Bricks(Content.Load<Texture2D>("brick"), 480, 295, 0));
            bricks_lv1.Add(new Bricks(Content.Load<Texture2D>("brick"), 530, 320, 0));
            bricks_lv1.Add(new Bricks(Content.Load<Texture2D>("brick"), 580, 345, 0));
            bricks_lv1.Add(new Bricks(Content.Load<Texture2D>("brick"), 630, 370, 0));
            bricks_lv1.Add(new Bricks(Content.Load<Texture2D>("brick"), 680, 395, 0));
            bricks_lv1.Add(new Bricks(Content.Load<Texture2D>("brick"), 730, 420, 0));
            bricks_lv1.Add(new Bricks(Content.Load<Texture2D>("brick"), 780, 445, 0));
            bricks_lv1.Add(new Bricks(Content.Load<Texture2D>("brick"), 830, 470, 0));
            bricks_lv1.Add(new Bricks(Content.Load<Texture2D>("brick"), 880, 495, 0));
            bricks_lv1.Add(new Bricks(Content.Load<Texture2D>("brick"), 80, 120, 0));
            bricks_lv1.Add(new Bricks(Content.Load<Texture2D>("brick"), 130, 95, 0));
            bricks_lv1.Add(new Bricks(Content.Load<Texture2D>("brick"), 180, 95, 0));
            bricks_lv1.Add(new Bricks(Content.Load<Texture2D>("brick"), 230, 95, 0));
            bricks_lv1.Add(new Bricks(Content.Load<Texture2D>("brick"), 280, 95, 0));
            bricks_lv1.Add(new Bricks(Content.Load<Texture2D>("brick"), 330, 95, 0));
            bricks_lv1.Add(new Bricks(Content.Load<Texture2D>("brick"), 380, 95, 0));
            bricks_lv1.Add(new Bricks(Content.Load<Texture2D>("brick"), 430, 95, 0));
            bricks_lv1.Add(new Bricks(Content.Load<Texture2D>("brick"), 480, 95, 0));
            bricks_lv1.Add(new Bricks(Content.Load<Texture2D>("brick"), 530, 95, 0));
            bricks_lv1.Add(new Bricks(Content.Load<Texture2D>("brick"), 580, 95, 0));
            bricks_lv1.Add(new Bricks(Content.Load<Texture2D>("brick"), 630, 95, 0));
            bricks_lv1.Add(new Bricks(Content.Load<Texture2D>("brick"), 680, 95, 0));
            bricks_lv1.Add(new Bricks(Content.Load<Texture2D>("brick"), 730, 95, 0));
            bricks_lv1.Add(new Bricks(Content.Load<Texture2D>("brick"), 780, 95, 0));
            bricks_lv1.Add(new Bricks(Content.Load<Texture2D>("brick"), 830, 95, 0));
            bricks_lv1.Add(new Bricks(Content.Load<Texture2D>("brick"), 880, 95, 0));
            #endregion  //all the bricks for lv1


            //all the bricks for lv2
            #region          bricks_lv2   
            bricks_lv2 = new List<Bricks>();
            bricks_lv2.Add(new Bricks(Content.Load<Texture2D>("brick"), 80, 70, 0));
            bricks_lv2.Add(new Bricks(Content.Load<Texture2D>("brick"), 130, 70, 0));
            bricks_lv2.Add(new Bricks(Content.Load<Texture2D>("brick"), 180, 70, 0));
            bricks_lv2.Add(new Bricks(Content.Load<Texture2D>("brick"), 230, 70, 0));
            bricks_lv2.Add(new Bricks(Content.Load<Texture2D>("brick"), 280, 70, 0));
            bricks_lv2.Add(new Bricks(Content.Load<Texture2D>("brick"), 330, 70, 0));
            bricks_lv2.Add(new Bricks(Content.Load<Texture2D>("brick"), 380, 70, 0));
            bricks_lv2.Add(new Bricks(Content.Load<Texture2D>("brick"), 430, 70, 0));
            bricks_lv2.Add(new Bricks(Content.Load<Texture2D>("brick"), 530, 70, 0));
            bricks_lv2.Add(new Bricks(Content.Load<Texture2D>("brick"), 580, 70, 0));
            bricks_lv2.Add(new Bricks(Content.Load<Texture2D>("brick"), 630, 70, 0));
            bricks_lv2.Add(new Bricks(Content.Load<Texture2D>("brick"), 680, 70, 0));
            bricks_lv2.Add(new Bricks(Content.Load<Texture2D>("brick"), 730, 70, 0));
            bricks_lv2.Add(new Bricks(Content.Load<Texture2D>("brick"), 780, 70, 0));
            bricks_lv2.Add(new Bricks(Content.Load<Texture2D>("brick"), 830, 70, 0));
            bricks_lv2.Add(new Bricks(Content.Load<Texture2D>("brick"), 880, 70, 0));
            bricks_lv2.Add(new Bricks(Content.Load<Texture2D>("brick"), 930, 70, 0));
            bricks_lv2.Add(new Bricks(Content.Load<Texture2D>("brick"), 980, 70, 0));
            bricks_lv2.Add(new Bricks(Content.Load<Texture2D>("brick"), 130, 95, 0));
            bricks_lv2.Add(new Bricks(Content.Load<Texture2D>("brick"), 180, 95, 0));
            bricks_lv2.Add(new Bricks(Content.Load<Texture2D>("brick"), 230, 95, 0));
            bricks_lv2.Add(new Bricks(Content.Load<Texture2D>("brick"), 280, 95, 0));
            bricks_lv2.Add(new Bricks(Content.Load<Texture2D>("brick"), 330, 95, 0));
            #endregion

            //all the bricks for lv3
            #region bricks_lv3

            bricks_lv3 = new List<Bricks>();
            bricks_lv3.Add(new Bricks(Content.Load<Texture2D>("brick"), 80, 70, 0));
            bricks_lv3.Add(new Bricks(Content.Load<Texture2D>("brick"), 130, 70, 0));
            bricks_lv3.Add(new Bricks(Content.Load<Texture2D>("brick_ball_spawn"), 180, 70, 1));
            bricks_lv3.Add(new Bricks(Content.Load<Texture2D>("brick"), 230, 70, 0));
            bricks_lv3.Add(new Bricks(Content.Load<Texture2D>("brick"), 280, 70, 0));
            bricks_lv3.Add(new Bricks(Content.Load<Texture2D>("brick"), 330, 70, 0));
            bricks_lv3.Add(new Bricks(Content.Load<Texture2D>("brick"), 380, 70, 0));
            bricks_lv3.Add(new Bricks(Content.Load<Texture2D>("brick"), 430, 70, 0));
            bricks_lv3.Add(new Bricks(Content.Load<Texture2D>("brick"), 530, 70, 0));
            bricks_lv3.Add(new Bricks(Content.Load<Texture2D>("brick"), 580, 70, 0));
            bricks_lv3.Add(new Bricks(Content.Load<Texture2D>("brick"), 630, 70, 0));
            bricks_lv3.Add(new Bricks(Content.Load<Texture2D>("brick"), 680, 70, 0));
            bricks_lv3.Add(new Bricks(Content.Load<Texture2D>("brick"), 730, 70, 0));
            bricks_lv3.Add(new Bricks(Content.Load<Texture2D>("brick"), 780, 70, 0));
            bricks_lv3.Add(new Bricks(Content.Load<Texture2D>("brick"), 830, 70, 0));
            bricks_lv3.Add(new Bricks(Content.Load<Texture2D>("brick"), 880, 70, 0));
            bricks_lv3.Add(new Bricks(Content.Load<Texture2D>("brick"), 930, 70, 0));
            bricks_lv3.Add(new Bricks(Content.Load<Texture2D>("brick"), 980, 70, 0));
            bricks_lv3.Add(new Bricks(Content.Load<Texture2D>("brick"), 130, 95, 0));
            bricks_lv3.Add(new Bricks(Content.Load<Texture2D>("brick"), 180, 95, 0));
            bricks_lv3.Add(new Bricks(Content.Load<Texture2D>("brick"), 230, 95, 0));
            bricks_lv3.Add(new Bricks(Content.Load<Texture2D>("brick"), 280, 95, 0));
            bricks_lv3.Add(new Bricks(Content.Load<Texture2D>("brick_ball_spawn"), 330, 95, 1));
            #endregion
            //all the bricks for lv4
            #region bricks_lv4

            bricks_lv4 = new List<Bricks>();
            bricks_lv4.Add(new Bricks(Content.Load<Texture2D>("brick"), 80, 70, 0));
            bricks_lv4.Add(new Bricks(Content.Load<Texture2D>("brick"), 130, 70, 0));
            bricks_lv4.Add(new Bricks(Content.Load<Texture2D>("brick_ball_spawn"), 180, 70, 1));
            bricks_lv4.Add(new Bricks(Content.Load<Texture2D>("brick"), 230, 70, 0));
            bricks_lv4.Add(new Bricks(Content.Load<Texture2D>("brick"), 280, 70, 0));
            bricks_lv4.Add(new Bricks(Content.Load<Texture2D>("brick"), 330, 70, 0));
            bricks_lv4.Add(new Bricks(Content.Load<Texture2D>("brick"), 380, 70, 0));
            bricks_lv4.Add(new Bricks(Content.Load<Texture2D>("brick"), 430, 70, 0));
            bricks_lv4.Add(new Bricks(Content.Load<Texture2D>("brick"), 530, 70, 0));
            bricks_lv4.Add(new Bricks(Content.Load<Texture2D>("brick"), 580, 70, 0));
            bricks_lv4.Add(new Bricks(Content.Load<Texture2D>("brick"), 630, 70, 0));
            bricks_lv4.Add(new Bricks(Content.Load<Texture2D>("brick"), 680, 70, 0));
            bricks_lv4.Add(new Bricks(Content.Load<Texture2D>("brick"), 730, 70, 0));
            bricks_lv4.Add(new Bricks(Content.Load<Texture2D>("brick"), 780, 70, 0));
            bricks_lv4.Add(new Bricks(Content.Load<Texture2D>("brick"), 830, 70, 0));
            bricks_lv4.Add(new Bricks(Content.Load<Texture2D>("brick"), 880, 70, 0));
            bricks_lv4.Add(new Bricks(Content.Load<Texture2D>("brick"), 930, 70, 0));
            bricks_lv4.Add(new Bricks(Content.Load<Texture2D>("brick"), 980, 70, 0));
            bricks_lv4.Add(new Bricks(Content.Load<Texture2D>("brick"), 130, 95, 0));
            bricks_lv4.Add(new Bricks(Content.Load<Texture2D>("brick"), 180, 95, 0));
            bricks_lv4.Add(new Bricks(Content.Load<Texture2D>("brick_fortify"), 230, 95, 2));
            bricks_lv4.Add(new Bricks(Content.Load<Texture2D>("brick_fortify"), 280, 95, 2));
            bricks_lv4.Add(new Bricks(Content.Load<Texture2D>("brick_ball_spawn"), 330, 95, 1));
            bricks_lv4.Add(new Bricks(Content.Load<Texture2D>("brick_fortify"), 330, 120, 2));
            bricks_lv4.Add(new Bricks(Content.Load<Texture2D>("brick_fortify"), 380, 120, 2));
            bricks_lv4.Add(new Bricks(Content.Load<Texture2D>("brick_fortify"), 430, 120, 2));
            bricks_lv4.Add(new Bricks(Content.Load<Texture2D>("brick_fortify"), 480, 120, 2));
            bricks_lv4.Add(new Bricks(Content.Load<Texture2D>("brick_fortify"), 530, 120, 2));
            bricks_lv4.Add(new Bricks(Content.Load<Texture2D>("brick_fortify"), 580, 120, 2));
            #endregion

            //all the bricks for lv5
            #region bricks_lv5

            bricks_lv5 = new List<Bricks>();
            bricks_lv5.Add(new Bricks(Content.Load<Texture2D>("brick"), 80, 70, 0));
            bricks_lv5.Add(new Bricks(Content.Load<Texture2D>("brick"), 130, 70, 0));
            bricks_lv5.Add(new Bricks(Content.Load<Texture2D>("brick_ball_spawn"), 180, 70, 1));
            bricks_lv5.Add(new Bricks(Content.Load<Texture2D>("brick"), 230, 70, 0));
            bricks_lv5.Add(new Bricks(Content.Load<Texture2D>("brick"), 280, 70, 0));
            bricks_lv5.Add(new Bricks(Content.Load<Texture2D>("brick"), 330, 70, 0));
            bricks_lv5.Add(new Bricks(Content.Load<Texture2D>("brick"), 380, 70, 0));
            bricks_lv5.Add(new Bricks(Content.Load<Texture2D>("brick"), 430, 70, 0));
            bricks_lv5.Add(new Bricks(Content.Load<Texture2D>("brick"), 530, 70, 0));
            bricks_lv5.Add(new Bricks(Content.Load<Texture2D>("brick"), 580, 70, 0));
            bricks_lv5.Add(new Bricks(Content.Load<Texture2D>("brick"), 630, 70, 0));
            bricks_lv5.Add(new Bricks(Content.Load<Texture2D>("brick"), 680, 70, 0));
            bricks_lv5.Add(new Bricks(Content.Load<Texture2D>("brick"), 730, 70, 0));
            bricks_lv5.Add(new Bricks(Content.Load<Texture2D>("brick"), 780, 70, 0));
            bricks_lv5.Add(new Bricks(Content.Load<Texture2D>("brick"), 830, 70, 0));
            bricks_lv5.Add(new Bricks(Content.Load<Texture2D>("brick"), 880, 70, 0));
            bricks_lv5.Add(new Bricks(Content.Load<Texture2D>("brick"), 930, 70, 0));
            bricks_lv5.Add(new Bricks(Content.Load<Texture2D>("brick"), 980, 70, 0));
            bricks_lv5.Add(new Bricks(Content.Load<Texture2D>("brick"), 130, 95, 0));
            bricks_lv5.Add(new Bricks(Content.Load<Texture2D>("brick"), 180, 95, 0));
            bricks_lv5.Add(new Bricks(Content.Load<Texture2D>("brick_fortify"), 230, 95, 2));
            bricks_lv5.Add(new Bricks(Content.Load<Texture2D>("brick_fortify"), 280, 95, 2));
            bricks_lv5.Add(new Bricks(Content.Load<Texture2D>("brick_ball_spawn"), 330, 95, 1));
            bricks_lv5.Add(new Bricks(Content.Load<Texture2D>("brick_fortify"), 330, 120, 2));
            bricks_lv5.Add(new Bricks(Content.Load<Texture2D>("brick_fortify"), 380, 120, 2));
            bricks_lv5.Add(new Bricks(Content.Load<Texture2D>("brick_fortify"), 430, 120, 2));
            bricks_lv5.Add(new Bricks(Content.Load<Texture2D>("brick_fortify"), 480, 120, 2));
            bricks_lv5.Add(new Bricks(Content.Load<Texture2D>("brick_fortify"), 530, 120, 2));
            bricks_lv5.Add(new Bricks(Content.Load<Texture2D>("brick_fortify"), 580, 120, 2));
            bricks_lv5.Add(new Bricks(Content.Load<Texture2D>("extra_life_brick"), 630, 120, 3));
            bricks_lv5.Add(new Bricks(Content.Load<Texture2D>("extra_life_brick"), 680, 120, 3));
            bricks_lv5.Add(new Bricks(Content.Load<Texture2D>("extra_life_brick"), 230, 220, 3));
            bricks_lv5.Add(new Bricks(Content.Load<Texture2D>("extra_life_brick"), 630, 320, 3));
            bricks_lv5.Add(new Bricks(Content.Load<Texture2D>("extra_life_brick"), 230, 145, 3));
            bricks_lv5.Add(new Bricks(Content.Load<Texture2D>("brick_glass"), 230, 170, 4));
            bricks_lv5.Add(new Bricks(Content.Load<Texture2D>("brick_glass"), 280, 170, 4));
            bricks_lv5.Add(new Bricks(Content.Load<Texture2D>("brick_glass"), 330, 170, 4));
            bricks_lv5.Add(new Bricks(Content.Load<Texture2D>("brick_glass"), 380, 170, 4));
            bricks_lv5.Add(new Bricks(Content.Load<Texture2D>("brick_glass"), 430, 170, 4));
            bricks_lv5.Add(new Bricks(Content.Load<Texture2D>("brick_glass"), 480, 170, 4));
            bricks_lv5.Add(new Bricks(Content.Load<Texture2D>("brick_glass"), 530, 170, 4));
            bricks_lv5.Add(new Bricks(Content.Load<Texture2D>("brick_glass"), 580, 170, 4));
            bricks_lv5.Add(new Bricks(Content.Load<Texture2D>("brick_glass"), 630, 170, 4));
            bricks_lv5.Add(new Bricks(Content.Load<Texture2D>("brick_glass"), 680, 170, 4));
            bricks_lv5.Add(new Bricks(Content.Load<Texture2D>("brick_glass"), 730, 170, 4));
            bricks_lv5.Add(new Bricks(Content.Load<Texture2D>("brick_glass"), 780, 170, 4));
            bricks_lv5.Add(new Bricks(Content.Load<Texture2D>("brick_glass"), 830, 170, 4));
            bricks_lv5.Add(new Bricks(Content.Load<Texture2D>("brick_glass"), 880, 170, 4));
            bricks_lv5.Add(new Bricks(Content.Load<Texture2D>("brick_glass"), 930, 170, 4));
            bricks_lv5.Add(new Bricks(Content.Load<Texture2D>("brick_glass"), 980, 170, 4));
            bricks_lv5.Add(new Bricks(Content.Load<Texture2D>("brick_glass"), 230, 320, 4));
            bricks_lv5.Add(new Bricks(Content.Load<Texture2D>("brick_glass"), 280, 320, 4));
            bricks_lv5.Add(new Bricks(Content.Load<Texture2D>("brick_glass"), 330, 320, 4));
            bricks_lv5.Add(new Bricks(Content.Load<Texture2D>("brick_glass"), 380, 320, 4));
            bricks_lv5.Add(new Bricks(Content.Load<Texture2D>("brick_glass"), 430, 420, 4));
            bricks_lv5.Add(new Bricks(Content.Load<Texture2D>("brick_glass"), 480, 420, 4));
            bricks_lv5.Add(new Bricks(Content.Load<Texture2D>("brick_glass"), 530, 420, 4));
            bricks_lv5.Add(new Bricks(Content.Load<Texture2D>("brick_glass"), 580, 420, 4));
            bricks_lv5.Add(new Bricks(Content.Load<Texture2D>("brick_glass"), 630, 420, 4));
            bricks_lv5.Add(new Bricks(Content.Load<Texture2D>("brick_glass"), 680, 420, 4));
            bricks_lv5.Add(new Bricks(Content.Load<Texture2D>("brick_glass"), 730, 420, 4));
            #endregion




            balls = new List<ball>();
            Vector2 startPos = new Vector2(GraphicsDevice.Viewport.Bounds.Center.X + RNG.Next(-100, 100),
                                                        GraphicsDevice.Viewport.Bounds.Center.Y + RNG.Next(-100, 100));

            Vector2 startVel = new Vector2((float)(RNG.NextDouble() * 2) - 5,
                                                        (float)(RNG.NextDouble() * 2) - 10);
            //balls.Add(new ball(Content.Load<Texture2D>("ball_poke"), startPos, startVel, 0));
            balls.Add(new ball(Content.Load<Texture2D>("ball_1"), startPos, startVel, 1));
            //balls.Add(new ball(Content.Load<Texture2D>("ball_ord"), startPos, startVel, 0));


            balls_skin_select = new List<ball>();
            balls_skin_select.Add(new ball(Content.Load<Texture2D>("ball_poke"), new Vector2(100, 50), new Vector2(0, 0), 0));
            balls_skin_select.Add(new ball(Content.Load<Texture2D>("ball_1"), new Vector2(200, 50), new Vector2(0, 0), 0));
            balls_skin_select.Add(new ball(Content.Load<Texture2D>("ball_ord"), new Vector2(300, 50), new Vector2(0, 0), 0));





            st_button = new buttons(Content.Load<Texture2D>("start_button1"), 430, 500, 2, 24,1);
            bk_button = new buttons(Content.Load<Texture2D>("back_button"), 50, 850, 2, 24, 1);
            sk_button = new buttons(Content.Load<Texture2D>("skin_select_button"), 430, 800, 2, 24, 1);
            nxt_button = new buttons(Content.Load<Texture2D>("next_button"), 450, 700, 2, 24, 0);
            htp_button = new buttons(Content.Load<Texture2D>("how_to_play_button"), 430, 675, 2, 24, 0);
            bgd1 = new background(Content.Load<Texture2D>("skin select screen"));
            bgd2 = new background(Content.Load<Texture2D>("game start screen"));
            bgd3 = new background(Content.Load<Texture2D>("htp_screen"));
            cut_scene1 = new background(Content.Load<Texture2D>("cut_scene1"));
            bat = new Bat(Content.Load<Texture2D>("Bat_1"), 400, 900);


        }
        public void GetBricks()
        {

        }





        protected override void Update(GameTime gameTime)
        {

             kb = Keyboard.GetState();
            _mouseState = Mouse.GetState();
           
            padcurr = GamePad.GetState(PlayerIndex.One);
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            
           

           


            // Move the backgrounds to the bottom
            for (int i = 0; i < NOOFSC_BACKGROUNDS; i++)
            {
                bgds[i]._rect.Y--;
            }

            // Go through the backgrounds one by one
            for (int i = 0; i < NOOFSC_BACKGROUNDS; i++)
            {
                // If the current background has completely vanished off the bottom hand side of the screen
                if (bgds[i]._rect.Y < -990) // Note that this is 0 only because of the fake screen layout. It'd need to be equal to something much lower in fullscreen mode
                {
                    // Flip it over to AFTER the topmost background
                    bgds[i]._rect.Y = bgds[(i + (NOOFSC_BACKGROUNDS - 1)) % NOOFSC_BACKGROUNDS]._rect.Bottom;
                }
            }



            oldkb = kb; 

            bat.UpdateMe(padcurr);

            

            //ball collision for lv2
            
            for (int i = 0; i < balls.Count; i++)
            {
                if (balls[i].Rect.Intersects(bat.CollisionRect))
                {

                    balls[i]._velocity.X *= +1;
                    balls[i]._velocity.Y *= -1;
                    balls[i].NOOF_bounces += 1;
                    //balls[i]._position.X = balls[i]._currpos.X;
                }
                balls[i].UpdateMe(GraphicsDevice.Viewport.Bounds);
            }


            



            switch (_currState)
            {
                case GameStates.St_screen:
                    St_screenUpdate(_mouseState);
                    break;

                case GameStates.Skin_select:
                    Skin_selectUpdate(_mouseState);
                    break;
                case GameStates.pause_screen:
                    pause_screenUpdate(_mouseState);
                    break;
                case GameStates.Htp_screen:
                    Htp_screenUpdate(_mouseState);
                    break;
                case GameStates.Gameplayscreen_level1:
                    GameplayscreenUpdate(_mouseState);
                    break;
                case GameStates.Gameplayscreen_level2:
                    Gameplayscreen_lv2Update(_mouseState);
                    break;
                case GameStates.Gameplayscreen_level3:
                    Gameplayscreen_lv3Update(_mouseState);
                    break;
                case GameStates.cut_scene1:
                    Cut_scene1Update(_mouseState);
                    break;
                case GameStates.Gameplayscreen_level4:
                    Gameplayscreen_lv4Update(_mouseState);
                    break;
                case GameStates.Gameplayscreen_level5:
                    Gameplayscreen_lv5Update(_mouseState);
                    break;
            }


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {




            switch (_currState)
            {
                case GameStates.St_screen:
                    St_screenDraw(gameTime);
                    break;

                case GameStates.Skin_select:
                    Skin_selectDraw(gameTime);
                    break;
                case GameStates.Htp_screen:
                    Htp_screenDraw(gameTime);
                    break;
                case GameStates.Gameplayscreen_level1:
                    GameplayscreenDraw(gameTime);
                    break;
                case GameStates.Gameplayscreen_level2:
                    Gameplayscreen_level2_Draw(gameTime);
                    break;
                case GameStates.Gameplayscreen_level3:
                    Gameplayscreen_level3_Draw(gameTime);
                    break;
                case GameStates.cut_scene1:
                    Cut_scene1Draw(gameTime);
                    break;
                case GameStates.Gameplayscreen_level4:
                    Gameplayscreen_level4_Draw(gameTime);
                    break;
                case GameStates.Gameplayscreen_level5:
                    Gameplayscreen_level5_Draw(gameTime);
                    break;
            }




            



            base.Draw(gameTime);
        }

        #region start_screen_update

        void St_screenUpdate(MouseState ms)
        {
            if (sk_button.CollisionRect.Contains(Mouse.GetState().X, Mouse.GetState().Y) && _mouseState.LeftButton == ButtonState.Pressed)
            {
                _currState = GameStates.Skin_select;

            }
            else if (st_button.CollisionRect.Contains(Mouse.GetState().X, Mouse.GetState().Y) && _mouseState.LeftButton == ButtonState.Pressed)
            {
                _currState = GameStates.Gameplayscreen_level1;

            }
            else if (htp_button.CollisionRect.Contains(Mouse.GetState().X, Mouse.GetState().Y) && _mouseState.LeftButton == ButtonState.Pressed)
            {
                _currState = GameStates.Htp_screen;

            }
        }
        #endregion

        #region skin_select_update
        void Skin_selectUpdate(MouseState ms)
        {
            if (bk_button.CollisionRect.Contains(Mouse.GetState().X, Mouse.GetState().Y) && _mouseState.LeftButton == ButtonState.Pressed)
            {
                _currState = GameStates.St_screen;

            }
            
            for (int i = 0; i < balls_skin_select.Count; i++)
            {
                if (balls_skin_select[i].Rect.Contains(Mouse.GetState().X, Mouse.GetState().Y) && _mouseState.LeftButton == ButtonState.Pressed)
                {

                   
                    balls[0]._art = balls_skin_select[i]._art;
                       break;
                    
                }
            }
                
            
        }
        #endregion

        #region how_to_play_screen_update

        void Htp_screenUpdate(MouseState ms)
        {
            if (bk_button.CollisionRect.Contains(Mouse.GetState().X, Mouse.GetState().Y) && _mouseState.LeftButton == ButtonState.Pressed)
            {
                _currState = GameStates.St_screen;

            }
            
        }
        #endregion
        void pause_screenUpdate(MouseState ms)
        {
            if (bk_button.CollisionRect.Contains(Mouse.GetState().X, Mouse.GetState().Y) && _mouseState.LeftButton == ButtonState.Pressed)
            {
                _currState = GameStates.St_screen;

            }
        }


        #region cut_scene_1_update

        void Cut_scene1Update(MouseState ms)
        {
            if (nxt_button.CollisionRect.Contains(Mouse.GetState().X, Mouse.GetState().Y) && _mouseState.LeftButton == ButtonState.Pressed)
            {
                _currState = GameStates.Gameplayscreen_level2;

            }
        }
        #endregion

        #region level1 update
        void GameplayscreenUpdate(MouseState ms)
        {
            /*for (int i = 0; i < bricks_lv1.Count; i++)
            {
                if (CanvasLocation == Vector2.Zero) // If the screen is settled in its correct place
                {
                    if (bouncesLeft > 0) // Check to see if its got any bounces left to do
                    {
                        // Bounce to a random location
                        CanvasLocation = new Vector2(RNG.Next(-MAXSHAKE.X, MAXSHAKE.X), RNG.Next(-MAXSHAKE.Y, MAXSHAKE.Y));
                        // Decrease the bounces left
                        bouncesLeft--;
                    }
                    else if (bricks_lv1[i]._rect.Intersects(balls[0].Rect))
                    {
                        // Start a shake by bouncing to a random location...
                        CanvasLocation = new Vector2(RNG.Next(-MAXSHAKE.X, MAXSHAKE.X), RNG.Next(-MAXSHAKE.Y, MAXSHAKE.Y));
                        bouncesLeft = BOUNCECOUNT;  // ... and setting the number of bounces left to maximum
                        //snore.Play(); // Play the sound effect
                    }
                }
                else    // We're not in the correct position...
                {
                    #region MOVE_BACK_TO_RESTING_POSITION // ... so work our way back to where we should be.
                    if (CanvasLocation.X < 0)
                    {
                        CanvasLocation.X++;
                    }
                    else if (CanvasLocation.X > 0)
                    {
                        CanvasLocation.X--;
                    }

                    if (CanvasLocation.Y < 0)
                    {
                        CanvasLocation.Y++;
                    }
                    else if (CanvasLocation.Y > 0)
                    {
                        CanvasLocation.Y--;
                    }
                    #endregion
                }
            }*/
            //ball collision for lv1
            for (int i = 0; i < bricks_lv1.Count; i++)
            {
                for (int j = 0; j < balls.Count; j++)
                {
                    if (bricks_lv1[i]._rect.Intersects(balls[j].Rect))
                    {
                        balls[j]._velocity.X *= +1;
                        balls[j]._velocity.Y *= -1;
                        balls[j].NOOF_bounces += 1;

                        bricks_lv1[i].brick_health -= 1;
                        if (bricks_lv1[i].brick_health <= 0)
                        {
                            bricks_lv1.RemoveAt(i);
                            bricks_destroyed += 1;
                        }

                        //Debug.WriteLine("Collision detected with brick at index " + i);
                        break;
                    }
                }

            }
            if (bricks_lv1.Count <= 0 || Keyboard.GetState().IsKeyDown(Keys.P))
            {
                _currState = GameStates.cut_scene1;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                _currState = GameStates.pause_screen;
            }

        }
        #endregion

        #region level2 update
        void Gameplayscreen_lv2Update(MouseState ms)
        {
            for (int i = 0; i < bricks_lv2.Count; i++)
            {

                for (int j = 0; j < balls.Count; j++)
                {

                    if (bricks_lv2[i]._rect.Intersects(balls[j].Rect))
                    {
                        balls[j]._velocity.X *= +1;
                        balls[j]._velocity.Y *= -1;
                        balls[j].NOOF_bounces += 1;

                        bricks_lv2[i].brick_health -= 1;
                        if (bricks_lv2[i].brick_health <= 0)
                        {
                            bricks_lv2.RemoveAt(i);
                            bricks_destroyed += 1;
                        }


                        //Debug.WriteLine("Collision detected with brick at index " + i);
                        break;
                    }
                }

            }
            if (bricks_lv2.Count <= 0 || Keyboard.GetState().IsKeyDown(Keys.P))
            {
                _currState = GameStates.Gameplayscreen_level3;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                _currState = GameStates.pause_screen;
            }
        }
        #endregion

        #region level3 update
        void Gameplayscreen_lv3Update(MouseState ms)
        {
            for (int i = 0; i < bricks_lv3.Count; i++)
            {
                for (int j = 0; j < balls.Count; j++)
                {
                    if (bricks_lv3[i]._rect.Intersects(balls[j].Rect))
                    {
                        if (bricks_lv3[i].extra_ball_brick == true)
                        {
                            balls.Add(new ball(Content.Load<Texture2D>("ball_ord"), new Vector2(bricks_lv3[i]._rect.X, bricks_lv3[i]._rect.Y), new Vector2((float)5,
                                                            (float)10),1));
                        }
                        balls[j]._velocity.X *= +1;
                        balls[j]._velocity.Y *= -1;
                        balls[j].NOOF_bounces += 1;

                        bricks_lv3[i].brick_health -= 1;
                        if (bricks_lv3[i].brick_health <= 0)
                        {
                            bricks_lv3.RemoveAt(i);
                            bricks_destroyed += 1;
                        }

                       
                        //Debug.WriteLine("Collision detected with brick at index " + i);
                        break;
                    }
                }
            }
            if (bricks_lv3.Count <= 0 || Keyboard.GetState().IsKeyDown(Keys.O))
            {
                _currState = GameStates.Gameplayscreen_level4;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                _currState = GameStates.pause_screen;
            }
        }
        #endregion

        #region level_4 update
        void Gameplayscreen_lv4Update(MouseState ms)
        {
            for (int i = 0; i < bricks_lv4.Count; i++)
            {
                for (int j = 0; j < balls.Count; j++)
                {
                    if (bricks_lv4[i]._rect.Intersects(balls[j].Rect))
                    {
                        if (bricks_lv4[i].extra_ball_brick == true)
                        {
                            balls.Add(new ball(Content.Load<Texture2D>("ball_ord"), new Vector2(bricks_lv4[i]._rect.X, bricks_lv4[i]._rect.Y), new Vector2((float)5,
                                                            (float)10),1));
                        }
                        

                        balls[j]._velocity.X *= +1;
                        balls[j]._velocity.Y *= -1;
                        balls[j].NOOF_bounces += 1;

                        bricks_lv4[i].brick_health -= 1;
                        if (bricks_lv4[i].brick_health <= 0)
                        {
                            bricks_lv4.RemoveAt(i);
                            bricks_destroyed += 1;
                        }


                        //Debug.WriteLine("Collision detected with brick at index " + i);
                        break;
                    }
                }
                
            }
            if (bricks_lv4.Count <= 0 || Keyboard.GetState().IsKeyDown(Keys.I))
            {
                _currState = GameStates.Gameplayscreen_level5;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                _currState = GameStates.pause_screen;
            }
        }
        #endregion

        #region level_5 update
        void Gameplayscreen_lv5Update(MouseState ms)
        {
            for (int i = 0; i < bricks_lv5.Count; i++)
            {
                for (int j = 0; j < balls.Count; j++)
                {
                    if (bricks_lv5[i]._rect.Intersects(balls[j].Rect))
                    {
                        if (bricks_lv5[i].extra_ball_brick == true)
                        {
                            balls.Add(new ball(Content.Load<Texture2D>("ball_ord"), new Vector2(bricks_lv4[i]._rect.X, bricks_lv4[i]._rect.Y), new Vector2((float)5,
                                                            (float)10), 1));
                        }
                        else if(bricks_lv5[i].extra_life_brick == true)
                        {
                            balls[j].Spaceship_health += 1;
                        }
                        


                        balls[j]._velocity.X *= +1;
                        balls[j]._velocity.Y *= -1;
                        balls[j].NOOF_bounces += 1;

                        if (bricks_lv5[i].glass_brick == true)
                        {
                            balls[j]._velocity.X *= +1;
                            balls[j]._velocity.Y *= -1;
                        }

                        bricks_lv5[i].brick_health -= 1;
                        if (bricks_lv5[i].brick_health <= 0)
                        {
                            bricks_lv5.RemoveAt(i);
                            bricks_destroyed += 1;
                        }


                        //Debug.WriteLine("Collision detected with brick at index " + i);
                        break;
                    }
                }

            }
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                _currState = GameStates.pause_screen;
            }
        }
        #endregion






        #region startscreen_draw 
        void St_screenDraw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();

            bgd1.DrawMe(_spriteBatch);
            st_button.DrawMe(_spriteBatch, gameTime);
            sk_button.DrawMe(_spriteBatch, gameTime);
            htp_button.DrawMe(_spriteBatch, gameTime);
            _spriteBatch.DrawString(debugFont, "Res: " + _graphics.PreferredBackBufferWidth
                                              + " x " + _graphics.PreferredBackBufferHeight,
                                              Vector2.Zero, Color.White);

            _spriteBatch.End();

        }
        #endregion

        #region how to play screen 
        void Htp_screenDraw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            _spriteBatch.Begin();
            bgd3.DrawMe(_spriteBatch);
            for (int i = 0; i < bricks_htp.Count; i++)
            {

                bricks_htp[i].DrawMe(_spriteBatch);

            }
            _spriteBatch.DrawString(debugFont, "You need to Control the barrier to avoid the asteroids from hitting your space ship",
                                             new Vector2(200, 150), Color.White);
            _spriteBatch.DrawString(debugFont, "This is just a simple brick, 1 hit is enough to break Through it ",
                                            new Vector2(250, 250), Color.White);
            _spriteBatch.DrawString(debugFont, "If you hit this brick, 1 more asteroid will spawn. ",
                                            new Vector2(250, 350), Color.White);
            _spriteBatch.DrawString(debugFont, "This brick is fortified and has 4 armor, each hit reduce it's armor by 1. ",
                                            new Vector2(250, 450), Color.White);
            _spriteBatch.DrawString(debugFont, "This brick is very vunerable and the asteroid will smash through it after being hit. ",
                                            new Vector2(250, 550), Color.White);
            _spriteBatch.DrawString(debugFont, "This brick gives you an extra life after it's been hit. ",
                                            new Vector2(250, 650), Color.White);
            _spriteBatch.DrawString(debugFont, "If your spaceship's health is reduced to 0, you will fall into the VOID and Lose. ",
                                            new Vector2(200, 750), Color.White);



            bk_button.DrawMe(_spriteBatch, gameTime);
            _spriteBatch.End();
        }
        #endregion
       
        #region skin_select
        void Skin_selectDraw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();
            bgd2.DrawMe(_spriteBatch);
            for (int i = 0; i < balls_skin_select.Count; i++)
            {                

                    balls_skin_select[i].DrawMe(_spriteBatch);
            }

            
            bk_button.DrawMe(_spriteBatch, gameTime);

            _spriteBatch.End();
        }
        #endregion

        #region gameplay_lv1 draw
        void GameplayscreenDraw(GameTime gameTime)
        {
            //_graphics.GraphicsDevice.SetRenderTarget(DrawCanvas);
            GraphicsDevice.Clear(Color.Black);
            _spriteBatch.Begin();
            // go through the backgrounds, drawing each one in its position
            for (int i = 0; i < NOOFSC_BACKGROUNDS; i++)
                _spriteBatch.Draw(bgds[i]._txr, bgds[i]._rect, Color.White);

            
            for (int i = 0; i < bricks_lv1.Count; i++)
            {

                bricks_lv1[i].DrawMe(_spriteBatch);
                
            }
            for (int i = 0; i < balls.Count; i++)
            {
              
                   
                        balls[i].DrawMe(_spriteBatch);
                   
            }
            _spriteBatch.DrawString(debugFont, "Bricks Destroyed: " + bricks_destroyed,
                                          new Vector2(744, 930), Color.White);
            _spriteBatch.DrawString(debugFont, "Number of Bounces: " + balls[0].NOOF_bounces,
                                              new Vector2(744, 950), Color.White);
            _spriteBatch.DrawString(debugFont, "SpaceShip's Health: " + balls[0].Spaceship_health,
                                              new Vector2(744, 970), Color.White);



            bat.DrawMe(_spriteBatch);

         
            _spriteBatch.End();
        }
        #endregion

        #region Cut_scene_1_draw 


        void Cut_scene1Draw(GameTime gameTime)
        {
            kb = Keyboard.GetState();
            GraphicsDevice.Clear(Color.Black);
            _spriteBatch.Begin();
            
            cut_scene1.DrawMe(_spriteBatch);
            nxt_button.DrawMe(_spriteBatch, gameTime);
            _spriteBatch.DrawString(GuideFont,
                "Now you have got the skills to protect your land, there are more challenges awaits......."
                , new Vector2 (200,200) , Color.Red);
            if(kb.IsKeyDown(Keys.Space) || oldkb.IsKeyDown(Keys.Space))
            {
                _spriteBatch.DrawString(GuideFont,"......", new Vector2(200,300), Color.Red);
                _spriteBatch.DrawString(GuideFont, "It's getting unstable....", new Vector2(200, 400), Color.Red);
                _spriteBatch.DrawString(GuideFont, "levels until protals appear.....3", new Vector2(200, 500), Color.Red);
            }
           
                



            _spriteBatch.End();
            oldkb = kb;
        }
        #endregion

        #region gameplay_lv2_draw

        void Gameplayscreen_level2_Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            _spriteBatch.Begin();
            for (int i = 0; i < bricks_lv2.Count; i++)
            {

                bricks_lv2[i].DrawMe(_spriteBatch);

            }
            for (int i = 0; i < balls.Count; i++)
            {

                if (balls[i].is_selected == true)
                {

                    balls[i].DrawMe(_spriteBatch);
                }
                else if(balls[i].is_selected == false)
                {
                    balls.RemoveAt(i);
                    break;
                }

            }
           

            bat.DrawMe(_spriteBatch);
            _spriteBatch.DrawString(debugFont, "Bricks Destroyed: " + bricks_destroyed,
                                              new Vector2(744, 930), Color.White);
            _spriteBatch.DrawString(debugFont, "Number of Bounces: " + balls[0].NOOF_bounces,
                                              new Vector2(744, 950), Color.White);
            _spriteBatch.DrawString(debugFont, "SpaceShip's Health: " + balls[0].Spaceship_health,
                                              new Vector2(744, 970), Color.White);


            _spriteBatch.End();
        }
        #endregion

        #region gameplay_lv3_draw
        void Gameplayscreen_level3_Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            _spriteBatch.Begin();
            for (int i = 0; i < bricks_lv3.Count; i++)
            {

                bricks_lv3[i].DrawMe(_spriteBatch);

            }
            for (int i = 0; i < balls.Count; i++)
            {
                if (balls[i].is_selected == true)
                {

                    balls[i].DrawMe(_spriteBatch);
                }
                
            }
            


            bat.DrawMe(_spriteBatch);
            _spriteBatch.DrawString(debugFont, "Bricks Destroyed: " + bricks_destroyed,
                                              new Vector2(744, 930), Color.White);
            _spriteBatch.DrawString(debugFont, "Number of Bounces: " + balls[0].NOOF_bounces,
                                              new Vector2(744, 950), Color.White);
            _spriteBatch.DrawString(debugFont, "SpaceShip's Health: " + balls[0].Spaceship_health,
                                              new Vector2(744, 970), Color.White);

            _spriteBatch.End();

        }
        #endregion

        #region gameplay_lv4_draw

        void Gameplayscreen_level4_Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            _spriteBatch.Begin();
            for (int i = 0; i < bricks_lv4.Count; i++)
            {

                bricks_lv4[i].DrawMe(_spriteBatch);

            }
            for (int i = 0; i < balls.Count; i++)
            {
                if (balls[i].is_selected == true)
                {

                    balls[i].DrawMe(_spriteBatch);
                }
            }
           
            bat.DrawMe( _spriteBatch);
            _spriteBatch.DrawString(debugFont, "Bricks Destroyed: " + bricks_destroyed,
                                              new Vector2(744, 930), Color.White);
            _spriteBatch.DrawString(debugFont, "Number of Bounces: " + balls[0].NOOF_bounces,
                                              new Vector2(744, 950), Color.White);
            _spriteBatch.DrawString(debugFont, "SpaceShip's Health: " + balls[0].Spaceship_health,
                                              new Vector2(744, 970), Color.White);

            _spriteBatch.End();
        }
        #endregion

        #region gameplay_lv5_draw
        void Gameplayscreen_level5_Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            _spriteBatch.Begin();
            for (int i = 0; i < bricks_lv5.Count; i++)
            {

                bricks_lv5[i].DrawMe(_spriteBatch);

            }
            for (int i = 0; i < balls.Count; i++)
            {
                if (balls[i].is_selected == true)
                {

                    balls[i].DrawMe(_spriteBatch);
                }
            }
            
            bat.DrawMe(_spriteBatch);
            _spriteBatch.DrawString(debugFont, "Bricks Destroyed: " + bricks_destroyed,
                                              new Vector2(744, 930), Color.White);
            _spriteBatch.DrawString(debugFont, "Number of Bounces: " + balls[0].NOOF_bounces,
                                              new Vector2(744, 950), Color.White);
            _spriteBatch.DrawString(debugFont, "SpaceShip's Health: " + balls[0].Spaceship_health,
                                              new Vector2(744, 970), Color.White);

            _spriteBatch.End();
        }
        #endregion

    }
}