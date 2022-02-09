using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldLeaf : MonoBehaviour
{
    public GameObject vanishParticles;

    private SpriteRenderer _renderer;
    private Collider2D _collider;

    void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _collider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) {
            GameManager.IncreaseLeaf();
            
            _collider.enabled = false;

            _renderer.enabled = false;
            vanishParticles.SetActive(true);            

            Destroy(gameObject, 0.6f);
        }
    }
}
