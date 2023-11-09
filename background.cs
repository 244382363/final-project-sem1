using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;


namespace final_project_sem1
{
    class background
    {
        Texture2D _tex;
        Vector2 _pos;
        Rectangle _rect;

        public background(Texture2D tex, int width, int height)
        {
            _tex = tex;
            _rect = new Rectangle(0, 0, width, height);


        }
        //can be used for background that uses vector
        public background(Texture2D tex)
        {
            _tex = tex;
            _rect = new Rectangle(0, 0, 1024, 990);
        }
        //can be used for background that does not use vector

        //lines above uses constructor overload
        public void DrawMe(SpriteBatch sb)
        {
            sb.Draw(_tex, _rect, Color.White);

        }
    }
}
