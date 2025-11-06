using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic.Devices;
using Microsoft.Xna.Framework;
using PikachuGame.Extensions;

namespace PikachuGame.States
{
    public class GameOverState(Game1 context) : AbstractState(context)
    {
        public override void Draw(GameTime gameTime)
        {
            Context._spriteBatch.DrawStringInCenter(
                Context._graphics,
                Context._font,
                "Game over. Druk enter om naar start te gaan.");
        }

        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                Context.ChangeState(new StartPlayingState(Context));
        }
    }
}
