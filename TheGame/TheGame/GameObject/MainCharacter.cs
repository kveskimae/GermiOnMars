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
using Germi.GameObject;
using Germi.Util;
using TheGame;


namespace Germi.GameObject
{
    public class MainCharacter : AbstractMovableObject
    {
        private MoveDirection direction;
        private static MainCharacter _instance;

        public static MainCharacter Instance
        {
            get { return MainCharacter._instance; }
            set { MainCharacter._instance = value; }
        }

        public MainCharacter(GameplayScreen gameplayScreen, Vector2 pkaardiAsukoht)
            : base(gameplayScreen, pkaardiAsukoht, "downmovesheet_2", 3, 40)
        {
        }


        public override ObjectType getObjectType()
        {
            return ObjectType.MAIN_CHARACTER;
        }

        public override void AnimateVerticalMovement(GameTime gameTime)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds / 2;
            if (timer > interval)
            {
                currentFrame++;
                timer = 0;

            }
            if (currentFrame > 3)
            {
                currentFrame = 0;
            }
        }

        public override void AnimateHorizontalMovement(GameTime gameTime)
        {
            //TBI
        }

        public override MoveDirection getDirection()
        {
            return direction;
        }
         
         

        public override void setDirection(MoveDirection newDirection)
        {
            direction = newDirection;
        }

        public override bool isAllowedLocation(Rectangle location)
        {
            throw new NotImplementedException("Collisions are not to be checked in Germo");
        }
        /*
        public override bool isGermiDeath(Rectangle location)
        {
            throw new NotImplementedException("Germo is not suicidal");
        }
        */

        public override Rectangle getSpace(Vector2 loc)
        {
            int shiftY = 15;
            Rectangle ret = new Rectangle((int)loc.X, (int)loc.Y + shiftY, getWidth(), getHeight() - shiftY);
            return ret;
        }

    }
}