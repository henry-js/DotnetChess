namespace ChessLibrary.Pieces;

public abstract class Piece
{
    public string Name { get; init; }
    public abstract char Rune { get; }
    public ConsoleColor Colour { get; }
    public bool HasMoved { get; set; } = false;

    public Piece(ConsoleColor colour)
    {
        Name = this.GetType().ToString().Split('.')[^1];
        Colour = colour;
    }
}

public class King : Piece
{
    public King(ConsoleColor colour) : base(colour)
    {
    }

    public override char Rune => (Colour == ConsoleColor.Black) ? '\u265A' : '\u2654';

}
public class Queen : Piece
{
    public Queen(ConsoleColor colour) : base(colour)
    {
    }

    public override char Rune => (Colour == ConsoleColor.Black) ? '\u265B' : '\u2655';

}

public class Rook : Piece
{
    public Rook(ConsoleColor colour) : base(colour)
    {
    }

    public override char Rune => (Colour == ConsoleColor.Black) ? '\u265C' : '\u2656';

}
public class Bishop : Piece
{
    public Bishop(ConsoleColor colour) : base(colour)
    {
    }

    public override char Rune => (Colour == ConsoleColor.Black) ? '\u265D' : '\u2657';

}

public class Knight : Piece
{
    public Knight(ConsoleColor colour) : base(colour)
    {
    }

    public override char Rune => (Colour == ConsoleColor.Black) ? '\u265E' : '\u2658';

}
public class Pawn : Piece
{
    public Pawn(ConsoleColor colour) : base(colour)
    {
    }

    public override char Rune => (Colour == ConsoleColor.Black) ? '\u265F' : '\u2659';
}
