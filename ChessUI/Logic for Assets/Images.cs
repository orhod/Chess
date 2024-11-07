using ChessLogic;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ChessUI
{
    public static class Images
    {
        // Dictionary of white piece images
        private static readonly Dictionary<PieceType, ImageSource> whiteSources = new()
        {
            { PieceType.Pawn,LoadImage("Assets/PawnW.png")},
            { PieceType.Rook,LoadImage("Assets/RookW.png")},
            { PieceType.Bishop,LoadImage("Assets/BishopW.png")},
            { PieceType.Knight,LoadImage("Assets/KnightW.png")},
            { PieceType.Queen,LoadImage("Assets/QueenW.png")},
            { PieceType.King,LoadImage("Assets/KingW.png")}
        };
        // Dictionary of black piece images
        private static readonly Dictionary<PieceType, ImageSource> blackSources = new()
        {
            { PieceType.Pawn,LoadImage("Assets/PawnB.png")},
            { PieceType.Rook,LoadImage("Assets/RookB.png")},
            { PieceType.Bishop,LoadImage("Assets/BishopB.png")},
            { PieceType.Knight,LoadImage("Assets/KnightB.png")},
            { PieceType.Queen,LoadImage("Assets/QueenB.png")},
            { PieceType.King,LoadImage("Assets/KingB.png")}
        };
        /*
         * In : string (Path to the image)
         * Out: ImageSource (The image)
         * Do : Load an image from the path
         */
        private static ImageSource LoadImage(string filePath)
        {
            return new BitmapImage(new Uri(filePath, UriKind.Relative));
        }
        /*
         * In : Player (Color of the piece), PieceType (Type of the piece)
         * Out: ImageSource (The image)
         * Do : Get the correct image for each piece
         */
        public static ImageSource GetImage(Player color, PieceType type)
        {
            return color switch
            {
                Player.Black => blackSources[type],
                Player.White => whiteSources[type],
                _ => null
            };
        }
        /*
         * In : Piece
         * Out: ImageSource (The image)
         * Do : Get the correct image for a piece
         */
        public static ImageSource GetImage(Piece piece)
        {
            if (piece == null)
            {
                return null;
            }
            return GetImage(piece.Color, piece.Type);
        }
    }
}
