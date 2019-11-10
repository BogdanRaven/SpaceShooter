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
    class SpaceFon
    {
        public Texture2D texture;
        public Vector2 fpos1,fpos2;
        public int speed ;

        public  SpaceFon()
        {
            texture = null;
            fpos1 = new Vector2(0, 0);
            fpos2 = new Vector2(0, -950);
            speed = 5;
        }

        public void LoadContent(ContentManager Content)
        {
            texture = Content.Load<Texture2D>("Fon");
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture,fpos1,Color.White);// сдесь мы создаем две картинки которые будут двигаться
            spriteBatch.Draw(texture, fpos2, Color.White);
        }
        
        public void Update(GameTime gameTime)// такая штука создает типа анимацию движения
        {
            fpos1.Y += speed;
            fpos2.Y += speed;

            if (fpos1.Y >= 950)
            {
                fpos1.Y = 0;
                fpos2.Y = -950;
            }
        }

        
    }
}
