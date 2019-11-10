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
    class Enemy
    {
        public Texture2D texture, bulettexture;
        public Rectangle bound;
        public Vector2 postion;
        public int speed, health, buletDelay, curentLevel;
        public List<Bullet> buletList;
        public bool isVisible;
        //Конструктор
        public Enemy(Texture2D newTexture,Vector2 newPosition, Texture2D newbulletTexture,int helthNew)
        {
            buletList = new List<Bullet>();
            texture = newTexture;
            bulettexture = newbulletTexture;
            postion = newPosition;
            health = helthNew;//здоровья врага
            curentLevel = 1;
            buletDelay = 40;
            isVisible = true;
            speed = 3;
        }

      

        //Update
        public void Update(GameTime gameTime)
        {
            bound = new Rectangle((int)postion.X,(int)postion.Y,texture.Width,texture.Height);

            postion.Y += speed;

            if (postion.Y >= 950)
            {
                isVisible = false;
            }
                

            EnemyShoot();//выстрел врага
            updateBullet();//обновления пули врага
        }

        //Draw
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture,postion,Color.White);

            foreach (Bullet b in buletList)
            {
                b.Draw(spriteBatch);
            }
        }

        // метод обновления пули
        public void updateBullet()
        {
            foreach (Bullet b in buletList)
            {
                b.position.Y += b.speed;
                b.bound = new Rectangle((int)b.position.X, (int)b.position.Y, b.texture.Width, b.texture.Height);
                if (b.position.Y >= 950)
                {
                    b.isVisible = false;
                }
            }
            for (int i = 0; i < buletList.Count; i++)
            {
                if (buletList[i].isVisible == false)
                {
                    buletList[i].position.Y = 0;
                    buletList.RemoveAt(i);//удаление пули

                    i--;

                }
            }
        }
        //Стрельба Врага
        public void EnemyShoot()
        {
            if (buletDelay >= 0)
            {
                buletDelay--;
                
            }
            if (buletDelay <= 0)
            {
                Bullet newBullet = new Bullet(bulettexture);
                newBullet.position = new Vector2(postion.X+46-newBullet.texture.Width/2,postion.Y+25);//позиция пули при вылете 
                newBullet.isVisible = true;
                if (buletList.Count < 5)// Максимальное количество пули врага на поле
                {
                    buletList.Add(newBullet);
                }
                if (buletDelay == 0)
                {
                    buletDelay = 80;//задержка пули
                }
            }
        }
    }
}
