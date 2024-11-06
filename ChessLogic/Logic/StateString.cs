using System.Text;

namespace ChessLogic
{
    public class StateString
    {
        // Properties
        private readonly StringBuilder sb = new StringBuilder();
        /*
         * In : Board, Player
         * Out: -
         * Do : Constructor to create a string representation of the game state
         */
        public StateString(Board board, Player currentPlayer)
        {
            // Add piece placement Data
            AddPiecePlacemnet(board);
            sb.Append(' ');
            // Add current player
            AddCurrentPlayer(currentPlayer);
            sb.Append(' ');
            // Add Castling Rights
            AddCastleRights(board);
            sb.Append(' ');
            // Add en Passant data
            AddEnPassant(board, currentPlayer);
        }
        /*
         * In : -
         * Out: string
         * Do : Convert the string builder to a string
         */
        public override string ToString()
        {
            return sb.ToString();
        }
        /*
         * In : Piece
         * Out: char
         * Do : Convert a piece to char (black piece lower case, White Piece upper case)
         */
        private static char PieceChar(Piece piece)
        {
            char c = piece.Type switch
            {
                PieceType.Pawn => 'p',
                PieceType.Rook => 'r',
                PieceType.Bishop => 'b',
                PieceType.Knight => 'n',
                PieceType.King => 'k',
                PieceType.Queen => 'q',
                _ => ' '
            };

            if (piece.Color == Player.White)
            {
                return char.ToUpper(c);
            }
            return c;
        }
        /*
         * In : Board, int
         * Out: -
         * Do: Add the row data to the string builder
         */
        private void AddRowData(Board board, int row)
        {
            int empty = 0;
            for (int c = 0; c < 8; c++)
            {
                if (board[row, c] == null)
                {
                    empty++;
                    continue;
                }

                if (empty > 0)
                {
                    sb.Append(empty);
                    empty = 0;
                }
                sb.Append(PieceChar(board[row, c]));
            }
            if (empty > 0)
            {
                sb.Append(empty);
            }

        }
        /*
         * In : Board
         * Out: -
         * Do : Add the piece placement data to the string builder
         */
        private void AddPiecePlacemnet(Board board)
        {
            for (int r = 0; r < 8; r++)
            {
                if (r != 0)
                {
                    sb.Append('/');
                }

                AddRowData(board, r);
            }
        }
        /*
         * In : Player (Current player)
         * Out: -
         * Do : Add the current player to the string builder
         */
        private void AddCurrentPlayer(Player currentPlayer)
        {
            if (currentPlayer == Player.White)
            {
                sb.Append('w');
            }
            else
            {
                sb.Append('b');
            }
        }
        /*
         * In : Board
         * Out: -
         * Do : Add the castling rights to the string builder
         */
        private void AddCastleRights(Board board)
        {
            bool castleWKS = board.CastleRightKs(Player.White);
            bool castleWQS = board.CastleRightQs(Player.White);
            bool castleBKS = board.CastleRightKs(Player.Black);
            bool castleBQS = board.CastleRightQs(Player.Black);

            if (!(castleWKS || castleWQS || castleBKS || castleBQS))
            {
                sb.Append('-');
                return;
            }
            if (castleWKS)
            {
                sb.Append('K');
            }
            if (castleWQS)
            {
                sb.Append('Q');
            }
            if (castleBKS)
            {
                sb.Append('k');
            }
            if (castleBQS)
            {
                sb.Append('q');
            }
        }
        /*
         * In : Board, Player
         * Out: -
         * Do : Add the en passant data to the string builder
         */
        private void AddEnPassant(Board board, Player currentPlayer)
        {
            if (!board.CanCaptureEnPassant(currentPlayer))
            {
                sb.Append('-');
                return;
            }

            Position pos = board.GetPawnSkipPosition(currentPlayer.Opponent());

            char file = (char)('a' + pos.Column);
            int rank = 8 - pos.Row;
            sb.Append(file);
            sb.Append(rank);
        }
    }
}
