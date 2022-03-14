using System;

/// <summary>
/// The map class, which gets uploaded to the Firebase Database
/// </summary>

[Serializable]
public class Map
{  
    public int completed;

    public Map()
    {
        this.completed = 0;
    }

    public Map(int completed)
    {
        this.completed = completed;
    }
}
