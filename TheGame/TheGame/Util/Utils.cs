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

namespace Germi.Util
{
    public class Utils
    {


        public static MoveDirection getGermiDirection(KeyboardState keyboard)
        {
            KeyboardState currentState = keyboard;
            if (currentState.IsKeyDown(Keys.Left) && currentState.IsKeyDown(Keys.Up))
                return MoveDirection.NW;
            else if (currentState.IsKeyDown(Keys.Right) && currentState.IsKeyDown(Keys.Up))
                return MoveDirection.NE;
            else if (currentState.IsKeyDown(Keys.Right) && currentState.IsKeyDown(Keys.Down))
                return MoveDirection.SE;
            else if (currentState.IsKeyDown(Keys.Left) && currentState.IsKeyDown(Keys.Down))
                return MoveDirection.SW;
            else if (currentState.IsKeyDown(Keys.Left))
                return MoveDirection.W;
            else if (currentState.IsKeyDown(Keys.Right))
                return MoveDirection.E;
            else if (currentState.IsKeyDown(Keys.Up))
                return MoveDirection.N;
            else if (currentState.IsKeyDown(Keys.Down))
                return MoveDirection.S;
            else
                return MoveDirection.STOP;
        }


        public static bool isNewDirection()
        {
            Random random = new Random();
            int randomNumber = random.Next(0, 50);
            bool ret = randomNumber == 5;
            return ret;
        }

        public static MoveDirection getNewRandomDirection(MoveDirection previousDirection)
        {
            List<MoveDirection> directions = new System.Collections.Generic.List<MoveDirection>();
            directions.Add(MoveDirection.S);
            directions.Add(MoveDirection.W);
            directions.Add(MoveDirection.E);
            directions.Add(MoveDirection.N);
            directions.Remove(previousDirection);
            int lenght = directions.Count;
            Random random = new Random();
            int randomNumber = random.Next(0, lenght);
            MoveDirection newDirection = directions[randomNumber];
            return newDirection;
        }



        public static bool isIllegalKeyCombination(KeyboardState presentKey)
        {
            bool upAndDown = presentKey.IsKeyDown(Keys.Up) && presentKey.IsKeyDown(Keys.Down);
            bool leftAndRight = presentKey.IsKeyDown(Keys.Left) && presentKey.IsKeyDown(Keys.Right);
            return upAndDown || leftAndRight;
        }

    }
}
