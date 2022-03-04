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
    private bool _isFullAttack = false;
    private bool _isFullShield = false;
    private bool _gameInProgress = false;
    private bool _gameIsPaused = false;

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
        Instance._gameInProgress = false;
        Instance._inputMovement = false;
        TimerController.RestartTimer();
    }

    public static void StartGame()
    {
        Instance._gameInProgress = true;
        Instance._inputMovement = true;
        TimerController.BeginTimer();
    }

    public static void UpdateHealth(int value)
    {
        Instance._currentHealthValue -= value;
        UIManager.UpdateHealthUI(Instance._currentHealthValue);
    }

    public static int GetHealth()
    {
        return Instance._currentHealthValue;
    }

    public static void RestoreHealth()
    {
        Instance._currentHealthValue = Instance._currentMap.health;
        UIManager.UpdateHealthUI(Instance._currentHealthValue);
    }

    public static void IncreaseLeaf()
    {
        Instance._currentLeafValue++;
        UIManager.UpdateLeafUI(Instance._currentLeafValue);
        
        if (Instance._currentLeafValue >= Instance._currentMap.goldLeafs) {
            Instance._gameInProgress = false;
            
            TimerController.EndTimer();
            StatesSoundController.CompletedPlay();

            UIManager.ShowMenuCompleted();
        }
    }

    public static void PlayerDeath()
    {
        Instance._gameInProgress = false;
        TimerController.EndTimer();
    }

    public static void GameOver()
    {
        UIManager.ShowMenuGameOver();
    }

    public static bool IsInputMovement()
    {
        return Instance._inputMovement;
    }

    public static bool IsGameInProgress()
    {
        return Instance._gameInProgress;
    }

    public static void SetGameIsPaused(bool value)
    {
        Instance._gameIsPaused = value;
    }

    public static bool GetGameIsPaused()
    {
        return Instance._gameIsPaused;
    }

    public static void InitFullAttack(float time)
    {
        Instance.StartCoroutine(Instance.CountdownAttack(time));
    }

    public static bool IsFullAttack()
    {
        if (!Instance._isFullAttack) {
            Instance.StopCoroutine(Instance.CountdownAttack(0f));
        }
        return Instance._isFullAttack;
    }

    private IEnumerator CountdownAttack(float time)
    {
        _isFullAttack = true;
        while(time > 0) {
            time -= Time.deltaTime;
            yield return null;
        }
        _isFullAttack = false;
        yield return null;
    }

    public static void InitFullShield(float time)
    {
        Instance.StartCoroutine(Instance.CountdownShield(time));
    }

    public static bool IsFullShield()
    {
        if (!Instance._isFullShield) {
            Instance.StopCoroutine(Instance.CountdownShield(0f));
        }
        return Instance._isFullShield;
    }

    private IEnumerator CountdownShield(float time)
    {
        _isFullShield = true;
        while(time > 0) {
            time -= Time.deltaTime;
            yield return null;
        }
        _isFullShield = false;
        yield return null;
    }

    public static int RandomNumber(int min, int max)
    {
        return Random.Range(min, max);
    }
}
