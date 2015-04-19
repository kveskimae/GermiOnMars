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
    public interface IObject
    {
        void reset();

        // previous IMovableObject
        MoveDirection getDirection();

        void setDirection(MoveDirection newDirection);

        int getSpeed();

        void setSpeed(int newSpeed);

        // Use it for immovable objects
        void Animate(GameTime gameTime);

        void AnimateVerticalMovement(GameTime gameTime);

        void AnimateHorizontalMovement(GameTime gameTime);
        // end of previous IMovableObject



        void Draw(GameTime gameTime, SpriteBatch spriteBatch);

        void LoadContent(ContentManager content);

        GameplayScreen getGameplayScreen();

        // Verifies that another object represented by parameter rectangle is not inside this object
        bool isAllowedLocation(Rectangle location);

        // Nyyd on hoopis interactWithGermi
        //bool isGermiDeath(Rectangle location);

        Vector2 getLocation();

        void setNewLocation(Vector2 newLocation);

        Rectangle getSpace();

        Rectangle getSpace(Vector2 location);

        ObjectType getObjectType();

        bool isMovable();

        void interactWithGermi(Rectangle germiSpace);

        bool isActive();

        bool isVisible();

        void setVisibility(bool status);

        float getScaleFactor();

        void setScaleFactor(float scaleFactor);

        ObjectState State {get; set;}

    }
}
