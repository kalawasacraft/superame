using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class NicknameWindow : MonoBehaviour
{
    [SerializeField] private GameObject _quitHelpButton;
    [SerializeField] private GameObject _windowNickname;
    [SerializeField] private TMP_InputField _inputNickname;
    [SerializeField] private GameObject _messageError;
    [SerializeField] private Button _saveButton;
    [SerializeField] private GameObject _nicknamePlace;

    public GameObject firstSelected;

    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey(GameManager.GetNicknamePrefs())) {

            _inputNickname.onValueChanged.AddListener (delegate { SetNickname(); });

            _windowNickname.SetActive(true);
            EventSystem eventSystem = EventSystem.current;
            eventSystem.SetSelectedGameObject(firstSelected, new BaseEventData(eventSystem));
        }
    }

    public void SetNickname()
    {
        if (!Regex.IsMatch(_inputNickname.text, "^[a-zA-Z][0-9a-zA-Z][0-9a-zA-Z]+$")) {
            _messageError.SetActive(true);
            _saveButton.interactable = false;
        } else {
            _messageError.SetActive(false);
            _saveButton.interactable = true;
        }
    }

    public void Save()
    {
        Invoke("SaveAction", 0.2f);
    }

    private void SaveAction()
    {        
        PlayerPrefs.SetString(GameManager.GetNicknamePrefs(), _inputNickname.text);
        PlayerPrefs.Save();

        _nicknamePlace.SetActive(true);
        _nicknamePlace.GetComponentInChildren<TMP_Text>().SetText(PlayerPrefs.GetString(GameManager.GetNicknamePrefs()));

        EventSystem eventSystem = EventSystem.current;
        eventSystem.SetSelectedGameObject(_quitHelpButton, new BaseEventData(eventSystem));
        _windowNickname.SetActive(false);
    }
}
