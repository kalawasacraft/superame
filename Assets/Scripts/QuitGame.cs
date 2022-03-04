using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitGame : MonoBehaviour
{
    public void Quit()
    {
        Invoke("QuitAction", 0.3f);
    }

    private void QuitAction()
    {
        Application.Quit();
    }
}
