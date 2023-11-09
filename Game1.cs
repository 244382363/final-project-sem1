using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace final_project_sem1
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public static readonly Random RNG = new Random();
        GamePadState padcurr;

        SpriteFont debugFont;
        background bgd1,bgd2;
        Bat bat;

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
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            debugFont = Content.Load<SpriteFont>("Ariel07");

            bgd1 = new background(Content.Load<Texture2D>("skin select screen"));
            bgd2 = new background(Content.Load<Texture2D>("game start screen"));
            bat = new Bat(Content.Load<Texture2D>("bounce disk"), 400, 900);

            
        }

        protected override void Update(GameTime gameTime)
        {
            padcurr = GamePad.GetState(PlayerIndex.One);
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            bat.UpdateMe(padcurr);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();

            bgd1.DrawMe(_spriteBatch);
            bat.DrawMe(_spriteBatch);


            _spriteBatch.DrawString(debugFont, "Res: " + _graphics.PreferredBackBufferWidth
                                              + " x " + _graphics.PreferredBackBufferHeight,
                                              Vector2.Zero, Color.White);
            _spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}