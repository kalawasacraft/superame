using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsManager : MonoBehaviour
{
    public static SoundsManager Instance;
    public AudioClip navigation;
    public AudioClip confirm;
    public AudioClip back;
    public AudioClip error;

    private AudioSource _audio;

    void Awake()
    {
        if (SoundsManager.Instance == null) {
            SoundsManager.Instance = this;
            DontDestroyOnLoad(this.gameObject);
        } else {
            Destroy(this.gameObject);
        }

        _audio = GetComponent<AudioSource>();
    }

    public static void NavigationPlay()
    {
        Instance._audio.clip = Instance.navigation;
        Instance.PlayAudio();
    }

    public static void ConfirmPlay()
    {
        Instance._audio.clip = Instance.confirm;
        Instance.PlayAudio();
    }

    public static void BackPlay()
    {
        Instance._audio.clip = Instance.back;
        Instance.PlayAudio();
    }

    private void PlayAudio()
    {
        _audio.Play();
    }
}
