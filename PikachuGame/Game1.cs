using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PikachuGame.Extensions;
using PikachuGame.States;

namespace PikachuGame
{
    public class Game1 : Game
    {
        /*private enum GameStates wordt niet meer gebruikt nu we gamemachine gebruiken
        {
            StartScreen, 
            Playing, 
            Paused, 
            GameOver
        }*/

        internal const int PLAYER_STEP = 8;
        internal const int BACKGROUND_STEP = 2;
        internal const int SHARK_STEP = 4;

        internal GraphicsDeviceManager _graphics;
        internal SpriteBatch _spriteBatch;

        internal SpriteFont _font;
        internal Texture2D _player;
        internal Texture2D _shark;
        internal Texture2D _background;

        private bool _isRunning = false;
        /*private bool _isStartScreen = true;
        private bool _isPaused = false;
        private bool _isGameOver = false;*/

        internal int _numberOfRemainLives;

        internal Vector2 _playerPosition;
        internal Vector2 _backgroundPosition;

        internal List<Vector2> _sharkPositions;

        internal double _elapsedTimeSinceLastSharkInMs;//je zou hier ook mod operator kunnen gebruiken, of timespan, whatever

        private AbstractState _currentState;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        internal void ChangeState(AbstractState newState)
        {
            _currentState = newState;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 720;
            _graphics.ApplyChanges();

            //reset alle variabelen
            ChangeState(new StartPlayingState(this));
            _elapsedTimeSinceLastSharkInMs = 0;
            _backgroundPosition = new Vector2(0, -1400);
            _playerPosition = new Vector2(0, 100);
            _numberOfRemainLives = 3;
            _sharkPositions = new List<Vector2>();
           
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _background = Content.Load<Texture2D>("tenerife"); 
            _player = Content.Load<Texture2D>("surfing-pikachu");
            _shark = Content.Load<Texture2D>("haai");

            _font = Content.Load<SpriteFont>("game-font");
        }

        protected override void Update(GameTime gameTime)
        {
            // No matter which state, we always check if the user wants to exit the game
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || 
                Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _currentState?.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
