using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame.Classes
{
    class Board
    {
        public Square[,] Squares { get; set; }
        public Texture2D Image { get; set; }

        public Board()
        {
            Squares = new Square[8, 8];
        }

        public Square GetSquare(int xPos, int yPos)
        {
            return Squares[xPos, yPos];
        }

        public void MovePiece(int xPos, int yPos, int xDest, int yDest)
        {

        }

        public void RemovePiece(int xPos, int yPos)
        {
            Squares[xPos, yPos].Piece = null; //placeholder
        }
    }
}
