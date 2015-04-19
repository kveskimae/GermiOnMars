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
    public class Stairway : AbstractObject
    {

        public Stairway(GameplayScreen screen, Vector2 pkaardiAsukoht)
            : base(screen, pkaardiAsukoht, "stairway3", -1)
        {
        }

        public override ObjectType getObjectType()
        {
            return ObjectType.STAIRWAY;
        }

        public override void interactWithGermi(Rectangle p)
        {
            
            if (p.Intersects(this.getSpace()))
            {
                if (getGameplayScreen().getCurrentWorld().PassToNextLevel)
                {
                    getGameplayScreen().goToNextWorld();
                }
            }
            
        }


    }
}
