using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using PikachuGame.Extensions;
using PikachuGame.Input;

namespace PikachuGame.States
{
    public class GameOverState(Game1 context) : AbstractState(context)
    {
        public override void Update(GameTime gameTime)
        {
            if (InputFacade.WasKeyJustPressed(Keys.Enter))
                Context.ChangeState(new StartScreenState(Context));
        }
        
        public override void Draw(GameTime gameTime)
        {
            Context._spriteBatch.DrawStringInCenter(
                Context._graphics,
                Context._font,
                "Game over. Druk enter om naar start te gaan.");
        }
    }
}
