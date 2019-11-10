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
   public class Score
    {
        public int playerScore, screenWidth, screenHeight,Level;
        public SpriteFont playerScoreFont;
        public Vector2 playerScorePos,LevelPos;
        public bool showMod,showModLevel;

       

        public Score()
        {
            playerScore = 0;
            screenWidth = 950;
            screenHeight = 800;
            showMod = true;
            showModLevel = true;
            playerScoreFont = null;
            playerScorePos = new Vector2(50, 25);
            LevelPos = new Vector2(50,75);
           
        }

        public void LoadContent(ContentManager Content)
        {
            playerScoreFont = Content.Load<SpriteFont>("MyText");
            
        }

        public void Update(GameTime gameTime,int level)
        {
            KeyboardState keystate = Keyboard.GetState();
            Level = level;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (showMod == true)
            {
                spriteBatch.DrawString(playerScoreFont, "Score - "+playerScore, playerScorePos,Color.Red);
               
            }
            if(showModLevel==true)
            spriteBatch.DrawString(playerScoreFont, "Level - " + Level, LevelPos, Color.Red);
        }

        
    }
}
