using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace final_project_sem1
{
    public class Game1 : Game
    {
        enum GameStates
        {
            St_screen,
            Skin_select,
            Gameplayscreen_level1,
            Gameplayscreen_level2,
            Gameplayscreen_level3,
            Gameplayscreen_level4

        }
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private MouseState _mouseState;


        public static readonly Random RNG = new Random();
        GamePadState padcurr;

        GameStates _currState;
        SpriteFont debugFont;
        background bgd1, bgd2;
        List<Bricks> bricks;
        Bat bat;
        buttons st_button, bk_button, sk_button;
        ball _ball;

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
            bricks = new List<Bricks>();

            bgds = new scrollingBG[NOOFSC_BACKGROUNDS];


            for (int i = 0; i < NOOFSC_BACKGROUNDS; i++)
            {
                // Set their X, Y, Height and Width
                bgds[i]._rect = new Rectangle(0,0, _graphics.PreferredBackBufferHeight, _graphics.PreferredBackBufferWidth);
            }

            // Set up the viewable overlay
            visibleScreen._rect = new Rectangle(300, 25, 200, 150);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            debugFont = Content.Load<SpriteFont>("Ariel07");
            // Go through the backgrounds one by one and set up their textures
            for (int i = 0; i < NOOFSC_BACKGROUNDS; i++)
            {
                bgds[i]._txr = Content.Load<Texture2D>("sc_bgd" + i);
            }

            // Load the visible area overlay
            //visibleScreen._txr = Content.Load<Texture2D>("viewable");
            
            
            bricks = new List<Bricks>();
            bricks.Add(new Bricks(Content.Load<Texture2D>("brick"), 80, 70));
            bricks.Add(new Bricks(Content.Load<Texture2D>("brick"), 130, 70));
            bricks.Add(new Bricks(Content.Load<Texture2D>("brick"), 180, 70));
            bricks.Add(new Bricks(Content.Load<Texture2D>("brick"), 230, 70));
            bricks.Add(new Bricks(Content.Load<Texture2D>("brick"), 280, 70));
            bricks.Add(new Bricks(Content.Load<Texture2D>("brick"), 330, 70));
            bricks.Add(new Bricks(Content.Load<Texture2D>("brick"), 380, 70));
            bricks.Add(new Bricks(Content.Load<Texture2D>("brick"), 430, 70));
            bricks.Add(new Bricks(Content.Load<Texture2D>("brick"), 480, 70));
            bricks.Add(new Bricks(Content.Load<Texture2D>("brick"), 530, 70));
            bricks.Add(new Bricks(Content.Load<Texture2D>("brick"), 580, 70));
            bricks.Add(new Bricks(Content.Load<Texture2D>("brick"), 630, 70));
            bricks.Add(new Bricks(Content.Load<Texture2D>("brick"), 680, 70));
            bricks.Add(new Bricks(Content.Load<Texture2D>("brick"), 730, 70));
            bricks.Add(new Bricks(Content.Load<Texture2D>("brick"), 780, 70));
            bricks.Add(new Bricks(Content.Load<Texture2D>("brick"), 830, 70));
            bricks.Add(new Bricks(Content.Load<Texture2D>("brick"), 880, 70));
            bricks.Add(new Bricks(Content.Load<Texture2D>("brick"), 80, 95));
            bricks.Add(new Bricks(Content.Load<Texture2D>("brick"), 130, 120));
            bricks.Add(new Bricks(Content.Load<Texture2D>("brick"), 180, 145));
            bricks.Add(new Bricks(Content.Load<Texture2D>("brick"), 230, 170));
            bricks.Add(new Bricks(Content.Load<Texture2D>("brick"), 280, 195));
            bricks.Add(new Bricks(Content.Load<Texture2D>("brick"), 330, 220));
            bricks.Add(new Bricks(Content.Load<Texture2D>("brick"), 380, 245));
            bricks.Add(new Bricks(Content.Load<Texture2D>("brick"), 430, 270));
            bricks.Add(new Bricks(Content.Load<Texture2D>("brick"), 480, 295));
            bricks.Add(new Bricks(Content.Load<Texture2D>("brick"), 530, 320));
            bricks.Add(new Bricks(Content.Load<Texture2D>("brick"), 580, 345));
            bricks.Add(new Bricks(Content.Load<Texture2D>("brick"), 630, 370));
            bricks.Add(new Bricks(Content.Load<Texture2D>("brick"), 680, 395));
            bricks.Add(new Bricks(Content.Load<Texture2D>("brick"), 730, 420));
            bricks.Add(new Bricks(Content.Load<Texture2D>("brick"), 780, 445));
            bricks.Add(new Bricks(Content.Load<Texture2D>("brick"), 830, 470));
            bricks.Add(new Bricks(Content.Load<Texture2D>("brick"), 880, 495));
            bricks.Add(new Bricks(Content.Load<Texture2D>("brick"), 80, 120));
            bricks.Add(new Bricks(Content.Load<Texture2D>("brick"), 130, 95));
            bricks.Add(new Bricks(Content.Load<Texture2D>("brick"), 180, 95));
            bricks.Add(new Bricks(Content.Load<Texture2D>("brick"), 230, 95));
            bricks.Add(new Bricks(Content.Load<Texture2D>("brick"), 280, 95));
            bricks.Add(new Bricks(Content.Load<Texture2D>("brick"), 330, 95));
            bricks.Add(new Bricks(Content.Load<Texture2D>("brick"), 380, 95));
            bricks.Add(new Bricks(Content.Load<Texture2D>("brick"), 430, 95));
            bricks.Add(new Bricks(Content.Load<Texture2D>("brick"), 480, 95));
            bricks.Add(new Bricks(Content.Load<Texture2D>("brick"), 530, 95));
            bricks.Add(new Bricks(Content.Load<Texture2D>("brick"), 580, 95));
            bricks.Add(new Bricks(Content.Load<Texture2D>("brick"), 630, 95));
            bricks.Add(new Bricks(Content.Load<Texture2D>("brick"), 680, 95));
            bricks.Add(new Bricks(Content.Load<Texture2D>("brick"), 730, 95));
            bricks.Add(new Bricks(Content.Load<Texture2D>("brick"), 780, 95));
            bricks.Add(new Bricks(Content.Load<Texture2D>("brick"), 830, 95));
            bricks.Add(new Bricks(Content.Load<Texture2D>("brick"), 880, 95));


            Vector2 startPos = new Vector2(GraphicsDevice.Viewport.Bounds.Center.X + RNG.Next(-100, 100),
                                                        GraphicsDevice.Viewport.Bounds.Center.Y + RNG.Next(-100, 100));

            Vector2 startVel = new Vector2((float)(RNG.NextDouble() * 2) - 5,
                                                        (float)(RNG.NextDouble() * 2) - 10);
            _ball = new ball(Content.Load<Texture2D>("ball_poke"), startPos, startVel);

            st_button = new buttons(Content.Load<Texture2D>("start_button1"), 430, 600, 2, 24);
            bk_button = new buttons(Content.Load<Texture2D>("back_button"), 50, 850, 2, 24);
            sk_button = new buttons(Content.Load<Texture2D>("skin_select_button"), 430, 700, 2, 24);
            bgd1 = new background(Content.Load<Texture2D>("skin select screen"));
            bgd2 = new background(Content.Load<Texture2D>("game start screen"));
            bat = new Bat(Content.Load<Texture2D>("bounce disk"), 400, 900);


        }





        protected override void Update(GameTime gameTime)
        {
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





            bat.UpdateMe(padcurr);
            for (int i = 0; i < bricks.Count; i++)
            {



                if (bricks[i]._rect.Intersects(_ball.Rect))
                {
                    _ball._velocity.X *= +1;
                    _ball._velocity.Y *= -1;
                    bricks.RemoveAt(i);
                    //Debug.WriteLine("Collision detected with brick at index " + i);
                    break;
                }
                if (bricks.Count == 0)
                {
                    _currState = GameStates.Gameplayscreen_level2;
                }
            }
           



            if (_ball.Rect.Intersects(bat.CollisionRect))
            {
                
                    _ball._velocity.X *= +1;               
                    _ball._velocity.Y *= -1;
            }
            _ball.UpdateMe(GraphicsDevice.Viewport.Bounds);




            switch (_currState)
            {
                case GameStates.St_screen:
                    St_screenUpdate(_mouseState);
                    break;

                case GameStates.Skin_select:
                    Skin_selectUpdate(_mouseState);
                    break;
                case GameStates.Gameplayscreen_level1:
                    GameplayscreenUpdate(_mouseState);
                    break;
                case GameStates.Gameplayscreen_level2:
                    GameplayscreenUpdate(_mouseState);
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
                case GameStates.Gameplayscreen_level1:
                    GameplayscreenDraw(gameTime);
                    break;
                case GameStates.Gameplayscreen_level2:
                    Gameplayscreen_level2_Draw(gameTime);
                    break;
            }




            /* _spriteBatch.DrawString(debugFont, "Res: " + _graphics.PreferredBackBufferWidth
                                               + " x " + _graphics.PreferredBackBufferHeight,
                                               Vector2.Zero, Color.White);*/



            base.Draw(gameTime);
        }

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
        }
        void Skin_selectUpdate(MouseState ms)
        {
            if (bk_button.CollisionRect.Contains(Mouse.GetState().X, Mouse.GetState().Y) && _mouseState.LeftButton == ButtonState.Pressed)
            {
                _currState = GameStates.St_screen;

            }
        }

        void GameplayscreenUpdate(MouseState ms)
        {
           
        }


        void St_screenDraw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();

            bgd1.DrawMe(_spriteBatch);
            st_button.DrawMe(_spriteBatch, gameTime);
            sk_button.DrawMe(_spriteBatch, gameTime);

            _spriteBatch.End();

        }

        void Skin_selectDraw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();

            bgd2.DrawMe(_spriteBatch);
            bk_button.DrawMe(_spriteBatch, gameTime);

            _spriteBatch.End();
        }
        void GameplayscreenDraw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            _spriteBatch.Begin();
            // go through the backgrounds, drawing each one in its position
            for (int i = 0; i < NOOFSC_BACKGROUNDS; i++)
                _spriteBatch.Draw(bgds[i]._txr, bgds[i]._rect, Color.White);

            
            for (int i = 0; i < bricks.Count; i++)
            {

                bricks[i].DrawMe(_spriteBatch);
                
            }
            _ball.DrawMe(_spriteBatch);
            bat.DrawMe(_spriteBatch);

            _spriteBatch.End();
        }
        void Gameplayscreen_level2_Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            _spriteBatch.Begin();
            for (int i = 0; i < bricks.Count; i++)
            {

                bricks[i].DrawMe(_spriteBatch);

            }
            _ball.DrawMe(_spriteBatch);
            bat.DrawMe(_spriteBatch);

            _spriteBatch.End();
        }

    }
}