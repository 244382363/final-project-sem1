using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace final_project_sem1
{
    class ball
    {
        public Vector2 _position;
        public Rectangle Rect;
        private Texture2D _art;
        public Vector2 _velocity;

        private float _rotation;
        private float _rotationSpeed;

        // Class Constructors
        public ball(Texture2D txr, Vector2 startPos, Vector2 startVel)
        {
            _position = startPos;
            _velocity = startVel;
            _art = txr;
            Rect = new Rectangle(_position.ToPoint(), txr.Bounds.Size);

            _rotation = 0;
            _rotationSpeed = _velocity.Length() / 32;
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
        }

        public void DrawMe(SpriteBatch sb)
        {
            // sBatch.Draw(Art, Position, Color.White);
            sb.Draw(_art, _position, null, Color.White, _rotation, _art.Bounds.Center.ToVector2(), new Vector2(0.1f), SpriteEffects.None, 0);
        }
    }
}
