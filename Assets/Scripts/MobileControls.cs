using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileControls : MonoBehaviour
{
    public GameObject controls;

    // Start is called before the first frame update
    void Start()
    {
        #if UNITY_ANDROID
            controls.SetActive(true);
        #endif
    }
}
