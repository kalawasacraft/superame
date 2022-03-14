using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NicknamePlace : MonoBehaviour
{
    [SerializeField] private GameObject _nicknamePlace;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey(GameManager.GetNicknamePrefs())) {
            ActiveNicknamePlace();
        }
    }

    private void ActiveNicknamePlace()
    {
        _nicknamePlace.SetActive(true);
        _nicknamePlace.GetComponentInChildren<TMP_Text>().SetText(PlayerPrefs.GetString(GameManager.GetNicknamePrefs()));
    }
}
