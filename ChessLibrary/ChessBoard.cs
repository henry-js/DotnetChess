using System.Diagnostics;
using System.Threading.Tasks.Dataflow;

namespace ChessLibrary;

public delegate void UpdateUI(ChessBoard chessBoard);

public class ChessBoard
{
    public Cell[,] BoardState { get; set; } = new Cell[8, 8];

    public ChessBoard()
    {
        for (int y = BoardState.GetLength(0) - 1; y >= 0; y--)
        {
            for (int x = 0; x < BoardState.GetLength(1); x++)
            {

                var colour = ((y + x) % 2 == 0) ? ConsoleColor.Black : ConsoleColor.White;
                var cell = new Cell(colour, (File)x, y);
                BoardState[x, y] = cell;
            }
        }

        var cellNumber = 0;
        foreach (var item in BoardState)
        {
            item.CellNumber = cellNumber++;
        }

    }
    /// <summary>
    /// Add a function to this event to be called whenever the board state changes
    /// </summary>
    public event UpdateUI? StateChanged;


    /// <summary>
    /// Call to initialize chessboard for a game.
    /// </summary>
    public void Init()
    {
        // TODO: remove position/Point variables
        var pieceColour = ConsoleColor.White;
        var position = new Point();
        for (int y = BoardState.GetLength(0) - 1; y >= 0; y--)
        {
            for (int x = 0; x < 8; x++)
            {
                ResetPosition();
                position.Offset(x, y);

                // Place pawns
                if (y == 1)
                {
                    pieceColour = ConsoleColor.White;
                    BoardState[x, y].ChessPiece = new Pawn(pieceColour);
                    Debug.WriteLine($"{BoardState[x, y].ChessPiece?.Name}: ${BoardState[x, y].ChessPiece?.Name}");
                    continue;
                }
                else if (y == 6)
                {
                    pieceColour = ConsoleColor.Black;
                    BoardState[x, y].ChessPiece = new Pawn(pieceColour);
                    Debug.WriteLine($"{BoardState[x, y].ChessPiece?.Name}: ${BoardState[x, y].ChessPiece?.Name}");
                    continue;
                }
                // TODO: Row 8 black pieces
                if (y == 0)
                {
                    pieceColour = ConsoleColor.White;
                    PlaceOtherPieces(BoardState, x, y, pieceColour);
                    Debug.WriteLine($"{BoardState[x, y].ChessPiece?.Name}: ${BoardState[x, y].ChessPiece?.Name}");
                    continue;
                }
                if (y == 7)
                {
                    pieceColour = ConsoleColor.Black;
                    PlaceOtherPieces(BoardState, x, y, pieceColour);
                    Debug.WriteLine($"{BoardState[x, y].ChessPiece?.Name}: ${BoardState[x, y].ChessPiece?.Name}");
                    continue;
                }
                // Reseet position
            }
        }
        void ResetPosition()
        {
            position.X = 0;
            position.Y = 0;
        }

        void PlaceOtherPieces(Cell[,] board, int x, int y, ConsoleColor pieceColour)
        {
            switch (x)
            {
                case 0:
                case 7:
                    BoardState[x, y].ChessPiece = new Rook(pieceColour);
                    break;

                case 1:
                case 6:
                    BoardState[x, y].ChessPiece = new Knight(pieceColour);
                    break;

                case 2:
                case 5:
                    BoardState[x, y].ChessPiece = new Bishop(pieceColour);
                    break;

                case 3:
                    BoardState[x, y].ChessPiece = new Queen(pieceColour);
                    break;

                case 4:
                    BoardState[x, y].ChessPiece = new King(pieceColour);
                    break;
            }
        }
        StateChanged?.Invoke(this);
    }

