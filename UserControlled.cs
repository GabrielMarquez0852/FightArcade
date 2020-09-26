using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;


namespace Frag_SauceV1
{
    public class UserControlled : Sprite
    {
        protected int millisecondsPerFrame = 90;
        KeyboardState old_ks = Keyboard.GetState();
        enum state
        {
            normal, walking, jumping, punching, kicking, damage
        }
        public Keys punch = Keys.W;
        public Keys kick = Keys.S;
        public Keys WalkL = Keys.A;
        public Keys WalkR = Keys.D;
        public Keys jumping = Keys.E;
        state mood = state.normal;
     
        public UserControlled (Texture2D textureImage, Vector2 position, Point frameSize,
                                    int collisionOffset, Point currentFrame, Point sheetSize, Vector2 speed,
                                    int millisecondsPerFrame, Point playerBox) :
                base(textureImage, position, frameSize, 
                     collisionOffset, currentFrame, sheetSize, speed, 
                     millisecondsPerFrame, playerBox)
      
        {
            
        }

        public override void Update(GameTime gameTime, Rectangle clientBounds)
        {
            base.Update(gameTime, clientBounds);
            Vector2 pmove = Vector2.Zero;
            KeyboardState ks = Keyboard.GetState();
            
            ////////////////////////////////////////////////
            ////////////////////moment & atacks/////////////
            ////////////////////////////////////////////////
            //Walking left
            if (ks.IsKeyDown(WalkL))
            {
                pmove.X -= 4;
                mood = state.walking;
                timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
                if (timeSinceLastFrame > millisecondsPerFrame)
                {
                    
                    timeSinceLastFrame = 0;
                    currentFrame.X--;
                    currentFrame.Y = 3;

                    if (currentFrame.X <= 0)
                    {
                        currentFrame.X = 4;
                        mood = state.normal;
                    }

                }
            }
            
            //Walking right
            else if (ks.IsKeyDown(WalkR))
            {
                //position.X += speed.X;
                pmove.X += 4;
                mood = state.walking;
                timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
                if (timeSinceLastFrame > millisecondsPerFrame)
                {
                    timeSinceLastFrame = 0;
                    currentFrame.X++;
                    currentFrame.Y = 3;

                    if (currentFrame.X >= 5)
                    {
                        currentFrame.X = 0;
                        mood = state.normal;
                    }

                }
            }
            Console.WriteLine("XY: " + currentFrame);
            //Punching mood & call animaition 
            if (ks.IsKeyDown(punch) && old_ks.IsKeyUp(punch))
            {
                Game1.punch.Play();
                mood = state.punching;
                currentFrame.X = 1;
                currentFrame.Y = 2;
                timeSinceLastFrame = 0;
                
            }
            //Punching mood & call animaition         
            else if (ks.IsKeyDown(kick) && old_ks.IsKeyUp(kick))
            {
                Game1.kick.Play();
                mood = state.kicking;
                currentFrame.X = 2;
                currentFrame.Y = 6;
                timeSinceLastFrame = 0;

                
            }

            //Default stance
            else if (mood == state.normal)
            {

                timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
                if (timeSinceLastFrame > millisecondsPerFrame)
                {
                    timeSinceLastFrame = 0;
                    ++currentFrame.X;
                    currentFrame.Y = 1;
                    if (currentFrame.X >= 4)
                    {
                        while (currentFrame.X != 0)
                        {
                            currentFrame.X--;
                        }
                    }
                }
                if (ks.IsKeyDown(jumping)
                        && old_ks.IsKeyUp(jumping))
                {
                    speed.Y -= 17;
                    mood = state.jumping;
                    currentFrame.X = 3;
                    currentFrame.Y = 8;

                }
            }


            /*
            if (Game1.fulgore.mood == state.walking && (ks.IsKeyDown(jumping)
                        && old_ks.IsKeyUp(jumping)))
            {
                    speed.Y -= 17;
                    mood = state.jumping;
                    currentFrame.X = 3;
                    currentFrame.Y = 8;
                    mood = state.normal;
            }
            */
            
            
            speed.Y += 0.5f;
            pmove += speed;

            //////////////////////////////////////////////
            ///////////////////colission//////////////////
            //////////////////////////////////////////////

            Rectangle x_box = getBB();
            x_box.X += (int)pmove.X;

            Rectangle y_box = getBB();
            y_box.Y += (int)pmove.Y;

            position += pmove;

            Rectangle X_P_hitbox = PunchingBB();
            Rectangle X_K_hitbox = KickingBB();
            Rectangle bb2 = Game1.fulgore2.getBB();
            Rectangle bb1 = Game1.fulgore.getBB();
            Rectangle Left_X_P_hitbox = Game1.fulgore.LeftPunchingBB();
            Rectangle Left_X_K_hitbox = Game1.fulgore.LeftKickingBB();
            Rectangle Left_X_P_hitbox2 = Game1.fulgore2.LeftPunchingBB();
            Rectangle Left_X_K_hitbox2 = Game1.fulgore2.LeftKickingBB();

            Rectangle ground = Game1.platform.getBB();
            if (ground.Intersects(y_box))
            {
                if (pmove.Y > 0)
                {
                    position.Y = ground.Y - y_box.Height;
                    speed.Y = 0;
                    mood = state.normal;
                }
                
            }
            ////////////////////////////////////// Left Intersections /////////////////////////////////////////////
            //PLAYER TWO
            if (bb2.Intersects(Left_X_P_hitbox) && Game1.fulgore2.mood == state.punching)
            {
                Game1.hit.Play();
                Game1.hurt.Play();
                Game1.health.frameSize.X -= 10;
                Game1.hit.Play();
                Game1.fulgore.currentFrame.X = 4;
                Game1.fulgore.currentFrame.Y = 7;
                mood = state.normal;
            }
            if (bb2.Intersects(Left_X_K_hitbox) && Game1.fulgore2.mood == state.kicking)
            {
                Game1.hit.Play();
                Game1.hurt.Play();
                Game1.health.frameSize.X -= 15;
                Game1.hit.Play();
                Game1.fulgore.currentFrame.X = 4;
                Game1.fulgore.currentFrame.Y = 7;
                mood = state.normal;
            }
            //PLAYER ONE
            if (bb1.Intersects(Left_X_P_hitbox2) && Game1.fulgore.mood == state.punching)
            {
                Game1.hit.Play();
                Game1.hurt.Play();
                Game1.health2.frameSize.X -= 10;
                Game1.hit.Play();
                Game1.fulgore2.currentFrame.X = 4;
                Game1.fulgore2.currentFrame.Y = 7;
                mood = state.normal;
            }
            
            if (bb1.Intersects(Left_X_K_hitbox2) && Game1.fulgore.mood == state.kicking)
            {
                Game1.hit.Play();
                Game1.hurt.Play();
                Game1.health2.frameSize.X -= 15;
                Game1.hit.Play();
                Game1.fulgore2.currentFrame.X = 4;
                Game1.fulgore2.currentFrame.Y = 7;
                mood = state.normal;
            }
            ///////////////////////////////////////Right Interestions////////////////////////////////////////////////////

            //Punching hit box
            if (bb2.Intersects(X_P_hitbox) && mood == state.punching)
            {
                Game1.hit.Play();
                Game1.hurt.Play();
                Game1.health2.frameSize.X -= 10;
                Game1.hit.Play();
                Game1.fulgore2.currentFrame.X = 4;
                Game1.fulgore2.currentFrame.Y = 7;
                mood = state.normal;

            }
            //Kicking hit box
            //kick hit detection
            if (bb2.Intersects(X_K_hitbox) && mood == state.kicking)
            {
                Game1.hit.Play();
                Game1.hurt.Play();
                Game1.health2.frameSize.X -= 15;
                Game1.hit.Play();
                Game1.fulgore2.currentFrame.X = 4;
                Game1.fulgore2.currentFrame.Y = 7;
                mood = state.normal;
            }
             if (bb1.Intersects(X_P_hitbox) && mood == state.punching)
            {
               
                Game1.hit.Play();
                Game1.hurt.Play();
                Game1.health.frameSize.X -= 10;
                Game1.hit.Play();
                Game1.fulgore.currentFrame.X = 4;
                Game1.fulgore.currentFrame.Y = 7;
                mood = state.normal;

            }
            //Kicking hit box
            //kick hit detection
            if (bb1.Intersects(X_K_hitbox) && mood == state.kicking)
            {
                Game1.hit.Play();
                Game1.hurt.Play();
                Game1.health.frameSize.X -= 15;
                Game1.hit.Play();
                Game1.fulgore.currentFrame.X = 4;
                Game1.fulgore.currentFrame.Y = 7;
                mood = state.normal;
            }

            //character collision/////////////////////////////////////////////////////////////////////////////////////////////////
            if (bb2.Intersects(bb1) && Game1.fulgore.mood == state.walking)
            {
                if (pmove.X > 0)
                {
                    position.X = bb2.X - bb1.Width;
                }

                if (pmove.X < 0)
                {
                    position.X = bb2.X + bb1.Width;
                }
            }
            
            if (bb2.Intersects(bb1) && Game1.fulgore2.mood == state.walking)
            {
                if (pmove.X < 0)
                {
                    position.X = bb1.X + bb2.Width;
                }
                if (pmove.X > 0)
                {
                    position.X = bb1.X - bb2.Width;
                }
            }
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            //boarder collision
            Rectangle bb = getBB();
            if (bb.Left < -40)
            {
                position.X = -40;
            }
            else if (bb.Right >= 720)
            {
                position.X = 680;
            }
            ///////////////////////////////////////////////////
            ///////////////////////////////////////////////////////
            //////////////////////////////////////////////////////////


            base.Update(gameTime, clientBounds);

            old_ks = ks;
            
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
        }
    }
}