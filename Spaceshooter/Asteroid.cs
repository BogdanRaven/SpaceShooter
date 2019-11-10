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
    class Asteroid
    {
        public Texture2D texture,animationTextureExplosion;
       public  Vector2 position;
        
        public Rectangle bound;
       
        public int halth = 3;
        public int speed;
        public bool isVisible;

        int frameWidth = 127;
        int frameHeight = 140;
        Point currentFrame = new Point(0, 0);
        Point spriteSize = new Point(8, 1);//размер астероида, количество кадров
        int currentTime = 0; // сколько времени прошло
        int period = 60; // частота обновления в миллисекундах

        //анимация взрыва астероида
        int frameWidthExplos = 120;
        int frameHeightExplos = 120;
        Point currentFrameExplos = new Point(0, 0);
        Point spriteSizeExplos = new Point(5, 1);
        int currentTimeExplos = 0;
        int periodExplos =40;
        public bool isVisibleExpl;
        public Vector2 posAnimExplos;

        Random r = new Random();
        public int randX, randY;
        public Asteroid()
        {
            randX = r.Next(20, 700);
            randY = r.Next(-2000, -1000);
            position = new Vector2(randX,randY);
            speed = 10;
            texture = null;
            isVisible = true;

            isVisibleExpl = false;
           
        }
        //load Content
        public void LoadContent(ContentManager Content)
        {
            texture = Content.Load<Texture2D>("AsteroidAnim");
            animationTextureExplosion = Content.Load<Texture2D>("ExplosionAnimationAsteroid");
            
        }

        //Update
        public void Update(GameTime gameTime)
        {
            bound= new Rectangle((int)position.X, (int)position.Y, 120, 90);//размер колизиана
            position.Y += speed;//движения
            if (position.Y >= 950)
            {
                randX = r.Next(0 + frameWidth, 750 - frameHeight);
                randY = r.Next(-2000, -1000);
                position=new Vector2(randX,randY);
                isVisible = true;
                halth = 3;
            }

            Explosion(gameTime);

            //вертения астероида(анимация)
            currentTime += gameTime.ElapsedGameTime.Milliseconds;
            if (currentTime > period)
            {
                currentTime -= period;

                ++currentFrame.X;

                if (currentFrame.X >= spriteSize.X)
                {
                    currentFrame.X = 0;
                }
            }


         
        }


        //Дров
        public void Draw(SpriteBatch spriteBatch)
        {
            if (isVisible == true)
            {
                spriteBatch.Draw(texture, position,
                new Rectangle(currentFrame.X * frameWidth,currentFrame.Y * frameHeight,frameWidth, frameHeight), 
                Color.White, 0, Vector2.Zero,
                1, SpriteEffects.None, 0);
            }
            if (isVisibleExpl == true)
           {
                spriteBatch.Draw(animationTextureExplosion, posAnimExplos,
                new Rectangle(currentFrameExplos.X * frameWidthExplos, currentFrameExplos.Y * frameHeightExplos, frameWidthExplos, frameHeightExplos),
                Color.White, 0, Vector2.Zero,
                1, SpriteEffects.None, 0);
            }


        }
        //анимация ростворения астероида
        public void Explosion(GameTime gameTime)
        {
           if (isVisibleExpl == true)
            {
                currentTimeExplos += gameTime.ElapsedGameTime.Milliseconds;
                if (currentTimeExplos > periodExplos)
                {
                    currentTimeExplos -= periodExplos;

                    ++currentFrameExplos.X;

                    if (currentFrameExplos.X >= spriteSizeExplos.X)
                    {
                        currentFrameExplos.X = 0;
                        isVisibleExpl = false;
                    }
                }
            }
        }

        
    }
}
