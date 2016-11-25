using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame.Classes
{
    enum MoveDirection
    {
        Up,
        Down
    }

    class Pawn : Piece
    {
        public override Texture2D Image { get; set; }
        public bool IsPromoted { get; set; }
        public Piece PromotedPiece { get; set; }
        public MoveDirection MoveDirection { get; set; }

        public Pawn(Color color) : base(color)
        {
            IsPromoted = false;
            if (color == Color.White)
                MoveDirection = MoveDirection.Up;
            else
                MoveDirection = MoveDirection.Down;
        }

        public override void Draw()
        {

        }
    }
}
