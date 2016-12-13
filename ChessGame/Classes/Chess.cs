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
            GeneratePseudoLegalMoves();
            foreach (Piece piece in Board.Pieces)
            {
                if (piece.Color == ActivePlayer.Color)
                    piece.IsSelectable = true;
                if (piece.Color != ActivePlayer.Color)
                    piece.IsSelectable = false;
            }
        }

        public bool IsValidMove(Square fromSquare, Square toSquare)
        {
            if (fromSquare.Piece.PseudoLegalMoves.Contains(toSquare))
            // Legal Move Test
            {


                return true;
            }
            return false;
        }

        /*public bool IsCheck(Square square)
        {

        }

        public bool IsCheckMate()
        {

        }

        public bool IsFinished()
        {

        }*/

        public bool MakeMove(Square fromSquare, Square toSquare)
        {
            if (IsValidMove(fromSquare, toSquare))
            {
                Piece capturedPiece = null;
                if (toSquare.Piece != null)
                {
                    capturedPiece = toSquare.Piece;
                    Board.RemovedPieces.Add(toSquare.Piece);
                    Board.Pieces.Remove(toSquare.Piece);
                }
                else if (fromSquare.Piece.Type == PieceType.Pawn)
                {
                    if (!fromSquare.Piece.HasMoved && (fromSquare.Row - toSquare.Row == 2 || fromSquare.Row - toSquare.Row == -2))
                    {
                        Pawn pawn = (Pawn)fromSquare.Piece;
                        pawn.UsedTwoSquareMove = true;
                    }
                }

                toSquare.Piece = fromSquare.Piece;
                toSquare.Piece.HasMoved = true;
                fromSquare.Piece = null;
                AddToMoveHistory(fromSquare, toSquare, capturedPiece);
                AdvanceTurn();
                return true;
            }
            return false;
        }

        public void AddToMoveHistory(Square fromSquare, Square toSquare, Piece capturedPiece)
        {
            int turnNumber = 1;
            if (MoveHistory.Count > 0)
            {
                if (MoveHistory.Last().Piece.Color == Color.White)
                    turnNumber = MoveHistory.Last().TurnNumber;
                else
                    turnNumber = MoveHistory.Last().TurnNumber + 1;
            }
            if (capturedPiece == null)
                MoveHistory.Add(new Move(turnNumber, toSquare.Piece, fromSquare, toSquare));
            else
                MoveHistory.Add(new Move(turnNumber, toSquare.Piece, capturedPiece, fromSquare, toSquare));
        }

        public void AdvanceTurn()
        {
            if (ActivePlayer == Players[0])
                ActivePlayer = Players[1];
            else
                ActivePlayer = Players[0];

            foreach (Piece piece in Board.Pieces)
            {
                if (piece.Color == ActivePlayer.Color)
                    piece.IsSelectable = true;
                if (piece.Color != ActivePlayer.Color)
                {
                    piece.IsSelectable = false;
                    if (piece.Type == PieceType.Pawn)
                    {
                        Pawn pawn = (Pawn)piece;
                        if (pawn.UsedTwoSquareMove)
                            pawn.UsedTwoSquareMove = false;
                    }
                }
            }

            GeneratePseudoLegalMoves();
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

            for (int i = square.Row - 1; i < square.Row + 2; i++)
            {
                for (int j = square.Column - 1; j < square.Column + 2; j++)
                {
                    if ((i >= 0 && i <= 7 && j >= 0 && j <= 7) && (Board.Squares[i, j].Piece == null || Board.Squares[i, j].Piece.Color != ActivePlayer.Color))
                        result.Add(Board.Squares[i, j]);
                }
            }

            return result;
        }

        public List<Square> FindKnightMoves(Square square)
        {
            List<Square> result = new List<Square>();
            int x = square.Row, y = square.Column;

            if ((x + 2 <= 7 && y + 1 <= 7) && (Board.Squares[x + 2, y + 1].Piece == null || Board.Squares[x + 2, y + 1].Piece.Color != ActivePlayer.Color))
                result.Add(Board.Squares[x + 2, y + 1]);

            if ((x + 2 <= 7 && y - 1 >= 0) && (Board.Squares[x + 2, y - 1].Piece == null || Board.Squares[x + 2, y - 1].Piece.Color != ActivePlayer.Color))
                result.Add(Board.Squares[x + 2, y - 1]);

            if ((x - 2 >= 0 && y + 1 <= 7) && (Board.Squares[x - 2, y + 1].Piece == null || Board.Squares[x - 2, y + 1].Piece.Color != ActivePlayer.Color))
                result.Add(Board.Squares[x - 2, y + 1]);

            if ((x - 2 >= 0 && y - 1 >= 0) && (Board.Squares[x - 2, y - 1].Piece == null || Board.Squares[x - 2, y - 1].Piece.Color != ActivePlayer.Color))
                result.Add(Board.Squares[x - 2, y - 1]);

            if ((x + 1 <= 7 && y + 2 <= 7) && (Board.Squares[x + 1, y + 2].Piece == null || Board.Squares[x + 1, y + 2].Piece.Color != ActivePlayer.Color))
                result.Add(Board.Squares[x + 1, y + 2]);

            if ((x + 1 <= 7 && y - 2 >= 0) && (Board.Squares[x + 1, y - 2].Piece == null || Board.Squares[x + 1, y - 2].Piece.Color != ActivePlayer.Color))
                result.Add(Board.Squares[x + 1, y - 2]);

            if ((x - 1 >= 0 && y + 2 <= 7) && (Board.Squares[x - 1, y + 2].Piece == null || Board.Squares[x - 1, y + 2].Piece.Color != ActivePlayer.Color))
                result.Add(Board.Squares[x - 1, y + 2]);

            if ((x - 1 >= 0 && y - 2 >= 0) && (Board.Squares[x - 1, y - 2].Piece == null || Board.Squares[x - 1, y - 2].Piece.Color != ActivePlayer.Color))
                result.Add(Board.Squares[x - 1, y - 2]);

            return result;
        }

        public List<Square> FindPawnMoves(Square square)
        {
            Pawn pawn = (Pawn)square.Piece;
            List<Square> result = new List<Square>();
            int x = square.Row;
            int y = square.Column;
            if (pawn.IsPromoted)
            {
                switch (pawn.PromotedPiece.Type)
                {
                    case PieceType.Bishop:
                        result = TraceDiagonal(Board.Squares[x, y]).ToList();
                        break;
                    case PieceType.Knight:
                        result = FindKnightMoves(Board.Squares[x, y]);
                        break;
                    case PieceType.Queen:
                        result = TraceDiagonal(Board.Squares[x, y]).Concat(TraceRow(Board.Squares[x, y])).ToList();
                        break;
                    case PieceType.Rook:
                        result = TraceDiagonal(Board.Squares[x, y]);
                        break;
                }
            }
            else if (pawn.MoveDirection == MoveDirection.Up)
            {
                // Square directly in front
                if ((x - 1 >= 0) && Board.Squares[x - 1, y].Piece == null)
                {
                    result.Add(Board.Squares[x - 1, y]);

                    // Second square if not moved
                    if (!pawn.HasMoved && Board.Squares[x - 2, y].Piece == null)
                        result.Add(Board.Squares[x - 2, y]);
                }

                // Capture
                if ((x - 1 >= 0 && y - 1 >= 0) && Board.Squares[x - 1, y - 1].Piece != null && Board.Squares[x - 1, y - 1].Piece.Color != ActivePlayer.Color)
                    result.Add(Board.Squares[x - 1, y - 1]);

                if ((x - 1 >= 0 && y + 1 <= 7) && Board.Squares[x - 1, y + 1].Piece != null && Board.Squares[x - 1, y + 1].Piece.Color != ActivePlayer.Color)
                    result.Add(Board.Squares[x - 1, y + 1]);

                // En Passant
                if (x == EN_PASSANT_ROW_WHITE)
                {
                    // The pawn able to be captured
                    Pawn oppositePawn = null;
                    if (y == 0 && Board.Squares[x, y + 1].Piece?.Type == PieceType.Pawn)
                    {
                        oppositePawn = (Pawn)Board.Squares[x, y + 1].Piece;
                        if (oppositePawn.UsedTwoSquareMove)
                            result.Add(Board.Squares[x - 1, y + 1]);
                    }
                    else if (y == 7 && Board.Squares[x, y - 1].Piece?.Type == PieceType.Pawn)
                    {
                        oppositePawn = (Pawn)Board.Squares[x, y - 1].Piece;
                        if (oppositePawn.UsedTwoSquareMove)
                            result.Add(Board.Squares[x - 1, y - 1]);
                    }
                    else
                    {
                        if (Board.Squares[x, y + 1].Piece?.Type == PieceType.Pawn)
                        {
                            oppositePawn = (Pawn)Board.Squares[x, y + 1].Piece;
                            if (oppositePawn.UsedTwoSquareMove)
                                result.Add(Board.Squares[x - 1, y + 1]);
                        }
                        if (Board.Squares[x, y - 1].Piece?.Type == PieceType.Pawn)
                        {
                            oppositePawn = (Pawn)Board.Squares[x, y - 1].Piece;
                            if (oppositePawn.UsedTwoSquareMove)
                                result.Add(Board.Squares[x - 1, y - 1]);
                        }
                    }
                }
            }
            else
            {
                // Square directly in front
                if ((x + 1 <= 7) && Board.Squares[x + 1, y].Piece == null)
                {
                    result.Add(Board.Squares[x + 1, y]);

                    // Second square if not moved
                    if (!pawn.HasMoved && Board.Squares[x + 2, y].Piece == null)
                        result.Add(Board.Squares[x + 2, y]);
                }

                // Capture
                if ((x + 1 <= 7 && y - 1 >= 0) && Board.Squares[x + 1, y - 1].Piece != null && Board.Squares[x + 1, y - 1].Piece.Color != ActivePlayer.Color)
                    result.Add(Board.Squares[x + 1, y - 1]);

                if ((x + 1 <= 7 && y + 1 <= 7) && Board.Squares[x + 1, y + 1].Piece != null && Board.Squares[x + 1, y + 1].Piece.Color != ActivePlayer.Color)
                    result.Add(Board.Squares[x + 1, y + 1]);

                // En Passant
                if (x == EN_PASSANT_ROW_BLACK)
                {
                    // The pawn able to be captured
                    Pawn oppositePawn = null;
                    if (y == 0 && Board.Squares[x, y + 1].Piece?.Type == PieceType.Pawn)
                    {
                        oppositePawn = (Pawn)Board.Squares[x, y + 1].Piece;
                        if (oppositePawn.UsedTwoSquareMove)
                            result.Add(Board.Squares[x + 1, y + 1]);
                    }
                    else if (y == 7 && Board.Squares[x, y - 1].Piece?.Type == PieceType.Pawn)
                    {
                        oppositePawn = (Pawn)Board.Squares[x, y - 1].Piece;
                        if (oppositePawn.UsedTwoSquareMove)
                            result.Add(Board.Squares[x + 1, y - 1]);
                    }
                    else
                    {
                        if (Board.Squares[x, y + 1].Piece?.Type == PieceType.Pawn)
                        {
                            oppositePawn = (Pawn)Board.Squares[x, y + 1].Piece;
                            if (oppositePawn.UsedTwoSquareMove)
                                result.Add(Board.Squares[x + 1, y + 1]);
                        }
                        if (Board.Squares[x, y - 1].Piece?.Type == PieceType.Pawn)
                        {
                            oppositePawn = (Pawn)Board.Squares[x, y - 1].Piece;
                            if (oppositePawn.UsedTwoSquareMove)
                                result.Add(Board.Squares[x + 1, y - 1]);
                        }
                    }
                }
            }

            return result;
        }

        public void GeneratePseudoLegalMoves()
        {
            for (int i = 0; i < Board.Squares.GetLength(0); i++)
            {
                for (int j = 0; j < Board.Squares.GetLength(1); j++)
                {
                    Piece piece = Board.Squares[i, j].Piece;
                    if (piece != null && piece.Color == ActivePlayer.Color)
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
                                piece.PseudoLegalMoves = FindPawnMoves(Board.Squares[i, j]);
                                break;
                            case PieceType.Queen:
                                piece.PseudoLegalMoves = TraceDiagonal(Board.Squares[i, j]).Concat(TraceRow(Board.Squares[i, j])).ToList();
                                break;
                            case PieceType.Rook:
                                piece.PseudoLegalMoves = TraceRow(Board.Squares[i, j]);
                                break;
                        }
                    }
                }
            }
        }
    }
}
