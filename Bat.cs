using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;


namespace final_project_sem1
{
    class Bat
    {

        public Rectangle CollisionRect;
        private Texture2D _txr;
        private bool _goingRight;
        public float speed;


        public Bat(Texture2D txr, int xPos, int yPos)
        {
            _txr = txr;
            CollisionRect = new Rectangle(xPos, yPos, _txr.Width, _txr.Height);
            speed = 8f;
        }


        public void UpdateMe(GamePadState pad)
        {
            if (pad.ThumbSticks.Left.X < 0 && CollisionRect.X > 0 || Keyboard.GetState().IsKeyDown(Keys.Left) && CollisionRect.X > 0)
            {
                _goingRight = false;
                CollisionRect.X -= (int)speed;
            }

            else if (pad.ThumbSticks.Left.X > 0 && CollisionRect.X < 1024 - _txr.Width || Keyboard.GetState().IsKeyDown(Keys.Right) && CollisionRect.X < 1024 - _txr.Width)
            {
                _goingRight = true;
                CollisionRect.X += (int)speed;
            }
        }

        public void DrawMe(SpriteBatch sb)
        {
                if (_goingRight)
                    sb.Draw(_txr, CollisionRect, Color.White);
                else
                    sb.Draw(_txr, CollisionRect, null, Color.White, 0, Vector2.Zero, SpriteEffects.FlipHorizontally, 0);

            sb.Draw(Game1.pixel, CollisionRect, Color.PaleGreen * 0.5f);

        }

    }
}
