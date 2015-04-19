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
using TheGame;
using Germi.Util;

namespace Germi.GameObject
{
    public abstract class AbstractMovableObject : AbstractObject, IObject
    {

        private int speed;
        private MoveDirection direction;

        public AbstractMovableObject(GameplayScreen screen, Vector2 pkaardiAsukoht, string texture, int initSpeed, int frameWidth) :
            base(screen, pkaardiAsukoht, texture, frameWidth)
        {
            speed = initSpeed;
        }

        public override void reset()
        {

            this.setNewLocation(getInitialPosition());
            base.reset();
        }

        public override MoveDirection getDirection()
        {
            return direction;
        }

        public override void setDirection(MoveDirection newDirection)
        {
            direction = newDirection;
        }

        public override int getSpeed()
        {
            return speed;
        }

        public override void setSpeed(int newSpeed)
        {
            this.speed = newSpeed;
        }


        public override bool isVisible()
        {
            return base.isVisible();
        }
        public override void setVisibility(bool visibility)
        {
            base.setVisibility(visibility);
        }

        public override bool isMovable()
        {
            return true;
        }

    }
}
