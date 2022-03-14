using System;

/// <summary>
/// The record class, which gets uploaded to the Firebase Database
/// </summary>

[Serializable]

public class Record
{
    public int playerId;
    public float time;

    public Record(int playerId, float time)
    {
        this.playerId = playerId;
        this.time = time;
    }
}
