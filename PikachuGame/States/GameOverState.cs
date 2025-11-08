using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic.Devices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
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
            if (Microsoft.Xna.Framework.Input.Keyboard.GetState().IsKeyDown(Keys.Enter))
                Context.ChangeState(new StartScreenState(Context));
        }
    }
}
