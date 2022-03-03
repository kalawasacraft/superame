using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MenuCompleted : MonoBehaviour
{
    [SerializeField] private GameObject _buttonPause;
    [SerializeField] private GameObject _menuCompleted;
    [SerializeField] private TMPro.TMP_Text _message;
    [SerializeField] private TMPro.TMP_Text _timePlayed;
    public GameObject firstSelected;

    private LocalizedStringTable _stringTable = new LocalizedStringTable { TableReference = "LanguageText" };
    
    private void Init()
    {
        Time.timeScale = 1f;
        _buttonPause.SetActive(false);
        _menuCompleted.SetActive(false);
    }

    public void Completed()
    {
        _message.SetText(
            _stringTable.GetTable().GetEntry("langMsgCompleted_" + GameManager.RandomNumber(0, 3).ToString()).GetLocalizedString());
        _timePlayed.SetText(TimerController.GetTimePlayed());

        Time.timeScale = 0f;
        _buttonPause.SetActive(false);
        _menuCompleted.SetActive(true);

        EventSystem eventSystem = EventSystem.current;
        eventSystem.SetSelectedGameObject(firstSelected, new BaseEventData(eventSystem));
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
