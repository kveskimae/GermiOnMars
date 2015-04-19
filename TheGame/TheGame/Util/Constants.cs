using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Germi.Util
{

    public enum MoveDirection
    {
        N, S, W, E, STOP, NW, NE, SW, SE,
        NONE // pseudo-direction for non-wall movement
    }

    public enum ObjectType
    {
        MAIN_CHARACTER, ROCK, ALIEN, POWERUP, DOOR, FIREBALL, KEY, STAIRWAY
    }

    public enum DoorPlacement
    {
        SCREEN_TOP, SCREEN_BOTTOM, SCREEN_LEFT, SCREEN_RIGHT
    }

    public enum ObjectState
    {
            ON_MAP, TO_BE_DELETED, DELETED
    }

    public enum WorldID 
    { 
        WORLD1, WORLD2    
    }

}