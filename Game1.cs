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
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public static int time;

        Sprite background;
        Sprite arena;
        Sprite bar;
        Sprite bar2;
        Sprite name_fulgore;
        Sprite name_fulgore2;
        Sprite player_one_win;
        Sprite player_two_win;
        Sprite Start_Screen;
        public static Sprite health;
        public static Sprite health2;


        public static Sprite platform;
        public static Sprite timer;
        public static UserControlled fulgore;
        public static UserControlled fulgore2;

        public static Song bg_music;
        public static Song title;
        //public static Song victory;
        public static SoundEffect punch;
        public static SoundEffect kick;
        public static SoundEffect hurt;
        public static SoundEffect hit;
        public static SoundEffect readyFight;
        public static SoundEffect victory;
        KeyboardState old_ks = Keyboard.GetState();
        bool draw = false;
        bool match = false;

         
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            background = new Sprite(Content.Load<Texture2D>("Images/backgroundbig"),
                                new Vector2(Window.ClientBounds.Width / 2 - 744, 0),
                                new Point(1280, 600),
                                10,
                                new Point(0, 0),
                                new Point(1, 1),
                                new Vector2(0, 0),
                                120,
                                new Point(0,0));
            fulgore = new UserControlled(Content.Load<Texture2D>("Images/ken"),
                                new Vector2(200, 450),
                                new Point(159 , 180),
                                10,
                                new Point(0, 1),
                                new Point(7 ,9),
                                new Vector2(0, 0), 
                                200,
                                new Point(40,140));
            fulgore2 = new UserControlled(Content.Load<Texture2D>("Images/ken"),
                               new Vector2(500, 450),
                               new Point(159, 180),
                               10,
                               new Point(0, 1),
                               new Point(7, 9),
                               new Vector2(0, 0),
                               200,
                               new Point(40, 140));
            arena = new Sprite(Content.Load<Texture2D>("Images/arenabig"),
                                new Vector2(Window.ClientBounds.Width/2 - 890 , Window.ClientBounds.Height - 200),
                                new Point(1780, 200),
                                10,
                                new Point(0, 0),
                                new Point(1, 1),
                                new Vector2(0, 0), 
                                120,
                                new Point(0,0));
            platform = new Sprite(Content.Load<Texture2D>("Images/arenabigplatform"),
                                new Vector2(Window.ClientBounds.Width / 2 - 890, 525),//Window.ClientBounds.Height - 200
                                new Point(1780, 200),
                                10,
                                new Point(0, 0),
                                new Point(1, 1),
                                new Vector2(0, 0),
                                120,
                                new Point(1780,200));
            health = new Sprite(Content.Load<Texture2D>("Images/health2"),
                                new Vector2(5, 10),//Window.ClientBounds.Height - 200
                                new Point(354, 33),
                                10,
                                new Point(0, 0),
                                new Point(1, 1),
                                new Vector2(0, 0),
                                120,
                                new Point(0, 0));
            health2 = new Sprite(Content.Load<Texture2D>("Images/health2"),
                                new Vector2(438, 10),//Window.ClientBounds.Height - 200
                                new Point(354, 33),
                                10,
                                new Point(0, 0),
                                new Point(1, 1),
                                new Vector2(0, 0),
                                120,
                                new Point(0, 0));
            bar = new Sprite(Content.Load<Texture2D>("Images/bar2"),
                                new Vector2(5, 10),//Window.ClientBounds.Height - 200
                                new Point(356, 35),
                                10,
                                new Point(0, 0),
                                new Point(1, 1),
                                new Vector2(0, 0),
                                120,
                                new Point(0, 0));
            bar2 = new Sprite(Content.Load<Texture2D>("Images/bar2"),
                                new Vector2(438, 10),//Window.ClientBounds.Height - 200
                                new Point(356, 35),
                                10,
                                new Point(0, 0),
                                new Point(1, 1),
                                new Vector2(0, 0),
                                120,
                                new Point(0, 0));
            name_fulgore = new Sprite(Content.Load<Texture2D>("Images/nameFulgore"),
                                new Vector2(-5, 40),//Window.ClientBounds.Height - 200
                                new Point(101, 38),
                                10,
                                new Point(0, 0),
                                new Point(1, 1),
                                new Vector2(0, 0),
                                120,
                                new Point(0, 0));
            name_fulgore2 = new Sprite(Content.Load<Texture2D>("Images/nameFulgore"),
                                new Vector2(700, 40),//Window.ClientBounds.Height - 200
                                new Point(101, 38),
                                10,
                                new Point(0, 0),
                                new Point(1, 1),
                                new Vector2(0, 0),
                                120,
                                new Point(0, 0));
            player_one_win = new Sprite(Content.Load<Texture2D>("Images/OneWin3"),
                                new Vector2(0,0),//Window.ClientBounds.Height - 200
                                new Point(800, 600),
                                10,
                                new Point(0, 0),
                                new Point(1, 1),
                                new Vector2(0, 0),
                                120,
                                new Point(0, 0));
            player_two_win = new Sprite(Content.Load<Texture2D>("Images/TwoWin2"),
                                new Vector2(0, 0),//Window.ClientBounds.Height - 200
                                new Point(800, 600),
                                10,
                                new Point(0, 0),
                                new Point(1, 1),
                                new Vector2(0, 0),
                                120,
                                new Point(0, 0));
             Start_Screen  = new Sprite(Content.Load<Texture2D>("Images/StartScreen"),
                                new Vector2(0, 0),//Window.ClientBounds.Height - 200
                                new Point(1101, 600),
                                10,
                                new Point(0, 0),
                                new Point(1, 1),
                                new Vector2(0, 0),
                                120,
                                new Point(0, 0));
             timer = new Sprite(Content.Load<Texture2D>("Images/Timer"),
                                 new Vector2(366, 10),//Window.ClientBounds.Height - 200
                                 new Point(69, 35),
                                 10,
                                 new Point(0, 0),
                                 new Point(1, 1),
                                 new Vector2(0, 0),
                                 120,
                                 new Point(0, 0));


            fulgore2.punch = Keys.I;
            fulgore2.kick = Keys.K;
            fulgore2.WalkL = Keys.J;
            fulgore2.WalkR = Keys.L;
            fulgore2.jumping = Keys.O;

            ////sound////
            
            
            bar2.effects = SpriteEffects.FlipHorizontally;
            health2.effects = SpriteEffects.FlipHorizontally;
            punch = Content.Load<SoundEffect>("Sound/punch");
            kick = Content.Load<SoundEffect>("Sound/kick");
            hurt = Content.Load<SoundEffect>("Sound/hurt");
            hit = Content.Load<SoundEffect>("Sound/hit");
            readyFight = Content.Load<SoundEffect>("Sound/readyFight");
            //victory = Content.Load<Song>("Sound/victory2");
            victory = Content.Load<SoundEffect>("Sound/victory");
            bg_music = Content.Load<Song>("Sound/kiTheme");
            title = Content.Load<Song>("Sound/title"); 

            //MediaPlayer.Play(bg_music);
            MediaPlayer.Play(title);

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here
            KeyboardState ks = Keyboard.GetState();

            if (ks.IsKeyDown(Keys.Space) && old_ks.IsKeyUp(Keys.Space))
            {
                MediaPlayer.Play(bg_music);
                readyFight.Play();
                draw = true;
            }

            if (draw)
            {
                
                if (health2.frameSize.X <= 0)
                {
                    player_one_win.Update(gameTime, Window.ClientBounds);
                }
                else if (health.frameSize.X <= 0)
                {
                    player_two_win.Update(gameTime, Window.ClientBounds);
                }
                else
                {
                    fulgore.Update(gameTime, Window.ClientBounds);
                    fulgore2.Update(gameTime, Window.ClientBounds);
                    background.Update(gameTime, Window.ClientBounds);
                    platform.Update(gameTime, Window.ClientBounds);
                    bar.Update(gameTime, Window.ClientBounds);
                    bar2.Update(gameTime, Window.ClientBounds);
                    health.Update(gameTime, Window.ClientBounds);
                    health2.Update(gameTime, Window.ClientBounds);
                    name_fulgore.Update(gameTime, Window.ClientBounds);
                    name_fulgore2.Update(gameTime, Window.ClientBounds);
                }
            }
            else
            {
                    Start_Screen.Update(gameTime, Window.ClientBounds);
            }

            if (match)
            {
                time  += gameTime.ElapsedGameTime.Milliseconds; 
                MediaPlayer.Stop();
                if (time >= 1000 && time <= 1030)
                {
                    victory.Play();
                }
                
                return;
                //victory.Play();
            }
            
                  
            base.Update(gameTime);

            old_ks = ks;
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            
            spriteBatch.Begin();
            if (draw)
            {
                if (health2.frameSize.X <= 0)
                {
                    //MediaPlayer.Play(colin);
                    match = true;
                    player_one_win.Draw(gameTime, spriteBatch);

                    

                }
                else if (health.frameSize.X <= 0)
                {
                    //MediaPlayer.Play(colin);
                    match = true;
                    player_two_win.Draw(gameTime, spriteBatch);
                    

                }
                else
                {
                    background.Draw(gameTime, spriteBatch);
                    platform.Draw(gameTime, spriteBatch);
                    arena.Draw(gameTime, spriteBatch);
                    fulgore.Draw(gameTime, spriteBatch);
                    fulgore2.Draw(gameTime, spriteBatch);
                    health.Draw(gameTime, spriteBatch);
                    health2.Draw(gameTime, spriteBatch);
                    bar.Draw(gameTime, spriteBatch);
                    bar2.Draw(gameTime, spriteBatch);
                    name_fulgore.Draw(gameTime, spriteBatch);
                    name_fulgore2.Draw(gameTime, spriteBatch);
                    timer.Draw(gameTime, spriteBatch);
                }
            }
            else
            {
                Start_Screen.Draw(gameTime, spriteBatch);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        } 
    }
}
