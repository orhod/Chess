using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLogic
{
    // Pawn promotion move
    public class PawnPromotion : Move
    {
        // Properties
        public override MoveType Type => MoveType.PawnPromotion;

        public override Position FromPos { get; }
        public override Position ToPos { get; }

        private readonly PieceType newType;
        /*
         * In : Position fromPos, Position toPos, PieceType newType
         * Out: -
         * Do : Constructor
         */
        public PawnPromotion(Position fromPos, Position toPos, PieceType newType)
        {
            FromPos = fromPos;
            ToPos = toPos;
            this.newType = newType;
        }
        /*
         * In : Player Color
         * Out: Piece
         * Do : Create a new piece of the type Selected
         */
        private Piece CreatePromotionPiece(Player Color)
        {
            return newType switch
            {
                PieceType.Knight => new Knight(Color),
                PieceType.Rook => new Rook(Color),
                PieceType.Bishop => new Bishop(Color),
                _ => new Queen(Color),
            };
        }
        /*
         * In : Board
         * Out: -
         * Do : Execute the move replacing the pawn with the new piece
         */
        public override void Execite(Board board)
        {
            Piece pawn = board[FromPos];
            board[FromPos] = null;
            Piece promotionPiece = CreatePromotionPiece(pawn.Color);
            promotionPiece.HasMoved = true;
            board[ToPos] = promotionPiece;

        }
    }
}
