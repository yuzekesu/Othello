using Othello.Model;
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
    /// Interaction logic for TempGrid.xaml
    /// </summary>
    public partial class TempGrid : UserControl
    {
        public TempGrid()
        {
            InitializeComponent();
        }
        public void MakeGrid(GameBoard Board, ICommand command) 
        {
            //if (Board == null) return;
            //UniformGrid.Rows = 8;
            //UniformGrid.Columns = 8;
            //for (int i = 0; i < 8; i++)
            //{
            //    for (int j = 0; j < 8; j++)
            //    {
            //        Button btn = new Button();
            //        btn.Margin = new Thickness(1);
            //        btn.Background = Brushes.Green;
            //        btn.Background = Board.Squares[i, j].Color == "Black" ? Brushes.Black :
            //                         Board.Squares[i, j].Color == "White" ? Brushes.White :
            //                         Brushes.Green;
            //        btn.Command = command;
            //        btn.CommandParameter = Board.Squares[i, j];
            //        UniformGrid.Children.Add(btn);
            //    }
            //}
        }
    }
}
