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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Othello.View
{
    /// <summary>
    /// Interaction logic for MonoDiskGrid.xaml
    /// </summary>
    /// <remarks>
    /// This display only the last discs and its animation on the game board.
    /// </remarks>
    public partial class MonoDiskGrid : UserControl
    {
        public MonoDiskGrid()
        {
            InitializeComponent();
        }
    }
}
