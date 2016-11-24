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
        public String WhiteMove { get; set; }
        public String BlackMove { get; set; }

        public Move(int turnNumber)
        {
            TurnNumber = turnNumber;
            WhiteMove = "";
            BlackMove = "";
        }

        /* Check inputs, use chess notation */
    }
}
