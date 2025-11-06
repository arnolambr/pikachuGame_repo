using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using PikachuGame.Extensions;

namespace PikachuGame.States
{
    internal class PauseState(Game1 context, AbstractState originState) 
        : AbstractState(context)
    {
        private AbstractState OriginState { get; } = originState;

        public override void Draw(GameTime gameTime)
        {
            OriginState.Draw(gameTime);

            Context._spriteBatch.DrawStringInCenter(
                Context._graphics,
                Context._font,
                "Game op pauze. Druk enter om verder te spelen.");
        }

        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                Context.ChangeState(OriginState);
            //geen nieuw spel, nieuwe state of niet alleen context, maar ook alle waarden, dus originState teruggeven
        }
    }
}
