using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PikachuGame.Extensions;

namespace PikachuGame.States
{
    public class StartPlayingState(Game1 context): AbstractState(context)
    {
        public override void Update(GameTime gameTime)
        {
            Context._elapsedTimeSinceLastSharkInMs = 0;
            Context._playerPosition = new Vector2(0, 300);
            Context._backgroundPosition = new Vector2(0, 0);
            Context._numberOfRemainLives = 3;
            Context._sharkPositions.Clear();

            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                Context.ChangeState(new PlayingState(Context));
        }

        public override void Draw(GameTime gameTime)
        {
            Context._spriteBatch.DrawStringInCenter(
                Context._graphics, Context._font, 
                "Druk enter om te spelen");
        }
    }
}
