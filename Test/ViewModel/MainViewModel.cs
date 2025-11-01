using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Test.Model;

namespace Test.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Square> ObservSquares { get; private set; }

        public GameManager _gameManager;
        public ICommand DebugCommand { get; private init; }
        public ICommand SquareClickCommand { get; private init; }
        public ICommand NewGameCommand { get; private init; }
        public ICommand ExitCommand { get; private init; }
        public MainViewModel()
        {
            Action<object?> _local_Debug = (something) =>
            {
                //Player player = new Player("Debug Player", "Black");
                //OnGameWon(player);
                OnGameDrawn();
            };
            DebugCommand = new RelayCommand(_local_Debug);

            Action<object?> _local_HandleSquareClick = (something) =>
            {
                // convert something to Square
                // depend on View implementation
                if (something is not Square || something is null)
                {
                    Exception e = new();
                    MessageBox.Show($"Invalid square clicked.\n {e.StackTrace}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                else
                {
                    Square square = (Square)something;
                    HandleSquareClick(square);
                }
            };
            SquareClickCommand = new RelayCommand(_local_HandleSquareClick);
            Action<object?> _local_NewGameCommand = (something) =>
            {
                // convert something to Player1 and Player2
                // depend on View implementation

                Player player1 = new HumanPlayer("p1", "Black");
                Player player2 = new HumanPlayer("p2", "White");

                StartNewGameWithPlayers(player1, player2);
                
            };
            NewGameCommand = new RelayCommand(_local_NewGameCommand);
            Action<object?> _local_ExitCommand = (something) =>
            {
                if (MessageBox.Show("Are you sure you want to exit?", "Exit Game", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    System.Windows.Application.Current.Shutdown();
            };
            ExitCommand = new RelayCommand(_local_ExitCommand);
            StartNewGame();
        }
        public void StartNewGame()
        {
            // new window to get player names and colors
            // then call StartNewGameWithPlayers(p1, p2)
        }
        public void StartNewGameWithPlayers(Player player1, Player player2)
        {
            // Replace the old game manager with a new one
            _gameManager = new GameManager(player1, player2);

            _gameManager.BoardUpdated += OnBoardUpdated;
            _gameManager.GameWon += OnGameWon;
            _gameManager.GameDrawn += OnGameDrawn;

            ObservSquares = new ObservableCollection<Square>(_gameManager.Board.Squares.Cast<Square>());
            OnPropertyChanged(nameof(ObservSquares));

            TryComputerTurn();
        }
        async void TryComputerTurn()
        {
            // waiting GameManager
        }
        void OnBoardUpdated(Square[,] board)
        {
            OnPropertyChanged(nameof(_gameManager.Board.Squares));
            TryComputerTurn();
        }
        void OnGameWon(Player winner)
        {
            //WinnerDialog w = new(Application.Current.MainWindow, winner.Name, Application.Current.MainWindow.Width, Application.Current.MainWindow.Height);
        }
        void OnGameDrawn()
        {
            //DrawnDialog d = new(Application.Current.MainWindow, Application.Current.MainWindow.Width, Application.Current.MainWindow.Height);
        }
        public void HandleSquareClick(Square square)
        {
            _gameManager.PlayMove(square.Row, square.Column);
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
