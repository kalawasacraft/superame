using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HelpWindow : MonoBehaviour
{
    [SerializeField] private GameObject _helpButton;
    [SerializeField] private GameObject _windowHelp;
    public GameObject firstSelected;

    private bool _isHelp = false;

    void Start()
    {
        if (GameManager.IsFirstOpenGame()) {
            GameManager.SetIsFirstOpenGame(false);
            Help();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (_isHelp) {
                Quit();
            } else {
                Help();
            }
        }
    }

    public void Help()
    {
        _isHelp = true;
        _windowHelp.SetActive(true);

        EventSystem eventSystem = EventSystem.current;
        eventSystem.SetSelectedGameObject(firstSelected, new BaseEventData(eventSystem));
    }

    public void Quit()
    {
        _isHelp = false;
        Invoke("QuitAction", 0.2f);
    }

    private void QuitAction()
    {
        EventSystem eventSystem = EventSystem.current;
        eventSystem.SetSelectedGameObject(_helpButton, new BaseEventData(eventSystem));
        _windowHelp.SetActive(false);
    }
}
