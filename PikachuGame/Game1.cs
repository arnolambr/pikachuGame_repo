using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PikachuGame.Extensions;
using PikachuGame.Input;
using PikachuGame.States;

namespace PikachuGame
{
    public class Game1 : Game
    {
        internal const int PLAYER_STEP = 8;
        internal const int BACKGROUND_STEP = 2;
        internal const int SHARK_STEP = 4;

        internal GraphicsDeviceManager _graphics;
        internal SpriteBatch _spriteBatch;

        internal SpriteFont _font;
        internal Texture2D _player;
        internal Texture2D _shark;
        internal Texture2D _background;
        internal Texture2D _background2;

        internal int _numberOfRemainLives;

        internal Vector2 _playerPosition;
        internal Vector2 _backgroundPosition;
        internal Vector2 _backgroundPosition2;

        internal List<Vector2> _sharkPositions;

        internal double _elapsedTimeSinceLastSharkInMs;
        //je zou hier ook mod operator kunnen gebruiken, of timespan, whatever

        private AbstractState _currentState;

        #region Constructor

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        #endregion

        internal void ChangeState(AbstractState newState)
        {
            _currentState = newState;
        }

        protected override void Initialize()
        {
            // bepaal de grootte van het window, anders op default 720x540

            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 720;
            _graphics.ApplyChanges();//niet vergeten toe te passen

            //deze state is de enige die Game1 hoeft te kennen: het begin van alles
            ChangeState(new StartScreenState(this));
           
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _background = Content.Load<Texture2D>("tenerife");
            _background2 = Content.Load<Texture2D>("tenerife flipped");
            _player = Content.Load<Texture2D>("surfing-pikachu");
            _shark = Content.Load<Texture2D>("haai");

            _font = Content.Load<SpriteFont>("game-font");
        }

        protected override void Update(GameTime gameTime)
        {
            InputFacade.Update();
            
            // No matter which state, we always check if the user wants to exit the game
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || 
                Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            //Game1 weet alleen maar af van 1 state: de currentstate, het is die state waarop Game1 kan updaten
            //van al de rest weet hij niets af, hij geeft er ook niks om of die state null is of niet..
            _currentState?.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            _currentState?.Draw(gameTime);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
