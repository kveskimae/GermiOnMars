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
    public class Fireball : AbstractMovableObject, IObject
    {

        private int _distance = 300;
        private int _liikumiskiirus = 6;
        private int _elapsedDistance;
        private Vector2 kiiruseSuund;
        private Vector2 initialPosition = new Vector2(MainCharacter.Instance.getLocation().X, MainCharacter.Instance.getLocation().Y);

        public Vector2 InitialPosition
        {
            get { return initialPosition; }
            set { initialPosition = value; }
        }
        
        public Rectangle fireballRectangle;

        public int ElapsedDistance
        {
            get { return _elapsedDistance; }
            set { _elapsedDistance = value; }
        }
        public int Distance
        {
            get { return _distance; }
            set { _distance = value; }
        }
        public int Liikumiskiirus
        {
            get { return _liikumiskiirus; }
            set { _liikumiskiirus = value; }
        }

        public Vector2 getKiiruseSuund()
        {
            return kiiruseSuund;
        }
        public Fireball(GameplayScreen gameplayScreen, Vector2 initialAsukoht)
            : base(gameplayScreen, initialAsukoht, "fireball", 6, 15)
        {
            if (MainCharacter.Instance.getDirection() == MoveDirection.N)
                this.kiiruseSuund = new Vector2(0, -getSpeed());
            else if (MainCharacter.Instance.getDirection() == MoveDirection.S)
            {
                this.initialPosition = new Vector2(MainCharacter.Instance.getLocation().X, MainCharacter.Instance.getLocation().Y + 40);
                this.kiiruseSuund = new Vector2(0, getSpeed());
            }
            else if (MainCharacter.Instance.getDirection() == MoveDirection.E)
                this.kiiruseSuund = new Vector2(getSpeed(), 0);
            else if (MainCharacter.Instance.getDirection() == MoveDirection.W)
                this.kiiruseSuund = new Vector2(-getSpeed(), 0);
            else if (MainCharacter.Instance.getDirection() == MoveDirection.NE)
                this.kiiruseSuund = new Vector2(getSpeed(), -getSpeed());
            else if (MainCharacter.Instance.getDirection() == MoveDirection.NW)
                this.kiiruseSuund = new Vector2(-getSpeed(), -getSpeed());
            else if (MainCharacter.Instance.getDirection() == MoveDirection.SE)
                this.kiiruseSuund = new Vector2(getSpeed(), getSpeed());
            else if (MainCharacter.Instance.getDirection() == MoveDirection.SW)
                this.kiiruseSuund = new Vector2(-getSpeed(), getSpeed());

                

        }

        public override void Animate(GameTime gameTime)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (timer >= interval)
            {
                currentFrame++;
                timer = 0;
            }
            if (currentFrame >= 6)
            {
                currentFrame = 0;
            }
            base.Animate(gameTime);
        }

        public override ObjectType getObjectType()
        {
            return ObjectType.FIREBALL;
        }

        public Rectangle fbrectangle()
        {
            return this.getSpace();
        }




    }
}

