using System.Runtime.InteropServices;

namespace ChessLogic
{
    // Abstract class for all Pieces
    public abstract class Piece
    {
        // return the Type of the piece
        public abstract PieceType Type{get;}
        // return the color of the piece
        public abstract Player Color { get;}
        // Check if the piece had moved (for spacial moves)
        public bool HasMoved { get; set;} = false;
        // Copy Function To generate more pieces
        public abstract Piece Copy();

        public abstract IEnumerable<Move> GetMoves(Position from ,Board board);

        // Return The places the Piece can move to in one direction
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
        // Return The places the Piece can move to in all directions
        protected IEnumerable<Position> MovePositionsInDirs(Position from, Board board, Direction[] dirs)
        {
            return dirs.SelectMany(dir=> MovePositionsInDir(from, board, dir));
        }

    }
}
