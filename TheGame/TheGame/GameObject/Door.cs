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
    public class Door : AbstractObject
    {

        protected readonly int doorShift = 10;

        private readonly DoorPlacement placement;

        private readonly int row, col;

        public DoorPlacement Placement
        {
            get { return placement; }
        }


        public Door(GameplayScreen gameplayScreen, Vector2 pkaardiAsukoht, DoorPlacement placement, int row, int col)
            : base(gameplayScreen, pkaardiAsukoht, "uks1_avatud_2t", -1)
        {
            this.placement = placement;
            this.row = row;
            this.col = col;
        }

        public override ObjectType getObjectType()
        {
            return ObjectType.DOOR;
        }


        public override void interactWithGermi(Rectangle germiSpace)
        {
            // Rectangle germiSpace = getGame().getGermi().getSpace();
            if (germiSpace.Intersects(this.getEntranceSpace()))
            {
                // getGame().getGermi().setNewLocation(getGame().getCurrentWorld().getCurrentTile().getGermisInitialLocation());
                getGameplayScreen().getCurrentWorld().switchTile(row, col, placement);
            }
        }

        public Vector2 getExitGermiPosition()
        {
            Vector2 ret = new Vector2(getEntranceSpace().X, getEntranceSpace().Y);
            MainCharacter germi = MainCharacter.Instance;
            switch (Placement)
            {
                case DoorPlacement.SCREEN_BOTTOM:
                    ret.X += getEntranceSpace().Width / 2;
                    ret.Y -=
                        getEntranceSpace().Height +
                         2 * doorShift
                        + germi.getSpace().Height;
                    break;
                case DoorPlacement.SCREEN_LEFT:
                    ret.X += getEntranceSpace().Width + doorShift;
                    ret.Y += getEntranceSpace().Height / 2;
                    break;
                case DoorPlacement.SCREEN_RIGHT:
                    ret.X -= getEntranceSpace().Width + doorShift + germi.getSpace().Width;
                    ret.Y += getEntranceSpace().Height / 2;
                    break;
                case DoorPlacement.SCREEN_TOP:
                    ret.X += getEntranceSpace().Width / 2;
                    ret.Y += getEntranceSpace().Height + 2 * doorShift + germi.getSpace().Height;
                    break;
                default:
                    throw new NotSupportedException("Unknown door placement: " + Placement);
            }
            return ret;
        }

        public Rectangle getEntranceSpace()
        {
            int shiftY = getSpace().Y, shiftX = getSpace().X, subWidth = getSpace().Width, subHeight = getSpace().Height;
            switch (Placement)
            {
                case DoorPlacement.SCREEN_RIGHT:
                    shiftX += 10;
                    shiftY += 50;
                    subWidth = getSpace().Height - 90;
                    subHeight = getSpace().Width - 100;
                    break;
                case DoorPlacement.SCREEN_LEFT:
                    subWidth = getSpace().Height - 90;
                    subHeight = getSpace().Width - 100;
                    shiftX -= 10;
                    shiftX += getSpace().Height;
                    shiftY += 50;
                    break;
                case DoorPlacement.SCREEN_TOP:
                    shiftY += 80;
                    shiftX += 50;
                    subWidth -= 100;
                    subHeight -= 90;
                    break;
                case DoorPlacement.SCREEN_BOTTOM:
                    shiftY += 10;
                    shiftX += 50;
                    subWidth -= 100;
                    subHeight -= 90;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("Unknown door placement: " + Placement);
            }

            return new Rectangle(shiftX, shiftY, subWidth, subHeight);
        }



    }
}
