using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame.Classes
{
    class Square
    {
        public int Row { get; set; }
        public int Column { get; set; }
        public Piece Piece { get; set; }

        public Square(int row, int column)
        {
            Row = row;
            Column = column;
        }
    }
}
