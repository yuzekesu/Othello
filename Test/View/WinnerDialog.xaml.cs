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

namespace Test.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class WinnerDialog : Window
    {
        private WinnerDialog()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="WinnerDialog"/> class with specified owner and dimensions,
        /// displaying the winner's name.
        /// </summary>
        /// <remarks>If the specified dimensions are smaller than the minimum required size, the dialog
        /// will default to a width of 800 pixels and a height of 450 pixels. The dialog's size and font are adjusted
        /// based on the provided dimensions.</remarks>
        /// <param name="owner">The window that owns this dialog.</param>
        /// <param name="winner">The name of the winner to display in the dialog.</param>
        /// <param name="window_size_x">The width of the window in pixels. Must be at least 800 pixels.</param>
        /// <param name="window_size_y">The height of the window in pixels. Must be at least 450 pixels.</param>
        public WinnerDialog(Window owner, string winner, double window_size_x, double window_size_y) : this()
        {
            // MessageBox.Show($"width[{window_size_x}], height[{window_size_y}]");
            if (window_size_x < 800 || window_size_y < 450)
            {
                window_size_x = 800;
                window_size_y = 450;
            }
            this.Width = window_size_x / 3;
            this.Height = window_size_y / 6;
            Winner_Dialog.Text = $"Winner is: {winner}";
            this.Owner = owner;

            // how did i get those magical numbers ? 
            // anwser: by testing one by one
            Winner_Dialog.FontSize = window_size_x / 33;
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
