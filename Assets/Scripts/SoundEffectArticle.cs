using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectArticle : MonoBehaviour
{
    private AudioSource _audio;

    void Awake()
    {
        _audio = GetComponent<AudioSource>();
    }

    public void SoundPlay()
    {
        _audio.Play();
    }
}
