using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public List<Players> players;
    public List<Maps> maps;

    private bool _inputMovement;
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

    public static void InitGame()
    {
        int mapIndex = PlayerPrefs.GetInt("mapIndex");
        Instance._currentMap = Instance.maps[mapIndex];
        Instance._currentLeafValue = 0;
        Instance._currentHealthValue = Instance._currentMap.health;

        UIManager.UpdateHealthUI(Instance._currentMap.health);
        UIManager.UpdateLeafTopUI(Instance._currentMap.goldLeafs);
        UIManager.UpdateLeafUI(Instance._currentLeafValue);
    }

    public static void RestartGame()
    {
        Instance._inputMovement = false;
        TimerController.RestartTimer();
    }

    public static void StartGame()
    {
        Instance._inputMovement = true;
        TimerController.BeginTimer();
    }

    public static void UpdateHealth(int value)
    {

    }

    public static void IncreaseLeaf()
    {
        Instance._currentLeafValue++;
        Debug.Log(Instance._currentLeafValue);
        if (Instance._currentLeafValue >= Instance._currentMap.goldLeafs) {
            TimerController.EndTimer();
            UIManager.ShowMenuCompleted();
        }
        UIManager.UpdateLeafUI(Instance._currentLeafValue);
    }

    public static bool IsInputMovement()
    {
        return Instance._inputMovement;
    }

    public static int RandomNumber(int min, int max)
    {
        return Random.Range(min, max);
    }
}
