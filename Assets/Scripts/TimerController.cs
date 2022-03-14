using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerController : MonoBehaviour
{
    public static TimerController Instance;

    private TimeSpan _timePlaying;
    private bool _timerGoing;
    private float _elapsedTime;

    void Awake()
    {
        if (TimerController.Instance == null) {
            TimerController.Instance = this;
            DontDestroyOnLoad(this.gameObject);
        } else {
            Destroy(this.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        RestartTimer();
    }

    public static void RestartTimer()
    {
        Instance._timerGoing = false;
        UIManager.UpdateTimeUI("00:00.00");
    }

    public static void BeginTimer()
    {
        Instance._timerGoing = true;
        Instance._elapsedTime = 0f;

        Instance.StartCoroutine(Instance.UpdateTimer());
    }

    public static void DecreaseTime(float value)
    {
        StatesSoundController.GainTimePlay();
        if (Instance._timerGoing) {
            Instance._elapsedTime = Mathf.Max(Instance._elapsedTime - value, 0f);
        }
    }

    public static void EndTimer()
    {
        Instance._timerGoing = false;
    }

    public static string GetTimePlayed()
    {
        return Instance.TimePlayed();
    }

    public static float GetTimePlayedFloat()
    {
        return Instance._elapsedTime;
    }

    private IEnumerator UpdateTimer()
    {
        while (_timerGoing) {
            _elapsedTime += Time.deltaTime;
            _timePlaying = TimeSpan.FromSeconds(_elapsedTime);

            UIManager.UpdateTimeUI(TimePlayed());

            yield return null;
        }
    }

    private string TimePlayed()
    {
        return _timePlaying.ToString("mm':'ss'.'ff");
    }
}
