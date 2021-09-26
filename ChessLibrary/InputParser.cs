namespace ChessLibrary;

public static class Input
{
    public static (int x, int y, Piece chessPiece) Parse(string userInput)
    {
        var fileRankAndPiece = userInput.Split("");

        var file = (int)(File)Char.Parse(fileRankAndPiece[0]);
        var rank = Int32.Parse(fileRankAndPiece[1]) - 1;

        (int x, int y, Piece chessPiece) coords = (file, rank, new Pawn(ConsoleColor.Black));
        return coords;




    }
}