using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player")]
public class Players : ScriptableObject
{
    public GameObject player;
    public string namePlayer;
    public Sprite face;
    public int animationIndex;
    public List<Sprite> statusFaces;
}
