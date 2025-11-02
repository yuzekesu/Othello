using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Othello.View
{
    /// <summary>
    /// Interaction logic for DrawnDialog.xaml
    /// </summary>
    /// <remarks>
    /// A simple copy paste from <see cref="WinnerDialog"/>.
    /// </remarks>
    public partial class DrawnDialog : Window
    {
        private DrawnDialog()
        {
            InitializeComponent();
        }
        public DrawnDialog(Window owner, double window_size_x, double window_size_y) : this()
        {
            // MessageBox.Show($"width[{window_size_x}], height[{window_size_y}]");
            if (window_size_x < 800 || window_size_y < 450)
            {
                window_size_x = 800;
                window_size_y = 450;
            }
            this.Width = window_size_x / 3;
            this.Height = window_size_y / 6;
            this.Owner = owner;

            Drawn_Dialog.FontSize = window_size_x / 33;
            Okej_Button.FontSize = window_size_x / 33;
            Okej_Button.Width = window_size_x / 10;
            Okej_Button.Height = window_size_x / 26;

            this.ShowDialog();
        }
        private void CloseTheDialog(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
