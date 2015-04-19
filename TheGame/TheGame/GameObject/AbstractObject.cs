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
    public abstract class AbstractObject : IObject
    {
        
        private Vector2 location;
        private string mTextureString;

        private Texture2D tagaTaust1;

        private bool visible = true;
        private bool active = true;

        private GameplayScreen mScreen;

        protected readonly Vector2 initialLocation;

        protected int frameWidth = 40;
        protected int currentFrame;
        protected float timer = 0;
        protected float interval = 75;
        protected float _scaleFactor = 1.0f;
        private ObjectState _state = ObjectState.ON_MAP;

        public ObjectState State
        {
            get { return _state; }
            set { _state = value; }
        }



        public AbstractObject(GameplayScreen screen, Vector2 pkaardiAsukoht, string texture, int xFrameWidth)
        {
            mScreen = screen;
            location = pkaardiAsukoht;
            initialLocation = new Vector2(pkaardiAsukoht.X, pkaardiAsukoht.Y);
            mTextureString = texture;
            frameWidth = xFrameWidth;
        }


        public GameplayScreen getGameplayScreen()
        {
            return mScreen;
        }

        public Vector2 getInitialPosition()
        {
            return initialLocation;
        }

        // previous IMoveableObject
         
        public virtual MoveDirection getDirection()
        {
            return MoveDirection.STOP;
        }


        public float getScaleFactor()
        {
            return _scaleFactor;
        }

        public void setScaleFactor(float scaleFactor)
        {
            this._scaleFactor = scaleFactor;
        }

        public virtual void setDirection(MoveDirection newDirection)
        {
            throw new NotImplementedException("Abstract Objects don't have direction.");
        }

        
        public virtual void setSpeed(int newSpeed)
        {
            throw new NotImplementedException("Abstract Objects don't have speed.");
            
        }


        public virtual void AnimateVerticalMovement(GameTime gameTime)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds / 2;
            if (timer > interval)
            {
                currentFrame++;
                timer = 0;

            }
            if (currentFrame > 8)
            {
                currentFrame = 0;
            }
        }


        public virtual void AnimateHorizontalMovement(GameTime gameTime)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds / 2;
            if (timer > interval)
            {
                currentFrame++;
                timer = 0;

            }
            if (currentFrame > 8)
            {
                currentFrame = 0;
            }
        }

        // */ end of previous IMovableObject

        public void LoadContent(ContentManager Content)
        {
            tagaTaust1 = Content.Load<Texture2D>(mTextureString);
            if (getObjectType() == ObjectType.ROCK || getObjectType() == ObjectType.DOOR || getObjectType() == ObjectType.KEY || getObjectType() == ObjectType.STAIRWAY)
            {
                frameWidth = getTexture().Width;
            }
        }

        public abstract ObjectType getObjectType();

        protected Texture2D getTexture()
        {
            return tagaTaust1;
        }

        public GameScreen getScreen()
        {
            return mScreen;
        }

        public Vector2 getLocation()
        {
            return location;
        }

        public void setNewLocation(Vector2 germiNewPosition)
        { 
            location = germiNewPosition;
        }


        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            Rectangle gerRectangle = new Rectangle(currentFrame * frameWidth, 0, getWidth(), getHeight());
            /*
            Texture2D rect = getGame().CreateRectangle(rockSpace.Width, rockSpace.Height, Color.AliceBlue);
            spriteBatch.Draw(rect, new Vector2(rockSpace.X, rockSpace.Y), Color.White);
            */

            if (getObjectType() == ObjectType.DOOR)
            {
                float angle = 0;
                Door thisDoor = this as Door;
                SpriteEffects effects = SpriteEffects.None;
                Vector2 newLocation = new Vector2(getLocation().X, getLocation().Y);
                switch (thisDoor.Placement)
                {
                    case DoorPlacement.SCREEN_RIGHT:
                        angle = (float)(MathHelper.Pi / 2.0);
                        newLocation.X += getHeight();
                        break;
                    case DoorPlacement.SCREEN_LEFT:
                        angle = -(float)(MathHelper.Pi / 2.0);
                        newLocation.Y += getWidth();
                        break;
                    case DoorPlacement.SCREEN_TOP:
                        break;
                    case DoorPlacement.SCREEN_BOTTOM:
                        effects = SpriteEffects.FlipVertically;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException("Unknown door placement: " + thisDoor.Placement);
                }
                spriteBatch.Draw(getTexture(), newLocation, gerRectangle, Color.White, angle, new Vector2(0, 0), 1.0f, effects, 0);
            }
            else if (getObjectType() == ObjectType.FIREBALL)
            {

                spriteBatch.Draw(getGameplayScreen().fireBallTexture, getLocation(), gerRectangle, Color.White, 0f, new Vector2(0, 0), 1.0f, SpriteEffects.None, 0);
            }
            else
            {
                spriteBatch.Draw(getTexture(), getLocation(), gerRectangle, Color.White, 0f, new Vector2(0, 0), getScaleFactor(), SpriteEffects.None, 0);
            }


            if (getObjectType() == ObjectType.DOOR)
            {
                Door thisDoor = this as Door;

                // Texture2D rect2 = getGame().CreateRectangle(width, height, Color.AliceBlue);
                // spriteBatch.Draw(rect2, new Vector2(thisDoor.getSpace().X, thisDoor.getSpace().Y), Color.CadetBlue);

                /*
                Texture2D rect = getGame().CreateRectangle(thisDoor.getEntranceSpace().Width, thisDoor.getEntranceSpace().Height, Color.AliceBlue);
                spriteBatch.Draw(rect, new Vector2(thisDoor.getEntranceSpace().X, thisDoor.getEntranceSpace().Y), Color.White);

                 */



                /*
                Texture2D rect = getGame().CreateRectangle(thisDoor.getEntranceSpace().Width, thisDoor.getEntranceSpace().Height, Color.AliceBlue);
                spriteBatch.Draw(rect, new Vector2(thisDoor.getEntranceSpace().X, thisDoor.getEntranceSpace().Y), Color.White);

                int height = thisDoor.getSpace().Height, width = thisDoor.getSpace().Width;
                if (thisDoor.Placement == DoorPlacement.SCREEN_LEFT || thisDoor.Placement == DoorPlacement.SCREEN_RIGHT)
                {
                    // Rotated -> height and with swapped!
                    height = thisDoor.getSpace().Width;
                    width = thisDoor.getSpace().Height;
                    Texture2D exitRect = getGame().CreateRectangle(10, 10, Color.AliceBlue);
                    spriteBatch.Draw(exitRect, new Vector2(thisDoor.getExitGermiPosition().X, thisDoor.getExitGermiPosition().Y), Color.CadetBlue);
                }
                 */
            }

        }

        public virtual void reset()
        {
        }


        public virtual bool isAllowedLocation(Rectangle p)
        {
            return !getSpace().Intersects(p);
        }

        public virtual void interactWithGermi(Rectangle germiSpace)
        {
            // Implement in subclass
        }

        public virtual void Animate(GameTime gameTime)
        {
            // Override in animating immovable objects
        }
        
        protected int getWidth()
        {
            return frameWidth; // (int)Math.Round(getScaleFactor() * frameWidth);
        }

        protected int getHeight()
        {
            if (getObjectType() == ObjectType.FIREBALL)
            {
                return getGameplayScreen().fireBallTexture.Height;
            }
            return tagaTaust1.Height; // (int)Math.Round(getScaleFactor() * tagaTaust1.Height);
        }

        public virtual Rectangle getSpace(Vector2 loc)
        {
            Rectangle ret = new Rectangle((int)loc.X, (int)loc.Y, getWidth(), getHeight());
            return ret;
        }

        public virtual Rectangle getSpace()
        {
            return getSpace(getLocation());
        }

        public virtual int getSpeed()
        {
            //throw new NotImplementedException();
            return 0;
        }

        public virtual bool isMovable()
        {
            return false;
        }


        public virtual bool isActive()
        {
            return active;
        }

        public virtual void setActive(bool newValue)
        {
            this.active = newValue;
        }
        public virtual bool isVisible()
        {
            return this.visible;
        }

        public virtual void setVisibility(bool visibility)
        {
            this.visible = visibility;
        }
    }
}
