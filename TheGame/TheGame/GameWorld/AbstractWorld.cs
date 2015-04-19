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
using Germi.GameObject;

namespace Germi.GameWorld
{
    public abstract class AbstractWorld : IWorld
    {

        private GameplayScreen _gameplayScreen;
        private ITile currentTile;
        private Texture2D bgTexture;
        private readonly Vector2 mBGUpperLeft;
        private readonly string mBGTextureName;
        /// <summary>
        /// Tiles matrix for the world. Use indices like following: [row][col].
        /// </summary>
        private List<List<ITile>> tiles;
        private bool _PassToNextLevel = true;

        private readonly WorldID _id;

        
        public WorldID ID
        {
            get { return _id; }
        }

        public bool PassToNextLevel
        {
            get { return _PassToNextLevel; }
            set { _PassToNextLevel = value; }
        }

        public AbstractWorld(GameplayScreen gameplayScreen, Vector2 bgUpperLeft, string bgTextureName, WorldID id)
        {
            _id = id;
            _gameplayScreen = gameplayScreen;
            mBGUpperLeft = bgUpperLeft;
            mBGTextureName = bgTextureName;
            initTiles();
        }

        public List<List<ITile>> getTiles()
        {
            return tiles;
        }

        public GameplayScreen getGameplayScreen()
        {
            return _gameplayScreen;
        }


        public void Init()
        {
            foreach (List<ITile> cols in tiles)
            {
                foreach (ITile cur in cols)
                {
                    cur.initObjects();
                }
            }
            switchTile(1, 1);
        }

        protected void initTiles()
        {
            tiles = new System.Collections.Generic.List<List<ITile>>();
            for (int rows = 0; rows < 3; rows++)
            {
                List<ITile> row = new System.Collections.Generic.List<ITile>();
                // Set dummy values into matrix so that implementing world can set tiles by just invoking the following: getTiles()[row][col] = ...
                row.Add(null);
                row.Add(null);
                row.Add(null);
                tiles.Add(row);
            }
        }

        public void switchTile(int row, int col)
        {
            currentTile = tiles[row][col];
            getCurrentTile().reset();
        }

        public void switchTile(int row, int col, DoorPlacement enterPlacement)
        {
            currentTile = tiles[row][col];
            getCurrentTile().reset();
            MainCharacter.Instance.setNewLocation(getCurrentTile().getGermiExitPosition(enterPlacement));
        }

        public ITile getCurrentTile()
        {
            return currentTile;
        }

        public abstract MoveDirection isInWall(Rectangle rect);


        public void LoadContent(ContentManager Content)
        {
            foreach (List<ITile> cols in tiles)
            {
                foreach (ITile cur in cols)
                {
                    cur.LoadContent(Content);
                }
            }
            bgTexture = Content.Load<Texture2D>(mBGTextureName);
        }

        // Redefine in conrete implementation for world-specific updates
        public virtual void UpdateWorld(GameTime gameTime, KeyboardState presentKey)
        {
            getCurrentTile().Update(gameTime, presentKey);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(bgTexture, mBGUpperLeft, Color.White);
            getCurrentTile().Draw(gameTime, spriteBatch);
        }

    }
}
