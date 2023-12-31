﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Monogame_time_and_sound
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        Texture2D bombTexture;
        Texture2D explosionTexture;
        Rectangle bombRect;
        SpriteFont timeFont;
        float seconds;
        float startTime;
        MouseState mouseState;
        SoundEffect explode;
        bool exploded;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 500;
            graphics.ApplyChanges();
            exploded = false;
        }

        protected override void Initialize()
        {
            bombRect = new Rectangle(50, 50, 700, 400);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            bombTexture = Content.Load<Texture2D>("bomb");
            timeFont = Content.Load<SpriteFont>("BombTimer");
            explode = Content.Load<SoundEffect>("explosion");
            explosionTexture = Content.Load<Texture2D>("Nuke");

        }

        protected override void Update(GameTime gameTime)
        {
            mouseState = Mouse.GetState();

            if (mouseState.LeftButton == ButtonState.Pressed && !exploded)
            {
                startTime = (float)gameTime.TotalGameTime.TotalSeconds;
            }

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            seconds = (float)gameTime.TotalGameTime.TotalSeconds - startTime;

            if (Math.Round(seconds) == 15 && !exploded) 
            {
                explode.Play();
                exploded = true;
            }
            else if (Math.Round(seconds, 3) == 12 || Math.Round(seconds, 3) == 13 || Math.Round(seconds, 3) == 14)
            {
                Console.Beep();
            }

            if (exploded && Math.Round(seconds) == 25)
            {
                Exit();
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);
            spriteBatch.Begin();
            spriteBatch.Draw(bombTexture, bombRect, Color.White);

            if (seconds < 15)
            {
                spriteBatch.DrawString(timeFont, (15 - seconds).ToString("00.0"), new Vector2(270, 200), Color.Black);
            }
            else if (seconds >= 15)
            {
                spriteBatch.Draw(explosionTexture, new Rectangle(0, 0, 800, 500), Color.White);
            }

            spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}