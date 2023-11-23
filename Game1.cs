﻿using Microsoft.Xna.Framework;
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
            cut_scene1,
            Gameplayscreen_level1,
            Gameplayscreen_level2,
            Gameplayscreen_level3,
            Gameplayscreen_level4

        }
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private MouseState _mouseState;
        KeyboardState kb, oldkb;
        


        public static Texture2D pixel;

        public static readonly Random RNG = new Random();
        GamePadState padcurr;

        GameStates _currState;
        SpriteFont debugFont, GuideFont;
        background bgd1, bgd2, cut_scene1;
        List<Bricks> bricks_lv1, bricks_lv2, bricks_lv3;
        Bat bat;
        buttons st_button, bk_button, sk_button, nxt_button;
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
            bricks_lv1 = new List<Bricks>();
            bricks_lv2 = new List<Bricks>();
            bricks_lv3 = new List<Bricks>();

            bgds = new scrollingBG[NOOFSC_BACKGROUNDS];


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



            Vector2 startPos = new Vector2(GraphicsDevice.Viewport.Bounds.Center.X + RNG.Next(-100, 100),
                                                        GraphicsDevice.Viewport.Bounds.Center.Y + RNG.Next(-100, 100));

            Vector2 startVel = new Vector2((float)(RNG.NextDouble() * 2) - 5,
                                                        (float)(RNG.NextDouble() * 2) - 10);
            _ball = new ball(Content.Load<Texture2D>("ball_poke"), startPos, startVel);

            st_button = new buttons(Content.Load<Texture2D>("start_button1"), 430, 600, 2, 24,1);
            bk_button = new buttons(Content.Load<Texture2D>("back_button"), 50, 850, 2, 24, 1);
            sk_button = new buttons(Content.Load<Texture2D>("skin_select_button"), 430, 700, 2, 24, 1);
            nxt_button = new buttons(Content.Load<Texture2D>("next_button"), 430, 700, 2, 24, 0);
            bgd1 = new background(Content.Load<Texture2D>("skin select screen"));
            bgd2 = new background(Content.Load<Texture2D>("game start screen"));
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
                    Gameplayscreen_lv2Update(_mouseState);
                    break;
                case GameStates.Gameplayscreen_level3:
                    Gameplayscreen_lv3Update(_mouseState);
                    break;
                case GameStates.cut_scene1:
                    Cut_scene1Update(_mouseState);
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
                case GameStates.cut_scene1:
                    Cut_scene1Draw(gameTime);
                    break;
            }




            



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

        void Cut_scene1Update(MouseState ms)
        {
            if (nxt_button.CollisionRect.Contains(Mouse.GetState().X, Mouse.GetState().Y) && _mouseState.LeftButton == ButtonState.Pressed)
            {
                _currState = GameStates.Gameplayscreen_level2;

            }
        }

        void GameplayscreenUpdate(MouseState ms)
        {
            //ball collision for lv1
            for (int i = 0; i < bricks_lv1.Count; i++)
            {



                if (bricks_lv1[i]._rect.Intersects(_ball.Rect))
                {
                    _ball._velocity.X *= +1;
                    _ball._velocity.Y *= -1;
                    bricks_lv1.RemoveAt(i);
                    //Debug.WriteLine("Collision detected with brick at index " + i);
                    break;
                }
            }
            if (bricks_lv1.Count <= 0 || Keyboard.GetState().IsKeyDown(Keys.P))
            {
                _currState = GameStates.cut_scene1;
            }

        }

        void Gameplayscreen_lv2Update(MouseState ms)
        {
            for (int i = 0; i < bricks_lv2.Count; i++)
            {



                if (bricks_lv2[i]._rect.Intersects(_ball.Rect))
                {
                    _ball._velocity.X *= +1;
                    _ball._velocity.Y *= -1;
                    bricks_lv2.RemoveAt(i);
                    //Debug.WriteLine("Collision detected with brick at index " + i);
                    break;
                }
            }
            if (bricks_lv2.Count <= 0)
            {
                _currState = GameStates.Gameplayscreen_level3;
            }
        }

        void Gameplayscreen_lv3Update(MouseState ms)
        {

        }


            
        


        void St_screenDraw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();

            bgd1.DrawMe(_spriteBatch);
            st_button.DrawMe(_spriteBatch, gameTime);
            sk_button.DrawMe(_spriteBatch, gameTime);
            _spriteBatch.DrawString(debugFont, "Res: " + _graphics.PreferredBackBufferWidth
                                              + " x " + _graphics.PreferredBackBufferHeight,
                                              Vector2.Zero, Color.White);

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

            
            for (int i = 0; i < bricks_lv1.Count; i++)
            {

                bricks_lv1[i].DrawMe(_spriteBatch);
                
            }
            _ball.DrawMe(_spriteBatch);
            bat.DrawMe(_spriteBatch);

            _spriteBatch.End();
        }

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
        void Gameplayscreen_level2_Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            _spriteBatch.Begin();
            for (int i = 0; i < bricks_lv2.Count; i++)
            {

                bricks_lv2[i].DrawMe(_spriteBatch);

            }
            _ball.DrawMe(_spriteBatch);
            bat.DrawMe(_spriteBatch);

            _spriteBatch.End();
        }

        void Gameplayscreen_level3_Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            _spriteBatch.Begin();

        }

    }
}