    /// <summary>
    /// Finds the piece that is in cell(x,y) and maps legal moves from that cell
    /// onto the board.
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>   
    public void GetLegalMoves(in int x, in int y)
    {
        var currentY = y;
        var currentX = x;
        // Reset legal move markers
        foreach (var cell in BoardState)
        {
            cell.IsLegal = false;
        }

        var coords = new Point(x, y);
        var currentCell = BoardState[x, y] ?? null;


        if (currentCell is null || currentCell.ChessPiece is null) return;
        currentCell.IsSelected = true;
        switch (currentCell.ChessPiece)
        {
            case Knight:
                // Using the possible translations a knight can move, if that cell exists, it's a legal move
                // TODO: If currentCell.chessPiece & BoardState[destination].chessPiece are same colour
                // move is NOT LEGAL
                var translations = Moves.GetKnightTranslations();
                currentY = y;
                currentX = x;
                translations.ForEach(move =>
                {
                    var destX = currentX + move.X;
                    var destY = currentY + move.Y;
                    if (destY <= BoardState.GetUpperBound(0) && destX <= BoardState.GetUpperBound(1) && destY >= BoardState.GetLowerBound(0) && destX >= BoardState.GetLowerBound(1))
                    {
                        Cell cell = BoardState[destX, destY];
                        cell.IsLegal = (!cell.IsOccupied || cell.ChessPiece?.Colour != currentCell.ChessPiece.Colour) ? true : false;
                    }
                });
                break;

            case Pawn:
                translations = Moves.GetPawnTranslations();
                currentY = y;
                currentX = x;
                translations.ForEach(move =>
                {
                    // Invert Y if Chess Piece colour is black
                    move.Y = (currentCell.ChessPiece.Colour == ConsoleColor.Black) ? move.Y * -1 : move.Y;
                    var destX = currentX + move.X;
                    var destY = currentY + move.Y;
                    if (destY <= BoardState.GetUpperBound(0) && destX <= BoardState.GetUpperBound(1) && destY >= BoardState.GetLowerBound(0) && destX >= BoardState.GetLowerBound(1))
                    {
                        // If it's not an attack move, cell must be empty for move to be legal
                        Cell cell = BoardState[destX, destY];
                        if (move.X == 0)
                            cell.IsLegal = (!cell.IsOccupied) ? true : false;
                        else
                            cell.IsLegal = (cell.IsOccupied && cell.ChessPiece?.Colour != currentCell.ChessPiece.Colour) ? true : false;
                    }
                });
                break;

            case Bishop:
                var dictTranslations = MoveDictionaries.GetBishopTranslations();
                foreach (var direction in dictTranslations)
                    GetMoveRepeater(x, y, currentCell, direction, 7);
                break;

            case Rook:
                dictTranslations = MoveDictionaries.GetRookTranslations();
                foreach (var direction in dictTranslations)
                    GetMoveRepeater(x, y, currentCell, direction, 7);
                break;

            case Queen:
                dictTranslations = MoveDictionaries.GetQueenTranslations();
                foreach (var direction in dictTranslations)
                    GetMoveRepeater(x, y, currentCell, direction, 7);
                break;

            case King:
                // check BoardState to make sure move is not check.
                dictTranslations = MoveDictionaries.GetQueenTranslations();
                foreach (var direction in dictTranslations)
                    GetMoveRepeater(x, y, currentCell, direction, 1);
                break;
        }

        StateChanged?.Invoke(this);


    }

    // private Dictionary<string, Point> PawnInvert(Piece chessPiece) => chessPiece switch
    // {
    //     Piece { Colour: ConsoleColor.Black } => MoveDictionaries.GetPawnTranslations().
    // };

    private void GetMoveRepeater(int x, int y, Cell currentCell, KeyValuePair<string, Point> direction, int moveRange)
    {
        for (int i = 1; i <= moveRange; i++)
        {
            // multiply i by the direction, and add to current position to get number of cells to move
            var transX = x + direction.Value.X * i;
            var transY = y + direction.Value.Y * i;
            if (transY > BoardState.GetUpperBound(0) || transX > BoardState.GetUpperBound(1) || transY < BoardState.GetLowerBound(0) || transX < BoardState.GetLowerBound(1))
                break;
            var bishopDest = BoardState[transX, transY];
            if (!bishopDest.IsOccupied)
                bishopDest.IsLegal = true;
            else if (bishopDest.IsOccupied && bishopDest.ChessPiece?.Colour != currentCell.ChessPiece?.Colour)
            {
                bishopDest.IsLegal = true;
                return;
            }
            else
                return;
            Debug.WriteLine($"{currentCell.ToString()} => {bishopDest.ToString()}");
        }
    }

    public void PlacePiece(int x, int y, Piece chessPiece)
    {
        var coords = new Point(x, y);
        var cell = BoardState[x, y] ?? null;

        if (cell is not null && !cell.IsOccupied)
        {
            cell.ChessPiece = chessPiece;
            BoardState[x, y] = cell;
        }
        StateChanged?.Invoke(this);

    }
}

