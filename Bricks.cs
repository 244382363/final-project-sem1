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
        string _text;
        Vector2 _pos;
        


        public Bricks(Texture2D txr, int xpos, int ypos)
        {
            _txr = txr;
            _pos = new Vector2(xpos, ypos);
            _rect = new Rectangle(xpos, ypos, _txr.Width, _txr.Height);

        }

        public void DrawMe(SpriteBatch sb)
        {
            sb.Draw(_txr, _pos, Color.White);
        }





    }
}
