using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLogic
{
    // abstract class
    public abstract class Move
    {
        // Properties
        public abstract MoveType Type { get; }
        public abstract Position FromPos { get; }
        public abstract Position ToPos { get; }
        /*
         * In: Board (Game board)
         * Out: -
         * Do: Abstract method to execute a move
         */
        public abstract void Execite(Board board);
    }
}
