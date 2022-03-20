using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Localization;
using TMPro;

public class Notifications : MonoBehaviour
{
    public Image playerStatusFace;
    public TMP_Text nicknameText;
    public TMP_Text messageText;
    public GameObject textsObject;
    public GameObject waitWriteImage;
    public GameObject trophyImage;
    public GameObject notificationsPanel;

    private string _messageTemp;
    private bool _openPanel;
    private List<Sprite> _facesStatus;

    private GameManager _gameManager;
    private LocalizedStringTable _stringTable = new LocalizedStringTable { TableReference = "LanguageText" };

    void Start()
    {
        _gameManager = GameManager.Instance;
    }

    public void Init()
    {
        _openPanel = false;
        _facesStatus = _gameManager.players[PlayerPrefs.GetInt("playerIndex")].statusFaces;
        playerStatusFace.sprite = _facesStatus[0];
    
        trophyImage.SetActive(false);
        waitWriteImage.SetActive(false);
        textsObject.SetActive(false);

        if (PlayerPrefs.HasKey(GameManager.GetNicknamePrefs())) {
            nicknameText.SetText(PlayerPrefs.GetString(GameManager.GetNicknamePrefs()));
        } else {
            nicknameText.SetText("???");
        }

        messageText.SetText("");
        GetFirstTopPlayer();

    }

    private void GetFirstTopPlayer()
    {
        string mapId = _gameManager.maps[PlayerPrefs.GetInt("mapIndex")].mapId;

        ShowWaitWrite();
        DatabaseHandler.GetTopRecords(mapId, 1, records => {
            
            string topPlayerMessage = _stringTable.GetTable().GetEntry("langMessageDefaultPlayer").GetLocalizedString();

            if (records.Count == 1) {
                var e = records.GetEnumerator();
                e.MoveNext();
                topPlayerMessage += " " + e.Current.Key + " (" + e.Current.Value.time.ToString("0.00") + ")";
            } else {
                topPlayerMessage += " " + _stringTable.GetTable().GetEntry("langAnyone").GetLocalizedString();
            }

            _messageTemp = topPlayerMessage;
            SetMessageText(topPlayerMessage);

            HideWaitWrite();     
        });
    }

    public void ShowWaitWrite()
    {
        textsObject.SetActive(false);
        waitWriteImage.SetActive(true);
    }

    public void HideWaitWrite()
    {
        waitWriteImage.SetActive(false);
        textsObject.SetActive(true);
    }

    public void ShowNotificationPause(string message)
    {
        _openPanel = true;
        playerStatusFace.sprite = _facesStatus[1];
        ShowNotification(message, 0.3f);
        notificationsPanel.GetComponent<Image>().color = new Color(1f, 1f, 1f);
    }

    public void ShowNotificationCompleted(string message)
    {
        _openPanel = true;
        playerStatusFace.sprite = _facesStatus[2];
        ShowNotification(message, 0.3f);
        notificationsPanel.GetComponent<Image>().color = new Color(1f, 1f, 1f);
    }

    public void ShowNotificationTopPlayer()
    {
        playerStatusFace.sprite = _facesStatus[3];

        ShowNotification(_stringTable.GetTable().GetEntry("langMessageTopPlayer").GetLocalizedString(), 0.3f);
        notificationsPanel.GetComponent<AudioSource>().Play();
        trophyImage.SetActive(true);
    }

    public void ShowNotificationGameOver(string message)
    {
        _openPanel = true;
        playerStatusFace.sprite = _facesStatus[4];
        ShowNotification(message, 0.3f);
        notificationsPanel.GetComponent<Image>().color = new Color(1f, 1f, 1f);
    }

    public void ShowNotificationDefault()
    {
        _openPanel = false;
        playerStatusFace.sprite = _facesStatus[0];
        ShowNotification(_messageTemp, 0.3f);
    }

    private void ShowNotificationDefaultCond()
    {
        if (!_openPanel) {
            playerStatusFace.sprite = _facesStatus[0];
            ShowNotification(_messageTemp, 0.3f);
            notificationsPanel.GetComponent<Image>().color = new Color(1f, 1f, 1f);
        }
    }

    public void ShowNotificationAlert(string message)
    {
        ShowNotification(message, 0.2f);
        StartCoroutine(InvokeRealtimeCoroutine(ShowNotificationDefaultCond, 4f));
        notificationsPanel.GetComponent<Image>().color = new Color(251f/255f, 223f/255f, 107f/255f);
    }

    private void ShowNotification(string message, float seconds)
    {
        ShowWaitWrite();
        SetMessageText(message);
        StartCoroutine(InvokeRealtimeCoroutine(HideWaitWrite, seconds));
    }

    private void SetMessageText(string message)
    {
        messageText.SetText(message);
    }

    private IEnumerator InvokeRealtimeCoroutine(System.Action action, float seconds)
    {   
        yield return new WaitForSecondsRealtime(seconds);
        if (action != null)
            action();
    }
}
