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
        public List<Piece> Pieces { get; set; }
        public Texture2D Image { get; set; }

        public Board()
        {
            Squares = new Square[8, 8];
            Pieces = new List<Piece>();
            for (int i = 0; i < Squares.GetLength(0); i++)
            {
                for (int j = 0; j < Squares.GetLength(1); j++)
                {
                    Squares[i, j] = new Square(i, j);
                }
            }

            Squares[0, 0].Piece = new Rook(Color.Black);
            Squares[0, 1].Piece = new Knight(Color.Black);
            Squares[0, 2].Piece = new Bishop(Color.Black);
            Squares[0, 3].Piece = new Queen(Color.Black);
            Squares[0, 4].Piece = new King(Color.Black);
            Squares[0, 5].Piece = new Bishop(Color.Black);
            Squares[0, 6].Piece = new Knight(Color.Black);
            Squares[0, 7].Piece = new Rook(Color.Black);
            for (int i = 0; i < Squares.GetLength(0); i++)
            {
                Squares[1, i].Piece = new Pawn(Color.Black);
            }

            Squares[7, 0].Piece = new Rook(Color.White);
            Squares[7, 1].Piece = new Knight(Color.White);
            Squares[7, 2].Piece = new Bishop(Color.White);
            Squares[7, 3].Piece = new Queen(Color.White);
            Squares[7, 4].Piece = new King(Color.White);
            Squares[7, 5].Piece = new Bishop(Color.White);
            Squares[7, 6].Piece = new Knight(Color.White);
            Squares[7, 7].Piece = new Rook(Color.White);
            for (int i = 0; i < Squares.GetLength(1); i++)
            {
                Squares[6, i].Piece = new Pawn(Color.White);
            }

            // Add pieces to the Piece list for easy access.
            for (int i = 0; i < Squares.GetLength(0); i++)
            {
                for (int j = 0; j < Squares.GetLength(1); j++)
                {
                    if (Squares[i, j].Piece != null)
                        Pieces.Add(Squares[i, j].Piece);
                }
            }
        }

        public void Draw()
        {

        }
    }
}
