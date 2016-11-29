using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame.Classes
{
    class Queen : Piece
    {
        public override Texture2D Image { get; set; }

        public Queen(Color color) : base(color)
        {
            Type = PieceType.Queen;
        }

        public override void Draw()
        {

        }
    }
}
