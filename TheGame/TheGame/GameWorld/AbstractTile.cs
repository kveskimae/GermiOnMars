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

    public abstract class AbstractTile: ITile
    {

        public static int score = 0;
        private GameplayScreen _gameplayScreen;
        private double timer;
        private double shootingInterval = 200;
        private IWorld mWorld;

        protected List<IObject> worldObjects = new System.Collections.Generic.List<IObject>();
        public List<Fireball> fireBalls = new System.Collections.Generic.List<Fireball>();

        protected Dictionary<DoorPlacement, Door> doors = new Dictionary<DoorPlacement, Door>();

        private readonly Vector2 gerInitialPosition; //  = new Vector2(150, 250);
        // Prevents unwanted behaviour from game motor inner optimizations
        private int randomUpdatesCount = 0;

        protected Powerup buff;

        public AbstractTile(GameplayScreen gameplayScreen, Vector2 gerInitPos, IWorld world)
        {
            _gameplayScreen = gameplayScreen;
            this.mWorld = world;
            gerInitialPosition = gerInitPos;
        }

        public Vector2 getGermiExitPosition(DoorPlacement prevDoorPlacement)
        {
            DoorPlacement exitPos;
            switch (prevDoorPlacement) {
                case DoorPlacement.SCREEN_BOTTOM:
                exitPos = DoorPlacement.SCREEN_TOP;
                break;

                case DoorPlacement.SCREEN_LEFT:
                exitPos = DoorPlacement.SCREEN_RIGHT;
                break;

                case DoorPlacement.SCREEN_RIGHT:
                exitPos = DoorPlacement.SCREEN_LEFT;
                break;

                case DoorPlacement.SCREEN_TOP:
                exitPos = DoorPlacement.SCREEN_BOTTOM;
                break;
                default:
                throw new NotSupportedException("Unknown door placement: " + prevDoorPlacement);
            }
            Door exitDoorPlacement = getDoorInPlacement(exitPos);
            Vector2 exitPosition = exitDoorPlacement.getExitGermiPosition();
            return exitPosition;
        }

        public Door getDoorOnPlacement(DoorPlacement placement)
        {
            return doors[placement];
        }

        public List<IObject> getObjects()
        {
            return worldObjects;
        }

        public GameplayScreen getGameplayScreen()
        {
            return _gameplayScreen;
        }

        public IWorld getWorld()
        {
            return mWorld;
        }

        public Door getDoorInPlacement(DoorPlacement placement)
        {
            Door ret = doors[placement];
            if (ret == null)
            {
                // Fail fast
                throw new NotSupportedException("Tile does not contain door on placement: " + placement);
            }
            return ret;
        }

        public virtual void initObjects()
        {

            foreach (IObject cur in worldObjects)
            {
                if (cur.getObjectType() == ObjectType.DOOR)
                {
                    Door curDoor = cur as Door;
                    doors.Add(curDoor.Placement, curDoor);
                }
            }
        }


        public Vector2 getGermisInitialLocation()
        {
            return gerInitialPosition;
        }

        public void reset()
        {
            foreach (IObject cur in worldObjects)
            {
                cur.reset();
            }
            MainCharacter.Instance.setNewLocation(getGermisInitialLocation());
        }

        public void UpdateFireball(GameTime gameTime, KeyboardState presentKey)
        {

            foreach (Fireball fireBall in fireBalls)
            {
                fireBall.setNewLocation(new Vector2(fireBall.getLocation().X + fireBall.getKiiruseSuund().X, fireBall.getLocation().Y + fireBall.getKiiruseSuund().Y));
                if (Vector2.Distance(MainCharacter.Instance.getLocation(), fireBall.getLocation()) >= fireBall.Distance)
                {
                    fireBall.setVisibility(false);
                }
            }
        }
        public void ShootFireball(GameTime gameTime, KeyboardState presentKey, MoveDirection germiSuund)
        {
            
                Fireball fireBall = new Fireball(getGameplayScreen(), new Vector2(MainCharacter.Instance.getLocation().X,
                                                                    MainCharacter.Instance.getLocation().Y));
            
         
                

                if (Vector2.Distance(MainCharacter.Instance.getLocation(), fireBall.getLocation()) >= fireBall.Distance)
                {
                    fireBall.setVisibility(false);
                }
            
            fireBall.setVisibility(true);
            
            UpdateFireball(gameTime, presentKey);
            //if (fireBalls.Count <= 20)
            //{
            fireBalls.Add(fireBall);
            //}

        }


        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (IObject cur in worldObjects)
            {
                if (cur.isVisible())
                {
                    cur.Draw(gameTime, spriteBatch);
                }
            }

            foreach (Fireball fireBall in fireBalls)
            {
                if (fireBall.isVisible())
                    fireBall.Draw(gameTime, spriteBatch);
            }
        }

        public void LoadContent(ContentManager Content)
        {
            foreach (IObject cur in worldObjects)
            {
                cur.LoadContent(Content);
            }
            foreach (Fireball fireBall in fireBalls)
            {
                fireBall.LoadContent(Content);
            }
        }


        /* START */



        public void germiDies()
        {
            MainCharacter.Instance.setNewLocation(gerInitialPosition);
        }

        public bool isAllowedLocation(Rectangle rec)
        {
            if ((getWorld().isInWall(rec)) != MoveDirection.NONE)
            {
                return false;
            }

            foreach (IObject cur in worldObjects)
            {
                if ((cur.getObjectType() == ObjectType.ROCK) && !cur.isAllowedLocation(rec))
                {
                    return false;
                }

                if((cur.getObjectType() == ObjectType.FIREBALL && !cur.isAllowedLocation(rec)))
                {
                    return true;
                }
            }
            return true;
        }
        /*
        private bool isGermisDeath(Rectangle germiSpace)
        {

            foreach (IObject cur in worldObjects)
            {
                if (cur.isGermiDeath(germiSpace))
                {
                    return true;
                }
            }
            return false;
        }
         */

        public void Update(GameTime gameTime, KeyboardState presentKey)
        {

            UpdateGermi(gameTime, presentKey);
            timer += gameTime.ElapsedGameTime.TotalMilliseconds;
            // UpdateDoors();

            UpdateAliens(gameTime, presentKey);
            // UpdatePowerup(gameTime);

            foreach (IObject cur in worldObjects)
            {
                if (cur.isActive())
                {
                    cur.interactWithGermi(MainCharacter.Instance.getSpace());
                    cur.Animate(gameTime);
                }
            }
            if (presentKey.IsKeyDown(Keys.Space) && timer >= shootingInterval )
            {
                ShootFireball(gameTime, presentKey, MainCharacter.Instance.getDirection());
                timer = 0;
            }
            UpdateFireball(gameTime, presentKey);

            List<IObject> removeList = new List<IObject>();
            foreach (IObject cur in worldObjects)
            {
                if (cur.State == ObjectState.TO_BE_DELETED)
                {
                    removeList.Add(cur);
                }
            }
            foreach (IObject cur in removeList)
            {
                if (cur.State == ObjectState.TO_BE_DELETED)
                {
                    worldObjects.Remove(cur);
                    cur.State = ObjectState.DELETED;
                }
            }


        }


        private void UpdateGermi(GameTime gameTime, KeyboardState presentKey)
        {
            if (Utils.isIllegalKeyCombination(presentKey))
            {
                return;
            }
            MainCharacter germi = MainCharacter.Instance;
            Vector2 germiLocation = new Vector2(germi.getLocation().X, germi.getLocation().Y);
            int speed = germi.getSpeed();
            MoveDirection prevDirection = Utils.getGermiDirection(presentKey);
            UpdateMovableObject(germi, gameTime, Keyboard.GetState(), prevDirection);
            Rectangle spaceRect = germi.getSpace(germiLocation);
            bool germiNewLocation = isAllowedLocation(spaceRect);
            Rectangle germiSpace = germi.getSpace(germiLocation);
            /*
            if (isGermisDeath(germiSpace))
            {
                germiDies();
            }
             */
        }

        private void UpdateAliens(GameTime gameTime, KeyboardState presentKey)
        {
            randomUpdatesCount = 0;
            foreach (IObject cur in this.worldObjects)
            {
                if (cur.isMovable())
                {
                    UpdateMovableObject(cur, gameTime, presentKey, cur.getDirection());
                }
                if (cur.isVisible() == false)
                    cur.setNewLocation(Vector2.Zero);
            }
        }


        private void UpdateMovableObject(IObject movableObject, GameTime gameTime, KeyboardState presentKey, MoveDirection prevDirection)
        {
            Vector2 newLocation = movableObject.getLocation();
            int speed = movableObject.getSpeed();


            if (movableObject.getObjectType() != ObjectType.MAIN_CHARACTER)
            {
                for (int i = 0; i < fireBalls.Count; i++)
                {
                    if (fireBalls[i].fbrectangle().Intersects(movableObject.getSpace()))
                    {
                        score += 1;
                        movableObject.setVisibility(false);
                        fireBalls.RemoveAt(i);
                    }
                }
            }


            bool movedVert = false, movedHoriz = false;
            switch (prevDirection)
            {
                case MoveDirection.N:
                case MoveDirection.NW:
                case MoveDirection.NE:
                    newLocation.Y -= speed;
                    movedVert = true;
                    break;
                case MoveDirection.S:
                case MoveDirection.SE:
                case MoveDirection.SW:
                    movedVert = true;
                    newLocation.Y += speed;
                    break;
                case MoveDirection.E:
                case MoveDirection.W:
                case MoveDirection.STOP:
                    break;
                default:
                    throw new NotImplementedException("Unsupported direction: " + prevDirection);

            }


            switch (prevDirection)
            {
                case MoveDirection.E:
                case MoveDirection.NE:
                case MoveDirection.SE:
                    movedHoriz = true;
                    newLocation.X += speed;
                    break;
                case MoveDirection.W:
                case MoveDirection.NW:
                case MoveDirection.SW:
                    movedHoriz = true;
                    newLocation.X -= speed;
                    break;
                case MoveDirection.N:
                case MoveDirection.S:
                case MoveDirection.STOP:
                    break;
                default:
                    throw new NotImplementedException("Unsupported direction: " + prevDirection);

            }



            Rectangle objectSpace = movableObject.getSpace(newLocation);
            bool allowedLocation = isAllowedLocation(objectSpace);

            if (allowedLocation)
            {
                movableObject.setNewLocation(newLocation);
                if (movedVert)
                {
                    movableObject.AnimateVerticalMovement(gameTime);
                }
                if (movedHoriz)
                {
                    movableObject.AnimateHorizontalMovement(gameTime);
                }
            }
            else if ((movableObject.getObjectType() == ObjectType.MAIN_CHARACTER) && (getWorld().isInWall(objectSpace) != MoveDirection.NONE))
            {
                // XXX
                MoveDirection wallDir = getWorld().isInWall(objectSpace);
                switch (wallDir)
                {
                    case MoveDirection.N:
                        if (prevDirection == MoveDirection.NE)
                        {
                            UpdateMovableObject(movableObject, gameTime, presentKey, MoveDirection.E);
                        }
                        else if (prevDirection == MoveDirection.NW)
                        {
                            UpdateMovableObject(movableObject, gameTime, presentKey, MoveDirection.W);
                        }
                        break;

                    case MoveDirection.S:
                        if (prevDirection == MoveDirection.SE)
                        {
                            UpdateMovableObject(movableObject, gameTime, presentKey, MoveDirection.E);
                        }
                        else if (prevDirection == MoveDirection.SW)
                        {
                            UpdateMovableObject(movableObject, gameTime, presentKey, MoveDirection.W);
                        }
                        break;

                    case MoveDirection.E:
                        if (prevDirection == MoveDirection.NE)
                        {
                            UpdateMovableObject(movableObject, gameTime, presentKey, MoveDirection.N);
                        }
                        else if (prevDirection == MoveDirection.SE)
                        {
                            UpdateMovableObject(movableObject, gameTime, presentKey, MoveDirection.S);
                        }

                        break;

                    case MoveDirection.W:
                        if (prevDirection == MoveDirection.NW)
                        {
                            UpdateMovableObject(movableObject, gameTime, presentKey, MoveDirection.N);
                        }
                        else if (prevDirection == MoveDirection.SW)
                        {
                            UpdateMovableObject(movableObject, gameTime, presentKey, MoveDirection.S);
                        }

                        break;
                    default:
                        throw new NotImplementedException("Unsupported direction for wall collision: " + prevDirection);

                }

            }

            if (movableObject.getObjectType() == ObjectType.ALIEN )
            {
                if (allowedLocation)
                {
                    if (randomUpdatesCount == 0 && Utils.isNewDirection())
                    {
                        randomUpdatesCount++;
                        movableObject.setDirection(Utils.getNewRandomDirection(prevDirection));
                    }
                }
                else
                {
                    movableObject.setDirection(Utils.getNewRandomDirection(prevDirection));
                }

            }
            if (movableObject.getObjectType() == ObjectType.MAIN_CHARACTER)
            {
                movableObject.setDirection(Utils.getGermiDirection(presentKey));
            }
        }
        /*
        public void UpdatePowerup(GameTime gameTime)
        {
            buff.PowerupAnimation(gameTime);
            if (buff.isPickedUp())
            {
                worldObjects.Remove(buff);
            }
        }
        */
        /*
        public void UpdateDoors()
        {
            foreach (IObject cur in this.worldObjects)
            {
                if (cur.getObjectType() == ObjectType.DOOR)
                {
                    Door curDoor = cur as Door;
                    curDoor.checkGermi();
                }
            }
        }
        */
        /* END */


    }
}
