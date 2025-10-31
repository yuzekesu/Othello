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
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class GameGrid : UserControl
    {
        public const int Size = 8;
        private readonly DependencyProperty SquareClickCommandProperty =
            DependencyProperty.Register(
                nameof(SquareClickCommand),
                typeof(ICommand),
                typeof(GameGrid),
                new PropertyMetadata(null, OnSquareClickCommandChanged));

        public ICommand? SquareClickCommand
        {
            get => (ICommand?)GetValue(SquareClickCommandProperty);
            set => SetValue(SquareClickCommandProperty, value);
        }

        private readonly Button[,] _buttons = new Button[Size, Size];

        public GameGrid()
        {
            InitializeComponent();
            BuildBoard();
            ShowInitialDiscs();
        }

        private static void OnSquareClickCommandChanged(DependencyObject d, 
            DependencyPropertyChangedEventArgs e)
        {
            if(d is GameGrid grid)
            {
                for(int r = 0; r < Size; r++)
                {
                    for(int c = 0; c < Size;)
                    {
                        var btn = grid._buttons[r, c];
                        if(btn != null)
                        {
                            btn.Command = grid.SquareClickCommand;
                        }
                    }
                }
            }
        }

        private void BuildBoard()
        {
            OthelloGrid.Rows = Size;
            OthelloGrid.Columns = Size;
            OthelloGrid.Children.Clear();

            var dark = (Brush)new SolidColorBrush(Color.FromRgb(34, 104, 69));
            var light = (Brush)new SolidColorBrush(Color.FromRgb(43, 132, 88));

            for (int r = 0; r < Size; r++)
            {
                for(int c = 0; c < Size; c++)
                {
                    var btn = new Button()
                    {
                        Margin = new Thickness(1),
                        Background = ((r + c) % 2 == 0) ? dark : light,
                        Command = SquareClickCommand,
                        CommandParameter = (r, c)
                    };

                    _buttons[r, c] = btn;
                    OthelloGrid.Children.Add(btn);

                }
            }
        }

        public void SetCell(int row, int col, int value)
        {
            if (row < 0 || row >= Size || col < 0 || col >= Size) return;

            var btn = _buttons[row, col];
            btn.Content = null;

            if (value == 0) return;

            var disc = new Ellipse
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                Margin = new Thickness(6),
                Fill = (value == 1) ? Brushes.Black : Brushes.White,
                Stroke = Brushes.Black,
                StrokeThickness = 1
            };

            btn.Content = disc;
        }

        public void SetBoard(int[,] board)
        {
            if (board == null || 
                board.GetLength(0) != Size || 
                board.GetLength(1) != Size)
                throw new ArgumentException("Board must be 8x8");

            for(int r = 0; r < Size; r++)
            {
                for(int c = 0; c < Size; c++)
                {
                    SetCell(r, c, board[r, c]);
                }
            }
        }

        private void ShowInitialDiscs()
        {
            SetCell(3, 3, 2);
            SetCell(4, 4, 2);
            SetCell(3, 4, 1);
            SetCell(4, 3, 1);

        }

    }
}
