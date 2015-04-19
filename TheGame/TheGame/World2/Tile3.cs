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

namespace Germi.World2
{
    public class Tile_w2_r1_c2 : AbstractTile
    {

        private GameplayScreen _gameplayScreen;

        public Tile_w2_r1_c2(GameplayScreen gameplayScreen, IWorld world)
            : base(gameplayScreen, new Vector2(300, 250), world)
        {
            _gameplayScreen = gameplayScreen;
        }

        public override void initObjects()
        {
            // buff = new Powerup(_gameplayScreen, new Vector2(250, 300));
            // worldObjects.Add(new Rock(_gameplayScreen, new Vector2(200, 250)));
            // worldObjects.Add(new Rock(_gameplayScreen, new Vector2(450, 250)));
            // worldObjects.Add(buff);

            // worldObjects.Add(new Door(_gameplayScreen, new Vector2(350, 80), DoorPlacement.SCREEN_TOP));
            // worldObjects.Add(new Door(_gameplayScreen, new Vector2(350, 505), DoorPlacement.SCREEN_BOTTOM));
            Door door1 = new Door(_gameplayScreen,World2.DoorLocationLeft, DoorPlacement.SCREEN_LEFT, 1, 1);
            //Door door2 = new Door(_gameplayScreen, new Vector2(780 - 93, 200), DoorPlacement.SCREEN_RIGHT, 0, 1);
            worldObjects.Add(door1);
            // worldObjects.Add(door2);

            IObject alien1 = new Alien(_gameplayScreen, new Vector2(150, 480), MoveDirection.N);
            IObject alien2 = new Alien(_gameplayScreen, new Vector2(450, 480), MoveDirection.W);
            worldObjects.Add(alien1);
            worldObjects.Add(alien2);
            base.initObjects();
        }

    }
}
