using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace final_project_sem1
{
    class Bricks
    {
        public Rectangle _rect;
        Texture2D _txr;
        
        Vector2 _pos;
        public bool extra_ball_brick, fortify_brick;


        public Bricks(Texture2D txr, int xpos, int ypos,int mod_brick)
        {
            _txr = txr;
            _pos = new Vector2(xpos, ypos);
            _rect = new Rectangle(xpos, ypos, _txr.Width, _txr.Height);

            if(mod_brick == 1)
            {
                extra_ball_brick = true;
            }
            else
            {
                extra_ball_brick = false;
            }
            if(mod_brick == 2)
            {
                fortify_brick = true;
            }
            else
            {
                fortify_brick= false;
            }
        }

        public void DrawMe(SpriteBatch sb)
        {
            sb.Draw(_txr, _pos, Color.White);
        }





    }
}
