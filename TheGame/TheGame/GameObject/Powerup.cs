using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using TheGame;
using Germi.Util;

namespace Germi.GameObject
{
    public class Powerup : AbstractObject
    {
        public Rectangle powerUpRectangle;
        
        public Powerup(GameplayScreen screen, Vector2 pkaardiAsukoht)
            : base(screen, pkaardiAsukoht, "powerup", 30)
        {
            
        }

        public override ObjectType getObjectType()
        {
            return ObjectType.POWERUP;
        }

        public override void Animate(GameTime gameTime)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds / 2;
            int locationX = (int)this.getLocation().X;
            int locationY = (int)this.getLocation().Y;
            int textureWidth = (int)this.getTexture().Width;
            int textureHeight = (int)this.getTexture().Height;
            if (timer >= interval)
            {
                currentFrame++;
                timer = 0;
            }
            if (currentFrame >= 12)
                currentFrame = 0;
            powerUpRectangle = new Rectangle(locationX, locationY, textureWidth, textureHeight);
        }

        public override void interactWithGermi(Rectangle p)
        {
            if (p.Intersects(this.getSpace()))
            {
                // getGame().getCurrentWorld().getCurrentTile().getObjects().Remove(this);
                MainCharacter.Instance.setSpeed(5);
                setActive(false);
            }
        }


    }
}
