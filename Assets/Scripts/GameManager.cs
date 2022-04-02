using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public List<Players> players;
    public List<Maps> maps;

    private GameObject waitLoad;
    private string _nicknamePrefs = "nickname";
    private bool _isFirstOpenGame = true;
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
        if (mapIndex >= Instance.maps.Count) {
            mapIndex = 0;
            PlayerPrefs.SetInt("mapIndex", mapIndex);
            PlayerPrefs.Save();
        }
        Instance._currentMap = Instance.maps[mapIndex];
        Instance._currentLeafValue = 0;
        Instance._currentHealthValue = Instance._currentMap.health;

        UIManager.UpdateHealthUI(Instance._currentMap.health);
        UIManager.UpdateLeafTopUI(Instance._currentMap.goldLeafs);
        UIManager.UpdateLeafUI(Instance._currentLeafValue);
        UIManager.InitNotification();
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

    public static int GetTotalHealth()
    {
        return Instance._currentMap.health;
    }

    public static void RestoreHealth(float time)
    {
        Instance._currentHealthValue = Instance._currentMap.health;
        UIManager.UpdateHealthUI(Instance._currentHealthValue);

        UIManager.SetPowerUpIcon(2);
        Instance.StartCoroutine(Instance.CountdownHealth(time));
    }

    private IEnumerator CountdownHealth(float time)
    {
        UIManager.ShowPowerUpCounter(true);
        int initValue = 10000;

        while(time > 0) {
            time -= Time.deltaTime;
            
            int tempValue = (int) time;
            if (initValue != tempValue) {
                UIManager.SetPowerUpCounter(tempValue + 1);
                initValue = tempValue;
            }

            yield return null;
        }

        UIManager.ShowPowerUpCounter(false);
        yield return null;
    }

    public static void IncreaseLeaf()
    {
        Instance._currentLeafValue++;
        UIManager.UpdateLeafUI(Instance._currentLeafValue);
        
        if (Instance._currentLeafValue >= Instance._currentMap.goldLeafs) {
            Instance._gameInProgress = false;
            
            TimerController.EndTimer();

            Instance.SaveTimePlayed();

            StatesSoundController.CompletedPlay();

            UIManager.ShowMenuCompleted();
        }
    }

    private void SaveTimePlayed()
    {
        string playerName = PlayerPrefs.GetString(GetNicknamePrefs());
        string mapId = Instance._currentMap.mapId;
        float myTimePlayed = TimerController.GetTimePlayedFloat();
        int playerId = PlayerPrefs.GetInt("playerIndex");
        var newRecord = new Record(playerId, myTimePlayed);

        GameManager.ShowWaitLoad(true);

        DatabaseHandler.GetTopRecords(mapId, 1, records => {
            
            bool iAmTheBest = true;
            if (records.Count == 1) {
                var e = records.GetEnumerator();
                e.MoveNext();
                if (e.Current.Value.time <= myTimePlayed) {
                    iAmTheBest = false;
                }
            }

            if (iAmTheBest) {
                UIManager.ShowNotificationTopPlayer();
                UIManager.ShowTrophiesInCompleted();
            }

            GameManager.ShowWaitLoad(false);

            DatabaseHandler.GetRecord(mapId, playerName, newRecord, record => {
                
                newRecord.completed = record.completed + 1;
                if (record.time <= newRecord.time) {
                    newRecord.time = record.time;
                    newRecord.playerId = record.playerId;
                }

                DatabaseHandler.PostRecord(newRecord, mapId, playerName, () => { });
            });
        
        });

        DatabaseHandler.GetPlayer(mapId, playerId, player => {

            var updatePlayer = new Player(player.completed + 1);
            DatabaseHandler.PatchPlayer(updatePlayer, mapId, playerId, () => { });
        });

        DatabaseHandler.GetMap(mapId, map => {

            var updateMap = new Map(map.completed + 1);
            DatabaseHandler.PatchMap(updateMap, mapId, () => { });
        });
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

    public static bool IsFirstOpenGame()
    {
        return Instance._isFirstOpenGame;
    }

    public static void SetIsFirstOpenGame(bool value)
    {
        Instance._isFirstOpenGame = value;
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
        UIManager.SetPowerUpIcon(0);
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
        UIManager.ShowPowerUpCounter(true);
        int initValue = 10000;

        while(time > 0) {
            time -= Time.deltaTime;
            
            int tempValue = (int) time;
            if (initValue != tempValue) {
                UIManager.SetPowerUpCounter(tempValue + 1);
                initValue = tempValue;
            }

            yield return null;
        }

        UIManager.ShowPowerUpCounter(false);
        _isFullAttack = false;
        yield return null;
    }

    public static void InitFullShield(float time)
    {
        Instance.StartCoroutine(Instance.CountdownShield(time));
        UIManager.SetPowerUpIcon(1);
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
        UIManager.ShowPowerUpCounter(true);
        int initValue = 10000;

        while(time > 0) {
            time -= Time.deltaTime;

            int tempValue = (int) time;
            if (initValue != tempValue) {
                UIManager.SetPowerUpCounter(tempValue + 1);
                initValue = tempValue;
            }

            yield return null;
        }

        UIManager.ShowPowerUpCounter(false);
        _isFullShield = false;
        yield return null;
    }

    public static string GetNicknamePrefs()
    {
        return Instance._nicknamePrefs;
    }

    public static void SetWaitLoad(GameObject objectLoad)
    {
        Instance.waitLoad = objectLoad;
    }

    public static void ShowWaitLoad(bool value)
    {
        if (Instance.waitLoad != null) {
            Instance.waitLoad.SetActive(value);
        }
    }

    public static int RandomNumber(int min, int max)
    {
        return Random.Range(min, max);
    }
}
