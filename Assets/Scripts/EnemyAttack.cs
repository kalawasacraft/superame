using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public GameObject hitCutParticles;
    public Transform pointCut;

    private float _damage = 1f;
    private bool _isAttacking;
    private bool _isHit = false;
    private Animator _animator;

    // Start is called before the first frame update
    void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        _isAttacking = _animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_isAttacking && !_isHit) {
            if (collision.CompareTag("PlayerHealth")) {
                _isHit = true;
                collision.SendMessageUpwards("TakeHit", _damage);

                Debug.Log("damage!!!!");

                GameObject instantiatedCut = Instantiate(hitCutParticles, pointCut.position, Quaternion.identity) as GameObject;
                instantiatedCut.transform.localScale = new Vector3(transform.localScale.x, 
                                                                    instantiatedCut.transform.localScale.y, 
                                                                    instantiatedCut.transform.localScale.z);

                Destroy(instantiatedCut, instantiatedCut.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _isHit = false;
    }
}
