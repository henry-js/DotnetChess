namespace ChessLibrary;

public class Cell
{
    private Piece? chessPiece;

    public ConsoleColor Colour { get; }
    public bool IsOccupied { get; private set; } = false;
    public bool IsLegal { get; set; } = false;
    public bool IsSelected { get; set; } = false;
    public int CellNumber { get; set; }
    public int Rank { get; init; }
    public char File { get; init; }
    public string Position { get; init; }
    public Piece? ChessPiece
    {
        get => chessPiece; set
        {
            chessPiece = value;
            IsOccupied = true;
            if (chessPiece == null)
            {
                IsOccupied = false;
            }
        }
    }

    public Cell(ConsoleColor cellColour, File x, int y)
    {
        Colour = cellColour;
        Rank = y + 1;
        File = x.ToString().ToCharArray()[0];
        Position = this.ToString();
    }
    public Cell(ConsoleColor cellColour, Piece startPiece, File x, int y) : this(cellColour, x, y)
    {
        ChessPiece = startPiece;

    }

    public override string ToString()
    {
        return $"{File}{Rank}";
    }
}
