using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame.Classes
{
    class Bishop : Piece
    {
        public override Texture2D Image { get; set; }

        public Bishop(Color color) : base(color)
        {
            Type = PieceType.Bishop;
        }

        public override void Draw()
        {
            
        }
    }
}
