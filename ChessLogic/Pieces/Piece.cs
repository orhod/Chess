using System.Runtime.InteropServices;

namespace ChessLogic
{
    // Abstract class for all Pieces
    public abstract class Piece
    {
        // Properties
        public abstract PieceType Type{get;}
        public abstract Player Color { get;}
        public bool HasMoved { get; set;} = false;
        /*
         * In: -
         * Out: A piece of the same kind
         * Do: Abstract method To copy a piece
         */
        public abstract Piece Copy();
        /*
         * In: Position (Where the piece placed), Board (Game board)
         * Out: enumerator of moves the piece can do
         * Do: Abstract method To get all the moves a piece can do
         */
        public abstract IEnumerable<Move> GetMoves(Position from ,Board board);

        /*
         * In: Position (Where the piece placed), Board (Game board) , Direction (Direction to move to)
         * Out: Places Where the piece can move to in one direction
         * Do: Check if a posistion in a direction is empty or have an opponent piece
         */
        protected IEnumerable<Position> MovePositionsInDir(Position from, Board board, Direction dir)
        {
            for (Position pos= from + dir; Board.OnBoard(pos); pos+=dir)
            {
                if (board.IsEmpty(pos))
                {
                    yield return pos;
                    continue;
                }
                Piece piece = board[pos];
                if (piece.Color != Color)
                {
                    yield return pos;
                }
                yield break;
            }
        }
        /*
         * In: Position (Where the piece placed), Board (Game board) , Direction[] (Directions to move to)
         * Out: Places Where the piece can move to in a collection of directions
         * Do: Check  for all posistions in a collection of direction is empty or have an opponent piece
         */
        protected IEnumerable<Position> MovePositionsInDirs(Position from, Board board, Direction[] dirs)
        {
            return dirs.SelectMany(dir=> MovePositionsInDir(from, board, dir));
        }

    }
}
