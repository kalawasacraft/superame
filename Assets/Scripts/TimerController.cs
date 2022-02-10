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
        UIManager.UpdateTimeUI("00:00.00");
        _timerGoing = false;
    }

    public static void BeginTimer()
    {
        Instance._timerGoing = true;
        Instance._elapsedTime = 0f;

        Instance.StartCoroutine(Instance.UpdateTimer());
    }

    public static void EndTimer()
    {
        Instance._timerGoing = false;
    }

    private IEnumerator UpdateTimer()
    {
        while (_timerGoing) {
            _elapsedTime += Time.deltaTime;
            _timePlaying = TimeSpan.FromSeconds(_elapsedTime);
            UIManager.UpdateTimeUI(_timePlaying.ToString("mm':'ss'.'ff"));

            yield return null;
        }
    }    
}
