using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace PikachuGame.Object
{
    public class Sprite(Texture2D texture, Vector2 position)
    {
        public Texture2D Texture { get; } = texture;

        public Vector2 Position { get; } = position;
    }
}
