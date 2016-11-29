using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame.Classes
{
    class Rook : Piece
    {
        public override Texture2D Image { get; set; }

        public Rook(Color color) : base(color)
        {
            Type = PieceType.Rook;
        }

        public override void Draw()
        {

        }
    }
}
