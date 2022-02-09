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

    public static void UpdateHealthUI(int health)
    {
        Instance.healthValue.SetText(Instance.getHealthString(health));
    }

    public static void UpdateLeafUI(int leaf)
    {
        Instance.leafValue.SetText(leaf.ToString());
    }

    public static void UpdateTimeUI(int time)
    {
        // convert int to ms, s, m and h
        Instance.timeValue.SetText(time.ToString());
    }

    public static void UpdateLeafTopUI(int topLeaf)
    {
        Instance.topLeafValue.SetText("/ " + topLeaf.ToString());
    }

    private string getHealthString(int health)
    {
        return new string('O', health);
    }

}
