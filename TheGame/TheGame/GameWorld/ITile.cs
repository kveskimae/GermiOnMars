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
using Germi.GameObject;
using Germi.Util;
using TheGame;

namespace Germi.GameWorld
{

    /// <summary>
    /// One tile of one world
    /// </summary>
    public interface ITile
    {

        /// <summary>
        /// Use this method to add all the objects (rocks, aliens, doors etc.) to the tile being constructed
        /// </summary>
        void initObjects();

        /// <summary>
        /// Retrieves the game instance that created this tile
        /// </summary>
        /// <returns>Game reference</returns>
        GameplayScreen getGameplayScreen();

        /// <summary>
        /// Draws all active objects in this tile onto parameter sprite batch. If this is the currently active tile, call during every redraw of the screen
        /// </summary>
        /// <param name="gameTime">Game time</param>
        /// <param name="spriteBatch">Sprite batch</param>
        void Draw(GameTime gameTime, SpriteBatch spriteBatch);

        /// <summary>
        /// Loads textures for current tile
        /// </summary>
        /// <param name="content">Content manager</param>
        void LoadContent(ContentManager content);

        /// <summary>
        /// Updates the state of this tile. This method is called after fixed time intervals and only when this tile is the currently presented tile on screen
        /// </summary>
        /// <param name="gameTime">Game time</param>
        /// <param name="presentKey">Keyboard state</param>
        void Update(GameTime gameTime, KeyboardState presentKey);

        /// <summary>
        /// Retrieves the Germi's position on this tile when the tile is first presented
        /// </summary>
        /// <returns>Germi's initial position</returns>
        Vector2 getGermisInitialLocation();

        /// <summary>
        /// <para>
        /// Resets the state of this tile, i.e. positions Germi and aliens in their initial positions, adds powerups that may have been picked etc.
        /// </para>
        /// <para>
        /// Be sure to set aliens where they don't block door exits where Germi can come
        /// </para>
        /// </summary>
        void reset();

        /// <summary>
        /// Performs necessary operations when Germi dies, e.g. decreases Germi's lives counter, presents intermission screen etc.
        /// </summary>
        void germiDies();

        /// <summary>
        /// Retrieves the objects for this tile e.g. rocks, aliens etc. Note: Germi is not in this objects list
        /// </summary>
        /// <returns>Tile's objects list</returns>
        List<IObject> getObjects();

        Vector2 getGermiExitPosition(DoorPlacement prevTileEnterPos);

    }
}
