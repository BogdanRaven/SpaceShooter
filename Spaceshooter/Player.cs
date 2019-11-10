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
    class Player
    {
        public Texture2D sprite,bulletsprite,helthTexture;
        public float bulletdelay;// задержка при выстреле
        public Vector2 position,helthBapPosition;
        public int speed,helth;
        public float life = 10f;
        SoundManager sound = new SoundManager();
    
        public Rectangle bound,helthRectangle;

        public List<Bullet> bulletlist;//список пуль

        int frameWidth = 120;
        int frameHeight = 140;
        Point currentFrame = new Point(0, 0);
        Point spriteSize = new Point(5, 1);//размер астероида, количество кадров
        int currentTime = 0; // сколько времени прошло
        int period = 60; // частота обновления в миллисекундах
        public Texture2D  animationTextureExplosion;
        public bool isVisibleExpl=false;
        public Vector2 posAnimExpl;

        public  Player()
        {
            bulletlist = new List<Bullet>();// список обьектов пуля
            bulletdelay = 10;
            speed = 8;
            position = new Vector2(600, 600);
            sprite = null;
            helth = 200;
            helthBapPosition = new Vector2(50, 50);

        }
        //Загрузка спрайтов
        public void LoadContent(ContentManager Content)
        {
            animationTextureExplosion = Content.Load<Texture2D>("ExplosionAnimationPlayer");
            sprite = Content.Load<Texture2D>("Pixelship");
            bulletsprite = Content.Load<Texture2D>("bullet");
            helthTexture = Content.Load<Texture2D>("Helth");
            sound.LoadContent(Content);
        }
        // Метод рисования
        public void Draw(SpriteBatch spriteBatch)
        {
            if(isVisibleExpl==false)
            spriteBatch.Draw(sprite, position, Color.White);
            spriteBatch.Draw(helthTexture,helthRectangle,Color.White);
            foreach (Bullet b in bulletlist)
                b.Draw(spriteBatch);
            if (isVisibleExpl == true)
            {
                spriteBatch.Draw(animationTextureExplosion, posAnimExpl,
                new Rectangle(currentFrame.X * frameWidth, currentFrame.Y * frameHeight, frameWidth, frameHeight),
                Color.White, 0, Vector2.Zero,
                1, SpriteEffects.None, 0);
            }
        }
        // метод апдейт
        public void Update(GameTime gameTime)
        {
            bound = new Rectangle((int)position.X, (int)position.Y, sprite.Width, sprite.Height);
            helthRectangle = new Rectangle((int)helthBapPosition.X, (int)helthBapPosition.Y, helth, 25);
            moove();
        }
        //метод движения
        public void moove()
        {
            KeyboardState keyboardState = Keyboard.GetState();
            //стрельба
            if (keyboardState.IsKeyDown(Keys.Space))
            {
                shoot();
            }
            updateBullet();
            //проверяем не улител наш корабль за рамки игры)
            if (position.X < 0)
                position.X = 0;
            if (position.Y < 0)
                position.Y = 0;
            if (position.X > 950 - sprite.Width)
                position.X = 950 - sprite.Width;
            if (position.Y > 800 - sprite.Height)
                position.Y = 800 - sprite.Height;

            //перемещения корабля с помощу клавиш
            if (isVisibleExpl == false)
            {
                if (keyboardState.IsKeyDown(Keys.Left) || keyboardState.IsKeyDown(Keys.A))
                    position.X -= speed;
                if (keyboardState.IsKeyDown(Keys.Right) || keyboardState.IsKeyDown(Keys.D))
                    position.X += speed;
                if (keyboardState.IsKeyDown(Keys.Up) || keyboardState.IsKeyDown(Keys.W))
                    position.Y -= speed;
                if (keyboardState.IsKeyDown(Keys.Down) || keyboardState.IsKeyDown(Keys.S))
                    position.Y += speed;
            }
        }
       
        public void shoot() //метод стрельбы
        {
            if (bulletdelay >= 0)
            {
                bulletdelay--;
            }
            if (bulletdelay <= 0)
            {
                sound.shoot.Play();
                Bullet newBullet = new Bullet(bulletsprite);
                newBullet.position = new Vector2(position.X+50 -newBullet.texture.Width,position.Y+5); //позиция пули при выстрели
                newBullet.isVisible = true;
                if (bulletlist.Count() < 20)// макс количество пуль на поле
                    bulletlist.Add(newBullet);
            }
            if (bulletdelay == 0)
            {
                bulletdelay = 10;
            }
        }
         
        public void updateBullet()// метод обновления пули
        {
            foreach(Bullet b in bulletlist)
            {
                b.position.Y -= b.speed;
                b.bound = new Rectangle((int)b.position.X,(int)b.position.Y, b.texture.Width,b.texture.Height);
                if (b.position.Y <= 0)
                {
                    b.isVisible = false;
                }
            }
            for( int i =0; i < bulletlist.Count; i++)
            {
                if (bulletlist[i].isVisible == false)
                {
                    bulletlist[i].position.Y = 0;
                    bulletlist.RemoveAt(i);//удаление пули
                  
                    i--;
                   
                }
            }
        }
        //взрыв персонажа
        public void Explosion(GameTime gameTime)
        {
            isVisibleExpl = true;
                currentTime += gameTime.ElapsedGameTime.Milliseconds;
                if (currentTime > period)
                {
                    currentTime -= period;

                    ++currentFrame.X;

                    if (currentFrame.X >= spriteSize.X)
                    {
                        currentFrame.X = 0;
                        isVisibleExpl = false;
                    }
                }
            
        }

    }

}
