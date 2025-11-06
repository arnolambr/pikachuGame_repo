using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace PikachuGame.States
{
    public abstract class AbstractState
    {
        protected Game1 Context { get; }//init wordt door vs zelf wel gedaan
        
        public AbstractState(Game1 context)
        {
            Context = context;
        }
        
        public abstract void Update(GameTime gameTime);
        public abstract void Draw(GameTime gameTime);
    }
}
