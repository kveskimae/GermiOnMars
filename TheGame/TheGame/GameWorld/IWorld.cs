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
using Germi.Util;


namespace Germi.GameWorld
{
    /// <summary>
    /// Represent a world i.e. one game level or, in another words, a set of tiles
    /// </summary>
    public interface IWorld
    {

        /// <summary>
        /// Redraws the current world. Call only when this world is currently presented on screen
        /// </summary>
        /// <param name="gameTime">Game time</param>
        /// <param name="spriteBatch">Sprite batch where objects are drawn</param>
        void Draw(GameTime gameTime, SpriteBatch spriteBatch);

        /// <summary>
        /// Loads textures used in this world. Loads only background texture used in world and delegates loading to tiles composing this world
        /// </summary>
        /// <param name="content">Content manager used to load textures</param>
        void LoadContent(ContentManager content);

        /// <summary>
        /// Updates this world. Delegates to currently presented tile
        /// </summary>
        /// <param name="gameTime">Game time</param>
        /// <param name="presentKey">Keyboard state</param>
        void UpdateWorld(GameTime gameTime, KeyboardState presentKey);

        /// <summary>
        /// Initializes this world object. Call after constructing it to populate this world with its tiles and tiles with their objects
        /// </summary>
        void Init();

        /// <summary>
        /// Verifies that movable object represented by its interaction space is not located in wall
        /// </summary>
        /// <param name="rec">Movable object's space</param>
        /// <returns>None-direction (standing) if parameter movable object is not in wall, direction of the colliding wall (e.g. for top of the screen north etc) otherwise</returns>
        MoveDirection isInWall(Rectangle rec);

        /// <summary>
        /// Retrieves the currently presented tile on screen
        /// </summary>
        /// <returns>Active tile</returns>
        ITile getCurrentTile();

        /// <summary>
        /// TO BE DELETED, NOT NECESSARY AS WE HAVE THIS METHOD IN TILE
        /// </summary>
        // void germiDies();

        /// <summary>
        /// <para>Switches currently active tile (i.e. tile presented on screen) to the tile located on parameter row and column in the tiles matrix.</para>
        /// <para>Use this method when first presenting the world. Use overloaded version with door for main character movements between tiles.</para>
        /// </summary>
        /// <param name="row">Row index (for a world of 3x3 tileset: top row - 0, middle row - 1, bottom row - 2)</param>
        /// <param name="col">Column index (for a world of 3x3 tileset: left row - 0, middle row - 1, right row - 2)</param>
        void switchTile(int row, int col);

        void switchTile(int row, int col, DoorPlacement enterPlacement);

        bool PassToNextLevel { get; set; }

        WorldID ID { get; }

    }
}
