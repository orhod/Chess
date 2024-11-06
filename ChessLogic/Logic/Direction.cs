namespace ChessLogic
{
    public class Direction
    {
        // Properties
        public int RowDel { get; }
        public int ColumnDel { get; }
        // Static directions
        public readonly static Direction Up = new Direction(-1, 0);
        public readonly static Direction Down = new Direction(1, 0);
        public readonly static Direction Left = new Direction(0, -1);
        public readonly static Direction Right = new Direction(0, 1);
        public readonly static Direction UpLeft = Up + Left;
        public readonly static Direction UpRight = Up + Right;
        public readonly static Direction DownLeft = Down + Left;
        public readonly static Direction DownRight = Down + Right;

        /*
         * In : rowDel (The change in row), columnDel (The change in column)
         * Out: -
         * Do : Constructor to create a direction
         */
        public Direction(int rowDel, int columnDel)
        {
            this.RowDel = rowDel;
            this.ColumnDel = columnDel;
        }
        /*
         * In : dir1 (The first direction), dir2 (The second direction)
         * Out: A new direction
         * Do : Add two directions together by adding the row and column changes
         */
        public static Direction operator +(Direction dir1, Direction dir2)
        {
            return new Direction(dir1.RowDel + dir2.RowDel, dir1.ColumnDel + dir2.ColumnDel);
        }
        /*
         * In : scalar (The scalar to multiply with), dir (The direction)
         * Out: A new direction
         * Do : Multiply a direction with a scalar by multiplying the row and column changes
         */
        public static Direction operator *(int scalar, Direction dir)
        {
            return new Direction(scalar * dir.RowDel, scalar * dir.ColumnDel);
        }

    }
}
