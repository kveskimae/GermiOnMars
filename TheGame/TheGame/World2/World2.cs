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
using Germi.GameWorld;

namespace Germi.World2
{
    // Teha AbstractWorld ja viia sina mäng, tagataust jms
    public class World2 : AbstractWorld
    {

        /// <summary>
        /// <para>Door locations on screen side for the current world</para>
        /// <para>For different worlds redefine these locations as the background image and/or doors are different</para>
        /// </summary>
        public static readonly Vector2
            DoorLocationRight = new Vector2(780 - 93, 300),
            DoorLocationLeft = new Vector2(15, 300),
            DoorLocationTop = new Vector2(350, 85),
            DoorLocationBottom = new Vector2(350, 505);

        public World2(GameplayScreen gameplayScreen)
            : base(gameplayScreen, new Vector2(0, 0), "Tagataust", WorldID.WORLD2)
        {
            getTiles()[0][0] = new Tile_w2_r0_c0(gameplayScreen, this);
            getTiles()[0][1] = new Tile_w2_r0_c1(gameplayScreen, this);
            getTiles()[0][2] = new Tile_w2_r0_c2(gameplayScreen, this);

            getTiles()[1][0] = new Tile_w2_r1_c0(gameplayScreen, this);
            getTiles()[1][1] = new Tile_w2_r1_c1(gameplayScreen, this);
            getTiles()[1][2] = new Tile_w2_r1_c2(gameplayScreen, this);

            getTiles()[2][0] = new Tile_w2_r2_c0(gameplayScreen, this);
            getTiles()[2][1] = new Tile_w2_r2_c1(gameplayScreen, this);
            getTiles()[2][2] = new Tile_w2_r2_c2(gameplayScreen, this);

            PassToNextLevel = false; // Has to find key first
        }



        public override MoveDirection isInWall(Rectangle rect)
        {
            float gerNewY = rect.Y, gerNewX = rect.X;
            if (gerNewY < 160)
            {
                // Top of the screen
                return MoveDirection.N;
            }
            if (gerNewX < 96)
            {
                // Left of the screen;
                return MoveDirection.W;
            }
            if (gerNewY + rect.Height + 80 > Game1.screenHeight)
            {
                // Bottom of the screen
                return MoveDirection.S;
            }
            if (gerNewX + rect.Width + 80 > Game1.screenWidth)
            {
                // Right of the screen
                return MoveDirection.E;
            }
            return MoveDirection.NONE;
        }

    }
}
