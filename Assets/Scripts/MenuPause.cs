using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.SceneManagement;

public class MenuPause : MonoBehaviour
{
    [SerializeField] private GameObject _buttonPause;
    [SerializeField] private GameObject _menuPause;
    [SerializeField] private TMPro.TMP_Text _message;

    private LocalizedStringTable _stringTable = new LocalizedStringTable { TableReference = "LanguageText" };

    private void Init()
    {
        Time.timeScale = 1f;
        _buttonPause.SetActive(false);
        _menuPause.SetActive(false);
    }

    public void Pause()
    {
        _message.SetText(
            _stringTable.GetTable().GetEntry("langMsgPause_" + GameManager.RandomNumber(0, 3).ToString()).GetLocalizedString());
        Time.timeScale = 0f;
        _buttonPause.SetActive(false);
        _menuPause.SetActive(true);
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        _buttonPause.SetActive(true);
        _menuPause.SetActive(false);
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

    /*private int RandomNumber(int min, int max)
    {
        return Random.Range(min, max);
    }*/
}
