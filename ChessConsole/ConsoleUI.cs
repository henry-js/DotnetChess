using System.Reflection.PortableExecutable;

namespace ChessConsole;

public static class ConsoleUI
{

    public static void UpdateChessUI(ChessBoard chessBoard)
    {
        Console.Clear();
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        for (int y = 8 - 1; y >= 0; y--)
        {
            var row = new Cell[8];
            for (int x = 0; x < 8; x++)
            {
                var location = new Point(x, y);
                var cell = chessBoard.BoardState[x, y];
                var piece = cell.ChessPiece;

                Console.BackgroundColor = (chessBoard.BoardState[x, y].Colour == ConsoleColor.Black) ? ConsoleColor.DarkGray : ConsoleColor.Gray;

                if (chessBoard.BoardState[x, y].IsLegal)
                    Console.BackgroundColor = System.ConsoleColor.Blue;

                Console.Write($"{piece?.Rune.ToString() ?? " "} ");
                Console.ResetColor();
            }
            Console.WriteLine();
        }
        Console.ResetColor();
    }

    public static void UpdateChessDataTable(ChessBoard chessBoard)
    {
        var dt = new DataTable();
        var cols = Enum.GetValues(typeof(ChessLibrary.File));
        foreach (var file in cols)
        {
            dt.Columns.Add(file.ToString());
        }
        for (int y = 8 - 1; y >= 0; y--)
        {

        }
    }

    public static (int userX, int userY) GetUserXAndY()
    {
        string? input = String.Empty;
        while (input?.Length != 2 || input.Length == 2 && (!FileIsValid(input[0]) || !RankIsValid(input[1])))
        {
            Console.WriteLine("Enter a valid board location:");
            input = Console.ReadLine();
            Console.SetCursorPosition(0, 8);
            for (int i = 0; i < Console.BufferWidth; i++)
            {
                Console.Write(" ");
            }
            Console.SetCursorPosition(0, 9);
            for (int i = 0; i < Console.BufferWidth; i++)
            {
                Console.Write(" ");
            }
            Console.SetCursorPosition(0, 8);
        }
        var userX = (int)Enum.Parse(typeof(ChessLibrary.File), Char.ToUpper(input[0]).ToString());
        var userY = (int)Char.GetNumericValue(input, 1) - 1;

        return (userX, userY);
    }

    public static void SetBoard(int userX, int userY, ChessBoard chessBoard)
    {
        var choice1 = 'z';
        Console.WriteLine("Place piece (a) or get legal moves (b)?: ");
        while (choice1 != 'a' && choice1 != 'b')
        {
            choice1 = Console.ReadKey().KeyChar;
        }

        var pieceChoice = 'k';
        var colourChoice = 'w';
        Piece chosenPiece;
        if (choice1 == 'a')
        {
            Console.WriteLine();
            Console.Write("Select piece. Default is knight:\nKing (k)\nQueen (q)\nRook (r)\nBishop (b)\nKnight (n)\nPawn (p)\n-> ");
            pieceChoice = Console.ReadKey().KeyChar;
            Console.WriteLine();
            Console.Write($"Select colour. Default is white:\nWhite (w)\nBlack (b)\n-> ");
            colourChoice = Console.ReadKey().KeyChar;

            chosenPiece = SelectPiece(pieceChoice, colourChoice);
            chessBoard.PlacePiece(userX, userY, chosenPiece);
        }
    }

    static bool FileIsValid(char file) =>
    Char.ToUpper(file) switch
    {
        (>= 'A') and (<= 'H') => true,
        _ => false
    };

    static bool RankIsValid(char rank) =>
        char.GetNumericValue(rank) switch
        {
            (>= 1) and (<= 7) => true,
            _ => false
        };

    static Piece SelectPiece(char pieceChoice, char colourChoice) => pieceChoice switch
    {
        'k' => new King(SelectColour(colourChoice)),
        'q' => new Queen(SelectColour(colourChoice)),
        'r' => new Rook(SelectColour(colourChoice)),
        'b' => new Bishop(SelectColour(colourChoice)),
        'n' => new Knight(SelectColour(colourChoice)),
        'p' => new Pawn(SelectColour(colourChoice)),
        _ => new Pawn(SelectColour(colourChoice))
    };
    static ConsoleColor SelectColour(char colourChoice) => colourChoice switch
    {
        'b' => ConsoleColor.Black,
        _ => ConsoleColor.White,
    };

}