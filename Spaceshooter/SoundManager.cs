using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;

namespace Spaceshooter
{
    class SoundManager
    {
        public SoundEffect shoot;
        public SoundEffect explosion;
        public Song mediaFon;

        public SoundManager()
        {
            shoot = null;
            explosion = null;
            mediaFon = null;
        }

        public void LoadContent(ContentManager Content)
        {
            shoot = Content.Load<SoundEffect>("Shoot");
            explosion = Content.Load<SoundEffect>("Explosion");
        }
    }
}
