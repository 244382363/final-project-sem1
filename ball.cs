using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;


namespace final_project_sem1
{
    class ball
    {
        public Vector2 _position;
        public Rectangle Rect;
        private Texture2D _art;
        public Vector2 _velocity;
        float ballSpeed = 6;

        private float _rotation;
        private float _rotationSpeed;
        public bool is_selected;
        public int skins_type;

        // Class Constructors
        public ball(Texture2D txr, Vector2 startPos, Vector2 startVel, int skins_type)
        {
            _position = startPos;
            _velocity = startVel;
            _art = txr;
            Rect = new Rectangle(_position.ToPoint(), txr.Bounds.Size);

            _rotation = 0;
            _rotationSpeed = _velocity.Length() / 32;
            if (skins_type == 1)
            {
                is_selected = true;
            }
            else
            {
                is_selected = false;
            }
        }

        // Class Methods
        public void UpdateMe(Rectangle bounds)
        {
            _position += _velocity;
            _rotation += _rotationSpeed;

            if (_position.X < bounds.Left || _position.X > bounds.Right)
                _velocity.X *= -1;

            if (_position.Y < bounds.Top || _position.Y > bounds.Bottom)
                _velocity.Y *= -1;

            

            

            Rect = new Rectangle(_position.ToPoint() - _art.Bounds.Center, _art.Bounds.Size);
        }

        

        public void DrawMe(SpriteBatch sb)
        {
            // sB.Draw(Art, Position, Color.White);
            sb.Draw(_art, _position, null, Color.White, _rotation, _art.Bounds.Center.ToVector2(), 1, SpriteEffects.None, 0);
            sb.Draw(Game1.pixel, Rect, Color.White * 0.5f);
        }
    }
}
