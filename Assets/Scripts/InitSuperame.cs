using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InitSuperame : MonoBehaviour
{
    public Image panelTransition;
    public GameObject startMenu;

    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.IsFirstOpenGame()) {
            StartCoroutine(InitTransition(4f, 1.5f));
        } else {
            FinishInit();
        }
    }

    private void FinishInit()
    {
        this.gameObject.SetActive(false);
    }

    IEnumerator InitTransition(float duration, float startTransition) {
        
        Color startColor = new Color(0f, 0f, 0f, 0f);
        Color endColor = new Color(0f, 0f, 0f, 1f);

        startMenu.SetActive(false);

        for (float t = 0f; t < duration; t += Time.deltaTime) {

            if (t > duration - startTransition) {
                float normalizedTime = (t - (duration - startTransition)) / startTransition;
                panelTransition.color = Color.Lerp(startColor, endColor, normalizedTime);
            }
            yield return null;
        }

        startMenu.SetActive(true);
        startMenu.GetComponent<SelectedPlayer>().ChangePlayer();

        FinishInit();
    }
}
