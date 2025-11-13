using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;

namespace PikachuGame.Input
{
    public static class InputFacade
    {
        private static KeyboardState CurrentState { get; set; }
        private static KeyboardState PreviousState { get; set; }

        public static void Update()
        {
            PreviousState = CurrentState;
            CurrentState = Keyboard.GetState();
        }

        public static bool IsKeyDown(Keys key)
        {
            return CurrentState.IsKeyDown(key);
        }

        public static bool WasKeyJustPressed(Keys key)
        {
            return PreviousState.IsKeyUp(key) && CurrentState.IsKeyDown(key);
        }
    }
}
