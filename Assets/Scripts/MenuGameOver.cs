using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MenuGameOver : MonoBehaviour
{
    [SerializeField] private GameObject _buttonPause;
    [SerializeField] private GameObject _menuGameOver;
    [SerializeField] private TMPro.TMP_Text _message;
    public GameObject firstSelected;

    private LocalizedStringTable _stringTable = new LocalizedStringTable { TableReference = "LanguageText" };

    private void Init(float timeValue)
    {
        Time.timeScale = 1f;
        Invoke("InitAction", timeValue);
    }

    private void InitAction()
    {
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

        EventSystem eventSystem = EventSystem.current;
        eventSystem.SetSelectedGameObject(firstSelected, new BaseEventData(eventSystem));

        SoundsManager.SetVolumeAtmosphere(0.3f);
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
