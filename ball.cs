using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;


namespace final_project_sem1
{

    
    class ball
    {
        
        public Vector2 _position, _currpos;
        public Rectangle Rect;
        public Texture2D _art;
        public Vector2 _velocity;
        float ballSpeed = 6;

        private float _rotation;
        private float _rotationSpeed;
        public bool is_selected;
        public int NOOF_bounces,Spaceship_health;

        // Class Constructors
        public ball(Texture2D txr, Vector2 startPos, Vector2 startVel)
        {
            
            _position = startPos;
            _velocity = startVel;
            _art = txr;
            Rect = new Rectangle(_position.ToPoint() - _art.Bounds.Center,txr.Bounds.Size);

            NOOF_bounces = 0;
            Spaceship_health = 100;
            _rotation = 0;
            _rotationSpeed = _velocity.Length() / 32;
            
        }

       

        // Class Methods
        public void UpdateMe(Rectangle bounds)
        {
            _position += _velocity;
            _rotation += _rotationSpeed;
            //if x position of the ball is less than left bound or greater than right bound
            if (_position.X < bounds.Left || _position.X > bounds.Right)
            {
                _velocity.X *= -1;
                NOOF_bounces += 1;
                
            }

            if (_position.Y < bounds.Top) 
            {
                _velocity.Y *= -1;
                NOOF_bounces += 1;

            }
            if (_position.Y > bounds.Bottom)
            {
                _velocity.Y *= -1;
                NOOF_bounces += 1;
                Spaceship_health -= 10;
            }
            _position += _currpos;




            Rect = new Rectangle(_position.ToPoint() - _art.Bounds.Center, _art.Bounds.Size);
        }

        

        public void DrawMe(SpriteBatch sb)
        {
            if (Rect.Contains(Mouse.GetState().X, Mouse.GetState().Y))
            {
                sb.Draw(_art, _position, null, Color.White * 0.5f, _rotation, _art.Bounds.Center.ToVector2(), 1, SpriteEffects.None, 0);
            }
            else
            {
                sb.Draw(_art, _position, null, Color.White, _rotation, _art.Bounds.Center.ToVector2(), 1, SpriteEffects.None, 0);
            }
            // sB.Draw(Art, Position, Color.White);
            sb.Draw(Game1.pixel, Rect, Color.White * 0.5f);
        }
    }
}
