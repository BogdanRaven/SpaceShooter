using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace Spaceshooter
{
    class EnemyExplosion
    {
        public Texture2D texture;
        public Vector2 position;
        int frameWidth = 140;
        int frameHeight = 140;
        Point currentFrame = new Point(0, 0);
        Point spriteSize = new Point(5, 1);//размер астероида, количество кадров
        int currentTime = 0; // сколько времени прошло
        int period = 80; // частота обновления в миллисекундах
        public bool isVisible;

        public EnemyExplosion(Texture2D newTexture, Vector2 newPosition)
        {
            position = newPosition;
            texture = newTexture;
            isVisible = true;
        }

        public void LoadContent(ContentManager Content)
        {

        }

        public void Update(GameTime gameTime)
        {
            if (isVisible == true)
            {//анимация взрыва
                currentTime += gameTime.ElapsedGameTime.Milliseconds;
                if (currentTime > period)
                {
                    currentTime -= period;

                    ++currentFrame.X;

                    if (currentFrame.X >= spriteSize.X)
                    {
                        currentFrame.X = 0;
                        isVisible = false;
                    }
                }
            }
        }
        //отрисовка
        public void Draw(SpriteBatch spriteBatch)
        {
            if (isVisible == true)
            {
                spriteBatch.Draw(texture, position,
                    new Rectangle(currentFrame.X * frameWidth, currentFrame.Y * frameHeight, frameWidth, frameHeight),
                    Color.White, 0, Vector2.Zero,
                    1, SpriteEffects.None, 0);
            }
        }
    }
}
