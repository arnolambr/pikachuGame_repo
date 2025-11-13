using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using PikachuGame.Extensions;
using PikachuGame.Input;

namespace PikachuGame.States
{
    public class StartScreenState(Game1 context): AbstractState(context)
    {
        private bool _isInitialized = false;
        
        public override void Update(GameTime gameTime)
        {
            //hier verzekeren we dat we de variabelen maar één keer resetten 
            if(!_isInitialized)
            {
                ResetContext();
                _isInitialized = true;
            }

            if (InputFacade.WasKeyJustPressed(Keys.Enter))  
                Context.ChangeState(new PlayingState(Context));
            //als je de key te lang ingedrukt houdt, treed er misschien een fout op, doordat het volgende
            //scherm opstart, en die ook iets doet met dezelfde enter toets. Je kan dit vermijden door IsKeyUp
            //te gebruiken, dan wacht de state tot de toets terug naar boven komt, om verder te doen
        }

        private void ResetContext()
        {
            Context._elapsedTimeSinceLastSharkInMs = 0;
            Context._playerPosition = new Vector2(0, 300);
            Context._player = new Object.Sprite(Context._playerTexture, new System.Numerics.Vector2(0, 100));
            Context._backgroundPosition = new Vector2(0, 0);
            Context._backgroundPosition2 = new Vector2(4000, 0);
            Context._numberOfRemainLives = 3;
            Context._sharkPositions = new List<Vector2>();
        }

        public override void Draw(GameTime gameTime)
        {
            Context._spriteBatch.DrawStringInCenter(
            Context._graphics, 
            Context._font, 
            "Druk enter om te spelen");
        }
    }
}
