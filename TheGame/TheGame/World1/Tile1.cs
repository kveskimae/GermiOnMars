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
using Germi.GameObject;
using Germi.Util;
using Germi.GameWorld;

namespace Germi.World1
{
    public class Tile_w1_r1_c0 : AbstractTile
    {

        private GameplayScreen _gameplayScreen;

        public Tile_w1_r1_c0(GameplayScreen gameplayScreen, IWorld world)
            : base(gameplayScreen, new Vector2(500, 400), world)
        {
            _gameplayScreen = gameplayScreen;
        }

        public override void initObjects()
        {
           // worldObjects.Add(new Rock(getGame(), new Vector2(380, 250)));
            
            // Right
            Door door2 = new Door(_gameplayScreen, World1.DoorLocationRight, DoorPlacement.SCREEN_RIGHT, 1, 1);
            worldObjects.Add(door2);

            worldObjects.Add(new Door(_gameplayScreen, World1.DoorLocationTop, DoorPlacement.SCREEN_TOP, 0, 0));
            worldObjects.Add(new Door(_gameplayScreen, World1.DoorLocationBottom, DoorPlacement.SCREEN_BOTTOM, 2, 0));

            // Bottom
            /*
            Door doorB = new Door(getGame(), new Vector2(780 - 93, 400), DoorPlacement.SCREEN_BOTTOM, 0, 2);
            worldObjects.Add(doorB);

            // Top
            Door doorT = new Door(getGame(), new Vector2(780 - 93, 400), DoorPlacement.SCREEN_TOP, 0, 0);
            worldObjects.Add(doorT);
            */
            base.initObjects();
        }

    }
}
