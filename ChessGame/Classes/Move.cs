using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame.Classes
{
    class Move
    {
        public int TurnNumber { get; set; }
        public Piece Piece { get; set; }
        public Piece CapturedPiece { get; set; }
        public Square FromSquare { get; set; }
        public Square ToSquare { get; set; }
        /* TODO Queenside/Kingside Castling, en passant, general notation in ToString() */

        public Move(int turnNumber, Piece piece, Square fromSquare, Square toSquare)
        {
            TurnNumber = turnNumber;
            Piece = piece;
            FromSquare = fromSquare;
            ToSquare = toSquare;
        }

        public Move(int turnNumber, Piece piece, Piece capturedPiece, Square fromSquare, Square toSquare) : this(turnNumber, piece, fromSquare, toSquare)
        {
            CapturedPiece = capturedPiece;
        }

        /*public override String ToString()
        {

        }*/
    }
}
