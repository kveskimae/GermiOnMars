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
    public class Key : AbstractObject
    {

        public Key(GameplayScreen screen, Vector2 pkaardiAsukoht)
            : base(screen, pkaardiAsukoht, "key2", -1)
        {
           // base.setScaleFactor(0.08f);
        }

        public override ObjectType getObjectType()
        {
            return ObjectType.KEY;
        }

        public override void interactWithGermi(Rectangle p)
        {
            
            if (p.Intersects(this.getSpace()))
            {
                // MainCharacter.Instance.setSpeed(5);
                // setActive(false);
                State = ObjectState.TO_BE_DELETED;
                getGameplayScreen().getCurrentWorld().PassToNextLevel = true;
            }
            
        }


    }
}
