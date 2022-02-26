using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectShield : MonoBehaviour
{
    public float timeEffect = 7f;

    private Collider2D _collider;

    void Awake()
    {
        _collider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Contestant")) {
            _collider.enabled = false;

            collision.SendMessageUpwards("InitEffectShield", timeEffect);
            GameManager.InitFullShield(timeEffect);

            Debug.Log("SHIELD!!!");
            Destroy(gameObject);
        }
    }
}
