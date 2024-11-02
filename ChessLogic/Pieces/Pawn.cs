namespace ChessLogic
{
    // Inheret from Piece
    public class Pawn : Piece
    {
        public override PieceType Type => PieceType.Pawn;
        public override Player Color { get; }

        private readonly Direction forward;

        // Constractor
        public Pawn (Player color)
        {
            this.Color = color;
            if (color == Player.White)
            {
                this.forward = Direction.Down;
            }
            else
            {
                this.forward = Direction.Up;
            }
        }
        // Copy Function To generate more pieces
        public override Piece Copy()
        {
            Pawn copy = new Pawn(Color);
            copy.HasMoved = HasMoved;
            return copy;
        }
        // Return if the pawn can move
        private  bool CanMoveTo(Position pos, Board board)
        {
            return Board.OnBoard(pos) && board.IsEmpty(pos);
        }
        private  bool CanCaptureAt(Position pos, Board board)
        {
            if( !Board.OnBoard(pos) || board.IsEmpty(pos)){
                return false;
            }
            else
            {
                return board[pos].Color != Color;
            }
        }
        // Return The possible forward Moves of the pawn
        private IEnumerable<Move> ForwardMoves(Position from, Board board) 
        {
            Position OneStep = from + forward;

            if(CanMoveTo(OneStep, board))
            {
                yield return new NormalMove(from, OneStep);
                Position TwoStep = OneStep + forward;
                if(!this.HasMoved && CanMoveTo(TwoStep, board))
                {
                    yield return new NormalMove(from, TwoStep);
                }
            }
        }
        // Return the possible diagonal moves of the pawn
        private IEnumerable<Move> DiagonalMoves(Position from, Board board)
        {
            foreach (Direction dir in new Direction[] { Direction.Left, Direction.Right, })
            {
                Position to = from + forward + dir;

                if (CanCaptureAt(to, board))
                {
                    yield return new NormalMove(from, to);
                }
            }
        }
        // Return all the possible moves of the pawn
        public override IEnumerable<Move> GetMoves(Position from, Board board)
        {
            return ForwardMoves(from, board).Concat(DiagonalMoves(from, board));
        }





    }
}
