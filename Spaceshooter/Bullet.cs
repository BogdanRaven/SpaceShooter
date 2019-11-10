using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace Spaceshooter
{
    class Bullet
    {
        public Texture2D texture;
        public Vector2 position;
  
        public Rectangle bound;
        public float speed;
        public bool isVisible;//видим ли обьект игроку
        //конструкто
        public Bullet(Texture2D newtexture2D)
        {
            isVisible = false;
            speed = 10f;
            texture = newtexture2D;
           
        }
        //Дров
        public void Draw(SpriteBatch spriteBatch)
        {
            
            spriteBatch.Draw(texture,position,Color.White);
        }
    }
}
