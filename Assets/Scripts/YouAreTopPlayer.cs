using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class YouAreTopPlayer : MonoBehaviour
{
    [SerializeField] private GameObject _topPlayerPanel;
    [SerializeField] private TMP_Text _nicknamePlace;

    public void Show()
    {
        _nicknamePlace.SetText(PlayerPrefs.GetString(GameManager.GetNicknamePrefs()));
        
        _topPlayerPanel.SetActive(true);
        _topPlayerPanel.GetComponent<AudioSource>().Play();

        Invoke("DisablePanel", 3f);
    }

    public void DisablePanel()
    {
        Debug.Log("disable panel!!!");
        _topPlayerPanel.SetActive(false);
    }
}
