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
    public class Alien : AbstractMovableObject
    {

        public Alien(GameplayScreen screen, Vector2 pkaardiAsukoht, MoveDirection initialDirection)
            : base(screen, pkaardiAsukoht, "blueslime", 1, 50)
        {
            setDirection(initialDirection);
        }

        public override void interactWithGermi(Rectangle p)
        {
            if (getSpace().Intersects(p))
            {
                //getGame().germiDies();
                getGameplayScreen().getCurrentWorld().getCurrentTile().germiDies();
            }
        }

         public override ObjectType getObjectType()
         {
             return ObjectType.ALIEN;
         }

    }
}
