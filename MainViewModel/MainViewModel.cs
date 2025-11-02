using Othello.Model;
using Othello.View;
using Othello.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Othello.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Square> ObservSquares { get; private set; }
        public string CurrentPlayerName { get; private set; }
        private GameManager _gameManager;
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
            DebugCommand = new RelayCommand<object?>(_local_Debug);

            Action<Square> _local_HandleSquareClick = (square) =>
            {
                // convert something to Square
                // depend on View implementation
                HandleSquareClick(square);
            };
            SquareClickCommand = new RelayCommand<Square>(_local_HandleSquareClick);
            Action<object?> _local_NewGameCommand = (something) =>
            {
                // convert something to Player1 and Player2
                // depend on View implementation


                SetupGameDialog set_up_dialog = new SetupGameDialog();
                set_up_dialog.ShowDialog();
                Player player1 = new ComputerPlayer("Error", "White");
                Player player2 = new ComputerPlayer("Error", "Black");
                if (set_up_dialog.Result != null) 
                {
                    player1 = set_up_dialog.Result.Player1Type == PlayerType.Human ? new HumanPlayer(set_up_dialog.Result.Player1Name, "Black") : new ComputerPlayer(set_up_dialog.Result.Player1Name, "Black");
                    player2 = set_up_dialog.Result.Player2Type == PlayerType.Human ? new HumanPlayer(set_up_dialog.Result.Player2Name, "White") : new ComputerPlayer(set_up_dialog.Result.Player2Name, "White");
                }
                StartNewGameWithPlayers(player1, player2);
            };
            NewGameCommand = new RelayCommand<object?>(_local_NewGameCommand);
            Action<object?> _local_ExitCommand = (something) =>
            {
                if (MessageBox.Show("Are you sure you want to exit?", "Exit Game", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    System.Windows.Application.Current.Shutdown();
            };
            ExitCommand = new RelayCommand<object?>(_local_ExitCommand);
        }
        public void StartNewGame()
        {
            ObservSquares = new ObservableCollection<Square>(_gameManager.Board.Squares.Cast<Square>());
            OnPropertyChanged(nameof(ObservSquares));
            TryComputerTurn();
        }
        public void StartNewGameWithPlayers(Player player1, Player player2)
        {
            // Replace the old game manager with a new one
            _gameManager = new GameManager(player1, player2);
            _gameManager.BoardUpdated += OnBoardUpdated;
            _gameManager.GameWon += OnGameWon;
            _gameManager.GameDrawn += OnGameDrawn;
            CurrentPlayerName = _gameManager.CurrentPlayer.Color.ToString();
            OnPropertyChanged(nameof(CurrentPlayerName));
            StartNewGame();
        }
        async void TryComputerTurn()
        {
            _gameManager.TryComputerMoveAsync();
        }
        void OnBoardUpdated(Square[,] board)
        {
            CurrentPlayerName = _gameManager.CurrentPlayer.Color;
            OnPropertyChanged(nameof(CurrentPlayerName));
            OnPropertyChanged(nameof(ObservSquares));
            TryComputerTurn();
            for (int i = 0; i < 64; i++)
            {
                ObservSquares[i].Color = ObservSquares[i].Color;
            }
        }
        void OnGameWon(Player winner)
        {
            WinnerDialog w = new(Application.Current.MainWindow, winner.Name, Application.Current.MainWindow.Width, Application.Current.MainWindow.Height);
        }
        void OnGameDrawn()
        {
            DrawnDialog d = new(Application.Current.MainWindow, Application.Current.MainWindow.Width, Application.Current.MainWindow.Height);
        }
        public void HandleSquareClick(Square square)
        {
            if (_gameManager.CurrentPlayer is HumanPlayer)
            _gameManager.PlayMove(square.Row, square.Column);
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
