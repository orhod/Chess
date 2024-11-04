namespace ChessLogic
{
    // Inheret from Piece
    public class Pawn : Piece
    {
        // Properties
        public override PieceType Type => PieceType.Pawn;
        public override Player Color { get; }

        private readonly Direction forward;

        /*
         * In : Player (Color of the piece)
         * Out: -
         * Do : Constructor to create a pawn
         */
        public Pawn (Player color)
        {
            this.Color = color;
            if (color == Player.White)
            {
                this.forward = Direction.Up;
            }
            else
            {
                this.forward = Direction.Down;
            }
        }
        /*
         * In : -
         * Out: A piece of the same kind
         * Do :  Copy a piece
         */
        public override Piece Copy()
        {
            Pawn copy = new Pawn(Color);
            copy.HasMoved = HasMoved;
            return copy;
        }
        /*
         * In : Position (To move to), Board (Game board)
         * Out: Boolean 
         * Do : Check if a position is on the board or empty
         */
        private bool CanMoveTo(Position pos, Board board)
        {
            return Board.OnBoard(pos) && board.IsEmpty(pos);
        }
        /*
         * In : Position (To capture at), Board (Game board)
         * Out: Boolean 
         * Do : Check if a position is on the board or have an opponent piece
         */
        private bool CanCaptureAt(Position pos, Board board)
        {
            if( !Board.OnBoard(pos) || board.IsEmpty(pos)){
                return false;
            }
            else
            {
                return board[pos].Color != Color;
            }
        }
        /*
         * In : Position (Where the pawn placed), Board (Game board)
         * Out: enumerator of moves (forward)
         * Do : Get all the moves a pawn can do
         */
        private IEnumerable<Move> ForwardMoves(Position from, Board board) 
        {
            Position OneStep = from + forward;

            if(CanMoveTo(OneStep, board))
            {
                if(OneStep.Row == 0 || OneStep.Row == 7)
                {
                    foreach(Move promMove in PromotionMoves(from, OneStep))
                    {
                        yield return promMove;
                    }
                }
                else
                {
                    yield return new NormalMove(from, OneStep);
                }

                Position TwoStep = OneStep + forward;
                if(!this.HasMoved && CanMoveTo(TwoStep, board))
                {
                    yield return new NormalMove(from, TwoStep);
                }
            }
        }
        private static IEnumerable<Move> PromotionMoves(Position from ,Position to)
        {
            yield return new PawnPromotion(from,to,PieceType.Rook);
            yield return new PawnPromotion(from, to, PieceType.Bishop);
            yield return new PawnPromotion(from, to, PieceType.Queen);
            yield return new PawnPromotion(from, to, PieceType.Knight);

        }

        /*
         * In : Position (Where the pawn placed), Board (Game board)
         * Out: enumerator of moves (Diagonal)
         * Do : Get all the moves a pawn can do
         */
        private IEnumerable<Move> DiagonalMoves(Position from, Board board)
        {
            foreach (Direction dir in new Direction[] { Direction.Left, Direction.Right, })
            {
                Position to = from + forward + dir;

                if (CanCaptureAt(to, board))
                {
                    if (to.Row == 0 || to.Row == 7)
                    {
                        foreach (Move promMove in PromotionMoves(from, to))
                        {
                            yield return promMove;
                        }
                    }
                    else
                    {
                        yield return new NormalMove(from, to);
                    }
                }
            }
        }
        /*
         * In : Position (Where the pawn placed), Board (Game board)
         * Out: enumerator of moves
         * Do : Get all the moves a pawn can do using ForwardMoves and DiagonalMoves methods
         */
        public override IEnumerable<Move> GetMoves(Position from, Board board)
        {
            return ForwardMoves(from, board).Concat(DiagonalMoves(from, board));
        }
        /*
           * In : Position , Board
           * Out: bool
           * Do : Check if the pawn can captucre an opponent king
           */
        public override bool CanCaptureOpponentKing(Position from, Board board)
        {
            return DiagonalMoves(from, board).Any(move =>
            {
                Piece piece = board[move.ToPos];
                return piece != null && piece.Type == PieceType.King;
            });
        }




    }
}
