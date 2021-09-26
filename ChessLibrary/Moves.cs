namespace ChessLibrary;

internal static class Moves
{
    public static Dictionary<string, Point> GetKingTranslations()
    {
        var PieceTranslations = new Dictionary<string, Point>();
        PieceTranslations.Add("N", new Point(0, 1));
        PieceTranslations.Add("NE", new Point(1, 1));
        PieceTranslations.Add("E", new Point(1, 0));
        PieceTranslations.Add("SE", new Point(1, -1));
        PieceTranslations.Add("S", new Point(0, -1));
        PieceTranslations.Add("SW", new Point(-1, -1));
        PieceTranslations.Add("W", new Point(-1, 0));
        PieceTranslations.Add("NW", new Point(-1, 1));
        return PieceTranslations;
    }
    public static Dictionary<string, Point> GetQueenTranslations()
    {
        var PieceTranslations = new Dictionary<string, Point>();
        PieceTranslations.Add("N", new Point(0, 1));
        PieceTranslations.Add("NE", new Point(1, 1));
        PieceTranslations.Add("E", new Point(1, 0));
        PieceTranslations.Add("SE", new Point(1, -1));
        PieceTranslations.Add("S", new Point(0, -1));
        PieceTranslations.Add("SW", new Point(-1, -1));
        PieceTranslations.Add("W", new Point(-1, 0));
        PieceTranslations.Add("NW", new Point(-1, 1));
        return PieceTranslations;
    }
    public static Dictionary<string, Point> GetRookTranslations()
    {
        var PieceTranslations = new Dictionary<string, Point>();
        PieceTranslations.Add("N", new Point(0, 1));
        PieceTranslations.Add("E", new Point(1, 0));
        PieceTranslations.Add("S", new Point(0, -1));
        PieceTranslations.Add("W", new Point(-1, 0));
        return PieceTranslations;
    }

    public static Dictionary<string, Point> GetBishopTranslations()
    {
        var PieceTranslations = new Dictionary<string, Point>();
        PieceTranslations.Add("NE", new Point(1, 1));
        PieceTranslations.Add("SE", new Point(1, -1));
        PieceTranslations.Add("SW", new Point(-1, -1));
        PieceTranslations.Add("NW", new Point(-1, 1));
        return PieceTranslations;
    }
    public static List<Point> GetKnightTranslations()
    {
        return new List<Point>()
        {
            new Point(-2,1),
            new Point(-2,-1),
            new Point(2,1),
            new Point(2,-1),
            new Point(-1,2),
            new Point(1,2),
            new Point(-1,-2),
            new Point(1,-2),
        };
    }

    public static List<Point> GetPawnTranslations()
    {
        return new List<Point>() {
        new Point(0,1),
        new Point(0,2),
        new Point(1,1),
        new Point(-1,1),
        };
    }
}

internal static class MoveDictionaries
{

}
