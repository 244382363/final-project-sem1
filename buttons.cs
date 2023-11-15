using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace final_project_sem1
{
    class buttons
    {

        private Rectangle _rect;
        private Texture2D st_SpriteSheet;
        private Vector2 st_position;
        private Rectangle st_animCell;
        public Rectangle CollisionRect;
        private float st_frameTimer;
        private float st_fps;

        public buttons(Texture2D spriteSheet, int xpos, int ypos, int frameCount, int fps)
        {
            st_SpriteSheet = spriteSheet;
            st_animCell = new Rectangle(0,0, st_SpriteSheet.Width / frameCount, spriteSheet.Height);
            st_position = new Vector2(xpos, ypos);
            st_frameTimer = 1;
            st_fps = fps;

            CollisionRect = new Rectangle(xpos, ypos,
                st_SpriteSheet.Width / frameCount, spriteSheet.Height);
         
            //m1 = new MouseState();
        }

        public void Clicked()
        {
            st_frameTimer = 16;
        }

        public void DrawMe(SpriteBatch sb, GameTime gt)
        {
            /* m_animCell.X = (m_animCell.X + m_animCell.Width);
             if (m_animCell.X >= m_SpriteSheet.Width)
                 m_animCell.X = 0;*/
            //modify the drawme method of spinning coin
            if (CollisionRect.Contains(Mouse.GetState().X, Mouse.GetState().Y) && st_frameTimer <= 0)
            {
                st_animCell.X = (st_animCell.X + st_animCell.Width);
                if (st_animCell.X >= st_SpriteSheet.Width)
                    st_animCell.X = 0;
                   
                

                st_frameTimer = 1;
            }

            else 
            {
                
            
            
                st_frameTimer -= (float)gt.ElapsedGameTime.TotalSeconds * st_fps;
                
            }
            sb.Draw(st_SpriteSheet, st_position, st_animCell, Color.White);


        }
    }
}
