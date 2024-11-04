using System.Windows.Controls;
using System.Windows.Input;
using ChessLogic;

namespace ChessUI
{
    /// <summary>
    /// Interaction logic for PromotionMenu.xaml
    /// </summary>
    public partial class PromotionMenu : UserControl
    {
        // Event for when a piece is selected
        public event Action<PieceType> PieceSelected;
        /*
         * In : Player player
         * Out: -
         * Do : Constructor for the promotion menu
         */
        public PromotionMenu(Player player)
        {
            InitializeComponent();
            QueenImg.Source = Images.GetImage(player, PieceType.Queen);
            RookImg.Source = Images.GetImage(player, PieceType.Rook);
            BishopImg.Source = Images.GetImage(player, PieceType.Bishop);
            KnightImg.Source = Images.GetImage(player, PieceType.Knight);

        }
        /*
         * In : -
         * Out: -
         * Do : Event handler for when the Queen image is clicked
         */
        private void QueenImg_MouseDown(object sender, MouseButtonEventArgs e)
        {
            PieceSelected?.Invoke(PieceType.Queen);
        }
        /*
         * In : -
         * Out: -
         * Do : Event handler for when the Rook image is clicked
         */
        private void RookImg_MouseDown(object sender, MouseButtonEventArgs e)
        {
            PieceSelected?.Invoke(PieceType.Rook);
        }
        /*
         * In : -
         * Out: -
         * Do : Event handler for when the Bishop image is clicked
         */
        private void BishopImg_MouseDown(object sender, MouseButtonEventArgs e)
        {
            PieceSelected?.Invoke(PieceType.Bishop);
        }
        /*
         * In : -
         * Out: -
         * Do : Event handler for when the Knight image is clicked
         */
        private void KnightImg_MouseDown(object sender, MouseButtonEventArgs e)
        {
            PieceSelected?.Invoke(PieceType.Knight);
        }
    }
}
