using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SecondsGained : MonoBehaviour
{
    public GameObject secondsObject;
    
    public void Show(Vector3 position, float decreasedSeconds)
    {
        secondsObject.transform.position = Camera.main.WorldToScreenPoint(position);
        secondsObject.SetActive(true);
        secondsObject.transform.Find("Seconds").GetComponent<TMPro.TMP_Text>().SetText("-" + decreasedSeconds.ToString("0.00"));
    }
}
