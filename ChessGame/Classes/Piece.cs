using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame.Classes
{
    enum PieceType
    {
        Bishop,
        King,
        Knight,
        Pawn,
        Queen,
        Rook
    }

    abstract class Piece
    {
        public Color Color { get; set; }
        public bool IsSelectable { get; set; }
        public abstract Texture2D Image { get; set; }
        public bool HasMoved { get; set; }
        public List<Square> PseudoLegalMoves { get; set; }
        public PieceType Type { get; set; }

        public Piece(Color color)
        {
            Color = color;
            IsSelectable = false;
            HasMoved = false;
            PseudoLegalMoves = new List<Square>();
        }

        public abstract void Draw();
    }
}
