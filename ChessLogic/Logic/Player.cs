namespace ChessLogic
{
    // Enum for the different players
    public enum Player
    {
        White,
        Black,
        None
    }
    // Extension methods for the player
    public static class PlayerExtensions
    {
        /*
         * In : Player
         * Out: Player
         * Do : Change the turn to the opponent turn
         */
        public static Player Opponent(this Player player)
        {
            return player switch
            {
                Player.White => Player.Black,
                Player.Black => Player.White,
                _ => Player.None,
            };
        }
    }
}
