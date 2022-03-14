using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitLoad : MonoBehaviour
{
    public GameObject waitLoad;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.SetWaitLoad(waitLoad);
    }
}
