using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;


namespace Spaceshooter
{
   
    public class Game1 : Game
    {
        //space shooter one love
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        int EnemyBulletDameg;//урон от врага
        int EnemyBulletDamegUp;//урон от врага 2
        public int Level=1;
        bool upLevel = false;
        int enemyNumber;
        int enemyNumberUp;

        Random r = new Random();
        SpaceFon spacefon = new SpaceFon();
        Menu menu = new Menu();
        Player player = new Player();

        SoundManager sound = new SoundManager();
        Asteroid asteroid = new Asteroid();
        Score score = new Score();


        List<Enemy> enemyList = new List<Enemy>();//список врагов 1
        List<Enemy> enemyListUp = new List<Enemy>();//список врагов2
        List<EnemyExplosion> explosionsList = new List<EnemyExplosion>();

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferHeight = 800;
            graphics.PreferredBackBufferWidth = 950;
            Window.Title = "----Spaceshooter---";
           IsMouseVisible = false;
            enemyNumber = 3;
            enemyNumberUp=0;

            EnemyBulletDameg = 5;
            EnemyBulletDamegUp = 10;
        }
        
        protected override void Initialize()
        {

          
            menu.Initialize(IsMouseVisible);
            base.Initialize();

        }

      
        protected override void LoadContent()
        {
           
            spriteBatch = new SpriteBatch(GraphicsDevice);
            menu.LoadContent(Content);
            player.LoadContent(Content);
            spacefon.LoadContent(Content);
            asteroid.LoadContent(Content);
            score.LoadContent(Content);
            sound.LoadContent(Content);
        }

      
        protected override void UnloadContent()
        {
           
        }

       
      
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                EXIT();
                menu.isVisibleBack = false;
                menu.isVisibleHelpText = false;
                menu.isVisibleGameOver = false;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Enter)&&menu.isVisibleGameOver==true)
            {
                menu.isVisibleGameOver = false;
                EXIT();
            }
            levelProgres();
            GameOver(gameTime);
            spacefon.Update(gameTime);
            menu.Update(gameTime);
            if (menu.pressedExit == true)
            {
                Exit();
            }
            if(menu.isVisibleMenu==true)//устанавливаем скорость фона в меню
            spacefon.speed = 1;
            if (menu.isVisibleMenu == false)
            {
                spacefon.speed = 5;
                score.Update(gameTime,Level);
                player.Update(gameTime);
               
                asteroid.Update(gameTime);

                LoadEnemy();
                ManagerExplosion();
                Colisions(gameTime);//метод косания
            }

            base.Update(gameTime);
        }

        
      //Дров
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            spacefon.Draw(spriteBatch);
            menu.Draw(spriteBatch);
           
            if (menu.isVisibleMenu == false)
            {

                score.Draw(spriteBatch);

                player.Draw(spriteBatch);

                foreach (Enemy e in enemyList)
                {
                    e.Draw(spriteBatch);
                }
                foreach (Enemy e in enemyListUp)
                {
                    e.Draw(spriteBatch);
                }
                foreach (EnemyExplosion exp in explosionsList)
                {
                    exp.Draw(spriteBatch);
                }
                asteroid.Draw(spriteBatch);
            }
            if(menu.isVisibleGameOver==true)
            spriteBatch.DrawString(score.playerScoreFont, " " + score.playerScore, new Vector2(480,575), Color.Red);
            spriteBatch.End();
            base.Draw(gameTime);
        }

        //Загрузка Врагов
        public void LoadEnemy()
        {
            int randY = r.Next(-600,-50);
            int randX = r.Next(0,750);
            int randY1 = r.Next(-600, -50);
            int randX1 = r.Next(0, 750);

            if (enemyList.Count < enemyNumber)//количество врагов на поле
            {
                enemyList.Add(new Enemy(Content.Load<Texture2D>("Enemy"),new Vector2(randX,randY),Content.Load<Texture2D>("bulletEnem"),3));
                
            }
            if (enemyListUp.Count < enemyNumberUp)//количество врагов2 на поле
            {
                if (Level >= 2)
                {
                    enemyListUp.Add(new Enemy(Content.Load<Texture2D>("EnemyUpLevel"), new Vector2(randX1, randY1), Content.Load<Texture2D>("bulletEnem"),6));
                }
            }

            for(int i = 0; i < enemyList.Count; i++)
            {
                if (enemyList[i].isVisible== false)
                {
                    enemyList.RemoveAt(i);
                    i--;
                }
            }

            for (int i = 0; i < enemyListUp.Count; i++)
            {
                if (enemyListUp[i].isVisible == false)
                {
                    enemyListUp.RemoveAt(i);
                    i--;
                }
            }
        }

        //Касания
        public void Colisions(GameTime gameTime)
        {
            //пересекания связаные с врагом 2 уровня
            foreach (Enemy eUp in enemyListUp)
            {
               
                //пересекания корабля и врага
                if (eUp.bound.Intersects(player.bound))
                {
                    sound.explosion.Play();
                    explosionsList.Add(new EnemyExplosion(Content.Load<Texture2D>("ExplosionAnimationEnemy"), eUp.postion));
                    eUp.isVisible = false;
                    player.helth -= 40;
                }
                //пересекания астероида и врага
                if (eUp.bound.Intersects(asteroid.bound))
                {
                    sound.explosion.Play();
                    explosionsList.Add(new EnemyExplosion(Content.Load<Texture2D>("ExplosionAnimationEnemy"), eUp.postion));
                    eUp.isVisible = false;
                    asteroid.posAnimExplos = asteroid.position;
                    asteroid.isVisibleExpl = true;
                    asteroid.isVisible = false;
                    asteroid.position = new Vector2(0, 950);
                }

                //пересекания пули врага и корабля
                for (int i = 0; i < eUp.buletList.Count; i++)
                {
                    if (player.bound.Intersects(eUp.buletList[i].bound))
                    {
                        player.helth -= EnemyBulletDamegUp;
                        eUp.buletList[i].isVisible = false;
                    }
                }

                //пересекания пули корабля и врага
                for (int i = 0; i < player.bulletlist.Count; i++)
                {
                    if (player.bulletlist[i].bound.Intersects(eUp.bound))
                    {
                        eUp.health--;

                        player.bulletlist[i].isVisible = false;
                        if (eUp.health <= 0)
                        {
                            sound.explosion.Play();
                            explosionsList.Add(new EnemyExplosion(Content.Load<Texture2D>("ExplosionAnimationEnemyUp"), eUp.postion));
                            score.playerScore += 40;
                            eUp.isVisible = false;
                        }
                    }

                }
                eUp.Update(gameTime);
            }

            //касания связаные с врагом 1 level
            foreach (Enemy e in enemyList)
            {
                //пересекания корабля и врага
                if (e.bound.Intersects(player.bound))
                {
                    sound.explosion.Play();
                    explosionsList.Add(new EnemyExplosion(Content.Load<Texture2D>("ExplosionAnimationEnemy"), e.postion));
                    e.isVisible = false;
                    player.helth -= 40;
                }
                //пересекания астероида и врага
                if (e.bound.Intersects(asteroid.bound))
                {
                    sound.explosion.Play();
                    explosionsList.Add(new EnemyExplosion(Content.Load<Texture2D>("ExplosionAnimationEnemy"), e.postion));
                    e.isVisible = false;
                    asteroid.posAnimExplos = asteroid.position;
                    asteroid.isVisibleExpl = true;
                    asteroid.isVisible = false;
                    asteroid.position = new Vector2(0, 950);
                }

                //пересекания пули врага и корабля
                for (int i = 0; i < e.buletList.Count; i++)
                {
                    if (player.bound.Intersects(e.buletList[i].bound))
                    {
                        player.helth -= EnemyBulletDameg;
                        e.buletList[i].isVisible = false;
                    }
                }

                //пересекания пули корабля и врага
                for (int i = 0; i < player.bulletlist.Count; i++)
                {
                    if (player.bulletlist[i].bound.Intersects(e.bound))
                    {
                        e.health--;

                        player.bulletlist[i].isVisible = false;
                        if (e.health <= 0)
                        {
                            sound.explosion.Play();
                            explosionsList.Add(new EnemyExplosion(Content.Load<Texture2D>("ExplosionAnimationEnemy"), e.postion));
                            score.playerScore += 20;
                            e.isVisible = false;
                        }
                    }

                }

                e.Update(gameTime);
            }
                

            //код связаный с взрывом
                foreach (EnemyExplosion exps in explosionsList)
                {
                    exps.Update(gameTime);
                }

                //пересекания астероида и корабля
                if (asteroid.bound.Intersects(player.bound))
                {
                    sound.explosion.Play();
                    asteroid.posAnimExplos = asteroid.position;
                    asteroid.isVisibleExpl = true;
                    asteroid.isVisible = false;

                    asteroid.position = new Vector2(0, 950);
                    player.helth -= 50;
                }
                //пересекания пули персонажа и астероида
                for (int i = 0; i < player.bulletlist.Count; i++)
                {
                    if (asteroid.bound.Intersects(player.bulletlist[i].bound))
                    {
                        asteroid.halth--;
                        player.bulletlist[i].isVisible = false;
                        if (asteroid.halth <= 0)
                        {
                            sound.explosion.Play();
                            asteroid.posAnimExplos = asteroid.position;
                            asteroid.isVisibleExpl = true;
                            asteroid.isVisible = false;
                            score.playerScore += 40;
                            asteroid.position = new Vector2(0, 950);
                        }
                    }
                }
            
        }
        public void ManagerExplosion()
        {
            for(int i = 0; i < explosionsList.Count; i++)
            {
                if (explosionsList[i].isVisible == false)
                {
                    explosionsList.RemoveAt(i);
                    i--;
                }
            }
        }
       public void GameOver(GameTime gameTime)
        {
            if (player.helth <= 0)
            {
                player.posAnimExpl = player.position;
                player.Explosion(gameTime);
                if (player.isVisibleExpl == false) {
                    menu.isVisibleGameOver = true;
                    menu.isVisibleMenu = true;
                    ClearField();
                   

                }
                
               
                
            }
        }
       public void EXIT()
            {
                score.playerScore = 0;
                menu.isVisibleMenu = true;
                menu.isVisiblePlay = true;
                menu.isVisibleExit = true;
                menu.isVisibleHelp = true;
            ClearField();

              
            }
        public void ClearField()
        {
       
            player.position = new Vector2(600, 600);
            enemyNumber = 3;
            enemyNumberUp = 0;
            EnemyBulletDameg = 5;
            EnemyBulletDamegUp = 10;
            Level = 1;
            player.helth = 200;
            //очистка от пуль взрывов, врагов
            for (int i = 0; i < enemyList.Count; i++)
            {

                enemyList.RemoveAt(i);
                i--;

            }
            for (int i = 0; i < player.bulletlist.Count; i++)
            {

                player.bulletlist.RemoveAt(i);
                i--;

            }
            for (int i = 0; i < enemyListUp.Count; i++)
            {

                enemyListUp.RemoveAt(i);
                i--;

            }
            for (int i = 0; i < explosionsList.Count; i++)
            {

                explosionsList.RemoveAt(i);
                i--;

            }
            asteroid.isVisible = false;
            asteroid.isVisibleExpl = false;
            asteroid.position = new Vector2(0, 950);
            asteroid.posAnimExplos = new Vector2(0, 950);
        }//очистка игрового поля

        public void levelProgres()//метод прогреса уровня
        {
         
            if ((score.playerScore >= 100&& Level==1))
            {
                EnemyBulletDameg +=5 ;
                enemyNumberUp=2;
                
                Level = 2;
            }
            if ((score.playerScore >= 200)&&Level==2)
            {
               
                EnemyBulletDamegUp += 5;
                enemyNumber = 4;
                Level = 3;
            }
            if ((score.playerScore >= 300)&&Level==3)
            {
                enemyNumberUp = 3;
                Level = 4;
            }
            if ((score.playerScore >= 400) && Level == 4)
            {
                enemyNumber = 5;
                Level = 5;
            }
            if ((score.playerScore >= 500) && Level == 5)
            {
                EnemyBulletDameg += 5;
               
                Level = 6;
            }

            if ((score.playerScore >= 600) && Level == 6)
            {
                EnemyBulletDameg += 5;
                enemyNumberUp = 4;
                Level = 7;
            }
            if ((score.playerScore >= 700) && Level == 7)
            {
                EnemyBulletDamegUp += 5;
                enemyNumber = 7;
                Level = 8;
            }
            if ((score.playerScore >= 800) && Level == 8)
            {
               
                enemyNumber = 7;
                enemyNumberUp = 5;
                Level = 9;
            }
            if ((score.playerScore >= 900) && Level == 9)
            {
                EnemyBulletDameg += 5;
                enemyNumber = 7;
                enemyNumberUp = 5;
                Level = 10;
            }
            if ((score.playerScore >= 1000) && Level == 10)
            {
                EnemyBulletDamegUp += 5;
                enemyNumber = 7;
                enemyNumberUp = 6;
                Level = 11;
            }

        }

    }
}
