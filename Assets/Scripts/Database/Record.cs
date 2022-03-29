using System;

/// <summary>
/// The record class, which gets uploaded to the Firebase Database
/// </summary>

[Serializable]
public class Record
{
    public int playerId;
    public float time;
    public int completed;

    public Record(int playerId, float time)
    {
        this.playerId = playerId;
        this.time = time;
        this.completed = 1;
    }

    public Record(int playerId, float time, int completed)
    {
        this.playerId = playerId;
        this.time = time;
        this.completed = completed;
    }
}
