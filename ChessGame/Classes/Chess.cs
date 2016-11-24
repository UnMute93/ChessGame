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

    enum CheckState
    {
        None,
        WhiteCheckedBlack,
        WhiteCheckmatedBlack,
        BlackCheckedWhite,
        BlackCheckmatedWhite
    }

    class Chess
    {
        public Player Player1 { get; set; }
        public Player Player2 { get; set; }
        public Board Board { get; set; }
        public List<Move> MoveHistory { get; set; }
        public Player ActivePlayer { get; set; }
        public int TurnNumber { get; set; }
        public CheckState CheckStatus { get; set; }

        public Chess()
        {

        }

        public bool IsValidMove(int xPos, int yPos, int xDest, int yDest)
        {

        }

        public bool IsCheck()
        {

        }

        public bool IsCheckMate()
        {

        }

        public bool IsFinished()
        {

        }
    }
}
