using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace final_project_sem1
{
    public class Game1 : Game
    {
        enum GameStates
        {
            St_screen,
            Skin_select,
            Gameplayscreen

        }
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private MouseState _mouseState;


        public static readonly Random RNG = new Random();
        GamePadState padcurr;

        GameStates _currState;
        SpriteFont debugFont;
        background bgd1, bgd2;
        Bat bat;
        buttons st_button, bk_button, sk_button;
        ball _ball;


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




            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            debugFont = Content.Load<SpriteFont>("Ariel07");

            Vector2 startPos = new Vector2(GraphicsDevice.Viewport.Bounds.Center.X + RNG.Next(-100, 100),
                                                        GraphicsDevice.Viewport.Bounds.Center.Y + RNG.Next(-100, 100));

            Vector2 startVel = new Vector2((float)(RNG.NextDouble() * 2) - 4,
                                                        (float)(RNG.NextDouble() * 2) - 4);
            _ball = new ball(Content.Load<Texture2D>("ball"), startPos, startVel);

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


            

           


            _ball.UpdateMe(GraphicsDevice.Viewport.Bounds, bat.CollisionRect);
            bat.UpdateMe(padcurr);





            switch (_currState)
            {
                case GameStates.St_screen:
                    St_screenUpdate(_mouseState);
                    break;

                case GameStates.Skin_select:
                    Skin_selectUpdate(_mouseState);
                    break;
                case GameStates.Gameplayscreen:
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
                case GameStates.Gameplayscreen:
                    GameplayscreenDraw(gameTime);
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
                _currState = GameStates.Gameplayscreen;

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

            _ball.DrawMe(_spriteBatch);
            bat.DrawMe(_spriteBatch);

            _spriteBatch.End();
        }
    }
}