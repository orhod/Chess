using System.Windows.Media;
using System.Windows.Media.Imaging;
using ChessLogic;

namespace ChessUI
{
    public static class Images
    {
        // Set A HashMap for white pieces images
        private static readonly Dictionary<PieceType, ImageSource> whiteSources = new()
        {
            { PieceType.Pawn,LoadImage("Assets/PawnW.png")},
            { PieceType.Rook,LoadImage("Assets/RookW.png")},
            { PieceType.Bishop,LoadImage("Assets/BishopW.png")},
            { PieceType.Knight,LoadImage("Assets/KnightW.png")},
            { PieceType.Queen,LoadImage("Assets/QueenW.png")},
            { PieceType.King,LoadImage("Assets/KingW.png")}
        };
        // Set A HashMap for black pieces images
        private static readonly Dictionary<PieceType, ImageSource> blackSources = new()
        {
            { PieceType.Pawn,LoadImage("Assets/PawnB.png")},
            { PieceType.Rook,LoadImage("Assets/RookB.png")},
            { PieceType.Bishop,LoadImage("Assets/BishopB.png")},
            { PieceType.Knight,LoadImage("Assets/KnightB.png")},
            { PieceType.Queen,LoadImage("Assets/QueenB.png")},
            { PieceType.King,LoadImage("Assets/KingB.png")}
        };

        // Load The Images Reletive to the Project
        private static ImageSource LoadImage(string filePath)
        {
            return new BitmapImage(new Uri(filePath, UriKind.Relative));
        }
        // Switch case to know where to find the Piece Image
        public static ImageSource GetImage(Player color, PieceType type)
        {
            return color switch
            {
                Player.Black => blackSources[type],
                Player.White => whiteSources[type],
                _ => null
            };
        }
        // Get the correct Image for each piece
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
