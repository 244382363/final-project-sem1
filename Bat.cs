using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace final_project_sem1
{
    class Bat
    {

        public Rectangle Rect;
        private Texture2D _txr;
        private bool _goingRight;
        public float speed;


        public Bat(Texture2D txr, int xPos, int yPos)
        {
            _txr = txr;
            Rect = new Rectangle(xPos, yPos, _txr.Width, _txr.Height);
            speed = 4f;
        }


        public void UpdateMe(GamePadState pad)
        {
            if (pad.ThumbSticks.Left.X < 0 && Rect.X > 0)
            {
                _goingRight = false;
                Rect.X -= (int)speed;
            }

            else if (pad.ThumbSticks.Left.X > 0 && Rect.X < 1280 - _txr.Width)
            {
                _goingRight = true;
                Rect.X += (int)speed;
            }
        }

        public void DrawMe(SpriteBatch sb)
        {
                if (_goingRight)
                    sb.Draw(_txr, Rect, Color.White);
                else
                    sb.Draw(_txr, Rect, null, Color.White, 0, Vector2.Zero, SpriteEffects.FlipHorizontally, 0);
        }
        
    }
}
