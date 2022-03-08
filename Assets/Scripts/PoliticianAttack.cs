using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliticianAttack : MonoBehaviour
{
    public GameObject hitImpactParticles;
    public AudioClip harmSound;
    public GameObject objectThrow;
    public Transform pointThrow;
    public float damage = 1f;
    public float speedThrow = 4.5f;

    private bool _isAttacking;
    private bool _isHit = false;
    private Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    void LateUpdate()
    {
        _isAttacking = _animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_isAttacking && !_isHit) {
            if (collision.CompareTag("PlayerHealth")) {
                _isHit = true;

                //collision.GetComponent<AudioSource>().Play();
                collision.SendMessageUpwards("TakeHit", damage);

                GameObject instantiatedImpact = Instantiate(hitImpactParticles, collision.transform.position, Quaternion.identity) as GameObject;
                instantiatedImpact.transform.localScale = new Vector3(transform.localScale.x, 
                                                                    instantiatedImpact.transform.localScale.y, 
                                                                    instantiatedImpact.transform.localScale.z);
                AudioSource audioInstanciated = instantiatedImpact.GetComponent<AudioSource>();
                audioInstanciated.clip = harmSound;
                audioInstanciated.Play();

                Destroy(instantiatedImpact, instantiatedImpact.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
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
            myObjectThrowComponent.SetKindThrow(false);
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
