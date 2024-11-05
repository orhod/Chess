using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ChessLogic
{
    public class Castle:Move
    {
        // Properties
        public override MoveType Type { get; }
        public override Position FromPos { get; }
        public override Position ToPos { get; }

        private readonly Direction kingMoveDir;
        private readonly Position rookFromPos;
        private readonly Position rookToPos;

        /*
         * In : MoveType, Position
         * out: -
         * Do : Constructor
         */
        public Castle (MoveType type, Position kingPos)
        {
            Type = type;
            FromPos = kingPos;

            if(Type == MoveType.CastleKS)
            {
                kingMoveDir = Direction.Left;
                ToPos = new Position(kingPos.Row, 6);
                rookFromPos = new Position(kingPos.Row, 7);
                rookToPos = new Position(kingPos.Row, 5);
            }
            else if (type == MoveType.castleQS)
            {
                kingMoveDir = Direction.Right;
                ToPos = new Position(kingPos.Row, 2);
                rookFromPos = new Position(kingPos.Row, 0);
                rookToPos = new Position(kingPos.Row, 3);
            }
        }
        /*
         * In : Board
         * Out: -
         * Do : execute castle move 
         */
        public override void Execite(Board board)
        {
            new NormalMove(FromPos, ToPos).Execite(board);
            new NormalMove(rookFromPos, rookToPos).Execite(board);
        }
        /*
         *  In : Board
         *  Out: bool
         *  Do : Check if the move is leagal (The king is not in check and the king is not moving through a check)
         */
        public override bool IsLeagal(Board board)
        {
            Player player = board[FromPos].Color;
            if (board.IsInCheck(player))
            {
                return false;
            }

            Board copy = board.copy();
            Position kingPosInCopy = FromPos;

            for(int i = 0; i <2; i++)
            {
                new NormalMove(kingPosInCopy, kingPosInCopy + kingMoveDir).Execite(copy);
                kingPosInCopy += kingMoveDir;
                if (copy.IsInCheck(player))
                {
                    return false;
                }
            }
            return true;

        }
    }
}
