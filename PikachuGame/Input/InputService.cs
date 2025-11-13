using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;

namespace PikachuGame.Input
{
    public class InputService
    {
        //deze maakt dat het niet uitmaakt hoe je het game speelt: met de muis, met het toetsenbord, of nog een ander device, 
        // je kan die allemaal hier implementeren en dan hetzelfde meegeven aan de facade

        public bool GoRight()
        {
            if (InputFacade.IsKeyDown([Keys.D, Keys.Right]))
                return true;
            return false;
        }
    }
}
