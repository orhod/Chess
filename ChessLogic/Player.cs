﻿namespace ChessLogic
{
    // enum of posible kind of player/squer color
    public enum Player
    {
        White,
        Black,
        None
    }
    public static class PlayerExtensions
    {
        // Next player
        public static Player Opponent (this Player player)
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
