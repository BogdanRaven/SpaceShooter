using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;


using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
namespace Spaceshooter
{
    class Menu
    {
        
        Texture2D play;
        Texture2D help;
        Texture2D exit;
        Texture2D cursor;
        Texture2D back;
        Texture2D helpText;
        Texture2D GameOver;
        Texture2D Avtor;
        Rectangle boundPlay, boundHelp, boundExit,boundBack;
        Vector2 posPlay, posHelp, posExit,posMous,posBack, posHelpText, posGameOver,posAvtor;
        public bool isVisibleMenu,isVisiblePlay,isVisibleHelp,isVisibleExit,  pressedExit,isVisibleBack, isVisibleHelpText, isVisibleGameOver;
        public Menu()
        {
            posPlay = new Vector2(150, 100);
            posExit = new Vector2(150,600);
            posHelp = new Vector2(150, 400);
            posBack = new Vector2(500,600);
            posGameOver = new Vector2(10, 10);
            posHelpText = new Vector2(20, 20);
            posAvtor = new Vector2(500,700);
        }

        public void Initialize(bool isMouseVisible)
        {
            isMouseVisible = true;
            isVisibleMenu = true;
            isVisiblePlay = true;
            isVisibleHelp = true;
            isVisibleExit = true;
            isVisibleBack = false;
            isVisibleHelpText = false;
            isVisibleGameOver = false;
            
        }

        public void LoadContent(ContentManager Content)
        {
            play = Content.Load<Texture2D>("Play");
            help = Content.Load<Texture2D>("Help");
            exit = Content.Load<Texture2D>("Exit");
            cursor = Content.Load<Texture2D>("Cursor");
            back = Content.Load<Texture2D>("Back");
            helpText = Content.Load<Texture2D>("HelpText");
            GameOver = Content.Load<Texture2D>("GameOver");
            Avtor = Content.Load<Texture2D>("Avtor");
            boundPlay = new Rectangle((int)posPlay.X, (int)posPlay.Y, play.Width, play.Height);//размер колизиана
            boundExit = new Rectangle((int)posExit.X, (int)posExit.Y, exit.Width, exit.Height);//размер колизиана
            boundHelp = new Rectangle((int)posHelp.X, (int)posHelp.Y, help.Width, help.Height);//размер колизиана
            boundBack = new Rectangle((int)posBack.X, (int)posBack.Y, back.Width, back.Height);//размер колизиана
        }

      
        public void Update(GameTime gameTime)
        {
            if (isVisibleMenu == true)
            {
             
                var mouseState = Mouse.GetState();
                posMous.X = mouseState.X;
                posMous.Y = mouseState.Y;

                if (boundPlay.Contains(mouseState.X, mouseState.Y) && mouseState.LeftButton == ButtonState.Pressed && isVisiblePlay == true)
                {
                    isVisiblePlay = false;
                    isVisibleMenu = false;
                    isVisibleExit = false;
                    isVisibleHelp = false;
                    isVisibleHelpText = false;
                }
                if (boundExit.Contains(mouseState.X, mouseState.Y) && mouseState.LeftButton == ButtonState.Pressed && isVisibleExit == true)
                {
                    isVisibleExit = false;

                    pressedExit = true;
                }
                if (boundHelp.Contains(mouseState.X, mouseState.Y) && mouseState.LeftButton == ButtonState.Pressed && isVisibleHelp == true)
                {
                    isVisibleHelp = false;
                    isVisibleExit = false;
                    isVisiblePlay = false;
                    isVisibleBack = true;
                    isVisibleHelpText = true;
                }
                if (boundBack.Contains(mouseState.X, mouseState.Y) && mouseState.LeftButton == ButtonState.Pressed && isVisibleBack == true)
                {
                    isVisibleHelp = true;
                    isVisibleExit = true;
                    isVisiblePlay = true;
                    isVisibleBack = false;
                    isVisibleHelpText = false;
                }
            }
           
            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
           
            if (isVisibleExit == true)
                spriteBatch.Draw(exit, posExit, Color.White);

            if (isVisibleHelp == true)
                spriteBatch.Draw(help, posHelp, Color.White);
            if (isVisibleHelpText == true)
                spriteBatch.Draw(helpText, posHelpText, Color.White);
            if (isVisibleBack == true)
                spriteBatch.Draw(back, posBack, Color.White);
            if (isVisiblePlay == true)
            {
                spriteBatch.Draw(play, posPlay, Color.White);
                spriteBatch.Draw(Avtor, posAvtor, Color.White);

            }
            if(isVisibleMenu==true)
            spriteBatch.Draw(cursor, posMous, Color.White);
            if (isVisibleGameOver == true)
            {
                spriteBatch.Draw(GameOver, posGameOver, Color.White);
            }
        }
       
    }
}
