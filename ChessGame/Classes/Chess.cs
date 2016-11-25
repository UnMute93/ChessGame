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
        }

        public bool IsValidMove(Square fromSquare, Square toSquare)
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
