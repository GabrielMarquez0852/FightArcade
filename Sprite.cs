using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Frag_SauceV1
{
    public class Sprite
    {
        protected Texture2D textureImage;
        protected Vector2 position;
        public Point frameSize;
        protected int collisionOffset;
        protected Point currentFrame;
        protected Point sheetSize;
        protected int timeSinceLastFrame = 0;
        int millisecondsPerFrame;
        public static int numberOfFrames;
        protected Vector2 speed;
        protected Color tint = Color.White;
        const int defaultMillisecondsPerFrame = 16;
        public SpriteEffects effects = SpriteEffects.None;
        protected Point playerBox;
             
        public Sprite(Texture2D textureImage, Vector2 position, Point frameSize,
        int collisionOffset, Point currentFrame, Point sheetSize, Vector2 speed,
        int millisecondsPerFrame, Point playerBox)
        {
            this.textureImage = textureImage;
            this.position = position;
            this.frameSize = frameSize;
            this.collisionOffset = collisionOffset;
            this.currentFrame = currentFrame;
            this.sheetSize = sheetSize;
            this.speed = speed;
            this.millisecondsPerFrame = millisecondsPerFrame;
            this.playerBox = playerBox;
        }
      
        public virtual void Update(GameTime gameTime, Rectangle clientBounds)
        {
            //Update animation frame
            //Update animation frame
            if (Game1.fulgore.position.X < Game1.fulgore2.position.X)
            {
                Game1.fulgore.effects = SpriteEffects.None;
                Game1.fulgore2.effects = SpriteEffects.FlipHorizontally;

            }
            else
            {
                Game1.fulgore.effects = SpriteEffects.FlipHorizontally;
                Game1.fulgore2.effects = SpriteEffects.None;
            }
        }
        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //Draw the sprite
            spriteBatch.Draw(textureImage,
                position,
                new Rectangle(currentFrame.X * frameSize.X,
                    currentFrame.Y * frameSize.Y,
                    frameSize.X, frameSize.Y),
                    tint, 0, Vector2.Zero,
                    1f, effects, 0);
        }

        public Rectangle getBB()
        {
            return new Rectangle((int)position.X,
                                (int)position.Y,
                                playerBox.X, //40
                                playerBox.Y);//170
        }

        public Rectangle PunchingBB()
        {
            return new Rectangle ((int)position.X + 50,
                                   (int)position.Y,
                                    playerBox.X + 10,
                                    playerBox.Y);
        }

        public Rectangle KickingBB()
        {
            return new Rectangle((int)position.X + 50,
                                   (int)position.Y,
                                    playerBox.X + 10,
                                    playerBox.Y);
        }

        public Rectangle LeftPunchingBB()
        {
            return new Rectangle((int)position.X + 36,
                                   (int)position.Y,
                                    playerBox.X + 20,
                                    playerBox.Y);
        }

        public Rectangle LeftKickingBB()
        {
            return new Rectangle((int)position.X + 36,
                                   (int)position.Y,
                                    playerBox.X + 20,
                                    playerBox.Y);
        }

        public Rectangle PlayerOneLeftPuching()
        {
            return new Rectangle((int)position.X + 36,
                                   (int)position.Y,
                                    playerBox.X + 20,
                                    playerBox.Y);
        }
        public Rectangle PlayerOneLeftKick()
        {
            return new Rectangle((int)position.X + 36,
                                   (int)position.Y,
                                    playerBox.X + 20,
                                    playerBox.Y);
        }

        public Rectangle BorderBB()
        {
            return new Rectangle((int)position.X,
                                  (int)position.Y,
                                    frameSize.X,
                                    frameSize.Y);
        }
    }
}