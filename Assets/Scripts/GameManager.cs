using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public List<Players> players;
    public List<Maps> maps;

    private int _currentLeafValue;
    private int _currentHealthValue;
    private Maps _currentMap;

    void Awake()
    {
        if (GameManager.Instance == null) {
            GameManager.Instance = this;
            DontDestroyOnLoad(this.gameObject);
        } else {
            Destroy(this.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //StartGame();
    }

    public static void StartGame()
    {
        int mapIndex = PlayerPrefs.GetInt("mapIndex");
        Instance._currentMap = Instance.maps[mapIndex];
        Instance._currentLeafValue = 0;
        Instance._currentHealthValue = Instance._currentMap.health;

        UIManager.UpdateHealthUI(Instance._currentMap.health);
        UIManager.UpdateLeafTopUI(Instance._currentMap.goldLeafs);
        UIManager.UpdateLeafUI(Instance._currentLeafValue);
        UIManager.UpdateTimeUI(0);
    }

    public static void UpdateHealth(int value)
    {

    }

    public static void IncreaseLeaf()
    {
        Instance._currentLeafValue++;
        Debug.Log(Instance._currentLeafValue);
        if (Instance._currentLeafValue >= Instance._currentMap.goldLeafs) {
            Debug.Log("WIN!!!!!!");
        }
        UIManager.UpdateLeafUI(Instance._currentLeafValue);
    }
}
