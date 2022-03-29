using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Map")]
public class Maps : ScriptableObject
{
    public string mapId;
    public string nameMap;
    public Sprite sprite;
    public int positionBuildScene;
    public int goldLeafs;
    public int health;
}
