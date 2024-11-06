using System.IO;
using System.Windows;
using System.Windows.Input;

namespace ChessUI
{
    public static class ChessCursors
    {
        // save the cursors as static readonly fields to prevent loading them multiple times
        public static readonly Cursor WhiteCursor = LoadCursor("Assets/CursorW.cur");
        public static readonly Cursor BlackCursor = LoadCursor("Assets/CursorB.cur");
        /*
         * In : string
         * Out: Cursor
         * Do : Load a cursor from a file
         */
        private static Cursor LoadCursor(string filePath)
        {
            Stream stream = Application.GetResourceStream(new Uri(filePath, UriKind.Relative)).Stream;
            return new Cursor(stream, true);
        }
    }
}
