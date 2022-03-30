using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class HelpWindow : MonoBehaviour
{
    [SerializeField] private GameObject _helpButton;
    [SerializeField] private GameObject _windowHelp;
    [SerializeField] private TMP_Text _versionText;
    public GameObject firstSelected;

    void Start()
    {
        if (GameManager.IsFirstOpenGame()) {
            Help();
        }
        _versionText.SetText(Application.version);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Help()
    {
        _windowHelp.SetActive(true);
        
        if (PlayerPrefs.HasKey(GameManager.GetNicknamePrefs())) {
            EventSystem eventSystem = EventSystem.current;
            eventSystem.SetSelectedGameObject(firstSelected, new BaseEventData(eventSystem));
        }
    }

    public void Quit()
    {
        GameManager.SetIsFirstOpenGame(false);

        Invoke("QuitAction", 0.2f);
    }

    private void QuitAction()
    {
        EventSystem eventSystem = EventSystem.current;
        eventSystem.SetSelectedGameObject(_helpButton, new BaseEventData(eventSystem));
        _windowHelp.SetActive(false);
    }
}
