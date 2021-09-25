using static ChessConsole.ConsoleUI;
// Initialise board and setup variables for taking user input

Console.Title = "DotnetChess";
var chessBoard = new ChessBoard();
chessBoard.StateChanged += UpdateChessUI;
chessBoard.Init();

do
{
    (int userX, int userY) = GetUserXAndY();

    SetBoard(userX, userY, chessBoard);

    chessBoard.GetLegalMoves(userX, userY);

} while (true);