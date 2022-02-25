using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.SceneManagement;

public class MenuGameOver : MonoBehaviour
{
    [SerializeField] private GameObject _buttonPause;
    [SerializeField] private GameObject _menuGameOver;
    [SerializeField] private TMPro.TMP_Text _message;

    private LocalizedStringTable _stringTable = new LocalizedStringTable { TableReference = "LanguageText" };

    private void Init()
    {
        Time.timeScale = 1f;
        _buttonPause.SetActive(false);
        _menuGameOver.SetActive(false);
    }

    public void GameOver()
    {
        _message.SetText(
            _stringTable.GetTable().GetEntry("langMsgDefeated_" + GameManager.RandomNumber(0, 3).ToString()).GetLocalizedString());
        
        Time.timeScale = 0f;
        _buttonPause.SetActive(false);
        _menuGameOver.SetActive(true);
    }

    public void Restart()
    {
        Init();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Quit()
    {
        Init();
        SceneManager.LoadScene(0);
    }
}
