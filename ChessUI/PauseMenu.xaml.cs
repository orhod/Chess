using System.Windows;
using System.Windows.Controls;

namespace ChessUI
{
    /// <summary>
    /// Interaction logic for PauseMenu.xaml
    /// </summary>
    public partial class PauseMenu : UserControl
    {
        public event Action<Option> OptionSelected;
        public PauseMenu()
        {
            InitializeComponent();
        }
        /*
         * In : -
         * Out: -
         * Do : Event handler for when the Exit button is clicked
         */
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            OptionSelected?.Invoke(Option.Exit);
        }
        /*
         * In : -
         * Out: -
         * Do : Event handler for when the Continue button is clicked
         */
        private void Continue_Click(object sender, RoutedEventArgs e)
        {
            OptionSelected?.Invoke(Option.Continue);
        }
        /*
         * In : -
         * Out: -
         * Do : Event handler for when the Restart button is clicked
         */
        private void Restart_Click(object sender, RoutedEventArgs e)
        {
            OptionSelected?.Invoke(Option.Restart);
        }
    }
}
