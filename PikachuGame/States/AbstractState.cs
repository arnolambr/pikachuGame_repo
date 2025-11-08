using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace PikachuGame.States
{
    public abstract class AbstractState(Game1 context)
    {
        protected Game1 Context { get; } = context;
        
        /* als je de schrijfwijze van hierboven gebruikt, moet je de constructor niet meer specifiek 
         * zo schrijven als hieronder, die is geïmpliceerd
         * protected Game1 Context { get; }//init wordt door vs zelf wel gedaan
        
        public AbstractState(Game1 context)
        {
            Context = context;
        }*/

        public abstract void Update(GameTime gameTime);
        public abstract void Draw(GameTime gameTime);
    }
}
