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
    public class Rock : AbstractObject
    {

        public Rock(GameplayScreen screen, Vector2 pkaardiAsukoht) 
            : base (screen, pkaardiAsukoht, "kivi1", -1)
        {
        }

        public override ObjectType getObjectType()
        {
            return ObjectType.ROCK;
        }

    }
}
