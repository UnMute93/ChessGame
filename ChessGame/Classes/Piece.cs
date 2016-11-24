using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame.Classes
{
    abstract class Piece
    {
        public Color Color { get; set; }
        public bool IsSelectable { get; set; }
        public abstract Texture2D Image { get; set; }

        public Piece()
        {

        }

        public void Draw()
        {

        }
    }
}
