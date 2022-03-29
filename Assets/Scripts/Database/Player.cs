using System;

/// <summary>
/// The player class, which gets uploaded to the Firebase Database
/// </summary>

[Serializable]
public class Player
{
    public int completed;

    public Player()
    {
        this.completed = 0;
    }

    public Player(int completed)
    {
        this.completed = completed;
    }
}
