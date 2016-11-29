using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame.Classes
{
    enum Color
    {
        White,
        Black
    }

    enum CheckStatus
    {
        None,
        WhiteCheckedBlack,
        WhiteCheckmatedBlack,
        BlackCheckedWhite,
        BlackCheckmatedWhite
    }

    class Chess
    {
        public const int EN_PASSANT_ROW_WHITE = 3;
        public const int EN_PASSANT_ROW_BLACK = 4;

        public Player[] Players { get; set; }
        public Timer[] Timers { get; set; }
        public Board Board { get; set; }
        public List<Move> MoveHistory { get; set; }
        public Player ActivePlayer { get; set; }
        public CheckStatus CheckStatus { get; set; }

        public Chess()
        {
            Players = new Player[2];
            Players[0] = new Player("Player 1", Color.White);
            Players[1] = new Player("Player 2", Color.Black);
            Timers = new Timer[2];
            Timers[0] = new Timer(100.0f, Color.White);
            Timers[1] = new Timer(100.0f, Color.Black);
            Board = new Board();
            MoveHistory = new List<Move>();
            ActivePlayer = Players[0];
            CheckStatus = CheckStatus.None;
        }

        /*public bool IsValidMove(Square fromSquare, Square toSquare)
        {

        }

        public bool IsCheck(Square square)
        {

        }

        public bool IsCheckMate()
        {

        }

        public bool IsFinished()
        {

        }*/

        public void MakeMove()
        {

        }

        public List<Square> TraceDiagonal(Square square)
        {
            List<Square> result = new List<Square>();

            for (int x = square.Row - 1, y = square.Column - 1; x >= 0 && x < Board.Squares.GetLength(0) && y >= 0 && y < Board.Squares.GetLength(1); x--, y--)
            {
                if (Board.Squares[x, y].Piece == null)
                    result.Add(Board.Squares[x, y]);
                else if (Board.Squares[x, y].Piece.Color == ActivePlayer.Color)
                    break;
                else if (Board.Squares[x, y].Piece.Color != ActivePlayer.Color)
                {
                    result.Add(Board.Squares[x, y]);
                    break;
                }
            }
            for (int x = square.Row - 1, y = square.Column + 1; x >= 0 && x < Board.Squares.GetLength(0) && y < Board.Squares.GetLength(1); x--, y++)
            {
                if (Board.Squares[x, y].Piece == null)
                    result.Add(Board.Squares[x, y]);
                else if (Board.Squares[x, y].Piece.Color == ActivePlayer.Color)
                    break;
                else if (Board.Squares[x, y].Piece.Color != ActivePlayer.Color)
                {
                    result.Add(Board.Squares[x, y]);
                    break;
                }
            }
            for (int x = square.Row + 1, y = square.Column - 1; x < Board.Squares.GetLength(0) && y >= 0 && y < Board.Squares.GetLength(1); x++, y--)
            {
                if (Board.Squares[x, y].Piece == null)
                    result.Add(Board.Squares[x, y]);
                else if (Board.Squares[x, y].Piece.Color == ActivePlayer.Color)
                    break;
                else if (Board.Squares[x, y].Piece.Color != ActivePlayer.Color)
                {
                    result.Add(Board.Squares[x, y]);
                    break;
                }
            }
            for (int x = square.Row + 1, y = square.Column + 1; x < Board.Squares.GetLength(0) && y < Board.Squares.GetLength(1); x++, y++)
            {
                if (Board.Squares[x, y].Piece == null)
                    result.Add(Board.Squares[x, y]);
                else if (Board.Squares[x, y].Piece.Color == ActivePlayer.Color)
                    break;
                else if (Board.Squares[x, y].Piece.Color != ActivePlayer.Color)
                {
                    result.Add(Board.Squares[x, y]);
                    break;
                }
            }

            return result;
        }

        public List<Square> TraceRow(Square square)
        {
            List<Square> result = new List<Square>();

            for (int x = square.Row - 1; x >= 0 && x < Board.Squares.GetLength(0); x--)
            {
                if (Board.Squares[x, square.Column].Piece == null)
                    result.Add(Board.Squares[x, square.Column]);
                else if (Board.Squares[x, square.Column].Piece.Color == ActivePlayer.Color)
                    break;
                else if (Board.Squares[x, square.Column].Piece.Color != ActivePlayer.Color)
                {
                    result.Add(Board.Squares[x, square.Column]);
                    break;
                }
            }
            for (int x = square.Row + 1; x < Board.Squares.GetLength(0); x++)
            {
                if (Board.Squares[x, square.Column].Piece == null)
                    result.Add(Board.Squares[x, square.Column]);
                else if (Board.Squares[x, square.Column].Piece.Color == ActivePlayer.Color)
                    break;
                else if (Board.Squares[x, square.Column].Piece.Color != ActivePlayer.Color)
                {
                    result.Add(Board.Squares[x, square.Column]);
                    break;
                }
            }
            for (int y = square.Column - 1; y >= 0 && y < Board.Squares.GetLength(1); y--)
            {
                if (Board.Squares[square.Row, y].Piece == null)
                    result.Add(Board.Squares[square.Row, y]);
                else if (Board.Squares[square.Row, y].Piece.Color == ActivePlayer.Color)
                    break;
                else if (Board.Squares[square.Row, y].Piece.Color != ActivePlayer.Color)
                {
                    result.Add(Board.Squares[square.Row, y]);
                    break;
                }
            }
            for (int y = square.Column + 1; y < Board.Squares.GetLength(1); y++)
            {
                if (Board.Squares[square.Row, y].Piece == null)
                    result.Add(Board.Squares[square.Row, y]);
                else if (Board.Squares[square.Row, y].Piece.Color == ActivePlayer.Color)
                    break;
                else if (Board.Squares[square.Row, y].Piece.Color != ActivePlayer.Color)
                {
                    result.Add(Board.Squares[square.Row, y]);
                    break;
                }
            }

            return result;
        }

        public List<Square> FindKingMoves(Square square)
        {
            List<Square> result = new List<Square>();

            for (int i = square.Row - 1; i < square.Row + 1; i++)
            {
                for (int j = square.Column - 1; j < square.Column + 1; j++)
                {
                    if (Board.Squares[i, j].Piece == null || Board.Squares[i, j].Piece.Color != ActivePlayer.Color)
                        result.Add(Board.Squares[i, j]);
                }
            }

            return result;
        }

        public List<Square> FindKnightMoves(Square square)
        {
            List<Square> result = new List<Square>();
            int x = square.Row, y = square.Column;

            try
            {
                if (Board.Squares[x + 2, y + 1].Piece == null || Board.Squares[x + 2, y + 1].Piece.Color != ActivePlayer.Color)
                    result.Add(Board.Squares[x + 2, y + 1]);
            }
            catch (IndexOutOfRangeException e) { }

            try
            {
                if (Board.Squares[x + 2, y - 1].Piece == null || Board.Squares[x + 2, y + 1].Piece.Color != ActivePlayer.Color)
                    result.Add(Board.Squares[x + 2, y + 1]);
            }
            catch (IndexOutOfRangeException e) { }

            try
            {
                if (Board.Squares[x - 2, y + 1].Piece == null || Board.Squares[x + 2, y + 1].Piece.Color != ActivePlayer.Color)
                    result.Add(Board.Squares[x + 2, y + 1]);
            }
            catch (IndexOutOfRangeException e) { }

            try
            {
                if (Board.Squares[x - 2, y - 1].Piece == null || Board.Squares[x + 2, y + 1].Piece.Color != ActivePlayer.Color)
                    result.Add(Board.Squares[x + 2, y + 1]);
            }
            catch (IndexOutOfRangeException e) { }

            try
            {
                if (Board.Squares[x + 1, y + 2].Piece == null || Board.Squares[x + 2, y + 1].Piece.Color != ActivePlayer.Color)
                    result.Add(Board.Squares[x + 2, y + 1]);
            }
            catch (IndexOutOfRangeException e) { }

            try
            {
                if (Board.Squares[x + 1, y - 2].Piece == null || Board.Squares[x + 2, y + 1].Piece.Color != ActivePlayer.Color)
                    result.Add(Board.Squares[x + 2, y + 1]);
            }
            catch (IndexOutOfRangeException e) { }

            try
            {
                if (Board.Squares[x - 1, y + 2].Piece == null || Board.Squares[x + 2, y + 1].Piece.Color != ActivePlayer.Color)
                    result.Add(Board.Squares[x + 2, y + 1]);
            }
            catch (IndexOutOfRangeException e) { }

            try
            {
                if (Board.Squares[x - 1, y - 2].Piece == null || Board.Squares[x + 2, y + 1].Piece.Color != ActivePlayer.Color)
                    result.Add(Board.Squares[x + 2, y + 1]);
            }
            catch (IndexOutOfRangeException e) { }

            return result;
        }

        public void GeneratePseudoLegalMoves()
        {
            for (int i = 0; i < Board.Squares.GetLength(0); i++)
            {
                for (int j = 0; j < Board.Squares.GetLength(1); j++)
                {
                    Piece piece = Board.Squares[i, j].Piece;
                    if (piece != null)
                    {
                        switch (piece.Type)
                        {
                            case PieceType.Bishop:
                                piece.PseudoLegalMoves = TraceDiagonal(Board.Squares[i, j]);
                                break;
                            case PieceType.King:
                                piece.PseudoLegalMoves = FindKingMoves(Board.Squares[i, j]);
                                break;
                            case PieceType.Knight:
                                piece.PseudoLegalMoves = FindKnightMoves(Board.Squares[i, j]);
                                break;
                            case PieceType.Pawn:
                                Pawn pawn = (Pawn)piece;
                                List<Square> result = new List<Square>();
                                if (pawn.IsPromoted)
                                {
                                    switch (pawn.PromotedPiece.Type)
                                    {
                                        case PieceType.Bishop:
                                            piece.PseudoLegalMoves = TraceDiagonal(Board.Squares[i, j]);
                                            break;
                                        case PieceType.Knight:
                                            piece.PseudoLegalMoves = FindKnightMoves(Board.Squares[i, j]);
                                            break;
                                        case PieceType.Queen:
                                            piece.PseudoLegalMoves = TraceDiagonal(Board.Squares[i, j]).Concat(TraceRow(Board.Squares[i, j])).ToList();
                                            break;
                                        case PieceType.Rook:
                                            piece.PseudoLegalMoves = TraceDiagonal(Board.Squares[i, j]);
                                            break;
                                    }
                                }
                                else if (pawn.MoveDirection == MoveDirection.Up)
                                {
                                    // Square directly in front
                                    if (Board.Squares[i, j - 1].Piece == null)
                                        result.Add(Board.Squares[i, j - 1]);
                                    // Second square if not moved
                                    if (!pawn.HasMoved && Board.Squares[i, j - 2].Piece == null)
                                        result.Add(Board.Squares[i, j - 2]);
                                    // En Passant
                                    if (j == EN_PASSANT_ROW_WHITE)
                                    {
                                        // The pawn able to be captured
                                        Pawn oppositePawn = null;
                                        if (i == 0 && Board.Squares[i + 1, j].Piece?.Type == PieceType.Pawn)
                                        {
                                            oppositePawn = (Pawn)Board.Squares[i + 1, j].Piece;
                                            if (oppositePawn.UsedTwoSquareMove)
                                                result.Add(Board.Squares[i + 1, j - 1]);
                                        }
                                        else if (i == 7 && Board.Squares[i - 1, j].Piece?.Type == PieceType.Pawn)
                                        {
                                            oppositePawn = (Pawn)Board.Squares[i - 1, j].Piece;
                                            if (oppositePawn.UsedTwoSquareMove)
                                                result.Add(Board.Squares[i - 1, j - 1]);
                                        }
                                        else
                                        {
                                            if (Board.Squares[i + 1, j].Piece?.Type == PieceType.Pawn)
                                            {
                                                oppositePawn = (Pawn)Board.Squares[i + 1, j].Piece;
                                                if (oppositePawn.UsedTwoSquareMove)
                                                    result.Add(Board.Squares[i + 1, j - 1]);
                                            }
                                            if (Board.Squares[i - 1, j].Piece?.Type == PieceType.Pawn)
                                            {
                                                oppositePawn = (Pawn)Board.Squares[i - 1, j].Piece;
                                                if (oppositePawn.UsedTwoSquareMove)
                                                    result.Add(Board.Squares[i - 1, j - 1]);
                                            }

                                        }
                                    }
                                }
                                else
                                {
                                    // Square directly in front
                                    if (Board.Squares[i, j + 1].Piece == null)
                                        result.Add(Board.Squares[i, j + 1]);
                                    // Second square if not moved
                                    if (!pawn.HasMoved && Board.Squares[i, j + 2].Piece == null)
                                        result.Add(Board.Squares[i, j + 2]);
                                    // En Passant
                                    if (j == EN_PASSANT_ROW_BLACK)
                                    {
                                        // The pawn able to be captured
                                        Pawn oppositePawn = null;
                                        if (i == 0 && Board.Squares[i + 1, j].Piece?.Type == PieceType.Pawn)
                                        {
                                            oppositePawn = (Pawn)Board.Squares[i + 1, j].Piece;
                                            if (oppositePawn.UsedTwoSquareMove)
                                                result.Add(Board.Squares[i + 1, j + 1]);
                                        }
                                        else if (i == 7 && Board.Squares[i - 1, j].Piece?.Type == PieceType.Pawn)
                                        {
                                            oppositePawn = (Pawn)Board.Squares[i - 1, j].Piece;
                                            if (oppositePawn.UsedTwoSquareMove)
                                                result.Add(Board.Squares[i - 1, j + 1]);
                                        }
                                        else
                                        {
                                            if (Board.Squares[i + 1, j].Piece?.Type == PieceType.Pawn)
                                            {
                                                oppositePawn = (Pawn)Board.Squares[i + 1, j].Piece;
                                                if (oppositePawn.UsedTwoSquareMove)
                                                    result.Add(Board.Squares[i + 1, j + 1]);
                                            }
                                            if (Board.Squares[i - 1, j].Piece?.Type == PieceType.Pawn)
                                            {
                                                oppositePawn = (Pawn)Board.Squares[i - 1, j].Piece;
                                                if (oppositePawn.UsedTwoSquareMove)
                                                    result.Add(Board.Squares[i - 1, j + 1]);
                                            }

                                        }
                                    }
                                }
                                break;
                            case PieceType.Queen:
                                piece.PseudoLegalMoves = TraceDiagonal(Board.Squares[i, j]).Concat(TraceRow(Board.Squares[i, j])).ToList();
                                break;
                            case PieceType.Rook:
                                piece.PseudoLegalMoves = TraceDiagonal(Board.Squares[i, j]);
                                break;
                        }
                    }
                }
            }
        }
    }
}
