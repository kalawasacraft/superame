using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MenuPause : MonoBehaviour
{
    [SerializeField] private GameObject _buttonPause;
    [SerializeField] private GameObject _menuPause;
    [SerializeField] private TMPro.TMP_Text _message;
    public GameObject firstSelected;

    private LocalizedStringTable _stringTable = new LocalizedStringTable { TableReference = "LanguageText" };
    private bool _isPaused = false;

    void Update()
    {
        if (GameManager.IsGameInProgress() && Input.GetKeyDown(KeyCode.Escape)) {
            if (_isPaused) {
                Resume();
            } else {
                Pause();
            }
        }
    }

    private void Init()
    {
        Time.timeScale = 1f;
        _isPaused = false;
        GameManager.SetGameIsPaused(_isPaused);

        _buttonPause.SetActive(false);
        _menuPause.SetActive(false);
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        _isPaused = true;
        GameManager.SetGameIsPaused(_isPaused);

        _message.SetText(
            _stringTable.GetTable().GetEntry("langMsgPause_" + GameManager.RandomNumber(0, 3).ToString()).GetLocalizedString());
        _buttonPause.SetActive(false);
        _menuPause.SetActive(true);

        EventSystem eventSystem = EventSystem.current;
        eventSystem.SetSelectedGameObject(firstSelected, new BaseEventData(eventSystem));
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        _isPaused = false;
        GameManager.SetGameIsPaused(_isPaused);

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
}