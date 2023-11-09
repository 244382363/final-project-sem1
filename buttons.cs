using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace final_project_sem1
{
    class buttons
    {

        private Rectangle _rect;
        private Texture2D _txr;

        public buttons(Texture2D newtxr, int xpos, int ypos)
        {
            _txr = newtxr;
            _rect = new Rectangle(xpos, ypos, newtxr.Width, newtxr.Height);
         
            //m1 = new MouseState();
        }
    }
}
