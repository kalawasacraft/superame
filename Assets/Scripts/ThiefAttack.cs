using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThiefAttack : MonoBehaviour
{
    public GameObject hitCutParticles;
    public Transform pointCut;
    public GameObject objectThrow;
    public Transform pointThrow;
    public float damage = 1f;
    public float speedThrow = 3.5f;

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
                collision.SendMessageUpwards("TakeHit", damage);

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

    public void CanThrow()
    {
        if (objectThrow != null) {

            GameObject myObjectThrow = Instantiate(objectThrow, pointThrow.position, Quaternion.identity) as GameObject;

            ObjectThrow myObjectThrowComponent = myObjectThrow.GetComponent<ObjectThrow>();
            myObjectThrowComponent.SetKindThrow(true);
            myObjectThrowComponent.SetDamage(damage);
            myObjectThrowComponent.SetSpeed(speedThrow);

            if (transform.localScale.x < 0f) {
                myObjectThrowComponent.SetDirection(Vector2.left);
            } else {
                myObjectThrowComponent.SetDirection(Vector2.right);
            }
        }
    }
}
