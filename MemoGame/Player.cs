namespace TARpe24_Mobiilirakendused;

public class Player
{
    public string Name { get; private set; }
    public int Score { get; private set; }
    public int Moves { get; private set; }

    public Player(string name)
    {
        Name = string.IsNullOrWhiteSpace(name) ? "Mängija 1" : name;
        Score = 0;
        Moves = 0;
    }

    public void AddPoint() => Score++;
    public void IncrementMoves() => Moves++;
    public void Reset()
    {
        Score = 0;
        Moves = 0;
    }
}
