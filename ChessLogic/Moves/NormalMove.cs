using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLogic
{
    internal class NormalMove : Move
    {
        // Properties
        public override MoveType Type => MoveType.Normal;
        public override Position FromPos { get; }
        public override Position ToPos { get; }
        /*
         * In : Position (Where the piece is), Position (Where the piece should be)
         * Out: -
         * Do : Constructor to create a normal move
         */
        public NormalMove(Position fromPos, Position toPos)
        {
            this.FromPos = fromPos;
            this.ToPos = toPos;
        }
        /*
         * In : Board (Game board)
         * Out: -
         * Do : Execute a move
         */
        public override void Execite(Board board)
        {
            Piece piece = board[FromPos];
            board[ToPos] = piece;
            board[FromPos] = null;
            piece.HasMoved = true;
        }
    }
}
