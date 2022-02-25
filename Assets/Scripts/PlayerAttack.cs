using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject hitJabParticles;
    public GameObject hitUppercutParticles;
    public Transform pointJab;
    public Transform pointUppercut;

    private float _damage = 1f;
    private bool _isAttacking;
    private bool _isHit = false;
    private Animator _animator;

    // Start is called before the first frame update
    void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    void LateUpdate()
    {
        _isAttacking = _animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack");
        if (!_isAttacking) {
            _isHit = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_isAttacking && !_isHit) {
            if (collision.CompareTag("EnemyHealth")) {
                _isHit = true;
                collision.SendMessageUpwards("TakeHit", _damage);

                if (_animator.GetCurrentAnimatorStateInfo(0).IsName("AttackOne")) {
                    GameObject instantiatedJab = Instantiate(hitJabParticles, pointJab.position, Quaternion.identity) as GameObject;
                    instantiatedJab.transform.localScale = new Vector3(transform.localScale.x, 
                                                                        instantiatedJab.transform.localScale.y, 
                                                                        instantiatedJab.transform.localScale.z);

                    Destroy(instantiatedJab, instantiatedJab.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
                } else {
                    GameObject instantiatedUppercut = Instantiate(hitUppercutParticles, pointJab.position, Quaternion.identity) as GameObject;
                    instantiatedUppercut.transform.localScale = new Vector3(transform.localScale.x, 
                                                                            instantiatedUppercut.transform.localScale.y, 
                                                                            instantiatedUppercut.transform.localScale.z);

                    Destroy(instantiatedUppercut, instantiatedUppercut.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
                }
            }
        }
    }
}
