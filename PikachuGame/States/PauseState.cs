using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using PikachuGame.Extensions;

namespace PikachuGame.States
{
    internal class PauseState(Game1 context, AbstractState originState) 
        : AbstractState(context)
    {
        private AbstractState OriginState { get; } = originState;

        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                Context.ChangeState(OriginState);
            //geen nieuw spel, nieuwe state of niet alleen context, maar ook alle waarden, dus originState teruggeven
            // When the user pressed enter, it will return the system to the Playing state, but we will be using the origin
            // We want to return to the exact state that 'paused' the game. Not a new state.
        }

        public override void Draw(GameTime gameTime)
        {
            OriginState.Draw(gameTime);

            Context._spriteBatch.DrawStringInCenter(
                Context._graphics,
                Context._font,
                "Pauze. Druk enter om verder te spelen.");
        }
    }
}
