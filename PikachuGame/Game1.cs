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
        private enum GameStates
        {
            StartScreen, 
            Playing, 
            Paused, 
            GameOver
        }
        private const int PLAYER_STEP = 8;
        private const int BACKGROUND_STEP = 2;
        private const int SHARK_STEP = 4;

        internal GraphicsDeviceManager _graphics;
        internal SpriteBatch _spriteBatch;

        internal SpriteFont _font;
        internal Texture2D _player;
        internal Texture2D _shark;
        internal Texture2D _background;
        private GameStates _gameState;

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

        internal void SetState(AbstractState newState)
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
            _gameState = GameStates.StartScreen;
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

            // TODO: use this.Content to load your game content here

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

            if (_gameState == GameStates.StartScreen)
            {
                _elapsedTimeSinceLastSharkInMs = 0;
                _playerPosition = new Vector2(0, 300);
                _backgroundPosition = new Vector2(0, 0);
                _numberOfRemainLives = 3;
                _sharkPositions.Clear();



                if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                    _gameState = GameStates.Playing;
            }

            else if (_gameState == GameStates.Playing)
            {
                // TODO: Add your update logic here
                
                if (Keyboard.GetState().IsKeyDown(Keys.D) || Keyboard.GetState().IsKeyDown(Keys.Right))   //right
                    _playerPosition.X += PLAYER_STEP;

                if (Keyboard.GetState().IsKeyDown(Keys.Q) || Keyboard.GetState().IsKeyDown(Keys.Left))   //left
                    _playerPosition.X -= PLAYER_STEP;

                if (Keyboard.GetState().IsKeyDown(Keys.Z) || Keyboard.GetState().IsKeyDown(Keys.Up))   //up
                    _playerPosition.Y -= PLAYER_STEP;

                if (Keyboard.GetState().IsKeyDown(Keys.S) || Keyboard.GetState().IsKeyDown(Keys.Down))   //down
                    _playerPosition.Y += PLAYER_STEP;

                // Check if the user wants to Pause the game
                if (Keyboard.GetState().IsKeyDown(Keys.P))
                    _gameState = GameStates.Paused;

                _backgroundPosition.X -= BACKGROUND_STEP;

                // Shark Update
                // Keep tracker of how much time has passed since the last shark generation
                _elapsedTimeSinceLastSharkInMs += gameTime.ElapsedGameTime.TotalMilliseconds;
                if (_elapsedTimeSinceLastSharkInMs >= 3_000)
                {
                    //resetting the eleapsedTime
                    _elapsedTimeSinceLastSharkInMs = 0;
                    _sharkPositions.Add(new Vector2(_graphics.PreferredBackBufferWidth, 
                        Random.Shared.Next(_graphics.PreferredBackBufferHeight)));
                }

                // Check if we have collided with a shark
                var pikachuBounds = new Rectangle((int)_playerPosition.X, (int)_playerPosition.Y, _player.Width, _player.Height);
                for (var i = _sharkPositions.Count -1; i >= 0; i--)//telt van achter naar voor: waarom? anders ga je eentje skippen
                    //zo kan je alles in één lus uitvoeren, anders niet, nu laat je rest van de lijst aansluiten op het begin ervan
                {
                    _sharkPositions[i] = _sharkPositions[i] with { X = _sharkPositions[i].X - SHARK_STEP };//alleen X wordt geüpdated

                    // Remove useless sharks (those who have passed the left side)
                    if (_sharkPositions[i].X < -_shark.Width)
                    {
                        _sharkPositions.RemoveAt(i);
                    }

                    // Check if it intersects with the pikachu bounds, if so it means that the shark hit pikachu
                    else if (pikachuBounds.Intersects(new Rectangle((int)_sharkPositions[i].X, (int)_sharkPositions[i].Y, 
                        _shark.Width, _shark.Height)))
                    {
                        _numberOfRemainLives--;

                        // If the player has no lives left change state to gameover
                        if (_numberOfRemainLives == 0)
                        {
                            _gameState = GameStates.GameOver;
                            continue; // State changed, we don't need to do the rest of the loop
                        }
                        // Still have lives left, so just remove the shark
                        else
                            _sharkPositions.RemoveAt(i);
                    }
                }
            }

            else if(_gameState==GameStates.Paused)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                    _gameState = GameStates.Playing;
            }
            else if (_gameState==GameStates.GameOver)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                    _gameState = GameStates.StartScreen;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            if(_gameState==GameStates.StartScreen)
            {
                _spriteBatch.DrawStringInCenter(_graphics, _font, "Druk enter om te spelen");
            }
            else if(_gameState==GameStates.Playing)
            {
                //draw the background
                _spriteBatch.Draw(_background, _backgroundPosition);

                //draw the player
                _spriteBatch.Draw(_player, _playerPosition);  //bij wit legt hij geen kleur over je afbeelding

                //draw all sharks
                foreach(var sharkPosition in _sharkPositions)
                {
                    _spriteBatch.Draw(_shark, sharkPosition, Color.White);
                }

                // Draw the number of lives the player has left
                _spriteBatch.DrawStringInTopLeft(_graphics, _font, "Levens: " + _numberOfRemainLives, Color.DimGray);
            }
            else if(_gameState== GameStates.Paused)
            {
                _spriteBatch.DrawStringInCenter(_graphics, _font, "Pauze. Druk enter om verder te spelen.");
            }
            else if(_gameState==GameStates.GameOver)
            {
                _spriteBatch.DrawStringInCenter(_graphics, _font, "Game Over. Druk op enter om terug naar het startscherm te gaan.");
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
