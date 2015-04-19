using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace TheGame
{
    public interface IImmovableObject
    {

        void Draw(GameTime gameTime, SpriteBatch spriteBatch);

        void LoadContent(ContentManager content);

        bool isAllowedLocation(Vector2 location);

    }
}
