using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectHealth : MonoBehaviour
{
    public float timeEffect = 0.5f;

    private Collider2D _collider;

    void Awake()
    {
        _collider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Contestant")) {
            _collider.enabled = false;
            
            GameManager.RestoreHealth();
            collision.SendMessageUpwards("InitEffectHealth", timeEffect);

            Debug.Log("HEALTH!!!");
            Destroy(gameObject);
        }
    }
}
