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
    public GameObject firstSelected;

    private LocalizedStringTable _stringTable = new LocalizedStringTable { TableReference = "LanguageText" };
    private bool _isPaused = false;

    void Update()
    {
        if (GameManager.IsGameInProgress() && Input.GetKeyDown(KeyCode.Escape)) {
            if (_isPaused) {
                SoundsManager.BackPlay();
                Resume();
            } else {
                SoundsManager.ConfirmPlay();
                Pause();
            }
        }
    }

    private void Init(float timeValue)
    {
        Time.timeScale = 1f;
        Invoke("InitAction", timeValue);
    }

    private void InitAction()
    {
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

        UIManager.ShowNotificationPause(
            _stringTable.GetTable().GetEntry("langMsgPause_" + GameManager.RandomNumber(0, 3).ToString()).GetLocalizedString());

        _buttonPause.SetActive(false);
        _menuPause.SetActive(true);

        EventSystem eventSystem = EventSystem.current;
        eventSystem.SetSelectedGameObject(firstSelected, new BaseEventData(eventSystem));

        SoundsManager.SetVolumeAtmosphere(0.3f);
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        UIManager.ShowNotificationDefault();
        Invoke("ResumeAction", 0.1f);
    }

    private void ResumeAction() 
    {
        _isPaused = false;
        GameManager.SetGameIsPaused(_isPaused);

        _buttonPause.SetActive(true);
        _menuPause.SetActive(false);

        SoundsManager.SetVolumeAtmosphere(0.8f);
    }

    public void Restart()
    {
        Init(0.1f);
        Invoke("RestartAction", 0.1f);
    }

    private void RestartAction()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Quit()
    {
        Init(0.2f);
        Invoke("QuitAction", 0.2f);
    }

    public void QuitAction()
    {
        SceneManager.LoadScene(0);
    }
}