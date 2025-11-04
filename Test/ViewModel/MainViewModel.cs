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
using Test.View;

namespace Test.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// the obserbable that has direct link to the GameBoard.
        /// </summary>
        public ObservableCollection<Square>? ObservSquares { get; private set; }

        public ObservableCollection<Square>? Hint { get; private set; }

        public string CurrentPlayerColor { get { return _currentPlayerColor; } private set { _currentPlayerColor = value; OnPropertyChanged(nameof(CurrentPlayerColor)); } }
        private string _currentPlayerColor = string.Empty;

        /// <summary>
        /// for the animation in the view
        /// </summary>
        private string _lastColor = "Transparent";

        private GameManager? _gameManager;
        public ICommand SquareClickCommand { get; private init; }
        public ICommand NewGameCommand { get; private init; }
        public ICommand ExitCommand { get; private init; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <remarks>
        /// Currently only initializing all the <see cref="ICommand"/> here.
        /// </remarks>
        public MainViewModel()
        {
            Action<Square> _local_HandleSquareClick = (square) =>
            {
                // convert something to Square
                // depend on View implementation
                if (_gameManager == null) return;
                if (square.Row == -1) return;
                var valids = _gameManager.Board.GetValidMoves(_gameManager.CurrentPlayer.Color);
               
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
                    StartNewGameWithPlayers(player1, player2);
                }
            };
            NewGameCommand = new RelayCommand<object?>(_local_NewGameCommand);

            Action<object?> _local_ExitCommand = (something) =>
            {
                if (MessageBox.Show("Are you sure you want to exit?", "Exit Game", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    System.Windows.Application.Current.Shutdown();
            };
            ExitCommand = new RelayCommand<object?>(_local_ExitCommand);
        }


        /// <summary>
        /// Initializes a new game session with the specified players.
        /// </summary>
        /// <remarks>This method sets up a new game by creating a new instance of <see
        /// cref="GameManager"/> with the provided players. It also subscribes to game events such as board updates,
        /// game wins, and draws. The current player's color is updated, and the observable collection of squares is
        /// refreshed to reflect the new game state.</remarks>
        /// <param name="player1">The first player. Cannot be null.</param>
        /// <param name="player2">The second player. Cannot be null.</param>
        public void StartNewGameWithPlayers(Player player1, Player player2)
        {
            // Unsubscribe
            if (_gameManager != null)
            {
                _gameManager.BoardUpdated -= OnBoardUpdated;
                _gameManager.GameWon -= OnGameWon;
                _gameManager.GameDrawn -= OnGameDrawn;
            }

            // Replace the old game manager with a new one
            _gameManager = new GameManager(player1, player2);
            _gameManager.BoardUpdated += OnBoardUpdated;
            _gameManager.GameWon += OnGameWon;
            _gameManager.GameDrawn += OnGameDrawn;
            CurrentPlayerColor = _gameManager.CurrentPlayer.Color.ToString();
            ObservSquares = new ObservableCollection<Square>(_gameManager.Board.Squares.Cast<Square>());
            
            OnPropertyChanged(nameof(ObservSquares));
            TryComputerTurn();
        }


        /// <summary>
        /// Do the ComputerPlayer turn.
        /// </summary>
        async void TryComputerTurn()
        {
            if (_gameManager == null) return;
            if (_gameManager.CurrentPlayer is ComputerPlayer)
            await _gameManager.TryComputerMoveAsync();
        }


        /// <summary>
        /// Handles updates to the game board and triggers necessary actions.
        /// </summary>
        /// <remarks>This method updates the current player's color in the view, refreshes hints, notifies observers for the play board of changes, and initiates the computer's turn if applicable.</remarks>
        /// <param name="board">The current state of the game board represented as a two-dimensional array of <see cref="Square"/> objects.</param>
        void OnBoardUpdated(Square[,] board)
        {
            if (_gameManager == null) return;
            CurrentPlayerColor = _gameManager.CurrentPlayer.Color.ToString();
 
            OnPropertyChanged(nameof(ObservSquares));
            TryComputerTurn();
        }


        /// <summary>
        /// Displays a dialog indicating that the game has been won by a player.
        /// </summary>
        /// <remarks>This method creates and shows a dialog window to announce the winner of the game. The
        /// dialog is centered on the main application window.</remarks>
        /// <param name="winner">The player who won the game. Cannot be null.</param>
        void OnGameWon(Player winner)
        {
            //UpdateHint(true);
            WinnerDialog w = new(Application.Current.MainWindow, winner.Name, Application.Current.MainWindow.Width, Application.Current.MainWindow.Height);
        }


        /// <summary>
        /// Handles the event when the game is drawn, initializing and displaying a dialog.
        /// </summary>
        /// <remarks>This method creates a new instance of <see cref="DrawnDialog"/> using the main
        /// window's dimensions.</remarks>
        void OnGameDrawn()
        {
            //UpdateHint(true);
            DrawnDialog d = new(Application.Current.MainWindow, Application.Current.MainWindow.Width, Application.Current.MainWindow.Height);
        }


        /// <summary>
        /// Handle the players click.
        /// </summary>
        /// <param name="square">The square that the player clicked</param>
        public void HandleSquareClick(Square square)
        {
            if (_gameManager == null) return;
            if (_gameManager.CurrentPlayer is HumanPlayer)
                _gameManager.PlayMove(square.Row, square.Column);
        }

        /// <summary>
        /// Nothing Special here
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Update the hint for the current turn. Do call it at the beginning of each turn.
        /// </summary>
        private void UpdateHint(bool reset = false)
        {
            if (reset)
            {
                Hint = new ObservableCollection<Square>();
                OnPropertyChanged(nameof(Hint));
                return;
            }
            if (_gameManager == null) return;
            if (ObservSquares == null) return;

            // haha, i dont know how to do this otherwise.
            // the gettype does work for comparison ;(
            // i read this "pattern maching" from the course book page 166 in case u wonder.
            // it skips bot turn for displaying the hints.
            switch (_gameManager.CurrentPlayer)
            {
                case ComputerPlayer _: return;
                default:
                    break;
            }
            Hint = new ObservableCollection<Square>();
            List<Square> temp_hints = _gameManager.Board.GetValidMoves(CurrentPlayerColor);
            if (temp_hints != null)
            {
                for (int i = 0; i < 64; i++)
                {
                    Square new_square = new Square();
                    new_square.Color = "Transparent";
                    //new_square.Color = ObservSquares[i].Color != "White" && ObservSquares[i].Color != "Black" ? "Transparent" : ObservSquares[i].Color;
                    new_square.Row = ObservSquares[i].Row;
                    new_square.Column = ObservSquares[i].Column;
                    foreach (Square square_hint in temp_hints)
                    {
                        if ((square_hint.Row * 8) + square_hint.Column == i)
                        {
                            new_square.Color = _gameManager.CurrentPlayer.Color == "Black" ? "Turquoise" : "Tomato";
                            break;
                        }
                    }
                    Hint.Add(new_square);
                }
            }
            OnPropertyChanged(nameof(Hint));
        }
        
        
    }
}
