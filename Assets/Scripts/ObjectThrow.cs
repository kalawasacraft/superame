using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectThrow : MonoBehaviour
{
    public GameObject hitCutParticles;
    public GameObject hitSplashParticles;
	
    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private bool _kindThrow = true;
    private Vector2 _direction;
    private float _damage = 1f;
	private float _speed = 2f;
    private bool _isLaunch = false;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_kindThrow) {
		    _rigidbody.velocity = _direction.normalized * _speed;
        } else {
            if (_isLaunch) {
                _isLaunch = false;

                _direction.y = 0.75f;
                _rigidbody.AddForce(_direction * _speed, ForceMode2D.Impulse);

                _animator.Play("Idle"+GameManager.RandomNumber(1,6).ToString());
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
	{
        if (collision.CompareTag("PlayerHealth")) {
            
            collision.SendMessageUpwards("TakeHit", _damage);

            GameObject instantiatedCut = Instantiate(hitCutParticles, transform.position, Quaternion.identity) as GameObject;
            instantiatedCut.transform.localScale = new Vector3(transform.localScale.x, 
                                                                instantiatedCut.transform.localScale.y, 
                                                                instantiatedCut.transform.localScale.z);

            Destroy(instantiatedCut, instantiatedCut.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);

            Destroy(gameObject);
        } else if (collision.CompareTag("Ground")) {

            GameObject instantiatedSplash = Instantiate(hitSplashParticles, transform.position, Quaternion.identity) as GameObject;
            instantiatedSplash.transform.localScale = new Vector3(transform.localScale.x, 
                                                                instantiatedSplash.transform.localScale.y, 
                                                                instantiatedSplash.transform.localScale.z);

            Destroy(instantiatedSplash, instantiatedSplash.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);

            Destroy(gameObject);
        } else if (collision.CompareTag("Player")) {

            GameObject instantiatedSplash = Instantiate(hitSplashParticles, transform.position, Quaternion.identity) as GameObject;
            instantiatedSplash.transform.localScale = new Vector3(transform.localScale.x, 
                                                                instantiatedSplash.transform.localScale.y, 
                                                                instantiatedSplash.transform.localScale.z);

            Destroy(instantiatedSplash, instantiatedSplash.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);

            Destroy(gameObject);
        }
    }

    public void SetDirection(Vector2 dir)
    {
        _direction = dir;
    }

    public void SetDamage(float value)
    {
        _damage = value;
    }

    public void SetSpeed(float value)
    {
        _speed = value;
    }

    public void SetKindThrow(bool type)
    {
        _kindThrow = type;
        if (_kindThrow) {
            _rigidbody.constraints = RigidbodyConstraints2D.FreezePositionY;
        } else {
            _isLaunch = true;
        }
    }
}
