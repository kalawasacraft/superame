using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatesSoundController : MonoBehaviour
{
    public static StatesSoundController Instance;

    public AudioClip counter;
    public AudioClip start;
    public AudioClip completed;
    public AudioClip defeated;
    public AudioClip gainTime;

    private AudioSource _audio;

    void Awake()
    {
        if (StatesSoundController.Instance == null) {
            StatesSoundController.Instance = this;
            DontDestroyOnLoad(this.gameObject);
        } else {
            Destroy(this.gameObject);
        }

        _audio = GetComponent<AudioSource>();
    }

    public static void CounterPlay()
    {
        Instance._audio.clip = Instance.counter;
        Instance.PlayAudio();
    }

    public static void StartPlay()
    {
        Instance._audio.clip = Instance.start;
        Instance.PlayAudio();
    }

    public static void CompletedPlay()
    {
        Instance._audio.clip = Instance.completed;
        Instance.PlayAudio();
    }

    public static void DefeatedPlay()
    {
        Instance._audio.clip = Instance.defeated;
        Instance.PlayAudio();
    }

    public static void GainTimePlay()
    {
        Instance._audio.clip = Instance.gainTime;
        Instance.PlayAudio();
    }

    private void PlayAudio()
    {
        _audio.Play();
    }
}
