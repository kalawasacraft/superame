using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public TMPro.TMP_Text leafValue;
    public TMPro.TMP_Text topLeafValue;
    public TMPro.TMP_Text timeValue;
    public TMPro.TMP_Text countdownValue;
    public RectTransform healthImage;
    public GameObject buttonPause;

    private float _sizeHeartLeaf = 36f;

    void Awake()
    {
        UIManager.Instance = this;
        /*if (UIManager.Instance == null) {
            UIManager.Instance = this;
            DontDestroyOnLoad(this.gameObject);
        } else {
            Destroy(this.gameObject);
        }*/
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    public static void EnabledPause(bool value)
    {
        Instance.buttonPause.SetActive(value);
    }

    public static void UpdateHealthUI(int health)
    {
        Instance.SetHealth(health);
    }

    public static void UpdateLeafUI(int leaf)
    {
        Instance.leafValue.SetText(leaf.ToString());
    }

    public static void UpdateTimeUI(string time)
    {
        Instance.timeValue.SetText(time);
    }

    public static void UpdateLeafTopUI(int topLeaf)
    {
        Instance.topLeafValue.SetText("/ " + topLeaf.ToString());
    }

    public static string GetCountdownUI()
    {
        return Instance.countdownValue.text;
    }

    public static void UpdateCountdownUI(string value)
    {
        Instance.countdownValue.SetText(value);
    }

    public static void SetActiveCountdownUI(bool value)
    {
        Instance.countdownValue.gameObject.SetActive(value);
    }

    public static void ShowMenuCompleted()
    {
        Instance.GetComponent<MenuCompleted>().Completed();
    }

    public static void ShowTrophiesInCompleted()
    {
        Instance.GetComponent<MenuCompleted>().ShowTrophies();
    }

    public static void ShowMenuGameOver()
    {
        Instance.GetComponent<MenuGameOver>().GameOver();
    }

    public static void InitNotification()
    {
        Instance.GetComponent<Notifications>().Init();
    }

    public static void ShowNotificationPause(string message)
    {
        Instance.GetComponent<Notifications>().ShowNotificationPause(message);
    }

    public static void ShowNotificationGameOver(string message)
    {
        Instance.GetComponent<Notifications>().ShowNotificationGameOver(message);
    }

    public static void ShowNotificationCompleted(string message)
    {
        Instance.GetComponent<Notifications>().ShowNotificationCompleted(message);
    }

    public static void ShowNotificationTopPlayer()
    {
        Instance.GetComponent<Notifications>().ShowNotificationTopPlayer();
    }

    public static void ShowNotificationDefault()
    {
        Instance.GetComponent<Notifications>().ShowNotificationDefault();
    }

    public static void ShowAlertInNotification(string message)
    {
        Instance.GetComponent<Notifications>().ShowNotificationAlert(message);
    }

    public static void ShowPowerUpCounter(bool value)
    {
        Instance.GetComponent<Notifications>().ShowPanelPowerUp(value);
    }

    public static void SetPowerUpCounter(int value)
    {
        Instance.GetComponent<Notifications>().SetCounterPowerUp(value);
    }

    public static void SetPowerUpIcon(int value)
    {
        Instance.GetComponent<Notifications>().SetIconPowerUp(value);
    }

    private void SetHealth(int health)
    {
        healthImage.sizeDelta = new Vector2(health * _sizeHeartLeaf, healthImage.sizeDelta.y);
    }
}
