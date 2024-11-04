using System.Windows;
using System.Windows.Controls;
using ChessLogic;

namespace ChessUI
{
    /// <summary>
    /// Interaction logic for GameOverMenu.xaml
    /// </summary>
    public partial class GameOverMenu : UserControl
    {
        public event Action<Option> OptionSelected;
        public GameOverMenu(GameState gameState)
        {
            InitializeComponent();
            Result result = gameState.Result;
            WinnerText.Text = GetWinnerText(result.Winner);
            ReasonText.Text = GetReasonText(result.Reason, gameState.CurrentPlayer);
        }
        /*
         * In : Player (Winner)
         * Out: string
         * Do : Return a string that represent who won the game
         */
        private static string GetWinnerText(Player winner)
        {
            return winner switch
            {
                Player.White => "WHITE WINS!",
                Player.Black => "BLACK WINS!",
                _ => "IT'S A DRAW"
            };
        }
        /*
         * In : Player (Winner)
         * Out: string
         * Do : ToString method
         */
        private static string PlayerString(Player player)
        {
            return player switch 
            { 
                Player.White => "WHITE",
                Player.Black => "BLACK",
                _ => "" 
            };
        }
        /*
         * In : EndReason , Player
         * Out: string
         * Do : ToString of the reason the game ended
         */
        private static string GetReasonText(EndReason reason, Player currentPlayer)
        {
            return reason switch
            {
                EndReason.Stalemate =>$"STALEMATE - {PlayerString(currentPlayer)} CAN'T MOVE",
                EndReason.Checkmate => $"CHECKMATE - {PlayerString(currentPlayer)} CAN'T MOVE",
                EndReason.ThreefoldRepatition => "THREEFOLD REPETITION",
                EndReason.InsuffcientMaterial => "INSUFFICIENT MATERIAL",
                EndReason.FiftyMoveRule => "FIFTY-MOVE RULE",
                _ => ""

            };
        }
        /*
         * Do: Exit the game after click on exit button
         */
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            OptionSelected?.Invoke(Option.Exit);
        }
        /*
         * Do: Restart the game after click on restart button
         */
        private void Restart_Click(object sender, RoutedEventArgs e)
        {
            OptionSelected?.Invoke(Option.Restart);
        }
    }
}
