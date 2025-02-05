﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace Jokemon_Team_2
{
    class PhysicsManager
    {
        private float speed = 0.35f;
        private int collisionOffset = 3;
        private bool SignInitialize = false;
        public bool CheckInBounds(int PosX1, int PosY1, int PosX2, int PosY2, int distBounds)

        {
            bool inBounds;
            int distX;
            int distY;


            distX = Math.Abs(PosX1 - PosX2);
            distY = Math.Abs(PosY1 - PosY2);
            if (distX < distBounds && distY < distBounds)
            {
                inBounds = true;
            }
            else
            {
                inBounds = false;

            }

            return inBounds;
        }
        public void checkCollision(Player p, Rectangle r)
        {
            if (p.goingUp)
            {
                p.projectedPos = new Vector2(p.spritePosition.X, p.spritePosition.Y - collisionOffset); //check if we are about to collide
                Rectangle projectedPlayerRect = new Rectangle((int)p.projectedPos.X, (int)p.projectedPos.Y, (int)p.spriteSize.X, (int)p.spriteSize.Y);

                if (projectedPlayerRect.Intersects(r)) //check if projection has collided
                {
                    p.hasCollidedTop = true;
                }
                if (p.hasCollidedTop == false) //if we're not at the top, let the player go up
                {
                    goUp(p);
                    p.hasCollidedBottom = false; //if we've gone up, can't be colliding with bottom

                }
            }
            else if (p.goingDown)
            {

                p.projectedPos = new Vector2(p.spritePosition.X, p.spritePosition.Y + collisionOffset); //check if we are about to collide
                Rectangle projectedPlayerSprite = new Rectangle((int)p.projectedPos.X, (int)p.projectedPos.Y, (int)p.spriteSize.X, (int)p.spriteSize.Y);

                if (projectedPlayerSprite.Intersects(r)) //check if projection has collided
                {
                    p.hasCollidedBottom = true;
                }


                if (p.hasCollidedBottom == false)
                {
                    goDown(p);
                    p.hasCollidedTop = false;
                }


            }
            else if (p.goingLeft)
            {

                p.projectedPos = new Vector2(p.spritePosition.X - collisionOffset, p.spritePosition.Y); //check if we are about to collide
                Rectangle projectedPlayerSprite = new Rectangle((int)p.projectedPos.X, (int)p.projectedPos.Y, (int)p.spriteSize.X, (int)p.spriteSize.Y);

                if (projectedPlayerSprite.Intersects(r)) //check if projection has collided
                {
                    p.hasCollidedLeft = true;
                }


                if (p.hasCollidedLeft == false)
                {
                    goLeft(p);
                    p.hasCollidedRight = false;
                }

            }
            else if (p.goingRight)
            {

                p.projectedPos = new Vector2(p.spritePosition.X + collisionOffset, p.spritePosition.Y); //check if we are about to collide
                Rectangle projectedPlayerSprite = new Rectangle((int)p.projectedPos.X, (int)p.projectedPos.Y, (int)p.spriteSize.X, (int)p.spriteSize.Y);

                if (projectedPlayerSprite.Intersects(r)) //check if projection has collided
                {
                    p.hasCollidedRight = true;
                }


                if (p.hasCollidedRight == false)
                {
                    goRight(p);
                    p.hasCollidedLeft = false;
                }
            }

        }
        public void CheckCollision(Player p, Tree t)
        {
            Rectangle treeRect = new Rectangle((int)t.spritePosition.X, (int)t.spritePosition.Y, (int)t.spriteSize.X, (int)t.spriteSize.Y);

            if (p.goingUp)
            {

                p.projectedPos = new Vector2((int)p.spritePosition.X, (int)p.spritePosition.Y - collisionOffset);
                Rectangle projectedPlayerRect = new Rectangle((int)p.projectedPos.X, (int)p.projectedPos.Y, (int)p.spriteSize.X, (int)p.spriteSize.Y);

                if (projectedPlayerRect.Intersects(treeRect))
                {
                    p.hasCollidedTop = true;
                    p.spritePosition = new Vector2(p.spritePosition.X, p.spritePosition.Y + 1);
                }
                if (p.hasCollidedTop == false)
                {
                    goUp(p);
                    p.hasCollidedBottom = false;
                }
            }
            else if (p.goingDown)
            {
                p.projectedPos = new Vector2((int)p.spritePosition.X, (int)p.spritePosition.Y + collisionOffset);
                Rectangle projectedPlayerRect = new Rectangle((int)p.projectedPos.X, (int)p.projectedPos.Y, (int)p.spriteSize.X, (int)p.spriteSize.Y);

                if (projectedPlayerRect.Intersects(treeRect))
                {
                    p.hasCollidedBottom = true;
                    p.spritePosition = new Vector2(p.spritePosition.X, p.spritePosition.Y - 1);
                }
                if (p.hasCollidedBottom == false)
                {
                    goDown(p);
                    p.hasCollidedTop = false;
                    p.hasCollidedRight = false;
                    p.hasCollidedLeft = false;
                }
            }
            else if (p.goingLeft)
            {
                p.projectedPos = new Vector2((int)p.spritePosition.X - collisionOffset, (int)p.spritePosition.Y);
                Rectangle projectedPlayerRect = new Rectangle((int)p.projectedPos.X, (int)p.projectedPos.Y, (int)p.spriteSize.X, (int)p.spriteSize.Y);

                if (projectedPlayerRect.Intersects(treeRect))
                {
                    p.hasCollidedLeft = true;
                    p.goingUp = false;
                    p.goingDown = false;
                    p.spritePosition = new Vector2(p.spritePosition.X + 1, p.spritePosition.Y);
                }
                if (p.hasCollidedLeft == false)
                {
                    goLeft(p);
                    p.hasCollidedRight = false;
                    p.hasCollidedTop = false;
                    p.hasCollidedBottom = false;
                }
            }
            else if (p.goingRight)
            {
                p.projectedPos = new Vector2((int)p.spritePosition.X + collisionOffset, (int)p.spritePosition.Y);
                Rectangle projectedPlayerRect = new Rectangle((int)p.projectedPos.X, (int)p.projectedPos.Y, (int)p.spriteSize.X, (int)p.spriteSize.Y);

                if (projectedPlayerRect.Intersects(treeRect))
                {
                    p.hasCollidedRight = true;
                    p.spritePosition = new Vector2(p.spritePosition.X - 1, p.spritePosition.Y);
                }
                if (p.hasCollidedRight == false)
                {
                    goRight(p);
                    p.hasCollidedLeft = false;
                    p.hasCollidedTop = false;
                    p.hasCollidedBottom = false;
                }
            }


        } // Dont think this is needed

        public bool CheckSignCollision(Player p, ReadableObject r)
        {
            Rectangle readableObjectRect = new Rectangle((int)r.spritePosition.X, (int)r.spritePosition.Y, (int)r.spriteSize.X, (int)r.spriteSize.Y);

            if (p.goingUp)
            {

                p.projectedPos = new Vector2((int)p.spritePosition.X, (int)p.spritePosition.Y - collisionOffset);
                Rectangle projectedPlayerRect = new Rectangle((int)p.projectedPos.X, (int)p.projectedPos.Y, (int)p.spriteSize.X, (int)p.spriteSize.Y);

                if (projectedPlayerRect.Intersects(readableObjectRect))
                {
                    p.hasCollidedTop = true;
                    SignInitialize = true;


                }
                if (p.hasCollidedTop == false)

                {

                    SignInitialize = false;
                    goUp(p);
                    p.hasCollidedBottom = false;
                    p.hasCollidedRight = false;
                    p.hasCollidedLeft = false;
                }
            }
            else if (p.goingDown)
            {
                p.projectedPos = new Vector2((int)p.spritePosition.X, (int)p.spritePosition.Y + collisionOffset);
                Rectangle projectedPlayerRect = new Rectangle((int)p.projectedPos.X, (int)p.projectedPos.Y, (int)p.spriteSize.X, (int)p.spriteSize.Y);

                if (projectedPlayerRect.Intersects(readableObjectRect))
                {
                    p.hasCollidedBottom = true;
                }
                if (p.hasCollidedBottom == false)
                {

                    SignInitialize = false;
                    goDown(p);
                    p.hasCollidedTop = false;
                    p.hasCollidedRight = false;
                    p.hasCollidedLeft = false;
                }
            }
            else if (p.goingLeft)
            {
                p.projectedPos = new Vector2((int)p.spritePosition.X - collisionOffset, (int)p.spritePosition.Y);
                Rectangle projectedPlayerRect = new Rectangle((int)p.projectedPos.X, (int)p.projectedPos.Y, (int)p.spriteSize.X, (int)p.spriteSize.Y);

                if (projectedPlayerRect.Intersects(readableObjectRect))
                {
                    p.hasCollidedLeft = true;
                }
                if (p.hasCollidedLeft == false)
                {
                    goLeft(p);
                    SignInitialize = false;
                    p.hasCollidedRight = false;
                    p.hasCollidedTop = false;
                    p.hasCollidedBottom = false;
                }
            }
            else if (p.goingRight)
            {
                p.projectedPos = new Vector2((int)p.spritePosition.X + collisionOffset, (int)p.spritePosition.Y);
                Rectangle projectedPlayerRect = new Rectangle((int)p.projectedPos.X, (int)p.projectedPos.Y, (int)p.spriteSize.X, (int)p.spriteSize.Y);

                if (projectedPlayerRect.Intersects(readableObjectRect))
                {
                    p.hasCollidedRight = true;
                }
                if (p.hasCollidedRight == false)
                {
                    goRight(p);

                    SignInitialize = false;
                    p.hasCollidedLeft = false;
                    p.hasCollidedTop = false;
                    p.hasCollidedBottom = false;
                }
            }
            return SignInitialize;

        } // Dont think this is needed
        public void CheckCollision(Player p, Building b)
        {
            Rectangle buildingRect = new Rectangle((int)b.spritePosition.X, (int)b.spritePosition.Y, (int)b.spriteSize.X, (int)b.spriteSize.Y);

            if (p.goingUp)
            {

                p.projectedPos = new Vector2((int)p.spritePosition.X, (int)p.spritePosition.Y - collisionOffset);
                Rectangle projectedPlayerRect = new Rectangle((int)p.projectedPos.X, (int)p.projectedPos.Y, (int)p.spriteSize.X, (int)p.spriteSize.Y);

                if (projectedPlayerRect.Intersects(buildingRect))
                {
                    p.hasCollidedTop = true;
                }
                if (p.hasCollidedTop == false)
                {
                    goUp(p);
                    p.hasCollidedBottom = false;
                    p.hasCollidedRight = false;
                    p.hasCollidedLeft = false;
                }
            }
            else if (p.goingDown)
            {
                p.projectedPos = new Vector2((int)p.spritePosition.X, (int)p.spritePosition.Y + collisionOffset);
                Rectangle projectedPlayerRect = new Rectangle((int)p.projectedPos.X, (int)p.projectedPos.Y, (int)p.spriteSize.X, (int)p.spriteSize.Y);

                if (projectedPlayerRect.Intersects(buildingRect))
                {
                    p.hasCollidedBottom = true;
                }
                if (p.hasCollidedBottom == false)
                {
                    goDown(p);
                    p.hasCollidedTop = false;
                    p.hasCollidedRight = false;
                    p.hasCollidedLeft = false;
                }
            }
            else if (p.goingLeft)
            {
                p.projectedPos = new Vector2((int)p.spritePosition.X - collisionOffset, (int)p.spritePosition.Y);
                Rectangle projectedPlayerRect = new Rectangle((int)p.projectedPos.X, (int)p.projectedPos.Y, (int)p.spriteSize.X, (int)p.spriteSize.Y);

                if (projectedPlayerRect.Intersects(buildingRect))
                {
                    p.hasCollidedLeft = true;
                }
                if (p.hasCollidedLeft == false)
                {
                    goLeft(p);
                    p.hasCollidedRight = false;
                    p.hasCollidedTop = false;
                    p.hasCollidedBottom = false;
                }
            }
            else if (p.goingRight)
            {
                p.projectedPos = new Vector2((int)p.spritePosition.X + collisionOffset, (int)p.spritePosition.Y);
                Rectangle projectedPlayerRect = new Rectangle((int)p.projectedPos.X, (int)p.projectedPos.Y, (int)p.spriteSize.X, (int)p.spriteSize.Y);

                if (projectedPlayerRect.Intersects(buildingRect))
                {
                    p.hasCollidedRight = true;
                }
                if (p.hasCollidedRight == false)
                {
                    goRight(p);
                    p.hasCollidedLeft = false;
                    p.hasCollidedTop = false;
                    p.hasCollidedBottom = false;
                }
            }
        } // Dont think this is needed
        
        public void GrassCollision(Rectangle p,Rectangle g)
        {
            if(p.Intersects(g))
            {

            }
        }

        public void goLeft(Player playerSprite)
        {
            playerSprite.spritePosition = new Vector2(playerSprite.spritePosition.X - speed, playerSprite.spritePosition.Y);
        }
        public void goRight(Player playerSprite)
        {
            playerSprite.spritePosition = new Vector2(playerSprite.spritePosition.X + speed, playerSprite.spritePosition.Y);
        }
        public void goUp(Player playerSprite)
        {
            playerSprite.spritePosition = new Vector2(playerSprite.spritePosition.X, playerSprite.spritePosition.Y - speed);
        }
        public void goDown(Player playerSprite)
        {
            playerSprite.spritePosition = new Vector2(playerSprite.spritePosition.X, playerSprite.spritePosition.Y + speed);
        }
    }
}