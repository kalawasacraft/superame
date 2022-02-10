using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public TMPro.TMP_Text healthValue;
    public TMPro.TMP_Text leafValue;
    public TMPro.TMP_Text topLeafValue;
    public TMPro.TMP_Text timeValue;
    public TMPro.TMP_Text countdownValue;
    public GameObject buttonPause;

    //private Maps _currentMap;

    void Awake()
    {
        if (UIManager.Instance == null) {
            UIManager.Instance = this;
            DontDestroyOnLoad(this.gameObject);
        } else {
            Destroy(this.gameObject);
        }
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
        Instance.healthValue.SetText(Instance.getHealthString(health));
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

    private string getHealthString(int health)
    {
        return new string('O', health);
    }
}
