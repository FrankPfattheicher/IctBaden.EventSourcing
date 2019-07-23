using System.Windows;
using System.Windows.Controls;

namespace TicTacToe.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void FieldClick(object sender, RoutedEventArgs e)
        {
            var button = (Button) sender;
            var uid = int.Parse(button.Uid);
            var row = (int)(uid / 3);
            var col = (int)(uid % 3);

            var gameVm = (GameViewModel)DataContext;
            gameVm.OnClick(row, col);
        }

        private void NewGame(object sender, RoutedEventArgs e)
        {
            var gameVm = (GameViewModel)DataContext;
            gameVm.OnNewGame();
        }
    }
}
